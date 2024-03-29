﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grapher {

    struct Sensor {
        public double[] acc;
        public double[] gyr;
        public double[] mag;
        public double[] q;
    }

    struct Packet {
        public byte BID;
        public byte MID;
        public byte len;
        public byte ext_len_mul;
        public byte ext_len_add;

        public bool IsExtLen;

        public int DataLength;
        public int SensorsNumber;
        public Sensor[] Sensors;
    }

    delegate void DataProviderServerStarted(DataProvider dataProvider);
    delegate void DataProviderClientConnected(DataProvider dataProvider, TcpClient client);
    delegate void DataProviderSampleWindow(DataProvider dataProvider, List<Packet> samplewin);
    delegate void DataProviderClientDisconnected(DataProvider dataProvider, TcpClient client);
    delegate void DataProviderServerStopped(DataProvider dataProvider);

    class DataProvider {

        private Int32 port;
        private IPAddress localAddr;
        private TcpListener server;
        private TcpClient client;

        private Boolean isClientConnected;
        private Boolean isServerActive;

        private int frequence;
        private int window;

        public int Port { get => port; }
        public IPAddress LocalAddr { get => localAddr; }
        public bool IsClientConnected { get => isClientConnected; }
        public bool IsServerActive { get => isServerActive; }
        public int Window { get => window; }
        public int Frequence { get => frequence; }

        public DataProviderServerStarted serverStartedDelegate;
        public DataProviderClientConnected clientConnectedDelegate;
        public DataProviderSampleWindow samplewinDelegate;
        public DataProviderClientDisconnected clientDisconnectedDelegate;
        public DataProviderServerStopped serverStoppedDelegate;

        public DataProvider() : this(45555, "127.0.0.1") { }

        public DataProvider(Int32 port, String localAddr) : this(port, localAddr, 50, 10) { }

        public DataProvider(Int32 port, String localAddr, int frequence, int window) {
            try {
                ChangeFrequenceAndWindow(frequence, window);
                SetAddressAndPort(port, localAddr);
                this.isClientConnected = false;
                this.isServerActive = false;
            }
            catch {
                throw;
            }
        }

        public void ChangeFrequenceAndWindow(int frequence, int window) {
            this.frequence = frequence;
            this.window = window;
        }

        public void ChangeAddressAndPort(Int32 port, String localAddr) {
            if (isServerActive) { Stop(); }
            SetAddressAndPort(port, localAddr);
        }

        private void SetAddressAndPort(Int32 port, String localAddr) {
            try {
                this.port = port;
                this.localAddr = IPAddress.Parse(localAddr);
            }
            catch (Exception ex) {
                MessageBox.Show("IP Addressing Error!\n" + ex.Message);
                this.isClientConnected = false;
                this.isServerActive = false;
                throw;
            }
        }

        public void Start() {
            if (!isServerActive) {
                this.server = new TcpListener(this.localAddr, port);
                this.server.Start();
                this.isServerActive = true;
            }
        }

        public void AcceptConnection() {
            while (isServerActive) {
                try {
                    serverStartedDelegate?.Invoke(this);
                    client = server.AcceptTcpClient();
                    this.isClientConnected = true;
                    clientConnectedDelegate?.Invoke(this, client);
                    ReceiveData();
                }
                catch { }
                finally {
                    if (isClientConnected) {
                        client.Close();
                        this.isClientConnected = false;
                        clientDisconnectedDelegate?.Invoke(this, client);
                        client = null;
                    }
                }
            }
        }

        public void Stop() {
            if (this.isClientConnected) { this.client.Close(); }
            this.server.Stop();
            this.isServerActive = false;
            this.isClientConnected = false;
            serverStoppedDelegate?.Invoke(this);
        }

        private void ReceiveData() {
            NetworkStream stream = client.GetStream();
            BinaryReader bin = new BinaryReader(stream);

            List<Packet> samplewin = new List<Packet>();

            byte bid;
            byte mid;
            byte len;
            byte ext_len_mul = 0;
            byte ext_len_add = 0;
            bool isExtLen;
            int dataLength;
            int sensorsNumber;

            bid = 0xFF;
            mid = 0x32;

            byte[] pream = new byte[3];

            // cerca la sequenza FF-32
            while (!(pream[0] == 0xFF && pream[1] == 0x32)) {
                pream[0] = pream[1];
                pream[1] = pream[2];
                byte[] read = bin.ReadBytes(1);
                pream[2] = read[0];
            }

            len = pream[2];
            isExtLen = pream[2] == 0xFF;

            // modalità normale
            if (!isExtLen) {
                dataLength = pream[2];
            }
            else {
                // modalità extended-length
                byte[] tmp = new byte[2];
                tmp = bin.ReadBytes(2);
                ext_len_mul = tmp[0];
                ext_len_add = tmp[1];
                dataLength = (ext_len_mul * 256) + ext_len_add;
            }

            sensorsNumber = (dataLength - 2) / 52; // calcolo del numero di sensori

            byte[] rawData = bin.ReadBytes(dataLength + 1); // lettura dei dati

            while (this.isClientConnected && rawData.Count() != 0) {

                Packet packet = new Packet() {
                    BID = bid,
                    MID = mid,
                    len = len,
                    ext_len_mul = ext_len_mul,
                    ext_len_add = ext_len_add,
                    IsExtLen = isExtLen,
                    DataLength = dataLength,
                    SensorsNumber = sensorsNumber,
                    Sensors = new Sensor[sensorsNumber]
                };

                for (int i = 0; i < sensorsNumber; i++) {
                    packet.Sensors[i].acc = new double[3];
                    packet.Sensors[i].gyr = new double[3];
                    packet.Sensors[i].mag = new double[3];
                    packet.Sensors[i].q = new double[4];
                }

                ParsePacket(packet, rawData);
                samplewin.Add(packet);

                int campNum = window * frequence;
                if (samplewin.Count % (campNum / 2) == 0 && samplewin.Count >= campNum) {
                    samplewinDelegate?.Invoke(this, samplewin);
                }

                if (!isExtLen) {
                    bin.ReadBytes(3);
                }
                else {
                    bin.ReadBytes(5);
                }

                rawData = bin.ReadBytes(dataLength + 1);
            }
            samplewinDelegate?.Invoke(this, samplewin);
        }

        private Packet ParsePacket(Packet packet, byte[] rawData) {
            for (int i = 0; i < packet.SensorsNumber; i++) {
                for (int j = 0; j < 13; j++) {
                    byte[] tmp = new byte[4];
                    int k = i * 52 + j * 4;

                    if (packet.SensorsNumber < 5) {
                        tmp[0] = rawData[k + 3]; // lettura inversa
                        tmp[1] = rawData[k + 2];
                        tmp[2] = rawData[k + 1];
                        tmp[3] = rawData[k];
                    }
                    else {
                        tmp[0] = rawData[k + 5];
                        tmp[1] = rawData[k + 4];
                        tmp[2] = rawData[k + 3];
                        tmp[3] = rawData[k + 2];
                    }

                    double value = BitConverter.ToSingle(tmp, 0);

                    if (j < 3) {
                        packet.Sensors[i].acc[j] = value;
                    }
                    else if (j < 6) {
                        packet.Sensors[i].gyr[j - 3] = value;
                    }
                    else if (j < 9) {
                        packet.Sensors[i].mag[j - 6] = value;
                    }
                    else {
                        packet.Sensors[i].q[j - 9] = value;
                    }
                }
            }

            return packet;
        }
    }
}

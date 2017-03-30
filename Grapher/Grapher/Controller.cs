﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // color
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // csv
using ZedGraph;
using System.Net.Sockets;

namespace Grapher
{
    public partial class Controller : Form
    {

        private DataProvider parser;
        private List<Packet> samplewin;
        private List<Packet> selected;
        private List<Packet> smoothed;
        /// Thread nel quale gira il parser, o meglio la funzione che gestisce il Server.
        Thread threadParser;
        /// Frequenza di campionamento.
        int frequence = 50;
        /// Dimensione della finestra in secondi.
        int window = 10;
        /// Pannello zedgraph sul quale disegnare i grafici.
        GraphPane myPane;
        /// Indice grafico selezionato.
        int selectedGraph;
        /// Indice posizione sensore selezionato.
        int selectedSensor;
        /// Indice tipo di sensore selezionato.
        int selectedSensorType;
        /// Path in cui salvare il file csv/actions_log.
        string csvPath;
        /// Sampwin salvata in locale dopo che il server viene stoppato per poter disegnare ancora i grafici relativi a quel campione.
        List<double[,]> mySampwin;
        /// Range per lo smoothing: anche raggio dell'intorno in cui guardare per fare la media per la deviazione standard.
        int smoothRange = 10;
        /*/// Var riconoscimento azione - moto : inizio del moto.
        double motoStart = 0;
        /// Var riconoscimento azione - moto : inizio dello stato di fermo.
        double fermoStart = 0;
        /// Var riconoscimento azioni : tempo fine finestra precedente.
        double winTime = 0;
        /// Var riconoscimento azione - moto : nome azione in corso.
        string action = null;
        /// Var riconoscimento azione - posizione : nome posizione attuale.
        string state = null;
        /// Var riconoscimento azione - posizione : inizio stato in corso.
        double stateStart = 0;
        /// Var riconoscimento azione - girata : tipo di girata.
        string turnAction = null;
        /// Var riconoscimento azione - girata : inizio girata attuale.
        double turnStart = 0;
        /// Var riconoscimento azione - girata : tipo di girata che potrebbe essere quella attuale.
        string turnPossibleAction = null;
        /// Var riconoscimento azione - girata : possibile inizio per la girata possibile attuale.
        double turnPossibleStart = 0;
        /// Var riconoscimento azione - girata : grado minimo per essere considerato significativo.
        double degree = 10;
        /// Var riconoscimento azione - girata : ultimo angolo di riferimento.
        double refAngolo = 0;

        String segAction = null;                    //!< Azione attuale rilevata nella segmentazione.
        private String segPossibleAction = null;    //!< Azione possibile che potrebbe essere in atto nell'operazione di segmentazione.
        int segStart = 0;                           //!< Inizio dell'azione corrente nell'operazione di segmentazione.
        int segPossibleStart = 0;                   //!< Inizio dell'azione plausibile nell'operazione di segmentazione.

        /// Var riconoscimento azioni : stringa da stampare su file.
        string outToFileStr = "";
        /// Numero di client che si vuole connettere al server.
        int clientsAmount = 0;
        /// Array di curve di supporto contenente il path di dead reckoning di ogni client (massimo 10).
       // Curve[] multiClientCurves = new Curve[10];
        /// Data iniziale di default.
        DateTime startTime = new DateTime( 1900, 1, 1, 0, 0, 0, 0 );
        bool printCSV;*/

        public Controller()
        {
            InitializeComponent();
            myPane = zedGraphControl1.GraphPane;
            // finestra - cazzo fa??
            window = (int)numericUpDownWindow.Value;
            // frequenza, setto 50 come default
            this.frequence_box.SelectedIndex = frequence_box.FindStringExact("50");
            frequence = Int32.Parse(frequence_box.Text);
            // csv location ... da completare
            //csvPath = Directory.GetCurrentDirectory() + @"\_output";

            // numeric smooting max value
            numericUpDown_smoothing.Maximum = Math.Floor((decimal)(window * frequence / 2));
            smoothRange = (int)numericUpDown_smoothing.Value;
            // client???? ...

            selectedGraph = type_of_grph_cb.SelectedIndex;
            // default value of segmentation
            segmentation_cb.Checked = false;
            //segmentation_cb.Enabled = ( (selectedGraph == 0 && selectedSensorType == 0) ? true : false );
            // dovrebbe avere anche client, csvPath, pritnCSV, printServerConsole, setButtonServerStart, Sampwin
            parser = new DataProvider();
            parser.samplewinDelegate = DataProviderSampleWindowReceived;
            parser.serverStartedDelegate = DataProviderServerDidStart;


            sensor_position.SelectedIndex = sensor_position.FindStringExact("1 (bacino)");
            selectedSensor = sensor_position.SelectedIndex;
            // selected sensor type
            sensor_type.SelectedIndex = sensor_type.FindStringExact("Acc");
            selectedSensorType = sensor_type.SelectedIndex;
            // selected graph
            type_of_grph_cb.SelectedIndex = type_of_grph_cb.FindStringExact("Modulo");
            selectedGraph = type_of_grph_cb.SelectedIndex;

        }

        // evento di click sul tasto start server
        private void btn_server_start_Click(object sender, EventArgs e)
        {
            if (parser.IsActive) {
                parser.Stop(); // bloccante, mi crea problemi
                // come usare il delegato di sampwin?? viene gia impostata alla chiamata di receive Data?
                zedGraphControl1.GraphPane.CurveList.Clear();
                zedGraphControl1.Invalidate();
                zedGraphControl1.GraphPane.Title.Text = "Graph";
                zedGraphControl1.GraphPane.XAxis.Title.Text = "x";
                zedGraphControl1.GraphPane.YAxis.Title.Text = "y";
                zedGraphControl1.AxisChange();
            }
            else {
                // se e' fermo lo starto
                frequence = Int32.Parse(frequence_box.Text);
                numericUpDown_smoothing.Maximum = Math.Floor((decimal)(window * frequence / 2));
                try {
                    // dovrei attiavare il server, ma il nostro costruttore crea il parser e attiva il server, quindi se 
                    // devo startarlo non ho nessun metodo che lo faccia?? lo creo io 
                    parser.ChangeAddressAndPort(
                        Int32.Parse(port.Text),
                        String.Format("{0}.{1}.{2}.{3}", ip1.Text, ip2.Text, ip3.Text, ip4.Text)
                        );
                    parser.serverStoppedDelegate = DataProviderServerDidStop;
                    threadParser = new Thread(new ThreadStart(parser.AcceptConnection));
                    threadParser.Start();
                }
                catch (SocketException exc) {
                    console.AppendText(String.Format("{0}\n", exc));
                }
            }
        }

        /// <summary>
		/// Delegato per scrivere sulla console del Server.
		/// </summary>
		/// <param name="s">Stringa da scrivere.</param>
		public delegate void printToServerConsoleDelegate(string s);

        public void printToServerConsole(string s)
        {
            if (this.console.InvokeRequired) {
                Invoke(new printToServerConsoleDelegate(printToServerConsole), new object[] { s });
            }
            else {
                console.AppendText(s);
                // auto scroll 
                //if (.Checked)
                //{
                console.SelectionStart = console.Text.Length;
                console.ScrollToCaret();
                //}
            }
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                csvPath = folderBrowserDialog1.SelectedPath;
                csv_path.Text = csvPath;
            }
        }

        private void btn_console_clear_Click(object sender, EventArgs e)
        {
            console.Text = "";
            if (parser.IsActive) {
                console.Text = "Server is Active.\n";
            }
            else {
                console.Text = "Server is Stopped.\n";
            }
        }


        // DataProviderDelegates

        void DataProviderClientConnectedWriter(DataProvider dataProvider, TcpClient aClient) {
            if (this.InvokeRequired)
            {
                Invoke(new DataProviderClientConnected(DataProviderClientConnectedWriter), new object[] { dataProvider, aClient });
            } else
            {
                // azione di scrivere che il client e'connesso, non funziona
                printToServerConsole("Client connected!\n");
            }
        }

         void DataProviderSampleWindowReceived(DataProvider dataProvider, List<Packet> sampwin)
        {

            if (this.InvokeRequired) {
                Invoke(new DataProviderSampleWindow(DataProviderSampleWindowReceived), new object[] { dataProvider, sampwin });
            }
            else {
                // esempio con modulo
                // devo controllare subito quali sono le opzioni dell'utente,dato che viene richiamata dopo una decina di secondi
                //samplewin = sampwin;
                //smoothed = DataAnalysis.SmoothData(samplewin, 10);
                //selected = smoothed;
                //double[,] data = ExtractData();
                //double[] modules = DataAnalysis.ComputeModules(data);
                DisplayData(sampwin);
            }
        }

        void DataProviderServerDidStart(DataProvider dataProvider)
        {
            if (this.InvokeRequired) {
                Invoke(new DataProviderServerStarted(DataProviderServerDidStart), new object[] { dataProvider });
            }
            else {
                port.Enabled = false;
                ip1.Enabled = false;
                ip2.Enabled = false;
                ip3.Enabled = false;
                ip4.Enabled = false;
                frequence_box.Enabled = false;
                numericUpDownWindow.Enabled = false;
                csv_path.Enabled = false;
                buttonSelectFolder.Enabled = false;
                //checkBoxSaveCsv.Enabled = false;
                btn_server_start.Text = "STOP";
                //aggiungere scritta console
                printToServerConsole("Waiting for a connection ...\n");
            }

        }

        void DataProviderServerDidStop(DataProvider dataProvider)
        {
            if (this.InvokeRequired) {
                Invoke(new DataProviderServerStopped(DataProviderServerDidStop), new object[] { dataProvider });
            }
            else { //Riabilita input server quando server inattivo.
                port.Enabled = true;
                ip1.Enabled = true;
                ip2.Enabled = true;
                ip3.Enabled = true;
                ip4.Enabled = true;
                frequence_box.Enabled = true;
                numericUpDownWindow.Enabled = true;
                csv_path.Enabled = true;
                buttonSelectFolder.Enabled = true;
                //checkBoxSaveCsv.Enabled = true;
                btn_server_start.Text = "START";
                printToServerConsole("Server stopped.\n"); // stampato due volte nel caso io stoppo e starto
            }

        }

        private double[] ExtractAngles(double[,] data, int kind)
        {
            int size = data.GetLength(0);
            double[] e = new double[size];

            for (int i = 0; i < size; i++) {
                e[i] = data[i, kind];
            }

            return e;
        }

        // non deve prendere un array di double, ma meglio una lista di packet. altrimenti
        // devo richiareextract data ogni volta che premo un qualsiasi bottone, meglio 
        // fare la chiamata allínterno di display data
        // ricevo un solo parametro, list<Packet>
        // all'interno del metodo richiamare la funzione extractData,
        // e passare i volire della funzione richiesta
        // pulire il grafico nel se non si devono visualizzare gli angoli di eulero
        private void DisplayData(List<Packet> sampwin)
        {
            // controllo se e' attivo lo smoothing!!
            samplewin = sampwin;
            smoothed = DataAnalysis.SmoothData(samplewin, smoothRange);
            selected = smoothing_cb.Checked ? smoothed : samplewin;
            double[,] data;
            myPane.CurveList.Clear();
            zedGraphControl1.Invalidate();
            //if (willClearPane) { myPane.CurveList.Clear(); }
            myPane.XAxis.Title.Text = "time (seconds)";
            switch (selectedSensorType) {
                case 0:
                    //acc
                    //printToServerConsole("acc");
                    myPane.YAxis.Title.Text = "m/s²";
                    break;
                case 1:
                    //gyr
                    //printToServerConsole("gry");
                    myPane.YAxis.Title.Text = "Rad/s²";
                    break;
                case 2:
                    //mag
                    //printToServerConsole("mag");
                    myPane.YAxis.Title.Text = "Tesla";
                    break;
                case 3:
                    //qua
                    //printToServerConsole("qua");
                    myPane.YAxis.Title.Text = "y";
                    break;
                default:
                    //bohh
                    myPane.YAxis.Title.Text = "none";
                    break;
            }

            // ottengo i dati in base al sensore selezionato e posizione
            data = ExtractData(selectedSensorType, selectedSensor);
            double[] modules = DataAnalysis.ComputeModules(data);
            PointPairList plist = new PointPairList();
            switch (selectedGraph) {
                case 0:
                    // module
                    //printToServerConsole("mod");
                    plist.Clear();
                    for (int i = 0; i < modules.Length; i++) {
                        plist.Add(new PointPair(i, modules[i]));
                    }
                    // problemi con i titoli
                    myPane.Title.Text = "Module";
                    myPane.AddCurve("Module", plist, Color.Blue, SymbolType.None);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 1:
                    // der
                    //printToServerConsole("dev");
                    plist.Clear();
                    double[] drv = DataAnalysis.ComputeDerivatives(modules, Int32.Parse(frequence_box.Text));
                    for (int i = 0; i < drv.Length; i++) {
                        plist.Add(new PointPair(i, drv[i]));
                    }
                    myPane.Title.Text = "Derivated";
                    myPane.AddCurve("Derivated", plist, Color.Blue, SymbolType.None);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 2:
                    // std
                    //printToServerConsole("std");
                    plist.Clear();
                    double[] dst = DataAnalysis.ComputeDerivatives(modules, smoothRange);
                    for (int i = 0; i < dst.Length; i++) {
                        plist.Add(new PointPair(i, dst[i]));
                    }
                    myPane.Title.Text = "Standard Deviation";
                    myPane.AddCurve("Standard Deviation", plist, Color.Blue, SymbolType.None);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
            } 
        }

        private double[,] ExtractData( int sensType, int sensPos )  
        {
            int size = selected.Count;
            double[,] data = new double[size, 3];
            // devo fare uno switch anche qua dentro per sapere quali richiamare
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < 3; j++) {
                    switch (sensType) {
                        case 0:
                            data[i, j] = selected[i].Sensors[sensPos].acc[j]; // dipende dal tipo sens
                            break;
                        case 1:
                            data[i, j] = selected[i].Sensors[sensPos].gyr[j]; // dipende dal tipo sens
                            break;
                        case 2:
                            data[i, j] = selected[i].Sensors[sensPos].mag[j]; // dipende dal tipo sens
                            break;

                        case 3:
                            data[i, j] = selected[i].Sensors[sensPos].q[j]; // dipende dal tipo sens
                            break;
                    }
                    
                }
            }
            return data;
        }

        private void smoothing_cb_CheckedChanged(object sender, EventArgs e)
        {
            selected = smoothing_cb.Checked ? smoothed : samplewin;
            DisplayData(samplewin);
        }

        private void sensor_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedSensorType = sensor_type.SelectedIndex;
            if (samplewin != null) {
                // fare una funzione che 
                DisplayData(samplewin);
            }
            //checkBoxSegmentation.Enabled = ((selectedChart == 0 && selectedSensorType == 0) ? true : false);
            //checkBoxSegmentation.Checked = false;
        }

        private void numericUpDown_smoothing_ValueChanged(object sender, EventArgs e)
        {
            smoothRange = (int)numericUpDown_smoothing.Value;
            if (samplewin!= null) {
                DisplayData(samplewin);
            }
        }

        private void type_of_grph_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGraph = type_of_grph_cb.SelectedIndex;
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void sensor_position_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedGraph = type_of_grph_cb.SelectedIndex;
            selectedSensor = sensor_position.SelectedIndex;
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }
    }
}


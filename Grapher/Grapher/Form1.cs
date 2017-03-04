using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grapher {
    public partial class Form1 : Form {

        private DataProvider dataProv;

        public Form1() {
            InitializeComponent();

            dataProv = new DataProvider(45556, "127.0.0.1");

            dataProv.samplewinDelegate = delegate (List<Packet> samplewin) {
                foreach (Packet p in samplewin) {
                    for(int i = 0; i < p.SensorsNumber; i++) {
                        for (int j = 0; j < 3; j++) {
                            System.Console.Write("{0} ", p.Sensors[i].acc[j]);
                        }
                        for (int j = 0; j < 3; j++) {
                            System.Console.Write("{0} ", p.Sensors[i].gyr[j]);
                        }
                        for (int j = 0; j < 3; j++) {
                            System.Console.Write("{0} ", p.Sensors[i].mag[j]);
                        }
                        for (int j = 0; j < 4; j++) {
                            System.Console.Write("{0} ", p.Sensors[i].q[j]);
                        }
                        System.Console.Write(";\n");
                    }
                    System.Console.WriteLine();
                }
            };
        }

        private void button1_Click(object sender, EventArgs e) {
            dataProv.AcceptConnection();
        }
    }
}

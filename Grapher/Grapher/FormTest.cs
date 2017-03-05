using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Grapher {
    public partial class FormTest : Form {

        private DataProvider dataProv;
        private List<Packet> samplewin;

        public FormTest() {
            InitializeComponent();

            dataProv = new DataProvider();

            dataProv.samplewinDelegate = delegate (List<Packet> samplewin) {
                /*foreach (Packet p in samplewin) {
                    for (int i = 0; i < p.SensorsNumber; i++) {
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
                }*/
                this.samplewin = samplewin;
            };
        }

        private void button1_Click(object sender, EventArgs e) {
            dataProv.AcceptConnection();
        }

        private void button2_Click(object sender, EventArgs e) {
            double[,,] modules = DataAnalysis.ComputeModules(samplewin);
            DisplayData(modules);
        }

        private void button3_Click(object sender, EventArgs e) {
            List<Packet> smoothed = DataAnalysis.SmoothData(samplewin, 10);
            double[,,] modules = DataAnalysis.ComputeModules(smoothed);
            DisplayData(modules);
        }

        private void DisplayData(double[,,] data) {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            PointPairList plist = new PointPairList();
            for (int i = 0; i < data.GetLength(0); i ++) {
                plist.Add(new PointPair(i,data[i,0,0]));
            }
            pane.AddCurve("Prova", plist, Color.Blue, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

    }
}

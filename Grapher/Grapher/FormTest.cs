using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grapher
{
    public partial class FormTest : Form
    {

        List<Packet> providedData;
        DataProvider dp = new DataProvider();

        public FormTest()
        {

            InitializeComponent();
            dp.samplewinDelegate = delegate (DataProvider dataProv, List<Packet> samplewin) {
                this.providedData = DataAnalysis.SmoothData(samplewin, 10);
                //this.providedData = samplewin;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dp.AcceptConnection();
        }

        // Usiamo il sensore 0 per capire se sta in piedi o seduto (Bacino)
        private void button2_Click(object sender, EventArgs e)
        {
            int size = providedData.Count();
            DateTime startTime = DateTime.Now;

            double[] dataArray = new double[size];
            for (int i = 0; i < size; i++) {
                dataArray[i] = providedData[i].Sensors[0].acc[0];
            }
            String[] array = DataAnalysis.LayStandSit(dataArray);
            String res = "";
            res += "inizio: " + startTime.ToString("h:mm:ss tt");
            for (int i = 1; i < size; i++) {
                if (array[i] != array[i - 1]) {
                    res += " fine: " + startTime.ToString("h:mm:ss tt") + " " + array[i - 1] + "\n";
                    startTime = startTime.AddSeconds(0.02);
                    res += "inizio: " + startTime.ToString("h:mm:ss tt");
                }
                else {
                    startTime = startTime.AddSeconds(0.02);
                }
                //res += startTime.ToString("h:mm:ss tt") + array[i] + "\n";

            }
            res += " fine: " + startTime.ToString("h:mm:ss tt") + " " + array[size - 1] + "\n";
            Console.WriteLine(res);
        }

        //Usiamo il sensore 0 per il movimento (bacino)
        private void button3_Click(object sender, EventArgs e)
        {
            int size = providedData.Count();

            double[,] dataArray = new double[size, 3];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < 3; j++) {
                    dataArray[i, j] = providedData[i].Sensors[0].acc[j];
                }

            }
            String[] array = DataAnalysis.MotoStazionamento(dataArray, 10);
            String res = "";
            for (int i = 0; i < size; i++) {
                res += array[i] + "\n";
            }
            Console.WriteLine(res);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int size = providedData.Count();
            DateTime startTime = DateTime.Now;

            double[,] dataArray = new double[size, 3];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < 3; j++) {
                    dataArray[i, j] = providedData[i].Sensors[0].mag[j];
                }

            }
            String[] array = DataAnalysis.Girata(dataArray);
            String res = "";
            int totalTurn = 0;
            res += "inizio: " + startTime.ToString("h:mm:ss tt");
            for (int i = 1; i < size; i++) {
                if (array[i] != array[i - 1]) {
                    if (array[i] == "Continua") {
                        res += " fine: " + startTime.ToString("h:mm:ss tt") + " girata" + array[i - 1] + "\n";
                    }
                    else {
                        totalTurn += Int32.Parse(array[i]);
                    }
                    res += " fine: " + startTime.ToString("h:mm:ss tt") + " " + array[i - 1] + "\n";
                    startTime = startTime.AddSeconds(0.02);
                    res += "inizio: " + startTime.ToString("h:mm:ss tt");
                }
                else {
                    startTime = startTime.AddSeconds(0.02);
                }
                //res += startTime.ToString("h:mm:ss tt") + array[i] + "\n";

            }
            res += " fine: " + startTime.ToString("h:mm:ss tt") + " " + array[size - 1] + "\n";
            Console.WriteLine(res);
        }
    }
}
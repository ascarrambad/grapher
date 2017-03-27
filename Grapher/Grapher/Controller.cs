using System;
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
        int smoothRange;
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
            this.frequence_box.SelectedIndex = frequence_box.FindStringExact( "50" );
            frequence = Int32.Parse(frequence_box.Text);
            // csv location ... da completare
            //csvPath = Directory.GetCurrentDirectory() + @"\_output";

            // numeric smooting max value
            numericUpDown_smoothing.Maximum = Math.Floor( (decimal)(window * frequence / 2) );
            smoothRange = (int)numericUpDown_smoothing.Value;
            // client???? ...

            selectedGraph = type_of_grph_cb.SelectedIndex;
            // default value of segmentation
            segmentation_cb.Checked = false;
            //segmentation_cb.Enabled = ( (selectedGraph == 0 && selectedSensorType == 0) ? true : false );
            // dovrebbe avere anche client, csvPath, pritnCSV, printServerConsole, setButtonServerStart, Sampwin
            parser = new DataProvider();
            parser.samplewinDelegate = DataProviderSampleWindowReceived;
            parser.startDelegate = DataProviderServerDidStart;
            parser.stopDelegate = DataProviderServerDidStop;

            sensor_position.SelectedIndex = sensor_position.FindStringExact( "1 (bacino)" );
            selectedSensor = sensor_position.SelectedIndex;
            // selected sensor type
            sensor_type.SelectedIndex = sensor_type.FindStringExact( "Acc" );
            selectedSensorType = sensor_type.SelectedIndex;
            // selected graph
            type_of_grph_cb.SelectedIndex = type_of_grph_cb.FindStringExact( "Modulo" );
            selectedGraph = type_of_grph_cb.SelectedIndex;

        }

        // evento di click sul tasto start server
        private void btn_server_start_Click(object sender, EventArgs e)
        {
            if (parser.IsActive)
            {
                parser.Stop(); // bloccante, mi crea problemi
                // come usare il delegato di sampwin?? viene gia impostata alla chiamata di receive Data?
                zedGraphControl1.GraphPane.CurveList.Clear();
                zedGraphControl1.Invalidate();
                zedGraphControl1.GraphPane.Title.Text = "Graph";
                zedGraphControl1.GraphPane.XAxis.Title.Text = "x";
                zedGraphControl1.GraphPane.YAxis.Title.Text = "y";
                zedGraphControl1.AxisChange();
            } else
            {
                // se e' fermo lo starto
                frequence = Int32.Parse( frequence_box.Text );
                numericUpDown_smoothing.Maximum = Math.Floor( ( decimal )( window * frequence / 2 ) );
                try
                {
                    // dovrei attiavare il server, ma il nostro costruttore crea il parser e attiva il server, quindi se 
                    // devo startarlo non ho nessun metodo che lo faccia?? lo creo io 
                    parser.ChangeAddressAndPort(
                        Int32.Parse(port.Text),
                        String.Format("{0}.{1}.{2}.{3}", ip1.Text, ip2.Text, ip3.Text, ip4.Text)
                        );
                    //parser.ChangeFrequenceAndWindow(frequence, window);
                    threadParser = new Thread(new ThreadStart(parser.AcceptConnection));
                    threadParser.Start();
                } catch (SocketException exc)
                {
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
            if (this.console.InvokeRequired)
            {
                Invoke(new printToServerConsoleDelegate(printToServerConsole), new object[] { s });
            }
            else
            {
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
            if (result == DialogResult.OK)
            {
                csvPath = folderBrowserDialog1.SelectedPath;
                csv_path.Text = csvPath;
            }
        }

        private void buttonClearConsole_Click(object sender, EventArgs e)
        {
            console.Text = "";
            if (parser.IsActive)
            {
                console.Text = "Server is Active.\n";
            }
        }


        // DataProviderDelegates

        public void DataProviderServerDidStart()
        {
            if (this.btn_server_start.InvokeRequired)
            {
               Invoke(new DataProviderServerStarted(DataProviderServerDidStart));
            } else {
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

        public void DataProviderSampleWindowReceived(List<Packet> samplewin)
        {

        }

        public void DataProviderServerDidStop()
        {
            if (this.btn_server_start.InvokeRequired)
            {
                Invoke(new DataProviderServerStopped(DataProviderServerDidStop));
            } else
            { //Riabilita input server quando server inattivo.
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
                printToServerConsole("Server stopped.\n");
            }
           
        }

        private double[] ExtractAngles(double[,] data, int kind)
        {
            int size = data.GetLength(0);
            double[] e = new double[size];

            for (int i = 0; i < size; i++)
            {
                e[i] = data[i, kind];
            }

            return e;
        }

        private void DisplayData(double[] data, Boolean willClearPane, Color col)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            if (willClearPane) { pane.CurveList.Clear(); }
            PointPairList plist = new PointPairList();
            for (int i = 0; i < data.Length; i++)
            {
                plist.Add(new PointPair(i, data[i]));
            }
            pane.AddCurve("Prova", plist, col, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        /*private double[,] ExtractData(int sensNum, int sensType)
        {
            int size = selected.Count;
            double[,] data = new double[size, 3];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data[i, j] = selected[i].Sensors[sensNum].acc[j];
                }
            }

            return data;
        }*/

    }
}


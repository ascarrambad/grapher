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

namespace Grapher {
    public partial class Controller : Form {

        private DataProvider parser;
        private List<Packet> samplewin;
        private List<Packet> selected;
        private List<Packet> smoothed;
        private List<Packet> filtered;
        
        Thread threadParser;
        int frequence = 50;
        int window = 10;
        GraphPane myPane;
        int selectedGraph;
        int selectedSensor;
        int selectedSensorType;
        string csvPath;
        int smoothRange = 10;
        Boolean smooth;
        double cutOff;


        public Controller() {
            InitializeComponent();
            myPane = zedGraphControl1.GraphPane;
            // finestra - cazzo fa??
            window = (int)numericUpDownWindow.Value;
            // frequenza, setto 50 come default
            this.frequence_box.SelectedIndex = frequence_box.FindStringExact("50");
            frequence = Int32.Parse(frequence_box.Text);
            // csv location ... da completare
            csvPath = Directory.GetCurrentDirectory() + @"\_own_output";
            try {
                System.IO.Directory.CreateDirectory(csvPath);
            }
            catch (Exception e) {
                printToServerConsole("Impossibile creare la cartella " + csvPath + "\n");
            }
            csv_path.Text = csvPath;

            // numeric smooting max value
            numericUpDown_smoothing.Maximum = Math.Floor((decimal)(window * frequence / 2));
            smoothRange = (int)numericUpDown_smoothing.Value;

            selectedGraph = type_of_grph_cb.SelectedIndex;

            parser = new DataProvider();
            parser.samplewinDelegate = DataProviderSampleWindowReceived;
            parser.serverStartedDelegate = DataProviderServerDidStart;
            parser.serverStoppedDelegate = DataProviderServerDidStop;
            parser.clientConnectedDelegate = DataProviderClientConnectedWriter;
            parser.clientDisconnectedDelegate = DataProviderClientDisconnectedWriter;

            sensor_position.SelectedIndex = sensor_position.FindStringExact("1 (Bacino)");
            selectedSensor = sensor_position.SelectedIndex;
            // selected sensor type
            sensor_type.SelectedIndex = sensor_type.FindStringExact("Acc");
            selectedSensorType = sensor_type.SelectedIndex;
            // selected graph
            type_of_grph_cb.SelectedIndex = type_of_grph_cb.FindStringExact("Modulo");
            selectedGraph = type_of_grph_cb.SelectedIndex;
            cutOff = (double)cutOff_value.Value;

            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.Invalidate();
            zedGraphControl1.GraphPane.Title.Text = "Graph";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "x";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "y";
            zedGraphControl1.AxisChange();
        }

        // evento di click sul tasto start server
        private void btn_server_start_Click(object sender, EventArgs e) {
            if (parser.IsServerActive) {
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
                    parser.ChangeAddressAndPort(
                        Int32.Parse(port.Text),
                        String.Format("{0}.{1}.{2}.{3}", ip1.Text, ip2.Text, ip3.Text, ip4.Text)
                        );
                    parser.Start();
                    threadParser = new Thread(new ThreadStart(parser.AcceptConnection));
                    threadParser.Start();
                }
                catch (SocketException exc) {
                    console.AppendText(String.Format("{0}\n", exc));
                }
            }
        }

        public void printToServerConsole(string s) {
            console.AppendText(s);
            console.SelectionStart = console.Text.Length;
            console.ScrollToCaret();
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                csvPath = folderBrowserDialog1.SelectedPath; // qua si potrebbe aggiungere una cartella predefinita dove inserire i 2 file, oppure far decidere all'utente di crearla o no
                csv_path.Text = csvPath;
            }
        }

        private void btn_console_clear_Click(object sender, EventArgs e) {
            console.Text = "";
            if (parser.IsServerActive) {
                //console.Text = "Server is Active.\n";

                console.Text = (String.Format("Server Started on port {0} at IP " +
                    "{1}.{2}.{3}.{4}\n", port.Text, ip1.Text, ip2.Text, ip3.Text, ip4.Text));
                printToServerConsole("Waiting for a connection ...\n");
            }
            else {
                console.Text = "Server is Stopped.\n";
            }
        }


        // DataProviderDelegates

        void DataProviderClientConnectedWriter(DataProvider dataProvider, TcpClient aClient) {
            if (this.InvokeRequired) {
                Invoke(new DataProviderClientConnected(DataProviderClientConnectedWriter), new object[] { dataProvider, aClient });
            }
            else {
                // azione di scrivere che il client e'connesso, non funziona
                printToServerConsole("Client connected!\n");
            }
        }

        void DataProviderClientDisconnectedWriter(DataProvider dataProvider, TcpClient aClient) {
            if (this.InvokeRequired) {
                Invoke(new DataProviderClientDisconnected(DataProviderClientDisconnectedWriter), new object[] { dataProvider, aClient });
            }
            else {
                // azione di scrivere che il client e'connesso, non funziona
                printToServerConsole("Client disconnected!\n");
                // chiamo le chiamate per analizzare le azioni e scrivere

                // moto stazionamento: acc x y z

                double[,] acc = ExtractData(10, 0, 0);
                String[] motoStaz = DataAnalysis.MotoStazionamento(acc, window);

                //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //System.IO.File.WriteAllText(csvPath + @"\output.txt", "Moto stazionamento:");
                //System.IO.File.WriteAllLines(csvPath + @"\output1.txt", motoStaz);
                // do a Luca ...

                // lat/sit/stand
                acc = ExtractData(20, 0, 0);
                int size = acc.GetLength(0);
                double[] accX = new double[size];

                for (int i = 0; i < size; i++) {
                    accX[i] = acc[i, 0];
                }

                String[] layStandSit = DataAnalysis.LayStandSit(accX);

                //System.IO.File.WriteAllText(csvPath + @"\output.txt", "lay Stand Sit:");
                //System.IO.File.WriteAllLines(csvPath + @"\output2.txt", layStandSit);

                // passo a Luca ... 

                // girata

                double[,] mag = ExtractData(10, 2, 0);
                double[] girate = DataAnalysis.Girata(mag);
                //System.IO.File.WriteAllText(csvPath + @"\output3.txt", "Girate:");

                String[] test = new String[girate.Count()];
                for (int i = 0; i < girate.Count(); ++i) {
                    test[i] = girate[i].ToString();
                }
                //System.IO.File.WriteAllLines(csvPath + @"\output3.txt", test);

                // passo a Luca ...
                DataWriter.DataWrite(motoStaz, layStandSit, girate, frequence, csvPath);
                if (samplewin != null) { DataWriter.PrintPacketsToFile(samplewin, csvPath); }


            }
        }

        void DataProviderSampleWindowReceived(DataProvider dataProvider, List<Packet> sampwin) {

            if (this.InvokeRequired) {
                Invoke(new DataProviderSampleWindow(DataProviderSampleWindowReceived), new object[] { dataProvider, sampwin });
            }
            else {
                DisplayData(sampwin);
            }
        }

        void DataProviderServerDidStart(DataProvider dataProvider) {
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
                printToServerConsole(String.Format("Server Started on port {0} at IP " +
                    "{1}.{2}.{3}.{4}\n", port.Text, ip1.Text, ip2.Text, ip3.Text, ip4.Text));
                printToServerConsole("Waiting for a connection ...\n");
            }

        }

        void DataProviderServerDidStop(DataProvider dataProvider) {
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

        private double[] ExtractAngles(double[,] data, int kind) {
            int size = data.GetLength(0);
            double[] e = new double[size];

            for (int i = 0; i < size; i++) {
                e[i] = data[i, kind];
            }

            return e;
        }

        private void EulerGraph() {
            int size = selected.Count;
            double[,] data = new double[size, 4];
            myPane.YAxis.Title.Text = "rad";

            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < 4; ++j) {
                    data[i, j] = selected[i].Sensors[selectedSensor].q[j];
                }
            }

            double[,] euler = DataAnalysis.ComputeEulerAngles(data);
            double[,] eucont = DataAnalysis.RemoveDiscontinuities(euler);

            double[] e0 = ExtractAngles(eucont, 0);
            double[] e1 = ExtractAngles(eucont, 1);
            double[] e2 = ExtractAngles(eucont, 2);
            smooth = smoothing_cb.Checked;
            if (!smooth) {
                DisplayEuler(e0, "Roll", true, Color.Blue);
                DisplayEuler(e1, "Pitch", false, Color.Green);
                DisplayEuler(e2, "Yaw", false, Color.Red);
            }
            else {
                DisplayEuler(e0, "Roll Smoothed", true, Color.Blue);
                DisplayEuler(e1, "Pitch Smoothed", false, Color.Green);
                DisplayEuler(e2, "Yaw Smoothed", false, Color.Red);
            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void DisplayEuler(double[] data, String label, Boolean clear, Color color) {
            // prima volta devo pulire?? altra funzione
            //if (clear) { myPane.CurveList.Clear(); }
            PointPairList plist = new PointPairList();
            double t = 0;
            for (int i = 0; i < data.Length; i++) {
                plist.Add(new PointPair(t, data[i]));
                t += 1.0 / frequence;
            }
            myPane.AddCurve(label, plist, color, SymbolType.None);
        }

        // non deve prendere un array di double, ma meglio una lista di packet. altrimenti
        // devo richiareextract data ogni volta che premo un qualsiasi bottone, meglio 
        // fare la chiamata allínterno di display data
        // ricevo un solo parametro, list<Packet>
        // all'interno del metodo richiamare la funzione extractData,
        // e passare i volire della funzione richiesta
        // pulire il grafico nel se non si devono visualizzare gli angoli di eulero
        private void DisplayData(List<Packet> sampwin) {
            // controllo se e' attivo lo smoothing!!
            if (smoothRange < 10) {
                checkBox3.Enabled = false;
                cutOff_value.Enabled = false;
            }
            samplewin = sampwin;
            smoothed = DataAnalysis.SmoothData(samplewin, smoothRange);

            filtered = smoothing_cb.Checked ? smoothed : samplewin;
            if (checkBox1.Checked) {
                selected = DataAnalysis.ComputeHighPass(filtered);
            }
            else {
                if (checkBox2.Checked) {
                    selected = DataAnalysis.ComputeLowPass(filtered);
                }
                else {
                    selected = filtered;
                }
            }
            smooth = smoothing_cb.Checked;
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
                    double t = 0;
                    for (int i = 0; i < modules.Length; i++) {
                        plist.Add(new PointPair(t, modules[i]));
                        t += 1.0 / frequence;
                    }
                    // problemi con i titoli
                    myPane.Title.Text = "Module";
                    if (!smooth) { myPane.AddCurve("Module", plist, Color.Blue, SymbolType.None); }
                    else { myPane.AddCurve("Module Smoothed", plist, Color.Blue, SymbolType.None); }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 1:
                    // der
                    //printToServerConsole("dev");
                    plist.Clear();
                    t = 0;
                    double[] drv = DataAnalysis.ComputeDerivatives(modules, Int32.Parse(frequence_box.Text));
                    for (int i = 0; i < drv.Length; i++) {
                        plist.Add(new PointPair(t, drv[i]));
                        t += 1.0 / frequence;
                    }
                    myPane.Title.Text = "Derivated";
                    if (!smooth) { myPane.AddCurve("Derivated", plist, Color.Blue, SymbolType.None); }
                    else { myPane.AddCurve("Derivated Smoothed", plist, Color.Blue, SymbolType.None); }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 2:
                    // std
                    //printToServerConsole("std");
                    plist.Clear();
                    t = 0;
                    double[] dst0 = DataAnalysis.ComputeStandardDeviations(modules, smoothRange);
                    double epsilon = 0.4;
                    // sotto i 10 non squadra nulla di corretto perche' e' troppo instabile, sopra il 25 tentenna. tra 10 e 25 ok
                    /*if (smoothing_cb.Checked) {
                        if (smoothRange >= 10 && smoothRange < 20) {
                            cutOff = 0.53;
                        }
                        else if (smoothRange >= 20 && smoothRange < 30) {
                            cutOff = 0.33;
                        }
                        else {
                            cutOff = 0.21;
                        }
                    }
                    else {
                        cutOff = 1.5;
                    }*/
                    double[] dst = checkBox3.Checked ? DataAnalysis.ComputeSquare(dst0, frequence, cutOff, epsilon) : dst0;
                    for (int i = 0; i < dst.Length; i++) {
                        plist.Add(new PointPair(t, dst[i]));
                        t += 1.0 / frequence;
                    }
                    myPane.Title.Text = "Module Standard Deviation";
                    if (!smooth) { myPane.AddCurve("Module Standard Deviation", plist, Color.Blue, SymbolType.None); }
                    else { myPane.AddCurve("Smoothed Module Standard Deviation", plist, Color.Blue, SymbolType.None); }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 3:
                    // euler
                    EulerGraph();
                    break;
                case 4:
                    // dead
                    int size = samplewin.Count;

                    double[,] q0 = new double[size, 4];

                    for (int p = 0; p < size; p++) {
                        for (int i = 0; i < 4; i++) {
                            q0[p, i] = samplewin[p].Sensors[0].q[i];
                        }
                    }

                    double[,] acc = new double[size, 3];

                    for (int p = 0; p < size; p++) {
                        for (int i = 0; i < 3; i++) {
                            acc[p, i] = samplewin[p].Sensors[0].acc[i];
                        }
                    }

                    double[,] mag = new double[size, 3];

                    for (int p = 0; p < size; p++) {
                        for (int i = 0; i < 3; i++) {
                            mag[p, i] = samplewin[p].Sensors[0].mag[i];
                        }
                    }


                    PointPairList dead = DataAnalysis.ComputeDeadReckoning(q0, acc, mag, frequence, window);
                    PointPairList start = new PointPairList();
                    start.Add(dead.First());
                    PointPairList end = new PointPairList();
                    end.Add(dead.Last());
                    dead.RemoveAt(0);
                    dead.RemoveAt(dead.Count - 1);

                    GraphPane pane = zedGraphControl1.GraphPane;
                    pane.CurveList.Clear();
                    myPane.Title.Text = "Dead Reckoning";
                    myPane.YAxis.Title.Text = "m";
                    myPane.XAxis.Title.Text = "m";
                    pane.AddCurve("Start point", start, Color.Green, SymbolType.Square);
                    pane.AddCurve("Path", dead, Color.Black, SymbolType.None);
                    pane.AddCurve("End point", end, Color.Red, SymbolType.Circle);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;

                case 5:
                    // arc tan
                    plist.Clear();
                    t = 0;
                    mag = ExtractData(2, selectedSensor);
                    size = mag.GetLength(0);
                    double[] magY = new double[size];
                    double[] magZ = new double[size];

                    for (int i = 0; i < size; i++) {
                        magY[i] = mag[i, 1];
                        magZ[i] = mag[i, 2];
                    }

                    double[,] tan = DataAnalysis.FunzioneOrientamento(magY, magZ);
                    double[,] smtan = DataAnalysis.RemoveDiscontinuities(tan);
                    for (int i = 0; i < size; i++) {
                        plist.Add(new PointPair(t, smtan[i, 0]));
                        t += 1.0 / frequence;
                    }
                    myPane.Title.Text = "Arc Tan(MagY/MagZ)";
                    if (!smooth) { myPane.AddCurve("Arc Tan(MagY/MagZ)", plist, Color.Blue, SymbolType.None); }
                    else { myPane.AddCurve("Arc Tan(MagY/MagZ) Smoothed", plist, Color.Blue, SymbolType.None); }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    break;
                case 6:
                    // acc x
                    plist.Clear();
                    t = 0;
                    acc = ExtractData(0, selectedSensor);
                    size = acc.GetLength(0);
                    double[] accX = new double[size];

                    for (int i = 0; i < size; i++) {
                        accX[i] = acc[i, 0];
                    }

                    double[] accX_opt = checkBox3.Checked ? DataAnalysis.ComputeSquare(accX, frequence, 7, 0.4) : accX;
                    for (int i = 0; i < size; i++) {
                        plist.Add(new PointPair(t, accX_opt[i]));
                        t += 1.0 / frequence;
                    }
                    myPane.YAxis.Title.Text = "N/kg";
                    myPane.Title.Text = "Acc X";
                    if (!smooth) { myPane.AddCurve("Acc X", plist, Color.Blue, SymbolType.None); }
                    else { myPane.AddCurve("Acc X Smoothed", plist, Color.Blue, SymbolType.None); }
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    // modulo acc x
                    // point pair list
                    break;
            }
        }

        private double[,] ExtractData(int range, int sensType, int sensPos) {
            // sampwin - smooto -for
            int size = samplewin.Count;
            double[,] data = new double[size, 3];
            List<Packet> smoothed = DataAnalysis.SmoothData(samplewin, range);
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < 3; j++) {
                    switch (sensType) {
                        case 0:
                            data[i, j] = smoothed[i].Sensors[sensPos].acc[j]; // dipende dal tipo sens
                            break;
                        case 1:
                            data[i, j] = smoothed[i].Sensors[sensPos].gyr[j]; // dipende dal tipo sens
                            break;
                        case 2:
                            data[i, j] = smoothed[i].Sensors[sensPos].mag[j]; // dipende dal tipo sens
                            break;

                        case 3:
                            data[i, j] = smoothed[i].Sensors[sensPos].q[j]; // dipende dal tipo sens
                            break;
                    }

                }
            }
            return data;
        }

        private double[,] ExtractData(int sensType, int sensPos) {
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

        private void smoothing_cb_CheckedChanged(object sender, EventArgs e) {
            if (samplewin != null) {
                //selected = smoothing_cb.Checked ? smoothed : samplewin;
                DisplayData(samplewin);
            }

        }

        private void sensor_type_SelectedIndexChanged(object sender, EventArgs e) {
            selectedSensorType = sensor_type.SelectedIndex;
            if (samplewin != null) {
                // fare una funzione che 
                DisplayData(samplewin);
            }
            //checkBoxSegmentation.Enabled = ((selectedChart == 0 && selectedSensorType == 0) ? true : false);
            //checkBoxSegmentation.Checked = false;
        }

        private void numericUpDown_smoothing_ValueChanged(object sender, EventArgs e) {
            smoothRange = (int)numericUpDown_smoothing.Value;
            if (smoothRange >= 10 && (selectedGraph == 2 || selectedGraph == 6)) { checkBox3.Enabled = true; cutOff_value.Enabled = true; }
            else { checkBox3.Enabled = false; cutOff_value.Enabled = false; }
            /*if (smoothRange >= 10 && smoothing_cb.Checked)
            {
                checkBox3.Enabled = true;
            } else
            {
                checkBox3.Enabled = false; // oppure se non e' checked lo smoothing, cambio il cutoff
            }*/
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void type_of_grph_cb_SelectedIndexChanged(object sender, EventArgs e) {
            selectedGraph = type_of_grph_cb.SelectedIndex;
            if (selectedGraph != 2 && selectedGraph != 6) {
                checkBox3.Enabled = false;
                checkBox3.Checked = false;
                cutOff_value.Enabled = false;
                sensor_type.Items.Clear(); // si potrebbe controllare il numero di item nella lista, se sono 3 agg Qua.
                if (selectedGraph == 3) {
                    sensor_type.Items.AddRange(new object[] {
                        "Qua"});
                    sensor_type.SelectedIndex = sensor_type.FindStringExact("Qua");
                    sensor_position.Enabled = false;
                }
                else {
                    sensor_type.Items.AddRange(new object[] {
                    "Acc",
                    "Gyr",
                    "Mag"});
                    sensor_type.SelectedIndex = sensor_type.FindStringExact("Acc");
                }

            }
            else {
                checkBox3.Enabled = true;
                cutOff_value.Enabled = true;
                sensor_type.Items.Clear();
                sensor_type.Items.AddRange(new object[] {
                    "Acc",
                    "Gyr",
                    "Mag"});
                sensor_type.SelectedIndex = sensor_type.FindStringExact("Acc");
            }

            if (selectedGraph == 5 || selectedGraph == 6) {
                sensor_type.Enabled = false;
                sensor_position.Enabled = true;
                smoothing_cb.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                numericUpDown_smoothing.Enabled = true;
            }
            else
            if (selectedGraph != 4) {
                // sblocco tutto
                sensor_type.Enabled = true;
                sensor_position.Enabled = true;
                smoothing_cb.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                numericUpDown_smoothing.Enabled = true;
                cutOff_value.Enabled = true;
            }
            else {
                sensor_type.Enabled = false;
                sensor_position.Enabled = false;
                smoothing_cb.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                numericUpDown_smoothing.Enabled = false;
                //cutOff_value.Enabled = false;
            }
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void sensor_position_SelectedIndexChanged(object sender, EventArgs e) {
            //selectedGraph = type_of_grph_cb.SelectedIndex;
            selectedSensor = sensor_position.SelectedIndex;
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked == true) { checkBox2.Checked = false; }
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            if (checkBox2.Checked == true) { checkBox1.Checked = false; }
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }

        private void cutOff_value_ValueChanged(object sender, EventArgs e) {
            cutOff = (double)cutOff_value.Value;
            
            if (samplewin != null) {
                DisplayData(samplewin);
            }
        }
    }
}


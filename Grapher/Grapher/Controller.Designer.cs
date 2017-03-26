namespace Grapher
{
    partial class Controller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_console_clear = new System.Windows.Forms.Button();
            this.server_side_label = new System.Windows.Forms.Label();
            this.numericUpDownWindow = new System.Windows.Forms.NumericUpDown();
            this.window_l = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.frequence_box = new System.Windows.Forms.ComboBox();
            this.frequence_l = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.Label();
            this.ip4 = new System.Windows.Forms.TextBox();
            this._dot3 = new System.Windows.Forms.Label();
            this.ip3 = new System.Windows.Forms.TextBox();
            this._dot2 = new System.Windows.Forms.Label();
            this.ip2 = new System.Windows.Forms.TextBox();
            this._dot1 = new System.Windows.Forms.Label();
            this.ip1 = new System.Windows.Forms.TextBox();
            this.label_port = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.csv_path = new System.Windows.Forms.TextBox();
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.btn_server_start = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.type_of_grph_cb = new System.Windows.Forms.ComboBox();
            this.type_of_graph = new System.Windows.Forms.Label();
            this.segmentation_cb = new System.Windows.Forms.CheckBox();
            this.numericUpDown_smoothing = new System.Windows.Forms.NumericUpDown();
            this.smoothing_cb = new System.Windows.Forms.CheckBox();
            this.plot_window_cb = new System.Windows.Forms.CheckBox();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.sensor_position = new System.Windows.Forms.ComboBox();
            this.sensor_type = new System.Windows.Forms.ComboBox();
            this.label_sensor_position = new System.Windows.Forms.Label();
            this.label_sensor_type = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smoothing)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_console_clear);
            this.splitContainer1.Panel1.Controls.Add(this.server_side_label);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownWindow);
            this.splitContainer1.Panel1.Controls.Add(this.window_l);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.frequence_box);
            this.splitContainer1.Panel1.Controls.Add(this.frequence_l);
            this.splitContainer1.Panel1.Controls.Add(this.ip);
            this.splitContainer1.Panel1.Controls.Add(this.ip4);
            this.splitContainer1.Panel1.Controls.Add(this._dot3);
            this.splitContainer1.Panel1.Controls.Add(this.ip3);
            this.splitContainer1.Panel1.Controls.Add(this._dot2);
            this.splitContainer1.Panel1.Controls.Add(this.ip2);
            this.splitContainer1.Panel1.Controls.Add(this._dot1);
            this.splitContainer1.Panel1.Controls.Add(this.ip1);
            this.splitContainer1.Panel1.Controls.Add(this.label_port);
            this.splitContainer1.Panel1.Controls.Add(this.port);
            this.splitContainer1.Panel1.Controls.Add(this.csv_path);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSelectFolder);
            this.splitContainer1.Panel1.Controls.Add(this.btn_server_start);
            this.splitContainer1.Panel1.Controls.Add(this.console);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.type_of_grph_cb);
            this.splitContainer1.Panel2.Controls.Add(this.type_of_graph);
            this.splitContainer1.Panel2.Controls.Add(this.segmentation_cb);
            this.splitContainer1.Panel2.Controls.Add(this.numericUpDown_smoothing);
            this.splitContainer1.Panel2.Controls.Add(this.smoothing_cb);
            this.splitContainer1.Panel2.Controls.Add(this.plot_window_cb);
            this.splitContainer1.Panel2.Controls.Add(this.zedGraphControl1);
            this.splitContainer1.Panel2.Controls.Add(this.sensor_position);
            this.splitContainer1.Panel2.Controls.Add(this.sensor_type);
            this.splitContainer1.Panel2.Controls.Add(this.label_sensor_position);
            this.splitContainer1.Panel2.Controls.Add(this.label_sensor_type);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 611);
            this.splitContainer1.SplitterDistance = 275;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_console_clear
            // 
            this.btn_console_clear.Location = new System.Drawing.Point(92, 576);
            this.btn_console_clear.Name = "btn_console_clear";
            this.btn_console_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_console_clear.TabIndex = 19;
            this.btn_console_clear.Text = "CLEAR";
            this.btn_console_clear.UseVisualStyleBackColor = true;
            // 
            // server_side_label
            // 
            this.server_side_label.AutoSize = true;
            this.server_side_label.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_side_label.Location = new System.Drawing.Point(54, 29);
            this.server_side_label.Name = "server_side_label";
            this.server_side_label.Size = new System.Drawing.Size(166, 27);
            this.server_side_label.TabIndex = 18;
            this.server_side_label.Text = "Server Side";
            // 
            // numericUpDownWindow
            // 
            this.numericUpDownWindow.Location = new System.Drawing.Point(93, 208);
            this.numericUpDownWindow.Name = "numericUpDownWindow";
            this.numericUpDownWindow.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownWindow.TabIndex = 17;
            this.numericUpDownWindow.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // window_l
            // 
            this.window_l.AutoSize = true;
            this.window_l.Location = new System.Drawing.Point(18, 210);
            this.window_l.Name = "window_l";
            this.window_l.Size = new System.Drawing.Size(46, 13);
            this.window_l.TabIndex = 16;
            this.window_l.Text = "Window";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Hz";
            // 
            // frequence_box
            // 
            this.frequence_box.FormattingEnabled = true;
            this.frequence_box.Items.AddRange(new object[] {
            "50",
            "100",
            "200"});
            this.frequence_box.Location = new System.Drawing.Point(93, 170);
            this.frequence_box.Name = "frequence_box";
            this.frequence_box.Size = new System.Drawing.Size(60, 21);
            this.frequence_box.TabIndex = 14;
            // 
            // frequence_l
            // 
            this.frequence_l.AutoSize = true;
            this.frequence_l.Location = new System.Drawing.Point(15, 170);
            this.frequence_l.Name = "frequence_l";
            this.frequence_l.Size = new System.Drawing.Size(58, 13);
            this.frequence_l.TabIndex = 13;
            this.frequence_l.Text = "Frequence";
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Location = new System.Drawing.Point(15, 136);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(17, 13);
            this.ip.TabIndex = 12;
            this.ip.Text = "IP";
            // 
            // ip4
            // 
            this.ip4.Location = new System.Drawing.Point(243, 132);
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(26, 20);
            this.ip4.TabIndex = 11;
            this.ip4.Text = "1";
            // 
            // _dot3
            // 
            this._dot3.AutoSize = true;
            this._dot3.Location = new System.Drawing.Point(226, 140);
            this._dot3.Name = "_dot3";
            this._dot3.Size = new System.Drawing.Size(10, 13);
            this._dot3.TabIndex = 10;
            this._dot3.Text = ".";
            // 
            // ip3
            // 
            this.ip3.Location = new System.Drawing.Point(194, 133);
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(26, 20);
            this.ip3.TabIndex = 9;
            this.ip3.Text = "0";
            // 
            // _dot2
            // 
            this._dot2.AutoSize = true;
            this._dot2.Location = new System.Drawing.Point(178, 140);
            this._dot2.Name = "_dot2";
            this._dot2.Size = new System.Drawing.Size(10, 13);
            this._dot2.TabIndex = 8;
            this._dot2.Text = ".";
            // 
            // ip2
            // 
            this.ip2.Location = new System.Drawing.Point(141, 133);
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(26, 20);
            this.ip2.TabIndex = 7;
            this.ip2.Text = "0";
            // 
            // _dot1
            // 
            this._dot1.AutoSize = true;
            this._dot1.Location = new System.Drawing.Point(125, 140);
            this._dot1.Name = "_dot1";
            this._dot1.Size = new System.Drawing.Size(10, 13);
            this._dot1.TabIndex = 6;
            this._dot1.Text = ".";
            // 
            // ip1
            // 
            this.ip1.Location = new System.Drawing.Point(93, 133);
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(26, 20);
            this.ip1.TabIndex = 5;
            this.ip1.Text = "127";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(15, 101);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(26, 13);
            this.label_port.TabIndex = 4;
            this.label_port.Text = "Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(93, 97);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(39, 20);
            this.port.TabIndex = 3;
            this.port.Text = "45555";
            // 
            // csv_path
            // 
            this.csv_path.Location = new System.Drawing.Point(12, 270);
            this.csv_path.Name = "csv_path";
            this.csv_path.Size = new System.Drawing.Size(194, 20);
            this.csv_path.TabIndex = 2;
            this.csv_path.Text = "CSV Location";
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(212, 270);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(24, 22);
            this.buttonSelectFolder.TabIndex = 22;
            this.buttonSelectFolder.Text = "...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // btn_server_start
            // 
            this.btn_server_start.Location = new System.Drawing.Point(93, 305);
            this.btn_server_start.Name = "btn_server_start";
            this.btn_server_start.Size = new System.Drawing.Size(75, 23);
            this.btn_server_start.TabIndex = 1;
            this.btn_server_start.Text = "START";
            this.btn_server_start.UseVisualStyleBackColor = true;
            this.btn_server_start.Click += new System.EventHandler(this.btn_server_start_Click);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.SystemColors.MenuText;
            this.console.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.console.Location = new System.Drawing.Point(12, 343);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(251, 212);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // type_of_grph_cb
            // 
            this.type_of_grph_cb.FormattingEnabled = true;
            this.type_of_grph_cb.Items.AddRange(new object[] {
            "Modulo",
            "Derivata",
            "Eulero",
            "Deviazione",
            "Dead Reckoning"});
            this.type_of_grph_cb.Location = new System.Drawing.Point(116, 25);
            this.type_of_grph_cb.Name = "type_of_grph_cb";
            this.type_of_grph_cb.Size = new System.Drawing.Size(100, 21);
            this.type_of_grph_cb.TabIndex = 10;
            // 
            // type_of_graph
            // 
            this.type_of_graph.AutoSize = true;
            this.type_of_graph.Location = new System.Drawing.Point(64, 29);
            this.type_of_graph.Name = "type_of_graph";
            this.type_of_graph.Size = new System.Drawing.Size(36, 13);
            this.type_of_graph.TabIndex = 9;
            this.type_of_graph.Text = "Graph";
            // 
            // segmentation_cb
            // 
            this.segmentation_cb.AutoSize = true;
            this.segmentation_cb.Location = new System.Drawing.Point(706, 581);
            this.segmentation_cb.Name = "segmentation_cb";
            this.segmentation_cb.Size = new System.Drawing.Size(91, 17);
            this.segmentation_cb.TabIndex = 8;
            this.segmentation_cb.Text = "Segmentation";
            this.segmentation_cb.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_smoothing
            // 
            this.numericUpDown_smoothing.Location = new System.Drawing.Point(515, 581);
            this.numericUpDown_smoothing.Name = "numericUpDown_smoothing";
            this.numericUpDown_smoothing.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown_smoothing.TabIndex = 7;
            this.numericUpDown_smoothing.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // smoothing_cb
            // 
            this.smoothing_cb.AutoSize = true;
            this.smoothing_cb.Checked = true;
            this.smoothing_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smoothing_cb.Location = new System.Drawing.Point(432, 581);
            this.smoothing_cb.Name = "smoothing_cb";
            this.smoothing_cb.Size = new System.Drawing.Size(76, 17);
            this.smoothing_cb.TabIndex = 6;
            this.smoothing_cb.Text = "Smoothing";
            this.smoothing_cb.UseVisualStyleBackColor = true;
            // 
            // plot_window_cb
            // 
            this.plot_window_cb.AutoSize = true;
            this.plot_window_cb.Location = new System.Drawing.Point(89, 582);
            this.plot_window_cb.Name = "plot_window_cb";
            this.plot_window_cb.Size = new System.Drawing.Size(124, 17);
            this.plot_window_cb.TabIndex = 5;
            this.plot_window_cb.Text = "Plot only last window";
            this.plot_window_cb.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(14, 72);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(838, 501);
            this.zedGraphControl1.TabIndex = 4;
            // 
            // sensor_position
            // 
            this.sensor_position.FormattingEnabled = true;
            this.sensor_position.Items.AddRange(new object[] {
            "1 (bacino)",
            "2 (Polso Dx)",
            "3 (Polso Sx)",
            "4 (Caviglia Dx)",
            "5 (Caviglia Sx)"});
            this.sensor_position.Location = new System.Drawing.Point(689, 25);
            this.sensor_position.Name = "sensor_position";
            this.sensor_position.Size = new System.Drawing.Size(100, 21);
            this.sensor_position.TabIndex = 3;
            // 
            // sensor_type
            // 
            this.sensor_type.FormattingEnabled = true;
            this.sensor_type.Items.AddRange(new object[] {
            "Acc",
            "Gyr",
            "Mag",
            "Qua"});
            this.sensor_type.Location = new System.Drawing.Point(372, 26);
            this.sensor_type.Name = "sensor_type";
            this.sensor_type.Size = new System.Drawing.Size(100, 21);
            this.sensor_type.TabIndex = 2;
            // 
            // label_sensor_position
            // 
            this.label_sensor_position.AutoSize = true;
            this.label_sensor_position.Location = new System.Drawing.Point(587, 29);
            this.label_sensor_position.Name = "label_sensor_position";
            this.label_sensor_position.Size = new System.Drawing.Size(80, 13);
            this.label_sensor_position.TabIndex = 1;
            this.label_sensor_position.Text = "Sensor Position";
            // 
            // label_sensor_type
            // 
            this.label_sensor_type.AutoSize = true;
            this.label_sensor_type.Location = new System.Drawing.Point(290, 29);
            this.label_sensor_type.Name = "label_sensor_type";
            this.label_sensor_type.Size = new System.Drawing.Size(67, 13);
            this.label_sensor_type.TabIndex = 0;
            this.label_sensor_type.Text = "Sensor Type";
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 611);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Controller";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smoothing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.TextBox csv_path;
        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_server_start;
        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.TextBox ip4;
        private System.Windows.Forms.Label _dot3;
        private System.Windows.Forms.TextBox ip3;
        private System.Windows.Forms.Label _dot2;
        private System.Windows.Forms.TextBox ip2;
        private System.Windows.Forms.Label _dot1;
        private System.Windows.Forms.TextBox ip1;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.ComboBox frequence_box;
        private System.Windows.Forms.Label frequence_l;
        private System.Windows.Forms.Button btn_console_clear;
        private System.Windows.Forms.Label server_side_label;
        private System.Windows.Forms.NumericUpDown numericUpDownWindow;
        private System.Windows.Forms.Label window_l;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox segmentation_cb;
        private System.Windows.Forms.NumericUpDown numericUpDown_smoothing;
        private System.Windows.Forms.CheckBox smoothing_cb;
        private System.Windows.Forms.CheckBox plot_window_cb;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox sensor_position;
        private System.Windows.Forms.ComboBox sensor_type;
        private System.Windows.Forms.Label label_sensor_position;
        private System.Windows.Forms.Label label_sensor_type;
        private System.Windows.Forms.ComboBox type_of_grph_cb;
        private System.Windows.Forms.Label type_of_graph;
    }
}


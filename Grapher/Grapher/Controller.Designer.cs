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
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.type_of_grph_cb = new System.Windows.Forms.ComboBox();
            this.type_of_graph = new System.Windows.Forms.Label();
            this.numericUpDown_smoothing = new System.Windows.Forms.NumericUpDown();
            this.smoothing_cb = new System.Windows.Forms.CheckBox();
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
            this.splitContainer1.Panel2.Controls.Add(this.checkBox3);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox2);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel2.Controls.Add(this.type_of_grph_cb);
            this.splitContainer1.Panel2.Controls.Add(this.type_of_graph);
            this.splitContainer1.Panel2.Controls.Add(this.numericUpDown_smoothing);
            this.splitContainer1.Panel2.Controls.Add(this.smoothing_cb);
            this.splitContainer1.Panel2.Controls.Add(this.zedGraphControl1);
            this.splitContainer1.Panel2.Controls.Add(this.sensor_position);
            this.splitContainer1.Panel2.Controls.Add(this.sensor_type);
            this.splitContainer1.Panel2.Controls.Add(this.label_sensor_position);
            this.splitContainer1.Panel2.Controls.Add(this.label_sensor_type);
            this.splitContainer1.Size = new System.Drawing.Size(1143, 611);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_console_clear
            // 
            this.btn_console_clear.Location = new System.Drawing.Point(101, 578);
            this.btn_console_clear.Name = "btn_console_clear";
            this.btn_console_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_console_clear.TabIndex = 19;
            this.btn_console_clear.Text = "CLEAR";
            this.btn_console_clear.UseVisualStyleBackColor = true;
            this.btn_console_clear.Click += new System.EventHandler(this.btn_console_clear_Click);
            // 
            // server_side_label
            // 
            this.server_side_label.AutoSize = true;
            this.server_side_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.server_side_label.Location = new System.Drawing.Point(43, 16);
            this.server_side_label.Name = "server_side_label";
            this.server_side_label.Size = new System.Drawing.Size(188, 58);
            this.server_side_label.TabIndex = 20;
            this.server_side_label.Text = "Grapher\r\nServer settings";
            this.server_side_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownWindow
            // 
            this.numericUpDownWindow.Location = new System.Drawing.Point(76, 199);
            this.numericUpDownWindow.Name = "numericUpDownWindow";
            this.numericUpDownWindow.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownWindow.TabIndex = 6;
            this.numericUpDownWindow.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // window_l
            // 
            this.window_l.AutoSize = true;
            this.window_l.Location = new System.Drawing.Point(12, 201);
            this.window_l.Name = "window_l";
            this.window_l.Size = new System.Drawing.Size(46, 13);
            this.window_l.TabIndex = 27;
            this.window_l.Text = "Window";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 166);
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
            this.frequence_box.Location = new System.Drawing.Point(76, 163);
            this.frequence_box.Name = "frequence_box";
            this.frequence_box.Size = new System.Drawing.Size(60, 21);
            this.frequence_box.TabIndex = 5;
            // 
            // frequence_l
            // 
            this.frequence_l.AutoSize = true;
            this.frequence_l.Location = new System.Drawing.Point(12, 166);
            this.frequence_l.Name = "frequence_l";
            this.frequence_l.Size = new System.Drawing.Size(58, 13);
            this.frequence_l.TabIndex = 26;
            this.frequence_l.Text = "Frequence";
            // 
            // ip
            // 
            this.ip.AutoSize = true;
            this.ip.Location = new System.Drawing.Point(12, 132);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(17, 13);
            this.ip.TabIndex = 25;
            this.ip.Text = "IP";
            // 
            // ip4
            // 
            this.ip4.Location = new System.Drawing.Point(186, 129);
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(26, 20);
            this.ip4.TabIndex = 4;
            this.ip4.Text = "1";
            this.ip4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _dot3
            // 
            this._dot3.AutoSize = true;
            this._dot3.Location = new System.Drawing.Point(176, 135);
            this._dot3.Name = "_dot3";
            this._dot3.Size = new System.Drawing.Size(10, 13);
            this._dot3.TabIndex = 30;
            this._dot3.Text = ".";
            // 
            // ip3
            // 
            this.ip3.Location = new System.Drawing.Point(150, 129);
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(26, 20);
            this.ip3.TabIndex = 3;
            this.ip3.Text = "0";
            this.ip3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _dot2
            // 
            this._dot2.AutoSize = true;
            this._dot2.Location = new System.Drawing.Point(139, 136);
            this._dot2.Name = "_dot2";
            this._dot2.Size = new System.Drawing.Size(10, 13);
            this._dot2.TabIndex = 29;
            this._dot2.Text = ".";
            // 
            // ip2
            // 
            this.ip2.Location = new System.Drawing.Point(112, 129);
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(26, 20);
            this.ip2.TabIndex = 2;
            this.ip2.Text = "0";
            this.ip2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _dot1
            // 
            this._dot1.AutoSize = true;
            this._dot1.Location = new System.Drawing.Point(102, 136);
            this._dot1.Name = "_dot1";
            this._dot1.Size = new System.Drawing.Size(10, 13);
            this._dot1.TabIndex = 28;
            this._dot1.Text = ".";
            // 
            // ip1
            // 
            this.ip1.Location = new System.Drawing.Point(76, 129);
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(26, 20);
            this.ip1.TabIndex = 1;
            this.ip1.Text = "127";
            this.ip1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(12, 97);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(26, 13);
            this.label_port.TabIndex = 24;
            this.label_port.Text = "Port";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(76, 95);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(39, 20);
            this.port.TabIndex = 0;
            this.port.Text = "45555";
            // 
            // csv_path
            // 
            this.csv_path.Location = new System.Drawing.Point(15, 229);
            this.csv_path.Name = "csv_path";
            this.csv_path.Size = new System.Drawing.Size(217, 20);
            this.csv_path.TabIndex = 7;
            this.csv_path.Text = "CSV Location";
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.Location = new System.Drawing.Point(238, 228);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(24, 22);
            this.buttonSelectFolder.TabIndex = 22;
            this.buttonSelectFolder.Text = "...";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // btn_server_start
            // 
            this.btn_server_start.Location = new System.Drawing.Point(101, 255);
            this.btn_server_start.Name = "btn_server_start";
            this.btn_server_start.Size = new System.Drawing.Size(75, 23);
            this.btn_server_start.TabIndex = 8;
            this.btn_server_start.Text = "START";
            this.btn_server_start.UseVisualStyleBackColor = true;
            this.btn_server_start.Click += new System.EventHandler(this.btn_server_start_Click);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.SystemColors.MenuText;
            this.console.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.console.Location = new System.Drawing.Point(15, 284);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(247, 288);
            this.console.TabIndex = 18;
            this.console.Text = "";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(785, 18);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 17);
            this.checkBox3.TabIndex = 16;
            this.checkBox3.Text = "Squared";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(673, 28);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(97, 17);
            this.checkBox2.TabIndex = 15;
            this.checkBox2.Text = "Low-Pass Filter";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(673, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "High-Pass Filter";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // type_of_grph_cb
            // 
            this.type_of_grph_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_of_grph_cb.FormattingEnabled = true;
            this.type_of_grph_cb.Items.AddRange(new object[] {
            "Modulo",
            "Derivata",
            "Deviazione",
            "Eulero",
            "Dead Reckoning"});
            this.type_of_grph_cb.Location = new System.Drawing.Point(66, 16);
            this.type_of_grph_cb.Name = "type_of_grph_cb";
            this.type_of_grph_cb.Size = new System.Drawing.Size(100, 21);
            this.type_of_grph_cb.TabIndex = 9;
            this.type_of_grph_cb.SelectedIndexChanged += new System.EventHandler(this.type_of_grph_cb_SelectedIndexChanged);
            // 
            // type_of_graph
            // 
            this.type_of_graph.AutoSize = true;
            this.type_of_graph.Location = new System.Drawing.Point(24, 20);
            this.type_of_graph.Name = "type_of_graph";
            this.type_of_graph.Size = new System.Drawing.Size(36, 13);
            this.type_of_graph.TabIndex = 21;
            this.type_of_graph.Text = "Graph";
            // 
            // numericUpDown_smoothing
            // 
            this.numericUpDown_smoothing.Location = new System.Drawing.Point(621, 16);
            this.numericUpDown_smoothing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_smoothing.Name = "numericUpDown_smoothing";
            this.numericUpDown_smoothing.Size = new System.Drawing.Size(37, 20);
            this.numericUpDown_smoothing.TabIndex = 13;
            this.numericUpDown_smoothing.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_smoothing.ValueChanged += new System.EventHandler(this.numericUpDown_smoothing_ValueChanged);
            // 
            // smoothing_cb
            // 
            this.smoothing_cb.AutoSize = true;
            this.smoothing_cb.Checked = true;
            this.smoothing_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smoothing_cb.Location = new System.Drawing.Point(543, 18);
            this.smoothing_cb.Name = "smoothing_cb";
            this.smoothing_cb.Size = new System.Drawing.Size(76, 17);
            this.smoothing_cb.TabIndex = 12;
            this.smoothing_cb.Text = "Smoothing";
            this.smoothing_cb.UseVisualStyleBackColor = true;
            this.smoothing_cb.CheckedChanged += new System.EventHandler(this.smoothing_cb_CheckedChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 53);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(853, 548);
            this.zedGraphControl1.TabIndex = 17;
            // 
            // sensor_position
            // 
            this.sensor_position.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sensor_position.FormattingEnabled = true;
            this.sensor_position.Items.AddRange(new object[] {
            "1 (bacino)",
            "2 (Polso Dx)",
            "3 (Polso Sx)",
            "4 (Caviglia Dx)",
            "5 (Caviglia Sx)"});
            this.sensor_position.Location = new System.Drawing.Point(437, 16);
            this.sensor_position.Name = "sensor_position";
            this.sensor_position.Size = new System.Drawing.Size(100, 21);
            this.sensor_position.TabIndex = 11;
            this.sensor_position.SelectedIndexChanged += new System.EventHandler(this.sensor_position_SelectedIndexChanged);
            // 
            // sensor_type
            // 
            this.sensor_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sensor_type.FormattingEnabled = true;
            this.sensor_type.Items.AddRange(new object[] {
            "Acc",
            "Gyr",
            "Mag",
            "Qua"});
            this.sensor_type.Location = new System.Drawing.Point(245, 17);
            this.sensor_type.Name = "sensor_type";
            this.sensor_type.Size = new System.Drawing.Size(100, 21);
            this.sensor_type.TabIndex = 10;
            this.sensor_type.SelectedIndexChanged += new System.EventHandler(this.sensor_type_SelectedIndexChanged);
            // 
            // label_sensor_position
            // 
            this.label_sensor_position.AutoSize = true;
            this.label_sensor_position.Location = new System.Drawing.Point(351, 20);
            this.label_sensor_position.Name = "label_sensor_position";
            this.label_sensor_position.Size = new System.Drawing.Size(80, 13);
            this.label_sensor_position.TabIndex = 23;
            this.label_sensor_position.Text = "Sensor Position";
            // 
            // label_sensor_type
            // 
            this.label_sensor_type.AutoSize = true;
            this.label_sensor_type.Location = new System.Drawing.Point(172, 20);
            this.label_sensor_type.Name = "label_sensor_type";
            this.label_sensor_type.Size = new System.Drawing.Size(67, 13);
            this.label_sensor_type.TabIndex = 22;
            this.label_sensor_type.Text = "Sensor Type";
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 611);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Controller";
            this.Text = "Grapher";
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
        private System.Windows.Forms.NumericUpDown numericUpDown_smoothing;
        private System.Windows.Forms.CheckBox smoothing_cb;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.ComboBox sensor_position;
        private System.Windows.Forms.ComboBox sensor_type;
        private System.Windows.Forms.Label label_sensor_position;
        private System.Windows.Forms.Label label_sensor_type;
        private System.Windows.Forms.ComboBox type_of_grph_cb;
        private System.Windows.Forms.Label type_of_graph;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}


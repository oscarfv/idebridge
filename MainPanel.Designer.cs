namespace IdeBridge
{
    partial class MainPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPanel));
            this.waitingConnectionWorker = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.clients = new System.Windows.Forms.ListBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.configurationGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.bufferBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.errors = new System.Windows.Forms.ListBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.serverStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.SharpPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            //
            // waitingConnectionWorker
            //
            this.waitingConnectionWorker.WorkerReportsProgress = true;
            this.waitingConnectionWorker.WorkerSupportsCancellation = true;
            //
            // notifyIcon
            //
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            //
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(896, 400);
            this.tabControl1.TabIndex = 5;
            //
            // tabPage1
            //
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(888, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // splitContainer1.Panel1
            //
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.clients);
            this.splitContainer1.Panel1.Controls.Add(this.stopButton);
            this.splitContainer1.Panel1.Controls.Add(this.startButton);
            //
            // splitContainer1.Panel2
            //
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer1.Size = new System.Drawing.Size(882, 368);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 4;
            //
            // button1
            //
            this.button1.Location = new System.Drawing.Point(480, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clients:";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(-1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "IdeBridge Server";
            //
            // clients
            //
            this.clients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clients.FormattingEnabled = true;
            this.clients.Location = new System.Drawing.Point(5, 63);
            this.clients.Name = "clients";
            this.clients.Size = new System.Drawing.Size(872, 82);
            this.clients.TabIndex = 4;
            //
            // stopButton
            //
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Location = new System.Drawing.Point(776, 32);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(102, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            //
            // startButton
            //
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(776, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(101, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            //
            // outputBox
            //
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Location = new System.Drawing.Point(3, 3);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(868, 169);
            this.outputBox.TabIndex = 0;
            this.outputBox.Text = "";
            //
            // tabPage2
            //
            this.tabPage2.Controls.Add(this.configurationGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(888, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // configurationGrid
            //
            this.configurationGrid.CategoryForeColor = System.Drawing.Color.Red;
            this.configurationGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationGrid.HelpBackColor = System.Drawing.Color.LightSkyBlue;
            this.configurationGrid.Location = new System.Drawing.Point(3, 3);
            this.configurationGrid.Name = "configurationGrid";
            this.configurationGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.configurationGrid.Size = new System.Drawing.Size(882, 368);
            this.configurationGrid.TabIndex = 0;
            this.configurationGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.configurationGrid_PropertyValueChanged);
            //
            // tabPage3
            //
            this.tabPage3.Controls.Add(this.bufferBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(888, 374);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Buffer";
            this.tabPage3.UseVisualStyleBackColor = true;
            //
            // bufferBox
            //
            this.bufferBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bufferBox.Location = new System.Drawing.Point(3, 3);
            this.bufferBox.Name = "bufferBox";
            this.bufferBox.Size = new System.Drawing.Size(882, 368);
            this.bufferBox.TabIndex = 0;
            this.bufferBox.Text = "";
            //
            // groupBox1
            //
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(659, 163);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            //
            // errors
            //
            this.errors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errors.FormattingEnabled = true;
            this.errors.Location = new System.Drawing.Point(3, 16);
            this.errors.Name = "errors";
            this.errors.Size = new System.Drawing.Size(653, 134);
            this.errors.TabIndex = 0;
            //
            // statusStrip2
            //
            this.statusStrip2.Location = new System.Drawing.Point(3, 138);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(653, 22);
            this.statusStrip2.TabIndex = 1;
            //
            // serverStatus
            //
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(69, 17);
            this.serverStatus.Text = "serverStatus";
            //
            // groupBox2
            //
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 167);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            //
            // listBox1
            //
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(667, 147);
            this.listBox1.TabIndex = 0;
            //
            // tabControl2
            //
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.SharpPage);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(882, 201);
            this.tabControl2.TabIndex = 1;
            //
            // tabPage4
            //
            this.tabPage4.Controls.Add(this.outputBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(874, 175);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Logs";
            this.tabPage4.UseVisualStyleBackColor = true;
            //
            // SharpPage
            //
            this.SharpPage.Location = new System.Drawing.Point(4, 22);
            this.SharpPage.Name = "SharpPage";
            this.SharpPage.Padding = new System.Windows.Forms.Padding(3);
            this.SharpPage.Size = new System.Drawing.Size(874, 175);
            this.SharpPage.TabIndex = 1;
            this.SharpPage.Text = "SharpDev";
            this.SharpPage.UseVisualStyleBackColor = true;
            //
            // MainPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 400);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainPanel";
            this.Text = "IdeBridge server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.Resize += new System.EventHandler(this.ServerForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.ComponentModel.BackgroundWorker waitingConnectionWorker;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid configurationGrid;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox errors;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel serverStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.RichTextBox bufferBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox clients;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.TabPage SharpPage;
     }
}


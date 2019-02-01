namespace ApexMongoMonitor
{
    partial class SelectConnection
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
            this.btnAddConn = new System.Windows.Forms.Button();
            this.btnEditConn = new System.Windows.Forms.Button();
            this.btnDelConn = new System.Windows.Forms.Button();
            this.btnConn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.txtDBname = new System.Windows.Forms.ComboBox();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.lstViewAvailableConnections = new ApexMongoMonitor.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddConn
            // 
            this.btnAddConn.Location = new System.Drawing.Point(15, 242);
            this.btnAddConn.Name = "btnAddConn";
            this.btnAddConn.Size = new System.Drawing.Size(105, 23);
            this.btnAddConn.TabIndex = 1;
            this.btnAddConn.Text = "Add Connection";
            this.btnAddConn.UseVisualStyleBackColor = true;
            this.btnAddConn.Click += new System.EventHandler(this.btnAddConn_Click);
            // 
            // btnEditConn
            // 
            this.btnEditConn.Location = new System.Drawing.Point(126, 242);
            this.btnEditConn.Name = "btnEditConn";
            this.btnEditConn.Size = new System.Drawing.Size(105, 23);
            this.btnEditConn.TabIndex = 2;
            this.btnEditConn.Text = "Edit Connection";
            this.btnEditConn.UseVisualStyleBackColor = true;
            this.btnEditConn.Click += new System.EventHandler(this.btnEditConn_Click);
            // 
            // btnDelConn
            // 
            this.btnDelConn.Location = new System.Drawing.Point(237, 242);
            this.btnDelConn.Name = "btnDelConn";
            this.btnDelConn.Size = new System.Drawing.Size(105, 23);
            this.btnDelConn.TabIndex = 3;
            this.btnDelConn.Text = "Delete Connection";
            this.btnDelConn.UseVisualStyleBackColor = true;
            this.btnDelConn.Click += new System.EventHandler(this.btnDelConn_Click);
            // 
            // btnConn
            // 
            this.btnConn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConn.Location = new System.Drawing.Point(389, 242);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(68, 23);
            this.btnConn.TabIndex = 4;
            this.btnConn.Text = "Connect";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(463, 242);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.HideTabsMode = Crownwood.Magic.Controls.TabControl.HideTabsModes.ShowAlways;
            this.tabControl1.Location = new System.Drawing.Point(15, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTab = this.tabPage1;
            this.tabControl1.ShowArrows = true;
            this.tabControl1.Size = new System.Drawing.Size(516, 214);
            this.tabControl1.TabIndex = 6;
            this.tabControl1.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.tabControl1.SelectionChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDBname);
            this.tabPage2.Controls.Add(this.btnRetrieve);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtPort);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtHost);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(516, 189);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Title = "Add/Edit Connection";
            // 
            // txtDBname
            // 
            this.txtDBname.FormattingEnabled = true;
            this.txtDBname.Location = new System.Drawing.Point(73, 52);
            this.txtDBname.Name = "txtDBname";
            this.txtDBname.Size = new System.Drawing.Size(303, 21);
            this.txtDBname.TabIndex = 9;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Location = new System.Drawing.Point(398, 50);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(75, 23);
            this.btnRetrieve.TabIndex = 8;
            this.btnRetrieve.Text = "Retrieve";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(433, 153);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(428, 16);
            this.txtPort.MaxLength = 9;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(73, 21);
            this.txtPort.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(73, 16);
            this.txtHost.MaxLength = 50;
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(303, 21);
            this.txtHost.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstViewAvailableConnections);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(516, 189);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Title = "Available";
            // 
            // lstViewAvailableConnections
            // 
            this.lstViewAvailableConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstViewAvailableConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewAvailableConnections.FullRowSelect = true;
            this.lstViewAvailableConnections.HideSelection = false;
            this.lstViewAvailableConnections.Location = new System.Drawing.Point(0, 0);
            this.lstViewAvailableConnections.MultiSelect = false;
            this.lstViewAvailableConnections.Name = "lstViewAvailableConnections";
            this.lstViewAvailableConnections.Size = new System.Drawing.Size(516, 189);
            this.lstViewAvailableConnections.TabIndex = 1;
            this.lstViewAvailableConnections.UseCompatibleStateImageBehavior = false;
            this.lstViewAvailableConnections.View = System.Windows.Forms.View.Details;
            this.lstViewAvailableConnections.DoubleClick += new System.EventHandler(this.lstViewAvailableConnections_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Connection";
            this.columnHeader1.Width = 505;
            // 
            // SelectConnection
            // 
            this.AcceptButton = this.btnConn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(543, 276);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.btnDelConn);
            this.Controls.Add(this.btnEditConn);
            this.Controls.Add(this.btnAddConn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectConnection";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Connection";
            this.Load += new System.EventHandler(this.SelectConnection_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectConnection_FormClosing);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddConn;
        private System.Windows.Forms.Button btnEditConn;
        private System.Windows.Forms.Button btnDelConn;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnClose;
        private Crownwood.Magic.Controls.TabControl tabControl1;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private ListView lstViewAvailableConnections;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.ComboBox txtDBname;
    }
}
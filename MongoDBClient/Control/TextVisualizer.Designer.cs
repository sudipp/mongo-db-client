namespace ApexMongoMonitor.Control
{
    partial class TextVisualizer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsSaveText = new System.Windows.Forms.ToolStripButton();
            this.txtFieldProp = new System.Windows.Forms.TextBox();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSaveText});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(188, 25);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.Text = "toolStrip1";
            // 
            // tsSaveText
            // 
            this.tsSaveText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSaveText.Image = global::ApexMongoMonitor.Properties.Resources.table_save;
            this.tsSaveText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveText.Name = "tsSaveText";
            this.tsSaveText.Size = new System.Drawing.Size(23, 22);
            this.tsSaveText.Text = "toolStripButton1";
            // 
            // txtFieldProp
            // 
            this.txtFieldProp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFieldProp.Location = new System.Drawing.Point(0, 25);
            this.txtFieldProp.Multiline = true;
            this.txtFieldProp.Name = "txtFieldProp";
            this.txtFieldProp.Size = new System.Drawing.Size(188, 153);
            this.txtFieldProp.TabIndex = 2;
            // 
            // TextVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtFieldProp);
            this.Controls.Add(this.ToolStrip1);
            this.Name = "TextVisualizer";
            this.Size = new System.Drawing.Size(188, 178);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ToolStrip1;
        private System.Windows.Forms.ToolStripButton tsSaveText;
        private System.Windows.Forms.TextBox txtFieldProp;
    }
}

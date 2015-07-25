namespace DatabaseManager.UI
{
    partial class ImportToSQLWindow
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.logList = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(453, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // mFile
            // 
            this.mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mExit});
            this.mFile.Name = "mFile";
            this.mFile.Size = new System.Drawing.Size(37, 20);
            this.mFile.Text = "File";
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(92, 22);
            this.mExit.Text = "Exit";
            // 
            // statusMain
            // 
            this.statusMain.Location = new System.Drawing.Point(0, 360);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(453, 22);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusStrip1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnExcel);
            this.pnlMain.Controls.Add(this.logList);
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.btnExport);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(453, 336);
            this.pnlMain.TabIndex = 2;
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(291, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(150, 30);
            this.btnExcel.TabIndex = 1;
            this.btnExcel.Text = "Import From &Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // logList
            // 
            this.logList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logList.FormattingEnabled = true;
            this.logList.Location = new System.Drawing.Point(12, 36);
            this.logList.Name = "logList";
            this.logList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.logList.Size = new System.Drawing.Size(429, 264);
            this.logList.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(12, 303);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 30);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Import from &Oracle";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // ImportToSQLWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 382);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "ImportToSQLWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import To SQL";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ListBox logList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnExcel;
    }
}
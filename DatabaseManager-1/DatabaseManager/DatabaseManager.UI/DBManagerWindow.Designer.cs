namespace DatabaseManager.UI
{
    partial class DBManagerWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBManagerWindow));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gbInsert = new System.Windows.Forms.GroupBox();
            this.comboTable = new System.Windows.Forms.ComboBox();
            this.lblData = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.gbInsert.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(391, 24);
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
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 351);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(391, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "stripMain";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gbInsert);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(391, 327);
            this.pnlMain.TabIndex = 2;
            // 
            // gbInsert
            // 
            this.gbInsert.Controls.Add(this.comboTable);
            this.gbInsert.Controls.Add(this.lblData);
            this.gbInsert.Location = new System.Drawing.Point(15, 14);
            this.gbInsert.Name = "gbInsert";
            this.gbInsert.Size = new System.Drawing.Size(361, 46);
            this.gbInsert.TabIndex = 0;
            this.gbInsert.TabStop = false;
            this.gbInsert.Text = "Insert Data";
            // 
            // comboTable
            // 
            this.comboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTable.FormattingEnabled = true;
            this.comboTable.Location = new System.Drawing.Point(61, 18);
            this.comboTable.Name = "comboTable";
            this.comboTable.Size = new System.Drawing.Size(294, 21);
            this.comboTable.TabIndex = 1;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(6, 21);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(49, 13);
            this.lblData.TabIndex = 0;
            this.lblData.Text = "In Table:";
            // 
            // DBManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 373);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(407, 412);
            this.Name = "DBManagerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Oracle DataBase";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.gbInsert.ResumeLayout(false);
            this.gbInsert.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox gbInsert;
        private System.Windows.Forms.ComboBox comboTable;
        private System.Windows.Forms.Label lblData;
    }
}
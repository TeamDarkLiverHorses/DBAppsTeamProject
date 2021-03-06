﻿namespace DatabaseManager.UI
{
    partial class JSONWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JSONWindow));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.listInfo = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.dateHelper = new System.Windows.Forms.DateTimePicker();
            this.dateSearch = new System.Windows.Forms.DateTimePicker();
            this.comboExportOptions = new System.Windows.Forms.ComboBox();
            this.menu.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menu.Size = new System.Drawing.Size(752, 47);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // mFile
            // 
            this.mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mExit});
            this.mFile.Name = "mFile";
            this.mFile.Size = new System.Drawing.Size(64, 39);
            this.mFile.Text = "File";
            // 
            // mExit
            // 
            this.mExit.Name = "mExit";
            this.mExit.Size = new System.Drawing.Size(128, 40);
            this.mExit.Text = "Exit";
            // 
            // statusMain
            // 
            this.statusMain.Location = new System.Drawing.Point(0, 718);
            this.statusMain.Name = "statusMain";
            this.statusMain.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusMain.Size = new System.Drawing.Size(752, 22);
            this.statusMain.TabIndex = 1;
            this.statusMain.Text = "statusStrip1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnClear);
            this.pnlMain.Controls.Add(this.listInfo);
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 47);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(752, 671);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(24, 571);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(180, 58);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // listInfo
            // 
            this.listInfo.FormattingEnabled = true;
            this.listInfo.ItemHeight = 25;
            this.listInfo.Location = new System.Drawing.Point(24, 202);
            this.listInfo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listInfo.Name = "listInfo";
            this.listInfo.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listInfo.Size = new System.Drawing.Size(704, 354);
            this.listInfo.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.dateHelper);
            this.groupBox1.Controls.Add(this.dateSearch);
            this.groupBox1.Controls.Add(this.comboExportOptions);
            this.groupBox1.Location = new System.Drawing.Point(24, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(708, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 108);
            this.btnExport.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(180, 58);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // dateHelper
            // 
            this.dateHelper.Enabled = false;
            this.dateHelper.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateHelper.Location = new System.Drawing.Point(508, 87);
            this.dateHelper.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dateHelper.Name = "dateHelper";
            this.dateHelper.Size = new System.Drawing.Size(176, 31);
            this.dateHelper.TabIndex = 2;
            // 
            // dateSearch
            // 
            this.dateSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateSearch.Location = new System.Drawing.Point(508, 37);
            this.dateSearch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dateSearch.Name = "dateSearch";
            this.dateSearch.Size = new System.Drawing.Size(176, 31);
            this.dateSearch.TabIndex = 1;
            // 
            // comboExportOptions
            // 
            this.comboExportOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboExportOptions.FormattingEnabled = true;
            this.comboExportOptions.Location = new System.Drawing.Point(12, 37);
            this.comboExportOptions.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboExportOptions.Name = "comboExportOptions";
            this.comboExportOptions.Size = new System.Drawing.Size(480, 33);
            this.comboExportOptions.TabIndex = 0;
            // 
            // JSONWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 740);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "JSONWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSON Export";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboExportOptions;
        private System.Windows.Forms.ListBox listInfo;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dateHelper;
        private System.Windows.Forms.DateTimePicker dateSearch;
        private System.Windows.Forms.Button btnClear;
    }
}
﻿namespace DatabaseManager.UI
{
    partial class MainWindow
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnCreateXmlReports = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnExportOracle = new System.Windows.Forms.Button();
            this.btnOracle = new System.Windows.Forms.Button();
            this.stripMain = new System.Windows.Forms.StatusStrip();
            this.btnJSON = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mFile});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(269, 24);
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
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnJSON);
            this.panelMain.Controls.Add(this.btnCreateXmlReports);
            this.panelMain.Controls.Add(this.btnExportPdf);
            this.panelMain.Controls.Add(this.btnExportOracle);
            this.panelMain.Controls.Add(this.btnOracle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(269, 301);
            this.panelMain.TabIndex = 1;
            // 
            // btnCreateXmlReports
            // 
            this.btnCreateXmlReports.Location = new System.Drawing.Point(4, 141);
            this.btnCreateXmlReports.Name = "btnCreateXmlReports";
            this.btnCreateXmlReports.Size = new System.Drawing.Size(260, 40);
            this.btnCreateXmlReports.TabIndex = 3;
            this.btnCreateXmlReports.Text = "4. I&ncomes and Expenses by Vendor (XML)";
            this.btnCreateXmlReports.UseVisualStyleBackColor = true;
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Location = new System.Drawing.Point(4, 95);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(260, 40);
            this.btnExportPdf.TabIndex = 2;
            this.btnExportPdf.Text = "3. &Create Sales Reports (PDF)";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            // 
            // btnExportOracle
            // 
            this.btnExportOracle.Location = new System.Drawing.Point(4, 49);
            this.btnExportOracle.Name = "btnExportOracle";
            this.btnExportOracle.Size = new System.Drawing.Size(260, 40);
            this.btnExportOracle.TabIndex = 1;
            this.btnExportOracle.Text = "2. &Import Product and Sales Data";
            this.btnExportOracle.UseVisualStyleBackColor = true;
            // 
            // btnOracle
            // 
            this.btnOracle.Location = new System.Drawing.Point(4, 3);
            this.btnOracle.Name = "btnOracle";
            this.btnOracle.Size = new System.Drawing.Size(260, 40);
            this.btnOracle.TabIndex = 0;
            this.btnOracle.Text = "1. &Update Product Data (Oracle DB)";
            this.btnOracle.UseVisualStyleBackColor = true;
            // 
            // stripMain
            // 
            this.stripMain.Location = new System.Drawing.Point(0, 303);
            this.stripMain.Name = "stripMain";
            this.stripMain.Size = new System.Drawing.Size(269, 22);
            this.stripMain.TabIndex = 2;
            this.stripMain.Text = "statusStrip1";
            // 
            // btnJSON
            // 
            this.btnJSON.Location = new System.Drawing.Point(4, 187);
            this.btnJSON.Name = "btnJSON";
            this.btnJSON.Size = new System.Drawing.Size(260, 40);
            this.btnJSON.TabIndex = 4;
            this.btnJSON.Text = "5. &JSON Reports";
            this.btnJSON.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 325);
            this.Controls.Add(this.stripMain);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menu;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mExit;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.StatusStrip stripMain;
        private System.Windows.Forms.Button btnOracle;
        private System.Windows.Forms.Button btnExportOracle;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnCreateXmlReports;
        private System.Windows.Forms.Button btnJSON;
    }
}


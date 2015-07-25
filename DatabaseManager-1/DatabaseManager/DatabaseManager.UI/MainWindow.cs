﻿namespace DatabaseManager.UI
{
    using System;
    using System.Windows.Forms;

    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            this.mExit.Click += (s, e) => this.Close();
            this.btnOracle.Click += StartOracleForm;
            this.btnExportOracle.Click += ExportOracleDB;
            this.btnExportPdf.Click += ExportToPdf;
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            using (var frmOracle = new DBManagerWindow())
            {
                frmOracle.ShowDialog(this);
            }
        }

        public void ExportOracleDB(object sender, EventArgs e)
        {
            using (var importWindow = new ImportToSQLWindow())
            {
                importWindow.ShowDialog(this);
            }
        }

        public void ExportToPdf(object sender, EventArgs e)
        {
            using (var exportToPdfWindow = new ExportToPdfWindow())
            {
                exportToPdfWindow.ShowDialog(this);
            }
        }
    }
}

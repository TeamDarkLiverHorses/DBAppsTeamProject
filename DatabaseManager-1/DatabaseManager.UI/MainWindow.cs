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
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            DBManagerWindow frmOracle = new DBManagerWindow();

            using (frmOracle)
            {
                frmOracle.ShowDialog(this);
            }
        }

        public void ExportOracleDB(object sender, EventArgs e)
        {
            ExportOracleWindow exportWindow = new ExportOracleWindow();

            using (exportWindow)
            {
                exportWindow.ShowDialog(this);
            }
        }
    }
}

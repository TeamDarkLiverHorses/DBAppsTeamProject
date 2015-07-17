namespace DatabaseManager.UI
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
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            DBManagerWindow frmOracle = new DBManagerWindow();

            using (frmOracle)
            {
                frmOracle.ShowDialog(this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseApplicationsCourse
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();

            LoadEvents();

        }

        public void LoadEvents()
        {
            this.mExit.Click += new EventHandler(ExitForm);

            this.btnOracle.Click += new EventHandler(StartOracleForm);
        }

        public void StartOracleForm(object sender, EventArgs e)
        {
            frmOracleDataBase frmOracle = new frmOracleDataBase();

            using (frmOracle)
            {
                frmOracle.ShowDialog(this);
            }
        }

        public void ExitForm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

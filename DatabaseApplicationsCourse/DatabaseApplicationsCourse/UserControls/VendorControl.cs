using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseApplicationsCourse.UserControls
{
    public partial class VendorControl : UserControl
    {
        public VendorControl()
        {
            InitializeComponent();
        }

        internal void SetEventHandler(EventHandler clickHandler)
        {
            this.btnUpdate.Click += clickHandler;
        }

        internal void RemoveEventHandler(EventHandler clickHandler)
        {
            this.btnUpdate.Click -= clickHandler;
        }

        internal string Vendor
        {
            get { return this.txtNewVendor.Text; }
        }
    }
}

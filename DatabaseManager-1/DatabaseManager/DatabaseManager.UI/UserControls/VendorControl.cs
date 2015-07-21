namespace DatabaseManager.UI.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DatabaseManager.UI.UserControls
{
    public partial class MeasureControl : UserControl
    {
        public MeasureControl()
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

        internal string Measure
        {
            get { return this.txtMeasure.Text; }
        }
    }
}

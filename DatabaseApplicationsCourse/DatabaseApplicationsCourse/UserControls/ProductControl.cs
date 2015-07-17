using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseApplicationsCourse.Models;

namespace DatabaseApplicationsCourse.UserControls
{
    public partial class ProductControl : UserControl
    {
        public ProductControl()
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

        internal string Product
        {
            get { return this.txtProduct.Text; }
        }

        internal string Price
        {
            get { return this.txtPrice.Text; }
        }

        internal string Vendor
        {
            get 
            {
                if (this.comboVendor.DataSource != null && this.comboVendor.SelectedValue.ToString() != null)
                {
                    return this.comboVendor.SelectedValue.ToString(); 
                }
                else
                {
                    throw new ArgumentNullException("There are no vendors.");
                }
            }
        }

        internal string Measure
        {
            get 
            {
                if (this.comboMeasure.DataSource != null && this.comboMeasure.SelectedValue.ToString() != string.Empty)
                {
                    return this.comboMeasure.SelectedValue.ToString();  
                }
                else
                {
                    throw new ArgumentNullException("There are no measures.");
                }   
            }
        }

        internal string Category
        {
            get
            {
                if (this.comboCategory.DataSource != null && this.comboCategory.SelectedValue.ToString() != string.Empty)
                {
                    return this.comboCategory.SelectedValue.ToString();
                }
                else
                {
                    throw new ArgumentNullException("There are no categories.");
                }
            }
        }

        internal void SetVendorsSource(List<Vendor> vendors)
        {
            this.comboVendor.DataSource = vendors;
            this.comboVendor.DisplayMember = "Name";
            this.comboVendor.ValueMember = "Id";
        }

        internal void SetMeasuresSource(List<Measure> measures)
        {
            this.comboMeasure.DataSource = measures;
            this.comboMeasure.DisplayMember = "Name";
            this.comboMeasure.ValueMember = "Id";
        }

        internal void SetCategoriesSource(List<Category> categories)
        {
            this.comboCategory.DataSource = categories;
            this.comboCategory.DisplayMember = "Name";
            this.comboCategory.ValueMember = "Id";
        }
    }
}

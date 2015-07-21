using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DatabaseManager.Models;

namespace DatabaseManager.UI.UserControls
{

    public partial class CategoryControl : UserControl
    {
        public CategoryControl()
        {
            InitializeComponent();

            this.checkNoParent.CheckedChanged += SetParent;
        }

        private void SetParent(object sender, EventArgs e)
        {
            if (this.checkNoParent.Checked)
            {
                this.comboParent.Enabled = false;
            }
            else
            {
                this.comboParent.Enabled = true;
            }

        }

        internal void SetEventHandler(EventHandler clickHandler)
        {
            this.btnUpdate.Click += clickHandler;
        }

        internal void RemoveEventHandler(EventHandler clickHandler)
        {
            this.btnUpdate.Click -= clickHandler;
        }

        internal string Category
        {
            get { return this.txtCategory.Text; }
        }

        internal string ParentCategory
        {
            get
            {
                if (this.comboParent.DataSource != null && this.comboParent.SelectedValue != null)
                {
                    if (checkNoParent.Checked)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return this.comboParent.SelectedValue.ToString();
                    }    
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal void SetParentCategorySource(List<Category> categories)
        {
            this.comboParent.DataSource = null;
            
            this.comboParent.DataSource = categories;
            this.comboParent.DisplayMember = "Name";
            this.comboParent.ValueMember = "Id";
        }
    }
}

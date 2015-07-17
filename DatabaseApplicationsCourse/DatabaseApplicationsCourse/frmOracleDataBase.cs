using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseApplicationsCourse.UserControls;
using Oracle.ManagedDataAccess.Client;
using DatabaseApplicationsCourse.OracleConnectionDB.OracleNotSelectConnection;
using DatabaseApplicationsCourse.OracleConnectionDB.OracleSelectConnection;
using DatabaseApplicationsCourse.Models;

namespace DatabaseApplicationsCourse
{
    public partial class frmOracleDataBase : Form
    {
        private const string PRODUCTS = "Products";
        private const string VENDORS = "Vendors";
        private const string MEASURES = "Measures";
        private const string CATEGORIES = "Categories";

        private const string ATTENTION = "Attention";

        private ProductControl pc;
        private MeasureControl mc;
        private VendorControl vc;
        private CategoryControl cc;

        private string DecimalSeparator
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; }
        }

        List<Vendor> vendors = new List<Vendor>();
        List<Measure> measures = new List<Measure>();
        List<Category> categories = new List<Category>();

        public frmOracleDataBase()
        {
            InitializeComponent();

            loadEvents();

            SelectData();
            populateOracleOptions();
        }

        private void loadEvents()
        {
            this.comboTable.SelectedIndexChanged += new EventHandler(DataBaseOption);
            this.mExit.Click += new EventHandler(ExitForm);
        }

        private void InsertProduct(object sender, EventArgs e)
        {

            string commandString = "INSERT INTO PRODUCTS (NAME, PRICE, VENDOR_ID, MEASURE_ID, CATEGORY_ID)" +
               "VALUES (:name, :price, :vendor, :measure, :category)";

            try
            {
                string name = pc.Product;
                string priceAsString = pc.Price;
                string vendorAsString = pc.Vendor;
                string measureAsString = pc.Measure;
                string categoryAsString = pc.Category;

                decimal price;
                int vendor;
                int measure;
                int category;

                if (name == string.Empty)
                {
                    throw new ArgumentNullException("Product name is empty.");
                }

                if (!decimal.TryParse(priceAsString, out price))
                {
                    throw new FormatException("Product price is not valid." + Environment.NewLine + "Decimal separator is '" + DecimalSeparator + "'.");
                }

                if (!int.TryParse(vendorAsString, out vendor))
                {
                    throw new FormatException("Product vendor is not valid");
                }

                if (!int.TryParse(measureAsString, out measure))
                {
                    throw new FormatException("Product measure is not valid");
                }

                if (!int.TryParse(categoryAsString, out category))
                {
                    throw new FormatException("Product category is not valid");
                }

                int result = -1;

                OracleParameter parameterName = new OracleParameter(":name", name);
                OracleParameter parameterPrice = new OracleParameter(":price", price);
                OracleParameter parameterVendor = new OracleParameter(":vendor", vendor);
                OracleParameter parameterMeasure = new OracleParameter(":measure", measure);
                OracleParameter parameterCategory = new OracleParameter(":category", category);

                result = UpdateDataBase(commandString, new OracleParameter[] { parameterName, parameterPrice, parameterVendor, parameterMeasure, parameterCategory });

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Product {0} was added to Products.", name), ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Product {0} was not added to Products. Check if this measure is already added.", name),
                        ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (ArgumentNullException nullEx)
            {
                MessageBox.Show(nullEx.Message);
            }
            catch (FormatException fEx)
            {
                MessageBox.Show(fEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }

        private void InsertMeasure(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO MEASURES (NAME) VALUES (:name)";

            int result = -1;

            string newMeasure = mc.Measure;

            if (newMeasure != string.Empty)
            {
                OracleParameter parameter = new OracleParameter(":name", newMeasure);

                try
                {
                    result = UpdateDataBase(commandString, new OracleParameter[] { parameter });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Measure {0} was added to Measures.", newMeasure), ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Measure {0} was not added to Measures. Check if this measure is already added.", newMeasure),
                        ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SelectData();
            }
            else
            {
                MessageBox.Show("Enter a measure.");
            }
        }

        private void InsertVendor(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO VENDORS (NAME) VALUES (:name)";

            string newVendor = vc.Vendor;

            if (newVendor != string.Empty)
            {
                int result = -1;

                OracleParameter parameter = new OracleParameter(":name", newVendor);

                try
                {
                    result = UpdateDataBase(commandString, new OracleParameter[] { parameter });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Vendor {0} was added to Vendors.", newVendor), ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Vendor {0} was not added to Vendors. Check if this measure is already added.", newVendor),
                        ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SelectData();
            }
            else
            {
                MessageBox.Show("Enter a vendor.");
            }
        }

        private void InsertCategory(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO CATEGORIES (NAME, PARENT_CATEGORY_ID) VALUES (:name, :category)";

            string categoryName = cc.Category;
            string categoryParent = cc.ParentCategory;

            if (categoryName != string.Empty)
            {
                int result = -1;

                OracleParameter parameterName = new OracleParameter(":name", categoryName);

                OracleParameter parameterParent = new OracleParameter(":category", categoryParent);

                if (categoryParent == string.Empty)
                {
                    parameterParent.Value = DBNull.Value;
                }

                try
                {
                    result = UpdateDataBase(commandString, new OracleParameter[] { parameterName, parameterParent });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Category {0} was added to Categories.", categoryName), ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Category {0} was not added to Categories. Check if this measure is already added.", categoryName),
                        ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                SelectData();

                cc.SetParentCategorySource(categories);
            }
            else
            {
                MessageBox.Show("Enter a category.");
            }
        }

        private int UpdateDataBase(string commandString, OracleParameter[] parameters)
        {
            OracleNotSelectConnection notSelectConnection = null;
            
            int result = -1;

            try
            {
                using (notSelectConnection = new OracleNotSelectConnection())
                {
                    result = notSelectConnection.UpdateDataBase(commandString, parameters);
                }
            }
            catch (Exception ex)
            {
               if (notSelectConnection != null)
               {
                   notSelectConnection.Dispose();
                   notSelectConnection = null;
               }
                
               MessageBox.Show(ex.Message, ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void SelectData()
        {
            string commandVendors = "SELECT * FROM VENDORS";
            string commandMeasures = "SELECT * FROM MEASURES";
            string commandCategories = "SELECT * FROM Categories";

            DataTable dtV = new DataTable();
            DataTable dtM = new DataTable();
            DataTable dtC = new DataTable();

            try
            {
                using (OracleSelectConnection oracleSelect = new OracleSelectConnection())
                {
                    dtV = oracleSelect.SelectData(commandVendors, null);
                    dtM = oracleSelect.SelectData(commandMeasures, null);
                    dtC = oracleSelect.SelectData(commandCategories, null);
                }

                if (dtV.Rows.Count > 0 && dtM.Rows.Count > 0)
                {
                    Vendor[] tempVendors = new Vendor[dtV.Rows.Count];
                    Measure[] tempMeasures = new Measure[dtM.Rows.Count];
                    Category[] tempCategories = new Category[dtC.Rows.Count];

                    int counter = 0;

                    foreach (DataRow currentRow in dtV.Rows)
                    {
                        int id;
                        string vendorName;

                        if (!int.TryParse(currentRow[0].ToString(), out id))
                        {
                            throw new ArgumentException("Id for a vendor is not integer.");
                        }

                        vendorName = currentRow[1].ToString();

                        if (vendorName == string.Empty)
                        {
                            throw new ArgumentNullException("Vendor name is empty.");
                        }

                        tempVendors[counter] = new Vendor(id, vendorName);

                        counter += 1;
                    }

                    vendors.Clear();
                    vendors.AddRange(tempVendors);

                    counter = 0;

                    foreach (DataRow currentRow in dtM.Rows)
                    {
                        int id;
                        string measureName;

                        if (!int.TryParse(currentRow[0].ToString(), out id))
                        {
                            throw new ArgumentNullException("Id for a measure is not integer.");
                        }

                        measureName = currentRow[1].ToString();

                        if (measureName == string.Empty)
                        {
                            throw new ArgumentNullException("Measure name is empty.");
                        }

                        tempMeasures[counter] = new Measure(id, measureName);

                        counter += 1;
                    }

                    measures.Clear();
                    measures.AddRange(tempMeasures);

                    counter = 0;

                    foreach (DataRow currentRow in dtC.Rows)
                    {
                        int id;
                        string categoryName;

                        if (!int.TryParse(currentRow[0].ToString(), out id))
                        {
                            throw new ArgumentNullException("Id for a category is not integer.");
                        }

                        categoryName = currentRow[1].ToString();

                        if (categoryName == string.Empty)
                        {
                            throw new ArgumentNullException("Category name is empty.");
                        }

                        tempCategories[counter] = new Category(id, categoryName);

                        counter += 1;
                    }

                    categories.Clear();
                    categories.AddRange(tempCategories);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void populateOracleOptions()
        {

            string[] databaseOptions = { PRODUCTS, MEASURES, VENDORS, CATEGORIES };

            comboTable.DataSource = databaseOptions;

        }

        private void DataBaseOption(object sender, EventArgs e)
        {
            if (this.comboTable.DataSource == null || this.comboTable.Items.Count != 0)
            {
                string currentOption = comboTable.SelectedItem.ToString();

                switch (currentOption)
                {
                    case PRODUCTS:
                        DeleteMeasureControl();
                        DeleteVendorControl();
                        DeleteCategoryControl();
                        AddProductControl();
                        break;
                    case VENDORS:
                        DeleteProductControl();
                        DeleteMeasureControl();
                        DeleteCategoryControl();
                        AddVendorControl();
                        break;
                    case MEASURES:
                        DeleteProductControl();
                        DeleteVendorControl();
                        DeleteCategoryControl();
                        AddMeasureControl();
                        break;
                    case CATEGORIES:
                        DeleteProductControl();
                        DeleteVendorControl();
                        DeleteMeasureControl();
                        AddCategoryControl();
                        break;
                    default:
                        MessageBox.Show("The are no tables.", ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("The are no tables.", ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddProductControl()
        {
            pc = new ProductControl();
            pc.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(pc);
            pc.SetEventHandler(new EventHandler(InsertProduct));
            pc.SetVendorsSource(vendors);
            pc.SetMeasuresSource(measures);
            pc.SetCategoriesSource(categories);
        }

        private void DeleteProductControl()
        {
            if (this.pnlMain.Controls.IndexOf(pc) > 0)
            {
                if (pc != null)
                {
                    this.pnlMain.Controls.Remove(pc);
                    pc.Dispose();
                    pc = null;
                }
            }
        }

        private void AddMeasureControl()
        {
            mc = new MeasureControl();
            mc.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(mc);
            mc.SetEventHandler(new EventHandler(InsertMeasure));
        }

        private void DeleteMeasureControl()
        {
            if (this.pnlMain.Controls.IndexOf(mc) > 0)
            {
                if (mc != null)
                {
                    this.pnlMain.Controls.Remove(mc);
                    mc.Dispose();
                    mc = null;
                }
            }
        }

        private void AddVendorControl()
        {
            vc = new VendorControl();
            vc.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(vc);
            vc.SetEventHandler(new EventHandler(InsertVendor));
        }

        private void DeleteVendorControl()
        {
            if (this.pnlMain.Controls.IndexOf(vc) > 0)
            {
                if (vc != null)
                {
                    this.pnlMain.Controls.Remove(vc);
                    vc.Dispose();
                    vc = null;
                }
            }
        }

        private void AddCategoryControl()
        {
            cc = new CategoryControl();
            cc.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(cc);
            cc.SetEventHandler(new EventHandler(InsertCategory));
            cc.SetParentCategorySource(categories);
        }

        private void DeleteCategoryControl()
        {
            if (this.pnlMain.Controls.IndexOf(cc) > 0)
            {
                if (cc != null)
                {
                    this.pnlMain.Controls.Remove(cc);
                    cc.Dispose();
                    cc = null;
                }
            }
        }

        private void ExitForm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

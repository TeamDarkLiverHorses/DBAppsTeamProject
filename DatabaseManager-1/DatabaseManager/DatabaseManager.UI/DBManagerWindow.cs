/* Single class must not be used for:
 * 1. Draw stuff on window
 * 2. Do database insertions/deletions/updates
 * 
 * Two different logical actions? -> two different classes!
 * 
 * What if i want to connect to MSSQL or MongoDB? Rewrite all code? Write the same code again?
 */

namespace DatabaseManager.UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DatabaseManager.Models;
    using UserControls;
    using DatabaseManager.ImportSalesData.OracleConnectionsDB;

    public partial class DBManagerWindow : Form
    {
        private const string Products = "Products";
        private const string Vendors = "Vendors";
        private const string Measures = "Measures";
        private const string Categories = "Categories";
        private const string Attention = "Attention";

        private ProductControl productControl = null;
        private MeasureControl measureControl = null;
        private VendorControl vendorControl = null;
        private CategoryControl categoryControl = null;
        private List<Vendor> vendors = new List<Vendor>();
        private List<Measure> measures = new List<Measure>();
        private List<Category> categories = new List<Category>();

        public DBManagerWindow()
        {
            InitializeComponent();
            this.comboTable.SelectedIndexChanged += DataBaseOption;
            this.mExit.Click += (s, e) => this.Close();
            SelectData();
            populateOracleOptions();
        }

        private string DecimalSeparator
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        private void InsertProduct(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO PRODUCTS (NAME, PRICE, VENDOR_ID, MEASURE_ID, CATEGORY_ID)" +
               "VALUES (:name, :price, :vendor, :measure, :category)";

            try
            {
                string name = productControl.Product;
                string priceAsString = productControl.Price;
                string vendorAsString = productControl.Vendor;
                string measureAsString = productControl.Measure;
                string categoryAsString = productControl.Category;

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

                OracleDBAction oracleAction = new OracleDBAction();

                result = oracleAction.InsertNewProduct(commandString, name, price, vendor, measure, category);

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Product {0} was added to Products.", name), Attention, 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Product {0} was not added to Products. Check if this measure is already added.", name),
                        Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            string newMeasure = measureControl.Measure;

            if (newMeasure != string.Empty)
            {
                try
                {
                    OracleDBAction oracleAction = new OracleDBAction();

                    result = oracleAction.InsertProductParameter(commandString, ":name", newMeasure);

                    SelectData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Attention, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Measure {0} was added to Measures.", newMeasure), Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Measure {0} was not added to Measures. Check if this measure is already added.", newMeasure),
                        Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter a measure.");
            }
        }

        private void InsertVendor(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO VENDORS (NAME) VALUES (:name)";

            string newVendor = vendorControl.Vendor;

            if (newVendor != string.Empty)
            {
                int result = -1;

                try
                {
                    OracleDBAction oracleAction = new OracleDBAction();

                    result = oracleAction.InsertProductParameter(commandString, ":name", newVendor);

                    SelectData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Attention, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    SelectData();
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Vendor {0} was added to Vendors.", newVendor), Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Vendor {0} was not added to Vendors. Check if this measure is already added.", newVendor),
                        Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter a vendor.");
            }
        }

        private void InsertCategory(object sender, EventArgs e)
        {
            string commandString = "INSERT INTO CATEGORIES (NAME, PARENT_CATEGORY_ID) VALUES (:name, :category)";

            string categoryName = categoryControl.Category;
            string categoryParent = categoryControl.ParentCategory;

            if (categoryName != string.Empty)
            {
                int result = -1;

                try
                {
                    OracleDBAction oracleAction = new OracleDBAction();

                    result = oracleAction.InsertProductCategory(commandString, ":name", categoryName, ":category", categoryParent);

                    SelectData();

                    categoryControl.SetParentCategorySource(categories);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Attention, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (result == 1)
                {
                    MessageBox.Show(string.Format("Category {0} was added to Categories.", categoryName), Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (result == 0)
                {
                    MessageBox.Show(string.Format("Category {0} was not added to Categories. Check if this measure is already added.", categoryName),
                        Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Enter a category.");
            }
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
                OracleDBAction oracleAction = new OracleDBAction();

                dtV = oracleAction.SelectProduct(commandVendors);
                dtM = oracleAction.SelectProduct(commandMeasures);
                dtC = oracleAction.SelectProduct(commandCategories);

                if (dtV.Rows.Count > 0 && dtM.Rows.Count > 0)
                {
                    Vendor[] tempVendors = new Vendor[dtV.Rows.Count];
                    Measure[] tempMeasures = new Measure[dtM.Rows.Count];
                    Category[] tempCategories = new Category[dtC.Rows.Count];

                    int counter = 0;

                    foreach (DataRow currentRow in dtV.Rows)
                    {
                        int vendorId;
                        string vendorName;

                        if (!int.TryParse(currentRow[0].ToString(), out vendorId))
                        {
                            throw new ArgumentException("Id for a vendor is not integer.");
                        }

                        vendorName = currentRow[1].ToString();

                        if (vendorName == string.Empty)
                        {
                            throw new ArgumentNullException("Vendor name is empty.");
                        }

                        tempVendors[counter] = new Vendor { Id= vendorId, Name = vendorName};

                        counter += 1;
                    }

                    vendors.Clear();
                    vendors.AddRange(tempVendors);

                    counter = 0;

                    foreach (DataRow currentRow in dtM.Rows)
                    {
                        int measureId;
                        string measureName;

                        if (!int.TryParse(currentRow[0].ToString(), out measureId))
                        {
                            throw new ArgumentNullException("Id for a measure is not integer.");
                        }

                        measureName = currentRow[1].ToString();

                        if (measureName == string.Empty)
                        {
                            throw new ArgumentNullException("Measure name is empty.");
                        }

                        tempMeasures[counter] = new Measure {Id = measureId,  Name = measureName};

                        counter += 1;
                    }

                    measures.Clear();
                    measures.AddRange(tempMeasures);

                    counter = 0;

                    foreach (DataRow currentRow in dtC.Rows)
                    {
                        int categoryId;
                        string categoryName;

                        if (!int.TryParse(currentRow[0].ToString(), out categoryId))
                        {
                            throw new ArgumentNullException("Id for a category is not integer.");
                        }

                        categoryName = currentRow[1].ToString();

                        if (categoryName == string.Empty)
                        {
                            throw new ArgumentNullException("Category name is empty.");
                        }

                        tempCategories[counter] = new Category() { Id = categoryId, Name = categoryName };

                        counter += 1;
                    }

                    categories.Clear();
                    categories.AddRange(tempCategories);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Attention, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void populateOracleOptions()
        {
            string[] databaseOptions = { Products, Measures, Vendors, Categories };

            comboTable.DataSource = databaseOptions;
        }

        private void DataBaseOption(object sender, EventArgs e)
        {
            if (this.comboTable.DataSource == null || this.comboTable.Items.Count != 0)
            {
                string currentOption = comboTable.SelectedItem.ToString();

                switch (currentOption)
                {
                    case Products:
                        DeleteMeasureControl();
                        DeleteVendorControl();
                        DeleteCategoryControl();
                        AddProductControl();
                        break;
                    case Vendors:
                        DeleteProductControl();
                        DeleteMeasureControl();
                        DeleteCategoryControl();
                        AddVendorControl();
                        break;
                    case Measures:
                        DeleteProductControl();
                        DeleteVendorControl();
                        DeleteCategoryControl();
                        AddMeasureControl();
                        break;
                    case Categories:
                        DeleteProductControl();
                        DeleteVendorControl();
                        DeleteMeasureControl();
                        AddCategoryControl();
                        break;
                    default:
                        MessageBox.Show("The are no tables.", Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            else
            {
                MessageBox.Show("The are no tables.", Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddProductControl()
        {
            productControl = new ProductControl();
            productControl.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(productControl);
            productControl.SetEventHandler(new EventHandler(InsertProduct));
            productControl.SetVendorsSource(vendors);
            productControl.SetMeasuresSource(measures);
            productControl.SetCategoriesSource(categories);
        }

        private void DeleteProductControl()
        {
            if (this.pnlMain.Controls.IndexOf(productControl) > 0)
            {
                if (productControl != null)
                {
                    this.pnlMain.Controls.Remove(productControl);
                    productControl.Dispose();
                    productControl = null;
                }
            }
        }

        private void AddMeasureControl()
        {
            measureControl = new MeasureControl();
            measureControl.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(measureControl);
            measureControl.SetEventHandler(new EventHandler(InsertMeasure));
        }

        private void DeleteMeasureControl()
        {
            if (this.pnlMain.Controls.IndexOf(measureControl) > 0)
            {
                if (measureControl != null)
                {
                    this.pnlMain.Controls.Remove(measureControl);
                    measureControl.Dispose();
                    measureControl = null;
                }
            }
        }

        private void AddVendorControl()
        {
            vendorControl = new VendorControl();
            vendorControl.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(vendorControl);
            vendorControl.SetEventHandler(new EventHandler(InsertVendor));
        }

        private void DeleteVendorControl()
        {
            if (this.pnlMain.Controls.IndexOf(vendorControl) > 0)
            {
                if (vendorControl != null)
                {
                    this.pnlMain.Controls.Remove(vendorControl);
                    vendorControl.Dispose();
                    vendorControl = null;
                }
            }
        }

        private void AddCategoryControl()
        {
            categoryControl = new CategoryControl();
            categoryControl.Location = new Point(15, 66);
            this.pnlMain.Controls.Add(categoryControl);
            categoryControl.SetEventHandler(new EventHandler(InsertCategory));
            categoryControl.SetParentCategorySource(categories);
        }

        private void DeleteCategoryControl()
        {
            if (this.pnlMain.Controls.IndexOf(categoryControl) > 0)
            {
                if (categoryControl != null)
                {
                    this.pnlMain.Controls.Remove(categoryControl);
                    categoryControl.Dispose();
                    categoryControl = null;
                }
            }
        }
    }
}

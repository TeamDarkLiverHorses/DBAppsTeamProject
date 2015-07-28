namespace DatabaseManager.MySqlImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using System.Data.Entity;
    using DatabaseManager.MySqlModels;

    public class ExpenseIncomeExporter
    {
        public string ImportDataToMySql()
        {
            try
            {
                List<MySqlVendor> vendorsToImport = new List<MySqlVendor>();
                List<MySqlProduct> productsToImport = new List<MySqlProduct>();

                using (SupermarketsContext context = new SupermarketsContext())
                {
                    vendorsToImport = SelectVendors(context);

                    productsToImport = SelectProducts(context);
                }

                // this checks for duplicate vendors
                // if there is duplicate it removes it from vendors and add it as vendor and adds it to all products with the same value
                for (int a = vendorsToImport.Count - 1; a >= 0; a--)
                {
                    bool isAdded = false;

                    for (int b = 0; b < productsToImport.Count; b++)
                    {
                        if (vendorsToImport[a].Name == productsToImport[b].Vendor.Name)
                        {
                            productsToImport[b].Vendor = vendorsToImport[a];

                            isAdded = true;
                        } 
                    }

                    if (isAdded)
                    {
                        vendorsToImport.RemoveAt(a);
                    }
                }

                string result = MySqlImporter.ImportData(vendorsToImport, productsToImport);

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // selects all vendors with Expenses
        // Vendors could or not have products
        private List<MySqlVendor> SelectVendors(SupermarketsContext context)
        {
            List<MySqlVendor> vendorsToImport = new List<MySqlVendor>();

            var vendors = from v in context.Vendors
                          where v.Expenses.Count > 0
                          select new { Name = v.Name, Expenses = v.Expenses.Sum(e => e.Ammount) };

            foreach (var currentVendor in vendors)
            {
                vendorsToImport.Add(new MySqlVendor() { Name = currentVendor.Name, Expenses = currentVendor.Expenses });
            }

            return vendorsToImport;
        }

        // select all products with Incomes
        // products could or not have vendors
        private List<MySqlProduct> SelectProducts(SupermarketsContext context)
        {
            List<MySqlProduct> productsToImport = new List<MySqlProduct>();

            var products = from p in context.Products
                           where p.Sales.Count > 0
                           select new { Name = p.Name, VendorName = p.Vendor.Name, Amount = p.Sales.Sum(s => s.UnitPrice * s.Quantity) };

            foreach (var currentProduct in products)
            {
                MySqlProduct newProductToImport = new MySqlProduct() { Name = currentProduct.Name, Incomes = currentProduct.Amount };
                newProductToImport.Vendor = new MySqlVendor() { Name = currentProduct.VendorName };

                productsToImport.Add(newProductToImport);
            }

            return productsToImport;
        }
    }
}

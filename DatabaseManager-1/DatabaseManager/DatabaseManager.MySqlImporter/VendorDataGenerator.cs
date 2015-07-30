namespace DatabaseManager.MySqlImporter
{
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.MySqlModels;

    public class VendorDataGenerator
    {
        public static IEnumerable<MySqlVendor> GetVendorExpenses()
        {
            var supermarketsContext = new SupermarketsContext();

            var vendorExpenses = supermarketsContext.Vendors
                .Select(v => new MySqlVendor()
                {
                    Name = v.Name,
                    Expenses = v.Expenses.Sum(e => e.Ammount) == null ? 0m : v.Expenses.Sum(e => e.Ammount)
                });

            return vendorExpenses;
        }

        public static IEnumerable<MySqlProduct> GetProductIncomes()
        {
            var supermarketsContext = new SupermarketsContext();

            var productIncomes = supermarketsContext.Products
                .Where(p => p.Sales.Count > 0)
                .Select(p => new MySqlProduct()
                {
                    Name = p.Name,
                    Incomes = p.Sales.Sum(s => s.Quantity * s.UnitPrice),
                    Vendor = new MySqlVendor() { Name = p.Vendor.Name}
                });

            return productIncomes;
        }
    }
}

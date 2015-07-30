namespace DatabaseManager.MySql.Console
{
    using System;
    using DatabaseManager.MySqlImporter;

    public class MySqlTests
    {
        public static void Main()
        {
            var vendorData = VendorDataGenerator.GetVendorExpenses();
            foreach (var vendor in vendorData)
            {
                Console.WriteLine("{0}: {1}", vendor.Name, vendor.Expenses);
            }

            var productIncomes = VendorDataGenerator.GetProductIncomes();
            foreach (var product in productIncomes)
            {
                Console.WriteLine("{0}, by {1}, sales: {2}", product.Name, product.Vendor.Name, product.Incomes);
            }
        }
    }
}

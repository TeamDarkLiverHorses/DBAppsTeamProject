namespace DatabaseManager.MySqlImporter
{
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.MySqlData;
    using DatabaseManager.MySqlModels;

    public class VendorDataImporter
    {
        public static ImportResult ImportVendors(IEnumerable<MySqlVendor> vendorsData) 
        {
            var context = new MySqlSupermarketContext();
            int inserted = 0;
            int updated = 0;

            foreach (var vendor in vendorsData)
            {
                var existingVendor = context.vendors.Where(v => v.Name == vendor.Name).FirstOrDefault();
                if (existingVendor == null)
                {
                    var newVendor = new MySqlVendor() { Name = vendor.Name, Expenses = vendor.Expenses };
                    context.vendors.Add(newVendor);
                    inserted++;
                }
                else
                {
                    existingVendor.Expenses = vendor.Expenses;
                    updated++;
                }
            }

            context.SaveChanges();

            return new ImportResult() { Inserted = inserted, Updated = updated};
        }

        public static ImportResult ImportProducts(IEnumerable<MySqlProduct> productsData)
        {
            var context = new MySqlSupermarketContext();
            int inserted = 0;
            int updated = 0;

            foreach (var product in productsData)
            {
                var existingProduct = context.products.Where(p => p.Name == product.Name).FirstOrDefault();
                if (existingProduct == null)
                {
                    var newProduct = new MySqlProduct() 
                    { 
                        Name = product.Name,
                        Incomes = product.Incomes,
                        Vendor = context.vendors.Where(v => v.Name == product.Vendor.Name).FirstOrDefault()
                    };
                    context.products.Add(newProduct);
                    inserted++;
                }
                else
                {
                    existingProduct.Incomes = product.Incomes;
                    updated++;
                }
            }

            context.SaveChanges();

            return new ImportResult() { Inserted = inserted, Updated = updated };
        }
    }
}

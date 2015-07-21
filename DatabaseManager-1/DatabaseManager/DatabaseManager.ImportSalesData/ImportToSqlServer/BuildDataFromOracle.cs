namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DatabaseManager.Models;

    public static class BuildDataFromOracle
    {
        public static ImportFromOracleDataHolder BuildProducts(DataTable tableProducts, string productNameColumn, string productPriceColumn,
            string categoryNameColumn, string measureNameColumn, string vendorNameColumn)
        {
            try
            {
                var categories = new HashSet<string>();
                var measures = new HashSet<string>();
                var vendors = new HashSet<string>();
                int productsCount = tableProducts.Rows.Count;
                var products = new Product[productsCount];

                for (int i = 0; i < productsCount; i++)
                {
                    string productName = tableProducts.Rows[i][productNameColumn].ToString();
                    string productPrice = tableProducts.Rows[i][productPriceColumn].ToString();
                    string productCategory = tableProducts.Rows[i][categoryNameColumn].ToString();
                    string productMeasure = tableProducts.Rows[i][measureNameColumn].ToString();
                    string productVednor = tableProducts.Rows[i][vendorNameColumn].ToString();

                    decimal price;
                    if (!decimal.TryParse(productPrice, out price))
                    {
                        throw new FormatException(string.Format("Product {0} has invalid price", productName));
                    }

                    categories.Add(productCategory);
                    measures.Add(productMeasure);
                    vendors.Add(productVednor);

                    var newProduct = new Product()
                    {
                        Name = productName,
                        Price = price,
                        Category = new Category() { Name = productCategory },
                        Measure = new Measure() { Name = productMeasure },
                        Vendor = new Vendor() { Name = productVednor }
                    };
                    products[i] = newProduct;
                }

                return new ImportFromOracleDataHolder(vendors, measures, categories, products);
            }
            catch (FormatException fEx)
            {
                throw fEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

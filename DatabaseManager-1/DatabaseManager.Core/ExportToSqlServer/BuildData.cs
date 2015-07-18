using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Core.Models;
using System.Data;

namespace DatabaseManager.Core.ExportToSqlServer
{
    public class BuildData
    {
        public ExportDataHolder BuildProducts(DataTable tableProducts, string productNameColumn, string productPriceColumn,
            string categoryNameColumn, string measureNameColumn, string vendorNameColumn)
        {
            try
            {
                Product[] products = new Product[tableProducts.Rows.Count];
                HashSet<string> categories = new HashSet<string>();
                HashSet<string> measures = new HashSet<string>();
                HashSet<string> vendors = new HashSet<string>();

                for (int i = 0; i < tableProducts.Rows.Count; i++)
                {
                    string productName = tableProducts.Rows[i][productNameColumn].ToString();
                    string productPrice = tableProducts.Rows[i][productPriceColumn].ToString();
                    string productCategory = tableProducts.Rows[i][categoryNameColumn].ToString();
                    string productMeasure = tableProducts.Rows[i][measureNameColumn].ToString();
                    string productVednor = tableProducts.Rows[i][vendorNameColumn].ToString();

                    decimal price;

                    if (!decimal.TryParse(productPrice, out price))
                    {
                        throw new FormatException(string.Format("{0} has invalid price", productName));
                    }

                    categories.Add(productCategory);

                    measures.Add(productMeasure);

                    vendors.Add(productVednor);

                    Product newProduct = new Product();
                    newProduct.Name = productName;
                    newProduct.Price = price;
                    newProduct.Category = new Category() { Name = productCategory };
                    newProduct.Measure = new Measure() { Name = productMeasure };
                    newProduct.Vendor = new Vendor() { Name = productVednor };

                    products[i] = newProduct;
                }

                return new ExportDataHolder(vendors, measures, categories, products);
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

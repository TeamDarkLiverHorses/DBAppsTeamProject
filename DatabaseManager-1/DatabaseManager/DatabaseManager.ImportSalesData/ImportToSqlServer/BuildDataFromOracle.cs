﻿namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using DatabaseManager.Models;

    public class BuildDataFromOracle : IDisposable
    {
        private bool isDisposed = false;
        private HashSet<string> categories = new HashSet<string>();
        private HashSet<string> measures = new HashSet<string>();
        private HashSet<string> vendors = new HashSet<string>();
        private Product[] products;
        
        public ImportFromOracleDataHolder BuildProducts(DataTable tableProducts, string productNameColumn, string productPriceColumn,
            string categoryNameColumn, string measureNameColumn, string vendorNameColumn)
        {
            try
            {
                int productsCount = tableProducts.Rows.Count;
                this.products = new Product[productsCount];

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

                    this.categories.Add(productCategory);
                    this.measures.Add(productMeasure);
                    this.vendors.Add(productVednor);

                    var newProduct = new Product()
                    {
                        Name = productName,
                        Price = price,
                        Category = new Category() { Name = productCategory },
                        Measure = new Measure() { Name = productMeasure },
                        Vendor = new Vendor() { Name = productVednor }
                    };
                    this.products[i] = newProduct;
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

        public void Dispose()
        {
            Dispose(isDisposed);
        }

        private void Dispose(bool disposed)
        {
            if (!disposed)
            {
                this.categories.Clear();
                this.categories = null;

                this.measures.Clear();
                this.measures = null;

                this.vendors.Clear();
                this.vendors = null;

                this.products = null;

                isDisposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
}

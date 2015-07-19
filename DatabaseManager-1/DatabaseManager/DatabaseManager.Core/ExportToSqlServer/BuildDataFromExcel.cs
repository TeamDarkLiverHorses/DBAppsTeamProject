using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Core.Models;

namespace DatabaseManager.Core.ExportToSqlServer
{
    class BuildDataFromExcel : IDisposable
    {
        bool isDisposed = false;

        HashSet<Product> products = new HashSet<Product>();
        HashSet<Shop> shops = new HashSet<Shop>();
        Sale[] sales;

        public ExportFromExcelDataHolder BuildSales(Sale[] newSales)
        {
            int salesCounter = newSales.Length;

            this.sales = newSales;

            // this builds the unique products and shop names
            for (int i = 0; i < salesCounter; i++)
            {
                string saleShop = newSales[i].Shop.Name;
                string saleProduct = newSales[i].Product.Name;
                int saleQuantity = newSales[i].Quantity;
                decimal saleUnitPrice = newSales[i].UnitPrice;

                this.products.Add(new Product() { Name = saleProduct, Price = saleUnitPrice });
                this.shops.Add(new Shop() { Name = saleShop });
            }

            return new ExportFromExcelDataHolder(products, shops, newSales);
        }

        public void Dispose()
        {
            Dispose(isDisposed);
        }

        private void Dispose(bool disposed)
        {
            if (!disposed)
            {
                this.products.Clear();
                this.products = null;

                this.shops.Clear();
                this.shops = null;

                this.sales = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}

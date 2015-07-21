namespace DatabaseManager.ImportSalesData.ImportFromExcel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DatabaseManager.Models;

    public class ExcelToSale
    {
        public Sale GetSale(string productName, string shopName, string quantity, string price)
        {
            int productsQuantity;
            decimal productPrice;

            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException("Product name is null.");
            }

            if (string.IsNullOrEmpty(shopName))
            {
                throw new ArgumentNullException("Shop name is null.");
            }

            if (!int.TryParse(quantity, out productsQuantity))
            {
                throw new ArgumentException("Quanity is not a integer.");
            }

            if (!decimal.TryParse(price, out productPrice))
            {
                throw new ArgumentException("Product price is not a decimal.");
            }

            Product newProduct = new Product() { Name = productName };
            Shop newShop = new Shop() { Name = shopName };

            Sale newSale = new Sale()
            {
                Product = newProduct,
                Shop = newShop,
                UnitPrice = productPrice,
                Quantity = productsQuantity,
            };

            return newSale;
        }
    }
}

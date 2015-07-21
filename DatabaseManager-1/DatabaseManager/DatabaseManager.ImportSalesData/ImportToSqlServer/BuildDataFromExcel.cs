namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using DatabaseManager.Models;

    public static class BuildDataFromExcel
    {
        public static ImportFromExcelDataHolder BuildSales(Sale[] newSales)
        {
            var products = new HashSet<string>();
            var shops = new HashSet<string>();

            for (int i = 0; i < newSales.Length; i++)
            {
                string saleShop = newSales[i].Shop.Name;
                string saleProduct = newSales[i].Product.Name;
                int saleQuantity = newSales[i].Quantity;
                decimal saleUnitPrice = newSales[i].UnitPrice;

                products.Add(saleProduct);
                shops.Add(saleShop);
            }

            return new ImportFromExcelDataHolder(products, shops, newSales);
        }
    }
}

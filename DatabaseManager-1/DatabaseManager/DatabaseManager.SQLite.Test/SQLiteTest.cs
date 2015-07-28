namespace DatabaseManager.SQLite.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Data;
    using DatabaseManager.Models;
    using DatabaseManager.SQLite.Data;

    public class SQLiteTest
    {
        private static Random rnd = new Random();
        public static void Main()
        {
            Console.WriteLine("Inserted {0} taxes.", UpdateProductsAndTaxes());

            //Console.WriteLine("Deleted {0} taxes.", DeleteAllTaxes());

            var taxesContext = new TaxesContext();
            var taxes = taxesContext.Taxes;
            foreach (var tax in taxes)
            {
                Console.WriteLine("{0} - {1}", tax.ProductName, tax.TaxSize);
            }
        }

        public static int UpdateProductsAndTaxes()
        {
            var taxesContext = new TaxesContext();
            var supermarketsContext = new SupermarketsContext();

            var allProductNames = supermarketsContext.Products.Select(p => p.Name).ToList();
            var existingProductNames = taxesContext.Taxes.Select(t => t.ProductName).ToList();

            var missingProducts = allProductNames.Except(existingProductNames);

            var productsToInsert = supermarketsContext.Products
                .Where(p => missingProducts.Contains(p.Name))
                .Select(p => new { ProductName = p.Name })
                .ToList();

            foreach (var product in productsToInsert)
            {
                taxesContext.Taxes.Add(new Tax()
                {
                    ProductName = product.ProductName,
                    TaxSize = (float)GetRandomNumber(0d, 0.3d)
                });
            }
            taxesContext.SaveChanges();

            return productsToInsert.Count;
        }

        public static int DeleteAllTaxes()
        {
            int count = 0;
            var context = new TaxesContext();
            var taxes = context.Taxes;
            foreach (var tax in taxes)
            {
                context.Taxes.Remove(tax);
                count++;
            }
            context.SaveChanges();
            return count;
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            double result =  rnd.NextDouble() * (maximum - minimum) + minimum;
            return result;
        }
    }
}

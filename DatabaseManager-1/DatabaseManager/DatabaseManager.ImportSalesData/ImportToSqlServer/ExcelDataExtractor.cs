namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using DatabaseManager.Models;
    using Excel;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;

    public class ExcelDataExtractor
    {
        private string filePath;

        public ExcelDataExtractor(string filePath)
        {
            this.filePath = filePath;
            this.Sales = new List<Sale>();
            this.Products = new HashSet<string>();
            this.Shops = new HashSet<string>();
        }

        public List<Sale> Sales { get; private set; }

        public ISet<string> Products { get; private set; }

        public ISet<string> Shops { get; private set; }

        public void Read()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("There is no file.");
            }

            var zipArchive = ZipFile.OpenRead(filePath);

            foreach (var entry in zipArchive.Entries)
            {
                var fullName = entry.FullName;
                var excelFile = new FileInfo(fullName);
                var extension = excelFile.Extension;
                var fileNames = fullName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (fileNames.Length <= 1)
                {
                    continue;
                }
                
                var dateAsString = fileNames[fileNames.Length - 2];
                DateTime fileDate;

                if (!DateTime.TryParseExact(dateAsString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileDate))
                {
                    continue;
                }

                var fileName = fileNames[fileNames.Length - 1];
                if (fileName.Length <= 4 || extension != ".xls")
                {
                    continue;
                }
                    
                var salesToAdd = this.ReadFile(entry);
                foreach (var sale in salesToAdd)
                {
                    sale.Date = fileDate;
                }

                this.Sales.AddRange(salesToAdd);
            }

            this.ExtractProductsAndShops();
        }

        private List<Sale> ReadFile(ZipArchiveEntry entry)
        {
            var sales = ExtractSales(entry);
            return sales;
        }

        private List<Sale> ExtractSales(ZipArchiveEntry entry)
        {
            using (var memoryStream = new MemoryStream())
            {
                entry.Open().CopyTo(memoryStream);

                var excelReader = ExcelReaderFactory.CreateBinaryReader(memoryStream);
                var sales = new List<Sale>();
                string shopName = string.Empty;
                string productName = string.Empty;
                string quantity = string.Empty;
                string price = string.Empty;
                string sum = string.Empty;
                bool inData = false;

                while (excelReader.Read())
                {
                    string firstColumnValue = excelReader.GetString(1);

                    if (firstColumnValue == null)
                    {
                        continue;
                    }

                    if (firstColumnValue == "Product")
                    {
                        inData = true;
                    }
                    else
                    {
                        if (firstColumnValue == "Total sum:")
                        {
                            inData = false;
                        }
                        else if (!inData)
                        {
                            string[] nameParts = firstColumnValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            shopName = string.Join(" ", nameParts.Skip(1).ToArray());
                        }

                        if (inData)
                        {
                            productName = excelReader.GetString(1);
                            quantity = excelReader.GetString(2);
                            price = excelReader.GetString(3);
                            sales.Add(this.CreateSale(productName, shopName, quantity, price));
                        }
                    }
                }

                return sales;
            }
        }

        private Sale CreateSale(string productName, string shopName, string quantity, string price)
        {
            int productsQuantity;
            decimal productPrice;

            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException("Product name must not be null or empty.");
            }

            if (string.IsNullOrEmpty(shopName))
            {
                throw new ArgumentNullException("Shop name must not be null or empty.");
            }

            if (!int.TryParse(quantity, out productsQuantity))
            {
                throw new ArgumentException("Quanity is not an integer.");
            }

            if (!decimal.TryParse(price, out productPrice))
            {
                throw new ArgumentException("Product price is not a decimal.");
            }

            Product product = new Product() { Name = productName };
            Shop shop = new Shop() { Name = shopName };

            Sale sale = new Sale()
            {
                Product = product,
                Shop = shop,
                UnitPrice = productPrice,
                Quantity = productsQuantity,
            };

            return sale;
        }

        private void ExtractProductsAndShops()
        {
            foreach (var sale in this.Sales)
            {
                string shopName = sale.Shop.Name;
                string productName = sale.Product.Name;
                this.Products.Add(productName);
                this.Shops.Add(shopName);
            }
        }
    }
}

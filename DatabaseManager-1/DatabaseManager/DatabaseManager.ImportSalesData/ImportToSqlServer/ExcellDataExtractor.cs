namespace DatabaseManager.ImportSalesData.ImportToSqlServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Globalization;
    using System.Linq;
    using DatabaseManager.Models;
    using Excel;

    public class ExcellDataExtractor
    {
        private string filePath;
        private List<Sale> sales;
        private HashSet<string> products;
        private HashSet<string> shops;

        public ExcellDataExtractor(string filePath)
        {
            this.filePath = filePath;
            this.sales = new List<Sale>();
            this.products = new HashSet<string>();
            this.shops = new HashSet<string>();
        }

        public void Read()
        {
            List<Sale> sales = new List<Sale>();

            ZipArchive zipArchive = null;

            try
            {
                if (File.Exists(filePath))
                {
                    zipArchive = ZipFile.OpenRead(filePath);

                    foreach (ZipArchiveEntry entry in zipArchive.Entries)
                    {
                        string fullName = entry.FullName;

                        FileInfo excelFile = new FileInfo(fullName);
                        string extension = excelFile.Extension;

                        string[] fileNames = fullName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                        if (fileNames.Length > 1)
                        {
                            string dateAsString = fileNames[fileNames.Length - 2];

                            DateTime fileDate;

                            if (DateTime.TryParseExact(dateAsString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileDate))
                            {
                                string fileName = fileNames[fileNames.Length - 1];

                                if (fileName.Length > 4)
                                {
                                    if (extension == ".xls")
                                    {
                                        List<Sale> salesToAdd = this.ReadFile(entry);

                                        for (int i = 0; i < salesToAdd.Count; i++)
                                        {
                                            salesToAdd[i].Date = fileDate;
                                        }

                                        this.sales.AddRange(salesToAdd);
                                    }
                                }
                            }
                        }
                    }
                    this.ExtractProductsAndShops();
                }
                else
                {
                    throw new FileNotFoundException("There is no file.");
                }
            }
            catch (FileNotFoundException notFoundEx)
            {
                throw notFoundEx;
            }
            catch (IOException ioEx)
            {
                throw ioEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Sale> Sales
        {
            get 
            {
                return this.sales;
            }
        }

        public HashSet<string> Products
        { 
            get 
            {
                return this.products;
            }
        }

        public HashSet<string> Shops 
        {
            get 
            {
                return this.shops;
            }
        }

        private List<Sale> ReadFile(ZipArchiveEntry entry)
        {
            List<Sale> sales;

            try
            {
                sales = this.ExtractSales(entry);
            }
            catch (FormatException formatEx)
            {
                throw formatEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
        }

        private List<Sale> ExtractSales(ZipArchiveEntry entry)
        {
            List<Sale> sales = new List<Sale>();

            MemoryStream memoryStream = new MemoryStream();

            string shopName = string.Empty;
            string productName = string.Empty;
            string quantity = string.Empty;
            string price = string.Empty;
            string sum = string.Empty;

            bool inData = false;

            try
            {
                using (memoryStream)
                {
                    entry.Open().CopyTo(memoryStream);

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(memoryStream);

                    while (excelReader.Read())
                    {
                        string firstColumnValue = excelReader.GetString(1);

                        if (firstColumnValue != null)
                        {
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
                                    string[] nameParts = firstColumnValue.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
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
                    }

                    excelReader.Close();

                    memoryStream.Close();
                }
            }
            catch (FormatException formatEx)
            {
                throw formatEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sales;
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
            for (int i = 0; i < this.sales.Count; i++)
            {
                string shopName = sales[i].Shop.Name;
                string productName = sales[i].Product.Name;
                this.products.Add(productName);
                this.shops.Add(shopName);
            }
        }
    }
}

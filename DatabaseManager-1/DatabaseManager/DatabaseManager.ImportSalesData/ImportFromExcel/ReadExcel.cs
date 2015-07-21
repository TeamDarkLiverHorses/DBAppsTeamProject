namespace DatabaseManager.ImportSalesData.ImportFromExcel
{
    using System;
    using System.Collections.Generic;
    using DatabaseManager.Models;
    using System.IO;
    using System.IO.Compression;
    using Excel;

    public class ReadExcel
    {
        public List<Sale> Read(ZipArchiveEntry entry)
        {
            List<Sale> sales = new List<Sale>();

            try
            {
                List<Sale> newSales = this.GetSale(entry);

                sales.AddRange(newSales);
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

        private List<Sale> GetSale(ZipArchiveEntry entry)
        {
            List<Sale> sales = new List<Sale>();

            MemoryStream memoryStream = new MemoryStream();

            string shopName = string.Empty;
            string productName = string.Empty;
            string quantity = string.Empty;
            string price = string.Empty;
            string sum = string.Empty;

            //string totalSum = string.Empty;

            ExcelToSale excelConverter = new ExcelToSale();

            bool inData = false;

            try
            {
                using (memoryStream)
                {
                    entry.Open().CopyTo(memoryStream);

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(memoryStream);

                    while (excelReader.Read())
                    {
                        string firstCell = excelReader.GetString(1);

                        if (firstCell != null)
                        {
                            if (firstCell == "Product")
                            {
                                inData = true;
                            }
                            else
                            {
                                if (firstCell == "Total sum:")
                                {
                                    //totalSum = excelReader.GetString(4);
                                    inData = false;
                                }
                                else if (!inData)
                                {
                                    shopName = firstCell;
                                }

                                if (inData)
                                {
                                    productName = excelReader.GetString(1);
                                    quantity = excelReader.GetString(2);
                                    price = excelReader.GetString(3);

                                    sales.Add(excelConverter.GetSale(productName, shopName, quantity, price));
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
    }
}

namespace DatabaseManager.ImportSalesData.ImportFromExcel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DatabaseManager.Models;
    using System.IO;
    using System.IO.Compression;
    using System.Globalization;

    public class ReadZipFile
    {
        public List<Sale> GetExcelEntriesFromZip(string filePath)
        {
            List<Sale> sales = new List<Sale>();

            ZipArchive zipAzrchive = null;

            try
            {
                if (File.Exists(filePath))
                {
                    zipAzrchive = ZipFile.OpenRead(filePath);

                    ReadExcel excelReader = new ReadExcel();

                    foreach (ZipArchiveEntry entry in zipAzrchive.Entries)
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
                                        List<Sale> salesToAdd = excelReader.Read(entry);

                                        for (int i = 0; i < salesToAdd.Count; i++)
                                        {
                                            salesToAdd[i].Date = fileDate;
                                        }

                                        sales.AddRange(salesToAdd);
                                    }
                                }
                            }
                        }
                    }
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

            return sales;
        }
    }
}

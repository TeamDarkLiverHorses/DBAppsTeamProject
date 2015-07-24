namespace DatabaseManager.SalesReports
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using iTextSharp.text;
    using itsFont = iTextSharp.text.Font;
    using iTextSharp.text.pdf;
    using DatabaseManager.Models;

    public class PdfReport
    {
        private const int MarginLeft = 15;
        private const int MarginRight = 15;
        private const int MarginTop = 15;
        private const int MarginBottom = 15;
        private BaseColor TableTitleColor = new BaseColor(75, 75, 75);
        private BaseColor TableTitleFontColor = new BaseColor(250, 250, 250);
        private BaseColor TableDateHeaderColor = new BaseColor(100, 100, 100);
        private BaseColor TableColumnHeaderColor = new BaseColor(75, 75, 75);
        private BaseColor TableSectionFooterColor = new BaseColor(200, 200, 200);
        private BaseColor TableFooterColor = new BaseColor(150, 50, 25);
        private BaseColor TableFooterFontColor = new BaseColor(0, 0, 0);
        private const int TableHeaderFontSize = 12;
        private const int TableColumnHeadersFontSize = 11;
        private const int TableRowsFontSize = 10;

        private string filePath;
        private float[] columnWidths = new float[] { 2f, 1f, 1f, 2f, 1f };


        public PdfReport(string filePath)
        {
            this.filePath = filePath;
        }

        public void Create(List<Sale> sales)
        {
            Document pdfDoc = new Document(PageSize.A4, MarginLeft, MarginRight, MarginTop, MarginBottom);

            var salesToAdd = from sale in sales
                             group sale by sale.Date into salesByDateGroup
                             orderby salesByDateGroup.Key descending
                             select salesByDateGroup;

            FileStream fileStream;

            try
            {
                using (fileStream = new FileStream(this.filePath, FileMode.CreateNew, FileAccess.Write))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, fileStream);
                    pdfWriter.Open();
                    PdfPTable table = new PdfPTable(5);
                    table.SetWidths(columnWidths);

                    WriteHeader(table, "Aggregated Sales Report", 5, 1, TableTitleColor);

                    decimal totalSum = 0M;

                    foreach (var dateKey in salesToAdd)
                    {
                        decimal totalSumForDate = 0M;

                        WriteHeader(
                            table,
                            "Date: " + dateKey.Key.ToString("dd-MM-yyyy"),
                            5,
                            0,
                            TableDateHeaderColor);

                        WriteColumnNames(table, "Product", "Quantity", "Unit Price", "Location", "Sum");

                        foreach (var sale in dateKey)
                        {
                            WriteRow(table, sale.Product.Name, sale.Quantity, sale.UnitPrice, sale.Shop.Name);

                            totalSumForDate += sale.Quantity * sale.UnitPrice;
                        }
                        totalSum += totalSumForDate;

                        WriteFooter(
                            table,
                            "Total for date " + dateKey.Key.ToString("dd-MM-yyyy") + ":",
                            totalSumForDate,
                            2,
                            TableSectionFooterColor);
                    }

                    WriteFooter(table, "Grand Total:", totalSum, 2, TableFooterColor);

                    pdfDoc.Open();
                    pdfDoc.Add(table);
                    pdfDoc.Close();

                    pdfWriter.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void WriteHeader(PdfPTable table, string header, int colSpan, int headerAlignment, BaseColor color)
        {
            itsFont headerFont = new itsFont(
                itsFont.FontFamily.HELVETICA,
                TableHeaderFontSize,
                itsFont.BOLD,
                TableTitleFontColor);

            table.AddCell(new PdfPCell(new Phrase(header, headerFont))
            {
                Colspan = colSpan,
                HorizontalAlignment = headerAlignment,
                BackgroundColor = color
            });
        }

        private void WriteFooter(PdfPTable table, string footer, decimal sum, int headerAlignment, BaseColor color)
        {
            itsFont footerFont = new itsFont(itsFont.FontFamily.HELVETICA, 12, itsFont.BOLDITALIC, TableFooterFontColor);

            table.AddCell(new PdfPCell(new Phrase(footer, footerFont))
            {
                Colspan = 4,
                HorizontalAlignment = headerAlignment,
                BackgroundColor = color
            });
            table.AddCell(new PdfPCell(new Phrase(string.Format("{0:# ###.00}", sum)))
            {
                Colspan = 1,
                HorizontalAlignment = headerAlignment,
                BackgroundColor = color
            });
        }

        private void WriteRow(PdfPTable table, string product, int quantity, decimal price, string supermarket)
        {
            itsFont cellFont = new itsFont(
                itsFont.FontFamily.HELVETICA,
                TableColumnHeadersFontSize,
                itsFont.NORMAL,
                BaseColor.BLACK);

            table.AddCell(new PdfPCell(new Phrase(product, cellFont)) { HorizontalAlignment = 0 });
            table.AddCell(new PdfPCell(new Phrase(quantity.ToString(), cellFont)) { HorizontalAlignment = 0 });
            table.AddCell(new PdfPCell(new Phrase(string.Format("{0:# ###.00}", price), cellFont)) { HorizontalAlignment = 0 });
            table.AddCell(new PdfPCell(new Phrase(supermarket, cellFont)) { HorizontalAlignment = 0 });
            table.AddCell(new PdfPCell(new Phrase(string.Format("{0:# ###.00}", (quantity * price)), cellFont)) { HorizontalAlignment = 0 });
        }

        private void WriteColumnNames(PdfPTable table, string col1Name, string col2Name, string col3Name, string col4Name, string col5Name)
        {
            itsFont cellFont = new itsFont(
                itsFont.FontFamily.HELVETICA,
                TableColumnHeadersFontSize,
                itsFont.BOLD,
                BaseColor.BLACK);

            table.AddCell(new PdfPCell(new Phrase(col1Name, cellFont)) { HorizontalAlignment = 1 });
            table.AddCell(new PdfPCell(new Phrase(col2Name, cellFont)) { HorizontalAlignment = 1 });
            table.AddCell(new PdfPCell(new Phrase(col3Name, cellFont)) { HorizontalAlignment = 1 });
            table.AddCell(new PdfPCell(new Phrase(col4Name, cellFont)) { HorizontalAlignment = 1 });
            table.AddCell(new PdfPCell(new Phrase(col5Name, cellFont)) { HorizontalAlignment = 1 });
        }
    }
}

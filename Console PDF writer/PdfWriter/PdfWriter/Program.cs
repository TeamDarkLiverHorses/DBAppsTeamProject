using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;

namespace PdfWriter
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

			string path = Path.Combine(@"D:\", "test.pdf");

			Sale newS = new Sale () { SaleId = 1, Product = new ProductClass() {Name="product 1"}, Supermarket = "ala", 
				ProductPrice = 1.1M, Quanity = 100, Date = DateTime.Now };
			Sale newSe = new Sale () { SaleId = 2, Product = new ProductClass() {Name="product 2"}, Supermarket = "fala", 
				ProductPrice = 2.1M, Quanity = 50, Date = DateTime.Now };
			Sale newSs = new Sale () { SaleId = 3, Product = new ProductClass() {Name="product 3"}, Supermarket = "gala", 
				ProductPrice = 3.1M, Quanity = 30, Date = DateTime.Now };

			Sale newSd = new Sale () { SaleId = 4, Product = new ProductClass() {Name="product 4"}, Supermarket = "alasdfsdfsdfsdfsdfsdfsdf", 
				ProductPrice = 1.1M, Quanity = 100, Date = new DateTime(2015, 7, 20) };
			Sale newSed = new Sale () { SaleId = 5, Product = new ProductClass() {Name="product 5"}, Supermarket = "fala", 
				ProductPrice = 2.1M, Quanity = 50, Date = new DateTime(2015, 7, 20) };
			Sale newSsd = new Sale () { SaleId = 6, Product = new ProductClass() {Name="product 6"}, Supermarket = "gala", 
				ProductPrice = 3.1M, Quanity = 30, Date = new DateTime(2015, 7, 20) };

			List<Sale> sales = new List<Sale> ();
			sales.AddRange (new Sale[] { newS, newSe, newSs, newSsd, newSed, newSd });

			Document pdfDoc = new Document (PageSize.A4, 36, 36, 36, 36);

			CreatePDF cpdf = new CreatePDF ();
			CreatePDF.Create(pdfDoc, path, sales);

			Console.WriteLine ("Success");
		}
	}

	class CreatePDF
	{
		public static void Create(Document pdf, string filePath, List<Sale> sales)
		{
			var salesToAdd = from s in sales
				group s by s.Date into k
				orderby k.Key
				select k;

			FileStream fs = null;

			try
			{
				using (fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
				{
					iTextSharp.text.pdf.PdfWriter pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdf, fs);

					pdfWriter.Open();

					PdfPTable table = new PdfPTable(5);
					table.SetWidths(new float[] {2f, 1f, 1f, 2f, 1f});

					WriteHeader(table, "Summary", 5, 1, iTextSharp.text.BaseColor.LIGHT_GRAY);

					decimal totalSum = 0M;

					foreach (var key in salesToAdd) 
					{
						decimal totalSumForDate = 0M;

						WriteHeader (table, key.Key.ToString("dd-MM-yyyy"), 5, 0, iTextSharp.text.BaseColor.CYAN);

						WriteBody (table, "Product", "Quantity", "Price", "Place", "Sum");

						foreach (var value in key) 
						{
							WriteBody (table, value.Product.Name, value.Quanity, value.ProductPrice, value.Supermarket);

							totalSumForDate += value.Quanity * value.ProductPrice;
						}

						totalSum += totalSumForDate;

						WriteFooter(table, "Total for date " + key.Key.ToString("dd-MM-yyyy") + ":", totalSumForDate, 2, iTextSharp.text.BaseColor.GRAY);
							
					}

					WriteFooter(table, "Grand Total:", totalSum, 2, iTextSharp.text.BaseColor.LIGHT_GRAY);

					pdf.Open();
					pdf.Add (table);
					pdf.Close();

					pdfWriter.Close();
				}
			}
			catch (Exception ex) 
			{
				Console.WriteLine (ex.Message);
			}
		}

		private static void WriteHeader(PdfPTable table, string header, int colSpan, int headerAlignment, iTextSharp.text.BaseColor color)
		{
			Font headerFont = new Font (Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);

			table.AddCell (new PdfPCell (new Phrase(header, headerFont)) { Colspan = colSpan, HorizontalAlignment = headerAlignment, 
				BackgroundColor = color});
		}

		private static void WriteFooter(PdfPTable table, string footer, decimal sum, int headerAlignment, iTextSharp.text.BaseColor color)
		{
			Font footerFont = new Font (Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);

			table.AddCell (new PdfPCell (new Phrase(footer, footerFont)) { Colspan = 4, HorizontalAlignment = headerAlignment, 
				BackgroundColor = color});
			table.AddCell (new PdfPCell (new Phrase(sum.ToString("c"))) { Colspan = 1, HorizontalAlignment = headerAlignment, 
				BackgroundColor = color});
		}


		private static void WriteBody(PdfPTable table, string product, int quantity, decimal price, string supermarket)
		{
			table.AddCell (new PdfPCell (new Phrase (product)) { HorizontalAlignment = 0 });
			table.AddCell (new PdfPCell (new Phrase (quantity.ToString())) { HorizontalAlignment = 0 });
			table.AddCell (new PdfPCell (new Phrase (price.ToString("c"))) { HorizontalAlignment = 0 });
			table.AddCell (new PdfPCell (new Phrase (supermarket)) { HorizontalAlignment = 0 });
			table.AddCell (new PdfPCell (new Phrase ((quantity * price).ToString())) { HorizontalAlignment = 0 });
		}

		private static void WriteBody(PdfPTable table, string fColumn, string sColumn, string tColumn, string fourthColumn, string fifthColumn)
		{
			Font cellFont = new Font (Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK);

			table.AddCell (new PdfPCell (new Phrase (fColumn, cellFont)) { HorizontalAlignment = 1 });
			table.AddCell (new PdfPCell (new Phrase (sColumn, cellFont)) { HorizontalAlignment = 1 });
			table.AddCell (new PdfPCell (new Phrase (tColumn, cellFont)) { HorizontalAlignment = 1 });
			table.AddCell (new PdfPCell (new Phrase (fourthColumn, cellFont)) { HorizontalAlignment = 1 });
			table.AddCell (new PdfPCell (new Phrase (fifthColumn, cellFont)) { HorizontalAlignment = 1 });
		}
	}

	class Sale
	{
		public int SaleId { get; set; }

		public int Quanity { get; set; }

		public decimal ProductPrice { get; set;}

		public ProductClass Product { get; set;}

		public DateTime Date { get; set;}

		public string Supermarket { get; set;}
	}

	class ProductClass
	{

		public int Id { get; set;}

		public string Name { get; set;}
	}
}

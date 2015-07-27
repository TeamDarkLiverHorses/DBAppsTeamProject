namespace DatabaseManager.XML.Serialization
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public static class XMLSerializer
    {
        public static async Task WriteXML(SalesReportsWrapper wrapper, string fileName)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException("Wrapper cannot be null");
            }

            using (var fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                string xml = ToXML(wrapper);
                await streamWriter.WriteAsync(xml);
            }
        }

        public static async Task<VendorExpensesWrapper> ReadXML(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                var xmlSerializer = new XmlSerializer(typeof(VendorExpensesWrapper));
                var result = await Task.Run(() => xmlSerializer.Deserialize(streamReader));
                return (VendorExpensesWrapper)result;
            }
        }

        private static string ToXML(SalesReportsWrapper wrapper)
        {
            var serializer = new XmlSerializer(typeof(SalesReportsWrapper));
            using (var writer = new UTF8StringWriter())
            {
                var xsn = new XmlSerializerNamespaces();
                xsn.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, wrapper, xsn);
                string xml = writer.ToString();
                return xml;
            }
        }
    }
}

namespace DatabaseManager.XML
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public static class Serializer
    {
        public static async Task WriteXML(SalesReportsWrapper wrapper, string fileName)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException("wrapper cannot be null");
            }

            using (var fileStream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                string xml = ToXML(wrapper);
                await streamWriter.WriteAsync(xml);
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

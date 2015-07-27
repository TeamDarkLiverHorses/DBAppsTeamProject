namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    public class SalesReport
    {
        [XmlAttribute("vendor")]
        public string Vendor { get; set; }

        [XmlElement("summary")]
        public Summary[] Summaries { get; set; }
    }
}

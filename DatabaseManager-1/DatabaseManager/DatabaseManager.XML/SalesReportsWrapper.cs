namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    [XmlRoot("sales")]
    public class SalesReportsWrapper
    {
        [XmlElement("sale")]
        public SalesReport[] SaleReports { get; set; }
    }
}

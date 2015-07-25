namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    [XmlRoot("expenses-by-month")]
    public class VendorExpensesWrapper
    {
        [XmlElement("vendor")]
        public Vendor[] Vendors { get; set; }
    }
}

namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    public class Expense
    {
        [XmlAttribute("month")]
        public string Month { get; set; }

        [XmlText]
        public string Price { get; set; }
    }
}

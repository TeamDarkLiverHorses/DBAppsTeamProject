namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    public class Vendor
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("expenses")]
        public Expense[] Summaries { get; set; }
    }
}

namespace DatabaseManager.XML
{
    using System.Xml.Serialization;

    public class Summary
    {
        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("total-sum")]
        public string TotalPrice { get; set; }
    }
}

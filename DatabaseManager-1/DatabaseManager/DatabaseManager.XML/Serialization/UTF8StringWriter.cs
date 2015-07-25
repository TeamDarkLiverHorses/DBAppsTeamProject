namespace DatabaseManager.XML.Serialization
{
    internal class UTF8StringWriter : System.IO.StringWriter
    {
        public override System.Text.Encoding Encoding
        {
            get
            {
                return System.Text.Encoding.UTF8;
            }
        }
    }
}

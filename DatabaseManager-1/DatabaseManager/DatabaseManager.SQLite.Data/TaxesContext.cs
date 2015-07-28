namespace DatabaseManager.SQLite.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TaxesContext : DbContext
    {
        public TaxesContext()
            : base("name=TaxesContext")
        {
        }

        public IDbSet<Tax> Taxes { get; set; }
    }
}
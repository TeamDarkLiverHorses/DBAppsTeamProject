using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Core.Models;

namespace DatabaseManager.Core.ImportToSqlServer
{
    public class ImportFromOracleDataHolder
    {
        public HashSet<string> Vendors { get; set; }

        public HashSet<string> Measures { get; set; }

        public HashSet<string> Categories { get; set; }

        public Product[] Products { get; set; }

        public ImportFromOracleDataHolder(HashSet<string> vendors, HashSet<string> measures, 
            HashSet<string> categories, Product[] products)
        {
            this.Categories = categories;

            this.Measures = measures;

            this.Vendors = vendors;

            this.Products = products;
        }
    }
}

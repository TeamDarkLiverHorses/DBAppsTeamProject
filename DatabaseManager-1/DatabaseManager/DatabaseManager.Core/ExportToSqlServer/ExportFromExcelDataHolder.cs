using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Core.Models;

namespace DatabaseManager.Core.ExportToSqlServer
{
    public class ExportFromExcelDataHolder
    {
        public HashSet<Product> Products { get; set; }
        
        public HashSet<Shop> Shops {get; set;}

        public Sale[] Sales { get; set; }

        public ExportFromExcelDataHolder(HashSet<Product> products, HashSet<Shop> shops, Sale[] salse)
        {
            this.Products = products;

            this.Shops = shops;

            this.Sales = salse;
        }
    }
}

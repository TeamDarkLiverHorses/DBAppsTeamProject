using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManager.Core.Models;

namespace DatabaseManager.Core.ImportToSqlServer
{
    public class ImportFromExcelDataHolder
    {
        public HashSet<string> Products { get; set; }
        
        public HashSet<string> Shops {get; set;}

        public Sale[] Sales { get; set; }

        public ImportFromExcelDataHolder(HashSet<string> products, HashSet<string> shops, Sale[] salse)
        {
            this.Products = products;

            this.Shops = shops;

            this.Sales = salse;
        }
    }
}

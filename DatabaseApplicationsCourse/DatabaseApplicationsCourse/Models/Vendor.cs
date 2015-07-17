using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApplicationsCourse.Models
{
    class Vendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Vendor(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}

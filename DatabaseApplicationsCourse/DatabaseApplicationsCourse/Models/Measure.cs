using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApplicationsCourse.Models
{
    class Measure
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Measure(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}

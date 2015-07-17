using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApplicationsCourse.Models
{
    class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category Parent { get; set; }

        public Category(int id, string name)
            :this(id, name, null)
        {

        }

        public Category(int id, string name, Category parent)
        {
            this.Id = id;

            this.Name = name;

            this.Parent = parent;
        }
    }
}

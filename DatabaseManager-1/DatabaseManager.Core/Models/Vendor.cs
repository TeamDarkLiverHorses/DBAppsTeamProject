namespace DatabaseManager.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vendor
    {
        public Vendor()
        {
            Expenses = new HashSet<Expens>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Expens> Expenses { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

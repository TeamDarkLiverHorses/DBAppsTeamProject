namespace DatabaseManager.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public Product()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public int VendorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int MeasureId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual Measure Measure { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}

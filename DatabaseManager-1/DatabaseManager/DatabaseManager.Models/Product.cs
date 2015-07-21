namespace DatabaseManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Product
    {
        private ICollection<Sale> sales;

        public Product()
        {
            this.sales = new HashSet<Sale>();
        }

        [Key]
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

        public virtual ICollection<Sale> Sales
        {
            get
            {
                return this.sales;
            }

            set
            {
                this.sales = value;
            }
        }

        public virtual Vendor Vendor { get; set; }

        public override bool Equals(object obj)
        {
            return this.Name == ((Product)obj).Name;
        }
    }
}

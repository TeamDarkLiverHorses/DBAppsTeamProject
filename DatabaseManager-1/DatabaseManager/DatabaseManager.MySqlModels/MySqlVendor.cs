using System;
namespace DatabaseManager.MySqlModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VendorExpenses")]
    public partial class MySqlVendor
    {
        private ICollection<MySqlProduct> products;

        public MySqlVendor()
        {
            this.products = new HashSet<MySqlProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Expenses { get; set; }

        public virtual ICollection<MySqlProduct> Products
        {
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
            }
        }
    }
}

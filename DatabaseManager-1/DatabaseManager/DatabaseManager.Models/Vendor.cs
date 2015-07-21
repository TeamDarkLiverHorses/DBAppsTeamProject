namespace DatabaseManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Vendor
    {
        private ICollection<Expense> expenses;
        private ICollection<Product> products;

        public Vendor()
        {
            this.expenses = new HashSet<Expense>();
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Expense> Expenses
        {
            get
            {
                return this.expenses;
            }

            set
            {
                this.expenses = value;
            }
        }

        public virtual ICollection<Product> Products
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

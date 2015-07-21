namespace DatabaseManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Expenses")]
    public partial class Expense
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        [Column(TypeName = "money")]
        public decimal Ammount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}

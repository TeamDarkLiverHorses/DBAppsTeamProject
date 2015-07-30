namespace DatabaseManager.MySqlModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductIncomes")]
    public partial class MySqlProduct
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public decimal Incomes { get; set; }

        public virtual MySqlVendor Vendor { get; set; }
    }
}

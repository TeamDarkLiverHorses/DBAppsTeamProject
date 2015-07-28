namespace DatabaseManager.SQLite.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Tax
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public float TaxSize { get; set; }
    }
}

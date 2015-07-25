namespace DatabaseManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Sale
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ShopId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        public virtual Product Product { get; set; }

        public virtual Shop Shop { get; set; }

        public override bool Equals(object obj)
        {
            return this.Date == ((Sale)obj).Date &&
                this.ProductId == ((Sale)obj).ProductId &&
                this.ShopId == ((Sale)obj).ShopId;
        }

        public override int GetHashCode()
        {
            return (int)this.Date.Ticks ^ this.ProductId ^ this.ShopId;
        }
    }
}

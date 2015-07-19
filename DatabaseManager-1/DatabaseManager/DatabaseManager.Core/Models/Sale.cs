namespace DatabaseManager.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
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
    }
}

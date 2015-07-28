namespace DatabaseManager.MySqlData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using MySql.Data.Entity;
    using DatabaseManager.MySqlModels;
    
    public class MySqlSupermarketContext : DbContext
    {
        public MySqlSupermarketContext() : base("name=MySqlConnection")
        { 
        
        }
        
        public virtual DbSet<MySqlProduct> products { get; set; }
        public virtual DbSet<MySqlVendor> vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MySqlProduct>()
                .Property(e => e.Incomes)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MySqlProduct>()
                .Property(e => e.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<MySqlVendor>()
                .Property(e => e.Expenses)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MySqlVendor>()
                .Property(e => e.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<MySqlVendor>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Vendor)
                .WillCascadeOnDelete(false);
        }
    }
}

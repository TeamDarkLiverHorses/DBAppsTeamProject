namespace DatabaseManager.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseManager.Data.SupermarketsContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "DatabaseManager.Data.SupermarketsContext";
        }

        protected override void Seed(DatabaseManager.Data.SupermarketsContext context)
        {
        }
    }
}

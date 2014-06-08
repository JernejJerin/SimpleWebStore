namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalUnitPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "UnitPrice", c => c.Double());
        }
    }
}

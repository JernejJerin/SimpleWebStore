namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShortDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ShortDescription", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Product", "FullDescription", c => c.String(nullable: false));
            DropColumn("dbo.Product", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Product", "FullDescription");
            DropColumn("dbo.Product", "ShortDescription");
        }
    }
}

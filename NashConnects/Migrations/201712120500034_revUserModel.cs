namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "Category", c => c.String(maxLength: 50));
            DropColumn("dbo.AspNetUsers", "FName");
            DropColumn("dbo.AspNetUsers", "LName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: false, maxLength: 25));
            DropColumn("dbo.AspNetUsers", "Category");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}

namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryandname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Category", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Category", c => c.String(maxLength: 50));
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}

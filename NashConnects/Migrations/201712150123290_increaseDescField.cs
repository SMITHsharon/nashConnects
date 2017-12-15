namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increaseDescField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 600));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 300));
        }
    }
}

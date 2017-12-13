namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropFLandNP_ModelKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "WebsiteURL", c => c.String(nullable: false, maxLength: 75));
            DropColumn("dbo.AspNetUsers", "FreelancerId");
            DropColumn("dbo.AspNetUsers", "NonProfitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NonProfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "FreelancerId", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "WebsiteURL", c => c.String(nullable: false, maxLength: 50));
        }
    }
}

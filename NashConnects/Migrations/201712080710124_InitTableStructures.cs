namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitTableStructures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100),
                        NonProfit_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.NonProfit_Id)
                .Index(t => t.NonProfit_Id);
            
            CreateTable(
                "dbo.FLRegEvent",
                c => new
                    {
                        FLRefId = c.Int(nullable: false),
                        EventRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FLRefId, t.EventRefId })
                .ForeignKey("dbo.Events", t => t.FLRefId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.EventRefId, cascadeDelete: true)
                .Index(t => t.FLRefId)
                .Index(t => t.EventRefId);
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "WebsiteURL", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 300));
            AddColumn("dbo.AspNetUsers", "RecommendCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "FreelancerId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Newsletter", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Public", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NonProfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CalendarLink", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "NonProfit_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FLRegEvent", "EventRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FLRegEvent", "FLRefId", "dbo.Events");
            DropIndex("dbo.FLRegEvent", new[] { "EventRefId" });
            DropIndex("dbo.FLRegEvent", new[] { "FLRefId" });
            DropIndex("dbo.Events", new[] { "NonProfit_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "CalendarLink");
            DropColumn("dbo.AspNetUsers", "NonProfitId");
            DropColumn("dbo.AspNetUsers", "Public");
            DropColumn("dbo.AspNetUsers", "Newsletter");
            DropColumn("dbo.AspNetUsers", "FreelancerId");
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "RecommendCount");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "WebsiteURL");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.FLRegEvent");
            DropTable("dbo.Events");
        }
    }
}

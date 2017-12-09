namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicModels : DbMigration
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
                "dbo.FreelancerEvents",
                c => new
                    {
                        Freelancer_Id = c.String(nullable: false, maxLength: 128),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Freelancer_Id, t.Event_EventId })
                .ForeignKey("dbo.AspNetUsers", t => t.Freelancer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.Freelancer_Id)
                .Index(t => t.Event_EventId);
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.AspNetUsers", "WebsiteURL", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 300));
            AddColumn("dbo.AspNetUsers", "RecommendCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "FreelancerId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Newsletter", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "PublicReveal", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NonProfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CalendarLink", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "NonProfit_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreelancerEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.FreelancerEvents", "Freelancer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FreelancerEvents", new[] { "Event_EventId" });
            DropIndex("dbo.FreelancerEvents", new[] { "Freelancer_Id" });
            DropIndex("dbo.Events", new[] { "NonProfit_Id" });
            DropColumn("dbo.AspNetUsers", "CalendarLink");
            DropColumn("dbo.AspNetUsers", "NonProfitId");
            DropColumn("dbo.AspNetUsers", "PublicReveal");
            DropColumn("dbo.AspNetUsers", "Newsletter");
            DropColumn("dbo.AspNetUsers", "FreelancerId");
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "RecommendCount");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "WebsiteURL");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.FreelancerEvents");
            DropTable("dbo.Events");
        }
    }
}

namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModels : DbMigration
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
                        Description = c.String(nullable: false),
                        NonProfit_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.NonProfit_Id)
                .Index(t => t.NonProfit_Id);
            
            CreateTable(
                "dbo.FLFLRecs",
                c => new
                    {
                        Freelancer_Id = c.String(nullable: false, maxLength: 128),
                        Freelancer_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Freelancer_Id, t.Freelancer_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.Freelancer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Freelancer_Id1)
                .Index(t => t.Freelancer_Id)
                .Index(t => t.Freelancer_Id1);
            
            CreateTable(
                "dbo.NPFLRecs",
                c => new
                    {
                        NonProfit_Id = c.String(nullable: false, maxLength: 128),
                        Freelancer_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.NonProfit_Id, t.Freelancer_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.NonProfit_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Freelancer_Id)
                .Index(t => t.NonProfit_Id)
                .Index(t => t.Freelancer_Id);
            
            CreateTable(
                "dbo.NPNPRecs",
                c => new
                    {
                        NonProfit_Id = c.String(nullable: false, maxLength: 128),
                        NonProfit_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.NonProfit_Id, t.NonProfit_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.NonProfit_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NonProfit_Id1)
                .Index(t => t.NonProfit_Id)
                .Index(t => t.NonProfit_Id1);
            
            CreateTable(
                "dbo.FLRegisteredEvents",
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
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "FreelancerId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "WebsiteURL", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String(maxLength: 300));
            AddColumn("dbo.AspNetUsers", "Newsletter", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Public", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "FLRecommendCount", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "NonProfitId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "WebsiteURL1", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "CalendarLink", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "Description", c => c.String(maxLength: 300));
            AddColumn("dbo.AspNetUsers", "NPRecommendCount", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Active1", c => c.Boolean());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreelancerEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.FreelancerEvents", "Freelancer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NonProfitNonProfits", "NonProfit_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.NonProfitNonProfits", "NonProfit_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NonProfitFreelancers", "Freelancer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NonProfitFreelancers", "NonProfit_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "NonProfit_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreelancerFreelancers", "Freelancer_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreelancerFreelancers", "Freelancer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FreelancerEvents", new[] { "Event_EventId" });
            DropIndex("dbo.FreelancerEvents", new[] { "Freelancer_Id" });
            DropIndex("dbo.NonProfitNonProfits", new[] { "NonProfit_Id1" });
            DropIndex("dbo.NonProfitNonProfits", new[] { "NonProfit_Id" });
            DropIndex("dbo.NonProfitFreelancers", new[] { "Freelancer_Id" });
            DropIndex("dbo.NonProfitFreelancers", new[] { "NonProfit_Id" });
            DropIndex("dbo.FreelancerFreelancers", new[] { "Freelancer_Id1" });
            DropIndex("dbo.FreelancerFreelancers", new[] { "Freelancer_Id" });
            DropIndex("dbo.Events", new[] { "NonProfit_Id" });
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "Active1");
            DropColumn("dbo.AspNetUsers", "NPRecommendCount");
            DropColumn("dbo.AspNetUsers", "Description");
            DropColumn("dbo.AspNetUsers", "CalendarLink");
            DropColumn("dbo.AspNetUsers", "WebsiteURL1");
            DropColumn("dbo.AspNetUsers", "NonProfitId");
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "FLRecommendCount");
            DropColumn("dbo.AspNetUsers", "Public");
            DropColumn("dbo.AspNetUsers", "Newsletter");
            DropColumn("dbo.AspNetUsers", "Bio");
            DropColumn("dbo.AspNetUsers", "WebsiteURL");
            DropColumn("dbo.AspNetUsers", "FreelancerId");
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.FreelancerEvents");
            DropTable("dbo.NonProfitNonProfits");
            DropTable("dbo.NonProfitFreelancers");
            DropTable("dbo.FreelancerFreelancers");
            DropTable("dbo.Events");
        }
    }
}

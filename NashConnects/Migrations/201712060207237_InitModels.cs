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
                        EId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EId);
            
            CreateTable(
                "dbo.NonProfits",
                c => new
                    {
                        NPId = c.Int(nullable: false, identity: true),
                        WebsiteURL = c.String(nullable: false, maxLength: 50),
                        CalendarLink = c.String(maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 300),
                        RecommondCount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Users_Id = c.String(maxLength: 128),
                        Event_EId = c.Int(),
                    })
                .PrimaryKey(t => t.NPId)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .ForeignKey("dbo.Events", t => t.Event_EId)
                .Index(t => t.Users_Id)
                .Index(t => t.Event_EId);
            
            CreateTable(
                "dbo.Freelancers",
                c => new
                    {
                        FLId = c.Int(nullable: false, identity: true),
                        WebsiteURL = c.String(nullable: false, maxLength: 50),
                        Bio = c.String(nullable: false, maxLength: 300),
                        Newsletter = c.Boolean(nullable: false),
                        Public = c.Boolean(nullable: false),
                        RecommondCount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Freelancer_FLId = c.Int(),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FLId)
                .ForeignKey("dbo.Freelancers", t => t.Freelancer_FLId)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.Freelancer_FLId)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.FreelancerNonProfits",
                c => new
                    {
                        Freelancer_FLId = c.Int(nullable: false),
                        NonProfit_NPId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Freelancer_FLId, t.NonProfit_NPId })
                .ForeignKey("dbo.Freelancers", t => t.Freelancer_FLId, cascadeDelete: true)
                .ForeignKey("dbo.NonProfits", t => t.NonProfit_NPId, cascadeDelete: true)
                .Index(t => t.Freelancer_FLId)
                .Index(t => t.NonProfit_NPId);
            
            CreateTable(
                "dbo.FreelancerEvents",
                c => new
                    {
                        Freelancer_FLId = c.Int(nullable: false),
                        Event_EId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Freelancer_FLId, t.Event_EId })
                .ForeignKey("dbo.Freelancers", t => t.Freelancer_FLId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EId, cascadeDelete: true)
                .Index(t => t.Freelancer_FLId)
                .Index(t => t.Event_EId);
            
            AddColumn("dbo.AspNetUsers", "FName", c => c.String(maxLength: 25));
            AddColumn("dbo.AspNetUsers", "LName", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NonProfits", "Event_EId", "dbo.Events");
            DropForeignKey("dbo.NonProfits", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Freelancers", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreelancerEvents", "Event_EId", "dbo.Events");
            DropForeignKey("dbo.FreelancerEvents", "Freelancer_FLId", "dbo.Freelancers");
            DropForeignKey("dbo.FreelancerNonProfits", "NonProfit_NPId", "dbo.NonProfits");
            DropForeignKey("dbo.FreelancerNonProfits", "Freelancer_FLId", "dbo.Freelancers");
            DropForeignKey("dbo.Freelancers", "Freelancer_FLId", "dbo.Freelancers");
            DropIndex("dbo.FreelancerEvents", new[] { "Event_EId" });
            DropIndex("dbo.FreelancerEvents", new[] { "Freelancer_FLId" });
            DropIndex("dbo.FreelancerNonProfits", new[] { "NonProfit_NPId" });
            DropIndex("dbo.FreelancerNonProfits", new[] { "Freelancer_FLId" });
            DropIndex("dbo.Freelancers", new[] { "Users_Id" });
            DropIndex("dbo.Freelancers", new[] { "Freelancer_FLId" });
            DropIndex("dbo.NonProfits", new[] { "Event_EId" });
            DropIndex("dbo.NonProfits", new[] { "Users_Id" });
            DropColumn("dbo.AspNetUsers", "LName");
            DropColumn("dbo.AspNetUsers", "FName");
            DropTable("dbo.FreelancerEvents");
            DropTable("dbo.FreelancerNonProfits");
            DropTable("dbo.Freelancers");
            DropTable("dbo.NonProfits");
            DropTable("dbo.Events");
        }
    }
}

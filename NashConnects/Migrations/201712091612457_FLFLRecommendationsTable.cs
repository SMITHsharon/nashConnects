namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FLFLRecommendationsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FLRegEvent", newName: "FLRegEvents");
            CreateTable(
                "dbo.FreelancerFreelancers",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreelancerFreelancers", "Freelancer_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.FreelancerFreelancers", "Freelancer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.FreelancerFreelancers", new[] { "Freelancer_Id1" });
            DropIndex("dbo.FreelancerFreelancers", new[] { "Freelancer_Id" });
            DropTable("dbo.FreelancerFreelancers");
            RenameTable(name: "dbo.FLRegEvents", newName: "FLRegEvent");
        }
    }
}

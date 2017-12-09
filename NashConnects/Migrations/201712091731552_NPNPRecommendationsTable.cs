namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NPNPRecommendationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NonProfitNonProfits",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NonProfitNonProfits", "NonProfit_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.NonProfitNonProfits", "NonProfit_Id", "dbo.AspNetUsers");
            DropIndex("dbo.NonProfitNonProfits", new[] { "NonProfit_Id1" });
            DropIndex("dbo.NonProfitNonProfits", new[] { "NonProfit_Id" });
            DropTable("dbo.NonProfitNonProfits");
        }
    }
}

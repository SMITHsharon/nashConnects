namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FLNPRecommendationsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FLRegEvents", name: "FLRefId", newName: "FLRegId");
            RenameColumn(table: "dbo.FLRegEvents", name: "EventRefId", newName: "EventRegId");
            RenameIndex(table: "dbo.FLRegEvents", name: "IX_FLRefId", newName: "IX_FLRegId");
            RenameIndex(table: "dbo.FLRegEvents", name: "IX_EventRefId", newName: "IX_EventRegId");
            CreateTable(
                "dbo.FLNPRecommendations",
                c => new
                    {
                        FLRecId = c.String(nullable: false, maxLength: 128),
                        NPRecId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FLRecId, t.NPRecId })
                .ForeignKey("dbo.AspNetUsers", t => t.FLRecId)
                .ForeignKey("dbo.AspNetUsers", t => t.NPRecId)
                .Index(t => t.FLRecId)
                .Index(t => t.NPRecId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FLNPRecommendations", "NPRecId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FLNPRecommendations", "FLRecId", "dbo.AspNetUsers");
            DropIndex("dbo.FLNPRecommendations", new[] { "NPRecId" });
            DropIndex("dbo.FLNPRecommendations", new[] { "FLRecId" });
            DropTable("dbo.FLNPRecommendations");
            RenameIndex(table: "dbo.FLRegEvents", name: "IX_EventRegId", newName: "IX_EventRefId");
            RenameIndex(table: "dbo.FLRegEvents", name: "IX_FLRegId", newName: "IX_FLRefId");
            RenameColumn(table: "dbo.FLRegEvents", name: "EventRegId", newName: "EventRefId");
            RenameColumn(table: "dbo.FLRegEvents", name: "FLRegId", newName: "FLRefId");
        }
    }
}

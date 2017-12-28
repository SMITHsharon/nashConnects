namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class swapKeysFLRegEventsTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FLRegEvents", new[] { "FLRegId" });
            DropIndex("dbo.FLRegEvents", new[] { "EventRegId" });
            RenameColumn(table: "dbo.FLRegEvents", name: "FLRegId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.FLRegEvents", name: "EventRegId", newName: "FLRegId");
            RenameColumn(table: "dbo.FLRegEvents", name: "__mig_tmp__0", newName: "EventRegId");
            DropPrimaryKey("dbo.FLRegEvents");
            AlterColumn("dbo.FLRegEvents", "FLRegId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.FLRegEvents", "EventRegId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.FLRegEvents", new[] { "EventRegId", "FLRegId" });
            CreateIndex("dbo.FLRegEvents", "EventRegId");
            CreateIndex("dbo.FLRegEvents", "FLRegId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FLRegEvents", new[] { "FLRegId" });
            DropIndex("dbo.FLRegEvents", new[] { "EventRegId" });
            DropPrimaryKey("dbo.FLRegEvents");
            AlterColumn("dbo.FLRegEvents", "EventRegId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.FLRegEvents", "FLRegId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.FLRegEvents", new[] { "FLRegId", "EventRegId" });
            RenameColumn(table: "dbo.FLRegEvents", name: "EventRegId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.FLRegEvents", name: "FLRegId", newName: "EventRegId");
            RenameColumn(table: "dbo.FLRegEvents", name: "__mig_tmp__0", newName: "FLRegId");
            CreateIndex("dbo.FLRegEvents", "EventRegId");
            CreateIndex("dbo.FLRegEvents", "FLRegId");
        }
    }
}

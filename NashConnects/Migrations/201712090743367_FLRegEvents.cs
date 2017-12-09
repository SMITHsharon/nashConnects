namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FLRegEvents : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FreelancerEvents", newName: "FLRegEvent");
            RenameColumn(table: "dbo.FLRegEvent", name: "Freelancer_Id", newName: "EventRefId");
            RenameColumn(table: "dbo.FLRegEvent", name: "Event_EventId", newName: "FLRefId");
            RenameIndex(table: "dbo.FLRegEvent", name: "IX_Event_EventId", newName: "IX_FLRefId");
            RenameIndex(table: "dbo.FLRegEvent", name: "IX_Freelancer_Id", newName: "IX_EventRefId");
            DropPrimaryKey("dbo.FLRegEvent");
            AddPrimaryKey("dbo.FLRegEvent", new[] { "FLRefId", "EventRefId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FLRegEvent");
            AddPrimaryKey("dbo.FLRegEvent", new[] { "Freelancer_Id", "Event_EventId" });
            RenameIndex(table: "dbo.FLRegEvent", name: "IX_EventRefId", newName: "IX_Freelancer_Id");
            RenameIndex(table: "dbo.FLRegEvent", name: "IX_FLRefId", newName: "IX_Event_EventId");
            RenameColumn(table: "dbo.FLRegEvent", name: "FLRefId", newName: "Event_EventId");
            RenameColumn(table: "dbo.FLRegEvent", name: "EventRefId", newName: "Freelancer_Id");
            RenameTable(name: "dbo.FLRegEvent", newName: "FreelancerEvents");
        }
    }
}

namespace NashConnects.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chgCalendarLinkField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "CalendarLink", c => c.String(maxLength: 75));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CalendarLink", c => c.String(maxLength: 50));
        }
    }
}

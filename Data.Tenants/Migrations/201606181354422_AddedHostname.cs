namespace Data.Tenants.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHostname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HostName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HostName");
        }
    }
}

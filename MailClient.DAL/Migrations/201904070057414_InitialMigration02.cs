namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration02 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MailAddress", "Value", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.MailAddress", new[] { "Value" });
        }
    }
}

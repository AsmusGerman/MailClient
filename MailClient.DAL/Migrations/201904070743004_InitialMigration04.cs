namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailMessage", "Subject", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailMessage", "Subject", c => c.String(nullable: false, maxLength: 100));
        }
    }
}

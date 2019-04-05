namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailAccount", "Password", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailAccount", "Password", c => c.String(nullable: false, maxLength: 256));
        }
    }
}

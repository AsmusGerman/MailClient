namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailAccount", "Deleted", c => c.Boolean());
            AlterColumn("dbo.MailAccount", "DownloadsFolder", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailAccount", "DownloadsFolder", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.MailAccount", "Deleted", c => c.Boolean(nullable: false));
        }
    }
}

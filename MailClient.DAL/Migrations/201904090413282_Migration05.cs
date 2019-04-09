namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MailMessage", "DateSent", c => c.DateTime());
            AlterColumn("dbo.Attachment", "FileName", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attachment", "FileName", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.MailMessage", "DateSent");
        }
    }
}

namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration05 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailMessage", "Body", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailMessage", "Body", c => c.String(maxLength: 4000));
        }
    }
}

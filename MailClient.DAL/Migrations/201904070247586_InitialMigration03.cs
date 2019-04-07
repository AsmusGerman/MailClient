namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MailMessage", "From_Id", "dbo.MailAddress");
            AddColumn("dbo.MailMessage", "MailAddress_Id", c => c.Int());
            CreateIndex("dbo.MailMessage", "MailAddress_Id");
            AddForeignKey("dbo.MailMessage", "MailAddress_Id", "dbo.MailAddress", "MailAddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MailMessage", "MailAddress_Id", "dbo.MailAddress");
            DropIndex("dbo.MailMessage", new[] { "MailAddress_Id" });
            DropColumn("dbo.MailMessage", "MailAddress_Id");
            AddForeignKey("dbo.MailMessage", "From_Id", "dbo.MailAddress", "MailAddressId");
        }
    }
}

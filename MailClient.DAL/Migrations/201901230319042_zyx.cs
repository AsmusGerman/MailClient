namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zyx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailAccount",
                c => new
                    {
                        MailAccountId = c.Int(nullable: false, identity: true),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Password = c.String(nullable: false, maxLength: 256),
                        MailAdressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MailAccountId)
                .ForeignKey("dbo.MailAddress", t => t.MailAdressId)
                .Index(t => t.MailAdressId);
            
            CreateTable(
                "dbo.MailAddress",
                c => new
                    {
                        MailAddressId = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 4000),
                    })
                .PrimaryKey(t => t.MailAddressId)
                .Index(t => t.Value, unique: true);
            
            CreateTable(
                "dbo.MailMessage",
                c => new
                    {
                        MailMessageId = c.Int(nullable: false, identity: true),
                        ExternalId = c.String(nullable: false, maxLength: 4000),
                        Body = c.String(maxLength: 4000),
                        Subject = c.String(nullable: false, maxLength: 100),
                        From_Id = c.Int(nullable: false),
                        MailAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MailMessageId)
                .ForeignKey("dbo.MailAddress", t => t.From_Id)
                .ForeignKey("dbo.MailAccount", t => t.MailAccount_Id)
                .Index(t => t.ExternalId, unique: true)
                .Index(t => t.From_Id)
                .Index(t => t.MailAccount_Id);
            
            CreateTable(
                "dbo.MailMessageToMailAddress",
                c => new
                    {
                        MailMessageId = c.Int(nullable: false),
                        MailAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MailMessageId, t.MailAddressId })
                .ForeignKey("dbo.MailMessage", t => t.MailMessageId, cascadeDelete: true)
                .ForeignKey("dbo.MailAddress", t => t.MailAddressId, cascadeDelete: true)
                .Index(t => t.MailMessageId)
                .Index(t => t.MailAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MailAccount", "MailAdressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageToMailAddress", "MailAddressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageToMailAddress", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.MailMessage", "MailAccount_Id", "dbo.MailAccount");
            DropForeignKey("dbo.MailMessage", "From_Id", "dbo.MailAddress");
            DropIndex("dbo.MailMessageToMailAddress", new[] { "MailAddressId" });
            DropIndex("dbo.MailMessageToMailAddress", new[] { "MailMessageId" });
            DropIndex("dbo.MailMessage", new[] { "MailAccount_Id" });
            DropIndex("dbo.MailMessage", new[] { "From_Id" });
            DropIndex("dbo.MailMessage", new[] { "ExternalId" });
            DropIndex("dbo.MailAddress", new[] { "Value" });
            DropIndex("dbo.MailAccount", new[] { "MailAdressId" });
            DropTable("dbo.MailMessageToMailAddress");
            DropTable("dbo.MailMessage");
            DropTable("dbo.MailAddress");
            DropTable("dbo.MailAccount");
        }
    }
}

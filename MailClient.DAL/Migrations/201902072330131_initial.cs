namespace MailClient.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Attachment",
                c => new
                    {
                        AttachmentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 256),
                        Message_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.MailMessage", t => t.Message_Id, cascadeDelete: true)
                .Index(t => t.Message_Id);
            
            CreateTable(
                "dbo.MailAccount",
                c => new
                    {
                        MailAccountId = c.Int(nullable: false, identity: true),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Password = c.String(nullable: false, maxLength: 256),
                        Deleted = c.Boolean(nullable: false),
                        DownloadsFolder = c.String(nullable: false, maxLength: 256),
                        MailAdressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MailAccountId)
                .ForeignKey("dbo.MailAddress", t => t.MailAdressId)
                .Index(t => t.MailAdressId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MailMessageBccMailAddress",
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
            
            CreateTable(
                "dbo.MailMessageCcMailAddress",
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
            
            CreateTable(
                "dbo.MailMessageReplyToMailAddress",
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
            
            CreateTable(
                "dbo.MailMessageTag",
                c => new
                    {
                        MailMessageId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MailMessageId, t.TagId })
                .ForeignKey("dbo.MailMessage", t => t.MailMessageId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.MailMessageId)
                .Index(t => t.TagId);
            
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
            DropForeignKey("dbo.MailMessageToMailAddress", "MailAddressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageToMailAddress", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.MailMessageTag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.MailMessageTag", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.MailMessageReplyToMailAddress", "MailAddressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageReplyToMailAddress", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.MailMessage", "MailAccount_Id", "dbo.MailAccount");
            DropForeignKey("dbo.MailAccount", "MailAdressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessage", "From_Id", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageCcMailAddress", "MailAddressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageCcMailAddress", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.MailMessageBccMailAddress", "MailAddressId", "dbo.MailAddress");
            DropForeignKey("dbo.MailMessageBccMailAddress", "MailMessageId", "dbo.MailMessage");
            DropForeignKey("dbo.Attachment", "Message_Id", "dbo.MailMessage");
            DropIndex("dbo.MailMessageToMailAddress", new[] { "MailAddressId" });
            DropIndex("dbo.MailMessageToMailAddress", new[] { "MailMessageId" });
            DropIndex("dbo.MailMessageTag", new[] { "TagId" });
            DropIndex("dbo.MailMessageTag", new[] { "MailMessageId" });
            DropIndex("dbo.MailMessageReplyToMailAddress", new[] { "MailAddressId" });
            DropIndex("dbo.MailMessageReplyToMailAddress", new[] { "MailMessageId" });
            DropIndex("dbo.MailMessageCcMailAddress", new[] { "MailAddressId" });
            DropIndex("dbo.MailMessageCcMailAddress", new[] { "MailMessageId" });
            DropIndex("dbo.MailMessageBccMailAddress", new[] { "MailAddressId" });
            DropIndex("dbo.MailMessageBccMailAddress", new[] { "MailMessageId" });
            DropIndex("dbo.MailAccount", new[] { "MailAdressId" });
            DropIndex("dbo.Attachment", new[] { "Message_Id" });
            DropIndex("dbo.MailMessage", new[] { "MailAccount_Id" });
            DropIndex("dbo.MailMessage", new[] { "From_Id" });
            DropIndex("dbo.MailMessage", new[] { "ExternalId" });
            DropIndex("dbo.MailAddress", new[] { "Value" });
            DropTable("dbo.MailMessageToMailAddress");
            DropTable("dbo.MailMessageTag");
            DropTable("dbo.MailMessageReplyToMailAddress");
            DropTable("dbo.MailMessageCcMailAddress");
            DropTable("dbo.MailMessageBccMailAddress");
            DropTable("dbo.Tags");
            DropTable("dbo.MailAccount");
            DropTable("dbo.Attachment");
            DropTable("dbo.MailMessage");
            DropTable("dbo.MailAddress");
        }
    }
}

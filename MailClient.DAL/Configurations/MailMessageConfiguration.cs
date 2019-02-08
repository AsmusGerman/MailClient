using MailClient.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MailClient.DAL
{
    internal class MailMessageConfiguration : EntityTypeConfiguration<MailMessage>
    {
        internal MailMessageConfiguration()
        {
            ToTable("MailMessage");

            HasKey(bMailMessage => bMailMessage.Id)
                .Property(x => x.Id)
                .HasColumnName("MailMessageId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(bMailMessage => bMailMessage.Body)
                .HasColumnType("nvarchar")
                .IsVariableLength()
                .IsOptional();

            Property(bMailMessage => bMailMessage.Subject)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(bMailMessage => bMailMessage.ExternalId)
                .HasColumnType("nvarchar")
                .IsVariableLength()
                .IsRequired();

            HasIndex(bMailMessage => bMailMessage.ExternalId).IsUnique(true);

            HasRequired(bMailMessage => bMailMessage.From)
                .WithMany(bMailAddress => bMailAddress.FromMessages)
                .WillCascadeOnDelete(false);

            HasMany(bMailMessage => bMailMessage.To)
                .WithMany(bMailAddress => bMailAddress.ToMessages)
                .Map(bConfiguration =>
            {
                bConfiguration.MapLeftKey("MailMessageId");
                bConfiguration.MapRightKey("MailAddressId");
                bConfiguration.ToTable("MailMessageToMailAddress");
            });

            HasMany(bMailMessage => bMailMessage.Bcc)
                .WithMany(bMailAddress => bMailAddress.BccMessages)
                .Map(bConfiguration =>
            {
                bConfiguration.MapLeftKey("MailMessageId");
                bConfiguration.MapRightKey("MailAddressId");
                bConfiguration.ToTable("MailMessageBccMailAddress");
            });

            HasMany(bMailMessage => bMailMessage.Cc)
                .WithMany(bMailAddress => bMailAddress.CcMessages)
                .Map(bConfiguration =>
                {
                    bConfiguration.MapLeftKey("MailMessageId");
                    bConfiguration.MapRightKey("MailAddressId");
                    bConfiguration.ToTable("MailMessageCcMailAddress");
                });

            HasMany(bMailMessage => bMailMessage.ReplyTo)
                .WithMany(bMailAddress => bMailAddress.ReplyToMessages)
                .Map(bConfiguration =>
                {
                    bConfiguration.MapLeftKey("MailMessageId");
                    bConfiguration.MapRightKey("MailAddressId");
                    bConfiguration.ToTable("MailMessageReplyToMailAddress");
                });

            HasMany(bMailMessage => bMailMessage.Tags)
                .WithMany(bTags => bTags.Messages)
                .Map(bConfiguration =>
                {
                    bConfiguration.MapLeftKey("MailMessageId");
                    bConfiguration.MapRightKey("TagId");
                    bConfiguration.ToTable("MailMessageTag");
                });

            HasMany(bMailMessage => bMailMessage.Attachments)
                .WithRequired(bAttachment => bAttachment.Message);
        }
    }
}

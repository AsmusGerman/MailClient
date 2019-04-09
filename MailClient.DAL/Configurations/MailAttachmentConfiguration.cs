using MailClient.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MailClient.DAL
{
	internal class MailAttachmentConfiguration : EntityTypeConfiguration<Attachment>
	{
		internal MailAttachmentConfiguration()
		{
			ToTable("Attachment");

			HasKey(bAttachment => bAttachment.Id);

			Property(bAttachment => bAttachment.Id)
				.HasColumnName("AttachmentId")
				.HasColumnType("int")
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
				.IsRequired();

			Property(bAttachment => bAttachment.FileName)
                .HasColumnType("nvarchar")
				.IsRequired();
        }
    }
}

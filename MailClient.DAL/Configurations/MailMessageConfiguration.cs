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

			HasMany(bMailMessage => bMailMessage.To).WithMany(bMailAddress => bMailAddress.ToMessages).Map(bConfiguration =>
			{
				bConfiguration.MapLeftKey("MailMessageId");
				bConfiguration.MapRightKey("MailAddressId");
				bConfiguration.ToTable("MailMessageToMailAddress");
			});
		}
	}
}

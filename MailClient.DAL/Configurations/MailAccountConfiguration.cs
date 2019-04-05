using MailClient.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MailClient.DAL
{
	internal class MailAccountConfiguration : EntityTypeConfiguration<MailAccount>
	{
		internal MailAccountConfiguration()
		{
			ToTable("MailAccount");

			HasKey(bMailAccount => bMailAccount.Id);

			Property(x => x.Id)
				.HasColumnName("MailAccountId")
				.HasColumnType("int")
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
				.IsRequired();

			Property(bMailAccount => bMailAccount.Alias)
				.HasColumnType("nvarchar")
				.HasMaxLength(256)
				.IsRequired();

			Property(bMailAccount => bMailAccount.Password)
				.HasColumnType("nvarchar")
				.IsRequired();

            Property(bMailAccount => bMailAccount.DownloadsFolder)
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsOptional();


            Property(bMailAccount => bMailAccount.Deleted)
                .IsOptional();

			HasRequired(bMailAccount => bMailAccount.MailAddress)
				.WithOptional(bMailAddress => bMailAddress.MailAccount).Map(v=> v.MapKey("MailAdressId"))
				.WillCascadeOnDelete(false);
		}
	}
}

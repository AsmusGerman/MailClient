using MailClient.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace MailClient.DAL
{
	internal class MailAddressConfiguration : EntityTypeConfiguration<MailAddress>
	{
		internal MailAddressConfiguration()
		{
			ToTable("MailAddress");

			HasKey(bMailAddress => bMailAddress.Id)
				.Property(x => x.Id)
				.HasColumnName("MailAddressId")
				.HasColumnType("int")
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(bMailAddress => bMailAddress.Value)
				.HasColumnType("nvarchar")
				.IsVariableLength()
				.IsRequired();

			HasIndex(bMailAddress => bMailAddress.Value).IsUnique(true);
		}
	}
}

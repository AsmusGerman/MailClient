namespace MailClient.DAL.Migrations
{
	using Shared;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<MailClient.DAL.Context>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(MailClient.DAL.Context context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			//se crea la direccion de correo
			//MailAddress mMailAddress = new MailAddress
			//{
			//	Value = "g@g.com"
			//};

			////se crea la cuenta de correo
			//MailAccount mMailAccount = new MailAccount
			//{
			//	Alias = "g",
			//	MailAddress = mMailAddress,
			//	Password = "1234",
			//	//Deleted = false,
			//	//DownloadsFolder = null
			//};

			//context.Set<MailAccount>().Add(mMailAccount);
			//context.SaveChanges();
		}
	}
}

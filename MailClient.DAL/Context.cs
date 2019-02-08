using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MailClient.DAL
{
	public class Context : DbContext
	{
		public Context() : base("name=DefaultConnection")
		{
		}

		protected override void OnModelCreating(DbModelBuilder pModelBuilder)
		{
			pModelBuilder.Configurations.Add(new MailAddressConfiguration());
			pModelBuilder.Configurations.Add(new MailAccountConfiguration());
			pModelBuilder.Configurations.Add(new MailMessageConfiguration());
		}
	}
}

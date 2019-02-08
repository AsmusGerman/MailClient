using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.DAL
{
	public class MCDAL
	{
		private static MCDAL iInstance;
		private IUnitOfWork iUnitOfWork;

		private IDictionary<string, IRepository> iRepositories;

		private MCDAL()
		{
			this.iUnitOfWork = new UnitOfWork();

			this.iRepositories = new Dictionary<string, IRepository>();
			this.iRepositories.Add(nameof(MailAccount), this.iUnitOfWork.Repository<MailAccount>());
			this.iRepositories.Add(nameof(MailAddress), this.iUnitOfWork.Repository<MailAddress>());
		}

		public IRepository MailAccountRepository
		{
			get { return this.iRepositories[nameof(MailAccount)]; }
		}

		public IRepository MailAddressRepository
		{
			get { return this.iRepositories[nameof(MailAddress)]; }
		}

		public void Save() {
			this.iUnitOfWork.Save();
		}

		public static MCDAL Instance
		{
			get
			{
				if (MCDAL.iInstance == default(MCDAL))
				{
					MCDAL.iInstance = new MCDAL();
				}
				return MCDAL.iInstance;
			}
		}
	}
}

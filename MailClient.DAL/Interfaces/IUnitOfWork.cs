using MailClient.DAL.Interfaces;
using MailClient.Shared;
using System;

namespace MailClient.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository Repository(IDataSet pDataSet);
		void Save();
	}
}




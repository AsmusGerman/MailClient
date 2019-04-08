using MailClient.Shared;
using System;

namespace MailClient.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> GetRepository<T>();
		void Save();
	}
}




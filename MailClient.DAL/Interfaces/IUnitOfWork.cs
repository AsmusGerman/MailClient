using MailClient.Shared;
using System;

namespace MailClient.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository Repository<T>() where T : BaseEntity;
		void Save();
	}
}




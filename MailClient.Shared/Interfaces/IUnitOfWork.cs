using System;

namespace MailClient.Shared
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository Repository<T>() where T : BaseEntity;
		void Save();
	}
}




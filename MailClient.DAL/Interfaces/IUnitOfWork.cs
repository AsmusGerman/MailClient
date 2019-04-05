using MailClient.DAL.Interfaces;
using MailClient.Shared;
using System;
using System.Data.Entity;

namespace MailClient.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> GetRepository<T>();
		void Save();
	}
}




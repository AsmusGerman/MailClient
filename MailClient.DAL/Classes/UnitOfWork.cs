using MailClient.DAL.Exceptions;
using MailClient.DAL.Interfaces;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MailClient.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly Context iContext = new Context();
		private readonly IDictionary<Type, IRepository> iRepositories;
		public UnitOfWork()
		{
			this.iRepositories = new Dictionary<Type, IRepository>();
			this.iRepositories.Add(typeof(MailAccount), new EntityFrameworkRespository<MailAccount>(this.iContext.Set<MailAccount>()));
			this.iRepositories.Add(typeof(MailAddress), new EntityFrameworkRespository<MailAddress>(this.iContext.Set<MailAddress>()));
		}

		public IRepository<T> GetRepository<T>()
		{
			return (IRepository<T>)this.iRepositories[typeof(T)];
		}
		/// <summary>
		/// metodo encargado de guardar los cambios del contexto de la aplicacion
		/// </summary>
		public void Save()
		{
			try
			{
				this.iContext.SaveChanges();
			}
			catch (Exception bException)
			{
				throw new SavingContextException(Resources.Exceptions.SavingContextException, bException);
			}

		}

		private bool iDisposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.iDisposed)
			{
				if (disposing)
				{
					this.iContext.Dispose();
				}
			}
			this.iDisposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}

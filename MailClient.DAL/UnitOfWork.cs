using MailClient.Shared;
using System;

namespace MailClient.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private Context iContext;
		public UnitOfWork()
		{
			this.iContext = new Context();
		}

		public IRepository Repository<T>() where T: BaseEntity {
			return new Repository(this.iContext.Set<T>());
}

		public void Save()
		{
			this.iContext.SaveChanges();
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

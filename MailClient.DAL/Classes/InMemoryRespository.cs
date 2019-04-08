using MailClient.DAL.Exceptions;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.DAL
{
	public class InMemoryRespository<T> : IRepository<T>
	{
		private readonly IQueryable<T> iSource;

		public InMemoryRespository(IEnumerable<T> pSource)
		{
			this.iSource = pSource.AsQueryable();
		}

		public void Create(T pEntity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> List(ISelector<T, bool> pSelector)
		{
			try
			{
				return pSelector != null ? this.iSource.Where(pSelector.Criteria).ToList() : this.iSource.ToList();
			}
			catch (Exception bException)
			{
				throw new RepositoryOperationException(Resources.Exceptions.GetAllEntitiesException, bException);
			}
		}

		public void Remove(T pEntity)
		{
			throw new NotImplementedException();
		}

		public T Single(ISelector<T, bool> pSelector)
		{
			try
			{
				return this.iSource.Single(pSelector.Criteria);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

using MailClient.DAL.Exceptions;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using MailClient.Shared.Selectors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MailClient.DAL
{
	public class Repository : IRepository
	{
		private readonly DbSet iDbSet;

		public Repository(DbSet pDbSet)
		{
			iDbSet = pDbSet;
		}

		public T Single<T>(ISelector <T> pSelector) where T : BaseEntity
		{
			return iDbSet.Cast<T>().SingleOrDefault(pSelector.Criteria);
		}

		public List<T> List<T>(ISelector <T> pSelector) where T : BaseEntity
		{
			DbSet<T> mDbSet = iDbSet.Cast<T>();
			return pSelector != null ? mDbSet.Where(pSelector.Criteria).ToList() : mDbSet.ToList();
		}

		public T Create<T>(T pEntity) where T : BaseEntity
		{
			iDbSet.Cast<T>().Add(pEntity);
			return pEntity;
		}

		public void Create<T>(List<T> pEntities) where T : BaseEntity
		{
			iDbSet.Cast<T>().AddRange(pEntities);
		}

		public void Remove<T>(T pEntity) where T : BaseEntity
		{
			iDbSet.Cast<T>().Remove(pEntity);
		}
	}
}

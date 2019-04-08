using MailClient.DAL.Exceptions;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MailClient.DAL
{
	public class EntityFrameworkRespository<T> : IRepository<T> where T : BaseEntity
	{
		private readonly IDbSet<T> iDbSet;

		public EntityFrameworkRespository(IDbSet<T> pDbSet)
		{
			iDbSet = pDbSet;
		}

		/// <summary>
		/// obtiene de la bbdd, una instancia de la clase T
		/// </summary>
		/// <param name="pSelector">selector (criterio de busqueda) por el cual se filtra la entidad persistida</param>
		/// <returns>instancia de T</returns>
		public T Single(ISelector<T, bool> pSelector)
		{
			try
			{
				return this.iDbSet.SingleOrDefault(pSelector.Criteria);
			}
			catch (Exception bException)
			{
				throw new RepositoryOperationException(Resources.Exceptions.GetSingleEntityException, bException);
			}
		}

		/// <summary>
		/// obtiene de la bbdd, una lista con todas las instancias de la clase T
		/// </summary>
		/// <param name="pSelector">selector (criterio de busqueda) por el cual se filtran las entidades persistidas</param>
		/// <returns></returns>
		public IEnumerable<T> List(ISelector<T, bool> pSelector)
		{
			try
			{
				return pSelector != null ? this.iDbSet.Where(pSelector.Criteria).ToList() : this.iDbSet.ToList();
			}
			catch (Exception bException)
			{
				throw new RepositoryOperationException(Resources.Exceptions.GetAllEntitiesException, bException);
			}
		}

		/// <summary>
		/// persiste una entidad
		/// </summary>
		public void Create(T pEntity)
		{
			try
			{
				this.iDbSet.Add(pEntity);
			}
			catch (Exception bException)
			{
				throw new RepositoryOperationException(Resources.Exceptions.CreateEntitiesException, bException);
			}
		}

		/// <summary>
		/// quita un elemento de la bbdd
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="pEntity"></param>
		public void Remove(T pEntity)
		{
			try
			{
				this.iDbSet.Remove(pEntity);

			}
			catch (Exception bException)
			{

				throw new RepositoryOperationException(Resources.Exceptions.RemoveEntityException, bException);
			}
		}
	}
}

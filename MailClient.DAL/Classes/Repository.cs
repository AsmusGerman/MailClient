using MailClient.DAL.Exceptions;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        /// <summary>
        /// resuelve el casteo del dbset
        /// </summary>
        /// <typeparam name="T">definición del tipo de entidad</typeparam>
        /// <returns>DbSet casteado</returns>
        private DbSet<T> CastedSet<T>() where T : BaseEntity
        {
            try
            {
                return iDbSet.Cast<T>();
            }
            catch (InvalidCastException bException)
            {
                throw new CastOperationException(Resources.Exceptions.CastOperationException, bException);
            }
        }

        /// <summary>
        /// obtiene de la bbdd, una instancia de la clase T
        /// </summary>
        /// <param name="pSelector">selector (criterio de busqueda) por el cual se filtra la entidad persistida</param>
        /// <returns>instancia de T</returns>
		public T Single<T>(ISelector<T, bool> pSelector) where T : BaseEntity
		{
            try
            {
                return this.CastedSet<T>().SingleOrDefault(pSelector.Criteria);
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
		public IEnumerable<T> List<T>(ISelector<T, bool> pSelector) where T : BaseEntity
		{
            try
            {
                DbSet<T> mDbSet = this.CastedSet<T>();
                return pSelector != null ? mDbSet.Where(pSelector.Criteria).ToList() : mDbSet.ToList();
            }
            catch (Exception bException)
            {
                throw new RepositoryOperationException(Resources.Exceptions.GetAllEntitiesException, bException);
            }
		}

        /// <summary>
        /// persiste una entidad
        /// </summary>
        /// <returns>la entidad persistida</returns>
		public T Create<T>(T pEntity) where T : BaseEntity
		{
            try
            {
                return this.CastedSet<T>().Add(pEntity);
            }
            catch (Exception bException)
            {
                throw new RepositoryOperationException(Resources.Exceptions.CreateEntityException, bException);
            }
        }

        /// <summary>
        /// persiste una colección de entidades
        /// </summary>
		public void Create<T>(IEnumerable<T> pEntities) where T : BaseEntity
		{
            try
            {
                this.CastedSet<T>().AddRange(pEntities);
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
		public void Remove<T>(T pEntity) where T : BaseEntity
		{
            try
            {
                this.CastedSet<T>().Remove(pEntity);

            }
            catch (Exception bException)
            {

                throw new RepositoryOperationException(Resources.Exceptions.RemoveEntityException, bException);
            }
		}
	}
}

using MailClient.DAL.Exceptions;
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

        /// <summary>
        /// obtiene un repositorio que se encarga de las entidades de tipo T
        /// </summary>
        public IRepository Repository<T>() where T : BaseEntity
        {
            return new Repository(this.iContext.Set<T>());
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

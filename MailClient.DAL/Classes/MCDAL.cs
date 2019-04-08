using MailClient.DAL.Exceptions;
using MailClient.Shared;
using System;
using System.Collections.Generic;

namespace MailClient.DAL
{
	public class MCDAL
	{
		private static MCDAL iInstance;
		private IUnitOfWork iUnitOfWork;
		private MailServiceCollection iMailServiceCollection;
        private IDictionary<string, IRepository> iRepositories;

		/// <summary>
		/// constructor de la clase donde se configuran los elementos necesarios para operar con la bbdd
		/// </summary>
		private MCDAL()
		{
			//instanciación del UoW para manejar el contexto de la aplicación
			this.iUnitOfWork = new UnitOfWork();
			this.iMailServiceCollection = Serializer.ReadFromFile<MailServiceCollection>("Configuration/Services.xml");
            this.iRepositories = new Dictionary<string, IRepository>();
            this.iRepositories.Add(nameof(MailAccount), this.iUnitOfWork.GetRepository<MailAccount>());
            this.iRepositories.Add(nameof(MailAddress), this.iUnitOfWork.GetRepository<MailAddress>());
            this.iRepositories.Add(nameof(MailService), new InMemoryRespository<MailService>(this.iMailServiceCollection.MailServices));
        }

        public IRepository<T> GetRepository<T>()
        {
            string mRepositoryName = typeof(T).Name;
            if (!this.iRepositories.ContainsKey(mRepositoryName))
                throw new Exception();

            return (IRepository<T>) this.iRepositories[mRepositoryName];
        }

		public void Save()
		{
			try
			{
				this.iUnitOfWork.Save();
			}
			catch (Exception bException)
			{
				throw new DataAccessLayerException(Resources.Exceptions.SavingContextException, bException);
			}
		}

		/// <summary>
		///  devuelve la única instancia de la clase
		/// </summary>
		public static MCDAL Instance
		{
			get
			{
				if (MCDAL.iInstance == default(MCDAL))
				{
					MCDAL.iInstance = new MCDAL();
				}
				return MCDAL.iInstance;
			}
		}
	}
}

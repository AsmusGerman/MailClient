using MailClient.DAL.Exceptions;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.DAL
{
	public class MCDAL
	{
		private static MCDAL iInstance;
		private IUnitOfWork iUnitOfWork;

		private IDictionary<string, IRepository> iRepositories;

        /// <summary>
        /// constructor de la clase donde se configuran los elementos necesarios para operar con la bbdd
        /// </summary>
		private MCDAL()
		{
            //instanciación del UoW para manejar el contexto de la aplicación
			this.iUnitOfWork = new UnitOfWork();

            //se registran los repositorios
			this.iRepositories = new Dictionary<string, IRepository>();
			this.iRepositories.Add(nameof(MailAccount), this.iUnitOfWork.Repository<MailAccount>());
			this.iRepositories.Add(nameof(MailAddress), this.iUnitOfWork.Repository<MailAddress>());
			this.iRepositories.Add(nameof(MailService), this.iUnitOfWork.Repository<MailService>());
		}

		/// <summary>
		/// repositorio de las cuentas de correo
		/// </summary>
		public IRepository MailAccountRepository
		{
			get { return this.iRepositories[nameof(MailAccount)]; }
		}

        /// <summary>
        /// repositorio de las direcciones de correo
        /// </summary>
		public IRepository MailAddressRepository
		{
			get { return this.iRepositories[nameof(MailAddress)]; }
		}

		/// <summary>
		/// repositorio de los servicios de correo
		/// </summary>
		public IRepository MailServiceRepository
		{
			get { return this.iRepositories[nameof(MailService)]; }
		}

		public void Save() {
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

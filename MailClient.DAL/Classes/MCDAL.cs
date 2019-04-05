using MailClient.DAL.Exceptions;
using MailClient.DAL.Interfaces;
using MailClient.Shared;
using System;

namespace MailClient.DAL
{
	public class MCDAL
	{
		private static MCDAL iInstance;
		private IUnitOfWork iUnitOfWork;
		private MailServiceCollection iMailServiceCollection;

		/// <summary>
		/// constructor de la clase donde se configuran los elementos necesarios para operar con la bbdd
		/// </summary>
		private MCDAL()
		{
			//instanciación del UoW para manejar el contexto de la aplicación
			this.iUnitOfWork = new UnitOfWork();
			this.iMailServiceCollection = Serializer.ReadFromFile<MailServiceCollection>("Configuration/Services.xml");
		}

		/// <summary>
		/// repositorio de las cuentas de correo
		/// </summary>
		public IRepository<MailAccount> MailAccountRepository
		{
			get
			{
				return this.iUnitOfWork.GetRepository<MailAccount>();
			}
		}

		/// <summary>
		/// repositorio de las direcciones de correo
		/// </summary>
		public IRepository<MailAddress> MailAddressRepository
		{
			get
			{
				return this.iUnitOfWork.GetRepository<MailAddress>();
			}
		}

		/// <summary>
		/// repositorio de los servicios de correo
		/// </summary>
		public IRepository<MailService> MailServiceRepository
		{
			get
			{
				return new InMemoryRespository<MailService>(this.iMailServiceCollection.MailServices);
			}
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

using MailClient.BLL.Selectors;
using MailClient.Core;
using MailClient.DAL;
using MailClient.DAL.Interfaces;
using MailClient.Shared;
using System;
using System.Linq;

namespace MailClient.BLL
{
	public class AuthenticationService : IAuthenticationService
	{
		private IEncryptor Encryptor { get; set; }

		public AuthenticationService()
		{
			this.Encryptor = new DPEntryptor();
		}

		/// <summary>
		/// obtiene la cuenta correspondiente al alias y a la contraseña
		/// </summary>
		public MailAccount LoginByAlias(string pAlias, string pPassword)
		{
			try
			{
				//se obtiene el repositorio de las cuentas de correo
				IRepository<MailAccount> mRespository = MCDAL.Instance.MailAccountRepository;
				//Se obtiene la existencia de la cuenta que corresponde a los argumentos
				MailAccount mMailAccount  = mRespository.Single(MailAccountSelector.ByAlias(pAlias));
				if (this.Encryptor.Decrypt(mMailAccount.Password) != pPassword)
					throw new LoginException();

				return mMailAccount;
			}
			catch (Exception bException)
			{
				throw new LoginException(Resources.Exceptions.LoginException, bException);
			}
		}

		/// <summary>
		/// obtiene la cuenta correspondiente al correo y a la contraseña
		/// </summary>
		public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
		{
			try
			{
				//se obtiene el repositorio de las cuentas de correo
				IRepository<MailAccount> mRespository = MCDAL.Instance.MailAccountRepository;
				//Se obtiene la existencia de la cuenta que corresponde a los argumentos
				MailAccount mMailAccount = mRespository.Single(MailAccountSelector.ByMailAddress(pMailAddress));
				if (this.Encryptor.Decrypt(mMailAccount.Password) != pPassword)
					throw new LoginException();

				return mMailAccount;
			}
			catch (Exception bException)
			{
				throw new LoginException(Resources.Exceptions.LoginException, bException);
			}
		}

		/// <summary>
		/// registra una nueva cuenta de correo
		/// </summary>
		public void Register(string pAlias, string pMailAddress, string pPassword)
		{
			try
			{
				//se obtiene el repositorio de las cuentas de correo
				IRepository<MailAccount> mRespository = MCDAL.Instance.MailAccountRepository;

				//se encripta la contraseña
				string mPassword = this.Encryptor.Encrypt(pPassword);

				//se crea la direccion de correo
				MailAddress mMailAddress = new MailAddress
				{
					Value = pMailAddress
				};

				//se crea la cuenta de correo
				MailAccount mMailAccount = new MailAccount
				{
					Alias = pAlias,
					MailAddress = mMailAddress,
					Password = mPassword
				};

				//se agrega la nueva cuenta al repositorio
				mRespository.Create(mMailAccount);
				MCDAL.Instance.Save();
			}
			catch (Exception bException)
			{
				throw new RegisterException(Resources.Exceptions.RegisterException, bException);
			}

		}
	}
}

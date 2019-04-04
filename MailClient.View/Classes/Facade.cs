using MailClient.Core;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace MailClient.View
{
	public class Facade
	{
		private static Facade iInstance;
		private MCCore iMCCore;

		private Facade()
		{
			this.iMCCore = MCCore.Instance;
		}

		/// <summary>
		/// Obtiene la única instancia de la fachada
		/// </summary>
		public static Facade Instance
		{
			get
			{
				if (Facade.iInstance == null)
				{
					Facade.iInstance = new Facade();
				}
				return Facade.iInstance;
			}
		}

		/// <summary>
		/// Llama al core para ingresar a la aplicación mediante el alias de la cuenta
		/// </summary>
		public MailAccount LoginByAlias(string pAlias, string pPassword)
		{
			try
			{
				return this.iMCCore.LoginByAlias(pAlias, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new LoginException(Resources.Exceptions.LoginWellKnownException, bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new LoginException(Resources.Exceptions.LoginUnknownErrorException,
					bException);
			}
			catch (Exception bException)
			{
				throw new LoginException(Resources.Exceptions.LoginException, bException);
			}

		}

		/// <summary>
		/// Llama al core para ingresar a la aplicación mediante el correo electrónico de la cuenta
		/// </summary>
		public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
		{
			try
			{
				return this.iMCCore.LoginByMailAddress(pMailAddress, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new LoginException(Resources.Exceptions.LoginWellKnownException, bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new LoginException(Resources.Exceptions.LoginUnknownErrorException,
					bException);
			}
			catch (Exception bException)
			{
				throw new LoginException(Resources.Exceptions.LoginException, bException);
			}
		}

		/// <summary>
		/// Pide al core que registre una cuenta con los valores parametrizados
		/// </summary>
		public void Register(string pAlias, string pMail, string pPassword)
		{
			try
			{
				this.iMCCore.Register(pAlias, pMail, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new RegisterException(Resources.Exceptions.RegisterWellKnownException, bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new RegisterException(Resources.Exceptions.RegisterUnkownErrorException, bException);
			}
			catch (Exception bException)
			{
				throw new RegisterException(Resources.Exceptions.RegisterException, bException);
			}
		}

		/// <summary>
		/// Pide al core que actualice la casilla de correos de la cuenta teniendo en cuenta la cantidad de mensajes que debe descargar
		/// </summary>
		public IEnumerable<MailMessage> UpdateInbox(MailAccount pUserAccount, int pWindow = 0)
		{
			try
			{
				return this.iMCCore.UpdateInbox(pUserAccount, pWindow);
			}
			catch (WellKnownException bException)
			{
				throw new UpdateInboxException(Resources.Exceptions.UpdateInboxWellKownException, bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new UpdateInboxException(Resources.Exceptions.UpdateInboxUnknownErrorException, bException);
			}
			catch (Exception bException)
			{
				throw new UpdateInboxException(Resources.Exceptions.UpdateInboxException, bException);
			}
		}
	}
}

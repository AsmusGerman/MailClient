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

		private Facade()
		{
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
				return MCCore.Instance.LoginByAlias(pAlias, pPassword);
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.LoginException, bException);
			}

		}

		/// <summary>
		/// Llama al core para ingresar a la aplicación mediante el correo electrónico de la cuenta
		/// </summary>
		public MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
		{
			try
			{
				return MCCore.Instance.LoginByMailAddress(pMailAddress, pPassword);
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.LoginException, bException);
			}
		}

		/// <summary>
		/// Pide al core que registre una cuenta con los valores parametrizados
		/// </summary>
		public void Register(string pAlias, string pMail, string pPassword)
		{
			try
			{
				MCCore.Instance.Register(pAlias, pMail, pPassword);
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.RegisterException, bException);
			}
		}

		/// <summary>
		/// Pide al core que actualice la casilla de correos de la cuenta teniendo en cuenta la cantidad de mensajes que debe descargar
		/// </summary>
		public IEnumerable<MailMessage> UpdateInbox(MailAccount pUserAccount, int pWindow = 0)
		{
			try
			{
				return MCCore.Instance.UpdateInbox(pUserAccount, pWindow);
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.UpdateInboxException, bException);
			}
		}

		/// <summary>
		/// Pide al core que envíe un correo
		/// </summary>
		public void Send(MailAccount pUserAccount, MailMessage pMailMessage)
		{
			try
			{
				MCCore.Instance.Send(pUserAccount, pMailMessage);
			}
			catch (Exception bException)
			{
				throw new InternalOperationException(Resources.Exceptions.SendException, bException);
			}
		}
	}
}

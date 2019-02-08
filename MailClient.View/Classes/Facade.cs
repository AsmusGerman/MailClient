using MailClient.Core;
using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace MailClient.View
{
	public static class Facade
	{

		public static MailAccount LoginByAlias(string pAlias, string pPassword)
		{
			try
			{
				return MCCore.Instance.LoginByAlias(pAlias, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Verifique que los campos ingresados sean correctos e intente nuevamente.",
					bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Algo durante la operación salio mal, intente nuevamente. Si el problema persiste, contacte con soporte técnico.",
					bException);
			}
			catch (Exception bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Contacte con soporte técnico.",
					bException);
			}

		}

		public static MailAccount LoginByMailAddress(string pMailAddress, string pPassword)
		{
			try
			{
				return MCCore.Instance.LoginByMailAddress(pMailAddress, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Verifique que los campos ingresados sean correctos e intente nuevamente.",
					bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Algo durante la operación salio mal, intente nuevamente. Si el problema persiste, contacte con soporte técnico.",
					bException);
			}
			catch (Exception bException)
			{
				throw new LoginException(@"No fue posible acceder a la cuenta de correo. Contacte con soporte técnico.",
					bException);
			}

		}

		public static void Register(string pAlias, string pMail, string pPassword)
		{
			try
			{
				MCCore.Instance.Register(pAlias, pMail, pPassword);
			}
			catch (WellKnownException bException)
			{
				throw new RegisterException(@"No fue posible registrar la cuenta de correo. Verifique que los campos ingresados sean correctos e intente nuevamente.",
					bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new RegisterException(@"No fue posible registrar la cuenta de correo. Algo durante la operación salio mal, intente nuevamente. Si el problema persiste, contacte con soporte técnico.",
					bException);
			}
			catch (Exception bException)
			{
				throw new RegisterException(@"No fue posible registrar la cuenta de correo. Contacte con soporte técnico.",
					bException);
			}
		}

		public static IEnumerable<MailMessage> UpdateInbox(MailAccount pUserAccount, int pWindow = 0)
		{
			try
			{
				return MCCore.Instance.UpdateInbox(pUserAccount, pWindow);
			}
			catch (WellKnownException bException)
			{
				throw new UpdateInboxException(@"No fue posible actualizar la casilla de correo. Verifique que haya conexión a internet.",
					bException);
			}
			catch (UnknownErrorException bException)
			{
				throw new UpdateInboxException(@"No fue posible actualizar la casilla de correo. Algo durante la operación salio mal, intente nuevamente. Si el problema persiste, contacte con soporte técnico.",
					bException);
			}
			catch (Exception bException)
			{
				throw new UpdateInboxException(@"No fue posible actualizar la casilla de correo. Contacte con soporte técnico.",
					bException);
			}
		}
	}
}

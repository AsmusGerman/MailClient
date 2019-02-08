using MailClient.Shared.Exceptions;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.BLL
{
	/// <summary>
	/// extensión de la clase Pop3Client,
	/// controla los flujos alternativos
	/// para la obtención de N mensajes de forma segura
	/// </summary>
	public static class Pop3ClientExtension
	{
		/// <summary>
		/// Realiza la conexión
		/// </summary>
		/// <returns></returns>
		public static Pop3Client Connection(this Pop3Client pPop3Client, string pHostName, int pPort, bool pUseSsl)
		{
			try
			{
				pPop3Client.Connect(pHostName, pPort, pUseSsl);
				return pPop3Client;
			}
			catch (Exception bException)
			{
				throw new FailOnConnect(Resources.Exceptions.Pop3ClientExtension_Connection_FailOnConnect, bException);
			}
		}

		/// <summary>
		/// Autentica la conexión
		/// </summary>
		/// <returns></returns>
		public static Pop3Client Authentication(this Pop3Client pPop3Client, string pUsername, string pPassword)
		{
			try
			{
				pPop3Client.Authenticate(pUsername, pPassword, AuthenticationMethod.UsernameAndPassword);
				return pPop3Client;
			}
			catch (Exception bException)
			{
				throw new FailOnAuth(Resources.Exceptions.Pop3ClientExtension_Authentication_FailOnAuth, bException);
			}

		}

		/// <summary>
		/// Obtiene N mensajes definidos por la ventana
		/// </summary>
		/// <param name="pPop3Client"></param>
		/// <param name="pWindow">ventana de N elementos</param>
		/// <returns></returns>
		public static IEnumerable<Message> GetWindowedMessages(this Pop3Client pPop3Client, int pOffset, int pWindow)
		{

			try
			{
				if (!pPop3Client.Connected)
					throw new ImNotConnectedException(Resources.Exceptions.Pop3ClientExtension_GetWindowedMessages_ImNotConnectedException);

				return Enumerable.Range(pOffset + 1, pWindow)
						.Select(bIndex => pPop3Client.GetMessage(bIndex))
						.ToList();
			}
			catch (ImNotConnectedException bException)
			{
				throw new FailOnGettingMessages(Resources.Exceptions.Pop3ClientExtension_GetWindowedMessages_FailOnGettingMessages, bException);
			}
			catch (Exception bException)
			{
				throw new UnknownErrorException(Resources.Exceptions.Pop3ClientExtension_GetWindowedMessages_UnknownErrorException, bException);
			}
		}
	}
}

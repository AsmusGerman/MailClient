using MailClient.Shared;
using MailClient.Shared.Exceptions;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailClient.BLL
{
	public class Pop3 : Protocol
	{
		#region public methods
		public Pop3() { }
		public IEnumerable<MailMessage> Retrieve(string pMailAddress, string pPassword, int pOffset = 0, int pWindow = 0)
		{
			try
			{
				if (pOffset > pWindow || pWindow == 0)
					return new List<MailMessage>();

				return this.FetchNFirst(pMailAddress, pPassword, pOffset, pWindow)
						.Select(bMessage => bMessage.ConvertToMailMessage())
						.ToList();
			}
			catch (WellKnownException bException)
			{
				throw new FailOnFetching(Resources.Exceptions.Pop3_Retrieve_FailOnFetching, bException);
			}
			catch (Exception bException)
			{
				throw new UnknownErrorException(Resources.Exceptions.Pop3_Retrieve_UnknownErrorException, bException);
			}

		}
		#endregion

		#region private methods
		/// <summary>
		/// Obtiene los N primeros mensajes tal que N > 0
		/// </summary>
		/// <param name="pUsername"></param>
		/// <param name="pPassword"></param>
		/// <returns></returns>
		private IEnumerable<Message> FetchNFirst(string pUsername, string pPassword, int pOffset, int pWindow)
		{
			try
			{
				return new Pop3Client()
						.Connection(this.Host, this.Port, this.SSL)
						.Authentication(pUsername, pPassword)
						.GetWindowedMessages(pOffset, pWindow);
			}
			catch (WellKnownException bException)
			{
				throw new FailOnFetching(Resources.Exceptions.Pop3_FetchNFirst_FailOnFetching, bException);
			}
			catch (Exception bException)
			{
				throw new UnknownErrorException(Resources.Exceptions.Pop3_FetchNFirst_UnknownErrorException, bException);
			}

		}
		#endregion
	}

}

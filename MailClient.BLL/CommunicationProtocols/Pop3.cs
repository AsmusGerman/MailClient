using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace MailClient.BLL
{
	public class Pop3 : MailClient.Shared.Protocol
	{
		public IEnumerable<MailMessage> Retrieve(string pMailAddress, string pPassword, int pOffset = 0, int pWindow = 0)
		{
			try
			{
				if (pOffset + 1 > pWindow || pWindow == 0)
					return new List<MailMessage>();

				Pop3Client mPop3Client = new Pop3Client();
				mPop3Client.Connect(this.Host, this.Port, this.SSL);
				mPop3Client.Authenticate(pMailAddress, pPassword);

				return Enumerable.Range(pOffset + 1, pWindow)
						.Select(bIndex => mPop3Client.GetMessage(bIndex).ToMailMessage());
			}
			catch (Exception bException)
			{
				throw new FailOnRetrieve(Resources.Exceptions.Pop3_Retrieve_UnknownErrorException, bException);
			}
		}
	}
}
using System;
using System.Net.Mail;

namespace MailClient.BLL
{
	public static class SmtpClientExtension
	{
		public static SmtpClient SendMail(this SmtpClient pSmtpClient, MailMessage pMailMessage) {

			try
			{
				pSmtpClient.Send(pMailMessage);
				return pSmtpClient;
			}
			catch (Exception bException)
			{
				throw new FailOnSend(Resources.Exceptions.SmtpClientExtension_SendMail_FailOnSend,bException);
			}
		}
	}
}

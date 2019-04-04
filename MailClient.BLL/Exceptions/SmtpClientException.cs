using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class SmtpClientException : WellKnownException
	{
		public SmtpClientException()
		{
		}

		public SmtpClientException(string message) : base(message)
		{
		}

		public SmtpClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

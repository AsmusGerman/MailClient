using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnEncrypt : WellKnownException
	{
		public FailOnEncrypt()
		{
		}

		public FailOnEncrypt(string message) : base(message)
		{
		}

		public FailOnEncrypt(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

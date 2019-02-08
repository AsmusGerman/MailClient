using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnDecrypt : WellKnownException
	{
		public FailOnDecrypt()
		{
		}

		public FailOnDecrypt(string message) : base(message)
		{
		}

		public FailOnDecrypt(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnSend : WellKnownException
	{
		public FailOnSend()
		{
		}

		public FailOnSend(string message) : base(message)
		{
		}

		public FailOnSend(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

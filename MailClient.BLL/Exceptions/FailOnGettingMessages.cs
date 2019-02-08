using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnGettingMessages : WellKnownException
	{
		public FailOnGettingMessages()
		{
		}

		public FailOnGettingMessages(string message) : base(message)
		{
		}

		public FailOnGettingMessages(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

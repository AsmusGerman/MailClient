using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnConnect : WellKnownException
	{
		public FailOnConnect()
		{
		}

		public FailOnConnect(string message) : base(message)
		{
		}

		public FailOnConnect(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

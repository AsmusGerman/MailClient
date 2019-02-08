using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnCreateMessage : WellKnownException
	{
		public FailOnCreateMessage()
		{
		}

		public FailOnCreateMessage(string message) : base(message)
		{
		}

		public FailOnCreateMessage(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

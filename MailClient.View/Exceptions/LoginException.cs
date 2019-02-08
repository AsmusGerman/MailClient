using MailClient.Shared.Exceptions;
using System;

namespace MailClient.View
{
	internal class LoginException : WellKnownException
	{
		public LoginException()
		{
		}

		public LoginException(string message) : base(message)
		{
		}

		public LoginException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
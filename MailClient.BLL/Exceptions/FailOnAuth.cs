using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnAuth : WellKnownException
	{
		public FailOnAuth()
		{
		}

		public FailOnAuth(string message) : base(message)
		{
		}

		public FailOnAuth(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

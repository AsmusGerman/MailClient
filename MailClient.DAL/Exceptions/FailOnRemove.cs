using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class FailOnRemove : WellKnownException
	{
		public FailOnRemove()
		{
		}

		public FailOnRemove(string message) : base(message)
		{
		}

		public FailOnRemove(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

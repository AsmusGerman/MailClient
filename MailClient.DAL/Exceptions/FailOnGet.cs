using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class FailOnGet : WellKnownException
	{
		public FailOnGet()
		{
		}

		public FailOnGet(string message) : base(message)
		{
		}

		public FailOnGet(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

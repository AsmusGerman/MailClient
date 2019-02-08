using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnFetching : WellKnownException
	{
		public FailOnFetching()
		{
		}

		public FailOnFetching(string message) : base(message)
		{
		}

		public FailOnFetching(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

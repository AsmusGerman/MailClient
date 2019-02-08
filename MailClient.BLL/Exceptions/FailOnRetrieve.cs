using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnRetrieve : WellKnownException
	{
		public FailOnRetrieve()
		{
		}

		public FailOnRetrieve(string message) : base(message)
		{
		}

		public FailOnRetrieve(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class FailOnAdd : WellKnownException
	{
		public FailOnAdd()
		{
		}

		public FailOnAdd(string message) : base(message)
		{
		}

		public FailOnAdd(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

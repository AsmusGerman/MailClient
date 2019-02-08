using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class FailOnGetServiceByName : WellKnownException
	{
		public FailOnGetServiceByName()
		{
		}

		public FailOnGetServiceByName(string message) : base(message)
		{
		}

		public FailOnGetServiceByName(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

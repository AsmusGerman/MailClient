using MailClient.Shared.Exceptions;
using System;

namespace MailClient.BLL
{
	public class ImNotConnectedException : WellKnownException
	{
		public ImNotConnectedException()
		{
		}

		public ImNotConnectedException(string message) : base(message)
		{
		}

		public ImNotConnectedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

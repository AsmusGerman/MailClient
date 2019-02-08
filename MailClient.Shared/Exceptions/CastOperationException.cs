using System;

namespace MailClient.Shared.Exceptions
{
	public class CastOperationException : WellKnownException
	{
		public CastOperationException()
		{
		}

		public CastOperationException(string message) : base(message)
		{
		}

		public CastOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

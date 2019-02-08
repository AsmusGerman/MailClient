using MailClient.Shared.Exceptions;
using System;

namespace MailClient.View
{
	internal class GenericException : WellKnownException
	{
		public GenericException()
		{
		}

		public GenericException(string message) : base(message)
		{
		}

		public GenericException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}
}
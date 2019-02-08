using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class DataAccessLayerException : WellKnownException
	{
		public DataAccessLayerException()
		{
		}

		public DataAccessLayerException(string message) : base(message)
		{
		}

		public DataAccessLayerException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

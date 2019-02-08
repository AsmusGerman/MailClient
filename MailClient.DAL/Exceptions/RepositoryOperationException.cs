using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class RepositoryOperationException : DataAccessLayerException
    {
		public RepositoryOperationException()
		{
		}

		public RepositoryOperationException(string message) : base(message)
		{
		}

		public RepositoryOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

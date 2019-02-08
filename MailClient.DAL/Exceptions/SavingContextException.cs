using MailClient.Shared.Exceptions;
using System;

namespace MailClient.DAL.Exceptions
{
	public class SavingContextException : DataAccessLayerException
    {
		public SavingContextException()
		{
		}

		public SavingContextException(string message) : base(message)
		{
		}

		public SavingContextException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

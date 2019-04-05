using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.View
{
	public class InternalOperationException : WellKnownException
	{
		public InternalOperationException()
		{
		}

		public InternalOperationException(string message) : base(message)
		{
		}

		public InternalOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

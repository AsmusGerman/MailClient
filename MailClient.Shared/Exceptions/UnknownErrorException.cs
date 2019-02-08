using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared.Exceptions
{
	public class UnknownErrorException : ApplicationException
	{
		public UnknownErrorException()
		{
		}

		public UnknownErrorException(string message) : base(message)
		{
		}

		public UnknownErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

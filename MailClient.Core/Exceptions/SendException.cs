using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class SendException : WellKnownException
	{
		public SendException()
		{
		}

		public SendException(string message) : base(message)
		{
		}

		public SendException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

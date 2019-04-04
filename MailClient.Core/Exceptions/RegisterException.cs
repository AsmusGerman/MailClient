using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class RegisterException : WellKnownException
	{
		public RegisterException()
		{
		}

		public RegisterException(string message) : base(message)
		{
		}

		public RegisterException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

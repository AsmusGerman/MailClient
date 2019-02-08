using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.View
{
	public class RegisterException : Exception
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

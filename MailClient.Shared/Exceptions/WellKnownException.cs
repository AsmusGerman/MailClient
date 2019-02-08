using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared.Exceptions
{
	public class WellKnownException : ApplicationException
	{
		public WellKnownException()
		{
		}

		public WellKnownException(string message) : base(message)
		{
		}

		public WellKnownException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class FailOnNewUser : WellKnownException
	{
		public FailOnNewUser()
		{
		}

		public FailOnNewUser(string message) : base(message)
		{
		}

		public FailOnNewUser(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

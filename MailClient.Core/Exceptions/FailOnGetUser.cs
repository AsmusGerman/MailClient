using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class FailOnGetUser : WellKnownException
	{
		public FailOnGetUser()
		{
		}

		public FailOnGetUser(string message) : base(message)
		{
		}

		public FailOnGetUser(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

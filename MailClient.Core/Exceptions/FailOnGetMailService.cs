using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class FailOnGetMailService : WellKnownException
	{
		public FailOnGetMailService()
		{
		}

		public FailOnGetMailService(string message) : base(message)
		{
		}

		public FailOnGetMailService(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

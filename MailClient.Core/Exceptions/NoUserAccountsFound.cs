using MailClient.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
	public class NoUserAccountsFound : WellKnownException
	{
		public NoUserAccountsFound()
		{
		}

		public NoUserAccountsFound(string message) : base(message)
		{
		}

		public NoUserAccountsFound(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

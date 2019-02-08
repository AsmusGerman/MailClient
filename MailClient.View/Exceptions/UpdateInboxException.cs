using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.View
{
	public class UpdateInboxException : Exception
	{
		public UpdateInboxException()
		{
		}

		public UpdateInboxException(string message) : base(message)
		{
		}

		public UpdateInboxException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

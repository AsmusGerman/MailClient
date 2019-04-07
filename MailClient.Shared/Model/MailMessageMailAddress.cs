using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public class MailMessageMailAddress
	{
		public int MailMessageId { get; set; }
		public MailMessage MailMessage { get; set; }

		public int MailAddressId { get; set; }
		public MailAddress MailAddress { get; set; }
	}
}

using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.View
{
	public class MailMessageVM
	{
		public int Id { get; private set; }
		public string Body { get; set; }
		public string From { get; set; }
		public string Subject { get; set; }
		public IList<string> To { get; set; }

		public MailMessageVM()
		{
			this.To = new List<string>();
		}

		public static MailMessageVM FromMailMessage(MailMessage pMailMessage)
		{
			return new MailMessageVM()
			{
				Id = pMailMessage.Id,
				Body = pMailMessage.Body,
				From = pMailMessage.From.Value,
				Subject = pMailMessage.Subject,
				To = pMailMessage.To.Select(bMailAddress => bMailAddress.Value).ToList()
			};
		}
	}
}

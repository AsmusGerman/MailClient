using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	[Serializable]
	public class Protocol
	{
		public string Name { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool SSL { get; set; }
	}
}

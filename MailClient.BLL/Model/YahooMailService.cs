using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.BLL
{
	public class YahooMailService : MailService
	{
		public YahooMailService(IEnumerable<ICommunicationProtocol> pComunicationProtocols) : base(pComunicationProtocols)
		{

			this.GetCommunicationProtocol<Smtp>()
				.Configure("smtp.mail.yahoo.com", 587, true);

			this.GetCommunicationProtocol<Pop3>()
				.Configure("pop.mail.yahoo.com", 995, true);
		}
	}
}

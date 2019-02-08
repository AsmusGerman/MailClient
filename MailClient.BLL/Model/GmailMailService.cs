using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.BLL
{
	public class GmailMailService : MailService
	{
		public GmailMailService(IEnumerable<ICommunicationProtocol> pComunicationProtocols): base(pComunicationProtocols)
		{

			this.GetCommunicationProtocol<Smtp>()
				.Configure("smtp.gmail.com", 587, true);


			this.GetCommunicationProtocol<Pop3>()
				.Configure("pop.gmail.com", 995, true);
		}
	}
}

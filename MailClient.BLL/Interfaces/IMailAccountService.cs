using System.Collections.Generic;

namespace MailClient.BLL
{
    public interface IMailAccountService
	{
        void Retrieve(int pMailAccountId, string pProtocolName, int pRetrieveWindowSize);
		void Send(int pMailAccountId, string pProtocolName, IEnumerable<string> pToMailAddresses, string pSubject, string pBody);
	}
}

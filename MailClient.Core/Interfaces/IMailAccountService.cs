using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.Core
{
    public interface IMailAccountService
	{
        void Retrieve(MailAccount pMailAccount, int pWindow = 0);
		void Send(MailAccount pMailAccount, MailMessage pMailMessage);
	}
}

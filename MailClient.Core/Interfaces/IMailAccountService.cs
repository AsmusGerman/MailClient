using MailClient.Shared;

namespace MailClient.Core
{
    public interface IMailAccountService
	{
        void Retrieve(MailAccount pMailAccount, int pWindow = 0);
		void Send(MailAccount pMailAccount, MailMessage pMailMessage);
	}
}

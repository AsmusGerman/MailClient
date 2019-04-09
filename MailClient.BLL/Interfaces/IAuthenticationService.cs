using MailClient.Shared;

namespace MailClient.BLL
{
	public interface IAuthenticationService
	{
		void Register(string pAlias, string pMailAddress, string pPassword);
		MailAccount LoginByAlias(string pAlias, string pPassword);
		MailAccount LoginByMailAddress(string pMailAddress, string pPassword);
        void UpdateAccount();
    }
}

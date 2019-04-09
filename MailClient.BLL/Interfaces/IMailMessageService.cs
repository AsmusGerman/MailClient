using MailClient.Shared;

namespace MailClient.BLL
{
    public interface IMailMessageService
    {
        string ConvertMailMessage(string pFormatName, MailMessage pMailMessage);
    }
}
using MailClient.Core.Selectors;
using MailClient.Shared;

namespace MailClient.Core
{
    public interface IMailServiceCollection
    {
        MailService[] MailServices { get; set; }
        MailService ResolveByName(MailServiceSelector pMailServiceSelector);
    }
}

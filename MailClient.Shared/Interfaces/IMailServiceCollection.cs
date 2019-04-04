namespace MailClient.Shared
{
    public interface IMailServiceCollection
    {
        MailService[] MailServices { get; set; }
        MailService ResolveByName(ISelector<MailService, bool> pSelector);
    }
}

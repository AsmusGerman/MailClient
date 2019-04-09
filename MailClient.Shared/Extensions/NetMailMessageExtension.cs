using System;
using System.Linq;
namespace MailClient.Shared.Extensions
{
    public static class NetMailMessageExtension
    {
        public static MailMessage ToMailClientMailMessage(this System.Net.Mail.MailMessage pNetMailMessage)
        {
            return new MailMessage()
            {
                ExternalId = pNetMailMessage.Headers["ExternalId"],
                DateSent = DateTime.Parse(pNetMailMessage.Headers["DateSent"]),
                Body = pNetMailMessage.Body ?? default(string),
                Subject = pNetMailMessage.Subject ?? default(string),
                From = new MailAddress() { Value = pNetMailMessage.From.Address },
                To = pNetMailMessage.To.Select(bMessage => new MailAddress() { Value = bMessage.Address }).ToList()
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared.Extensions
{
    public static class MailMessageExtension
    {
        public static System.Net.Mail.MailMessage ToNetMailMessage(this MailMessage pMailMessage)
        {
            System.Net.Mail.MailMessage mMailMessage = new System.Net.Mail.MailMessage()
            {
                From = new System.Net.Mail.MailAddress(pMailMessage.From.Value),
                Subject = pMailMessage.Subject,
                Body = pMailMessage.Body,
            };

            pMailMessage.To.ToList()
                        .ForEach(bAddress => mMailMessage.To.Add(bAddress.Value));

            return mMailMessage;
        }
    }
}

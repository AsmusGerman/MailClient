using System;
using System.Collections.Generic;
using System.Linq;
using MailClient.Shared;
using MailClient.DAL;
using MailClient.BLL.Selectors;
using MailClient.Shared.Extensions;
using OpenPop.Pop3;

namespace MailClient.BLL
{
    public class MailMessageService : IMailMessageService
    {
        private IDictionary<string, MailMessageConverter> iMailMessageConverter;

        public MailMessageService()
        {
            this.iMailMessageConverter = new Dictionary<string, MailMessageConverter>();
            this.iMailMessageConverter.Add("txt", new TXTMailMessageConverter("txt"));
            this.iMailMessageConverter.Add("eml", new EMLMailMessageConverter("eml"));
        }

        public string ConvertMailMessage(string pFormatName, MailMessage pMailMessage)
        {
            if (!this.iMailMessageConverter.ContainsKey(pFormatName))
                throw new UnexistentMailConverterException();

            return this.iMailMessageConverter[pFormatName].Export(pMailMessage.ToNetMailMessage());

        }
    }
}

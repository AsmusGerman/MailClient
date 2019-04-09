using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL
{
    public abstract class MailMessageConverter
    {
        public string iName;
        public MailMessageConverter(string pName)
        {
            this.iName = pName;
        }

        public abstract string Export(MailMessage pMailMessage);
    }
}

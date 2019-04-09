using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL
{
    public class TXTMailMessageConverter : MailMessageConverter
    {
        public TXTMailMessageConverter(string pName): base(pName)
        {
        }

        public override string Export(MailMessage pMailMessage)
        {
            string mTxt = string.Empty;
            mTxt += pMailMessage.From.Address + Environment.NewLine;
            mTxt += string.Join(";", pMailMessage.To.Select(bMailAddress => bMailAddress.Address)) + Environment.NewLine;
            mTxt += pMailMessage.Subject + Environment.NewLine;
            mTxt += pMailMessage.Body + Environment.NewLine;
            return mTxt;
        }
    }
}

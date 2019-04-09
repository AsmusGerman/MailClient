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
    public class EMLMailMessageConverter: MailMessageConverter
    {
        public EMLMailMessageConverter(string pName): base(pName)
        {
        }

        public override string Export(MailMessage pMailMessage)
        {
            Assembly mSmtpClientAssembly = typeof(SmtpClient).Assembly;
            Type mMailWriterType = mSmtpClientAssembly.GetType("System.Net.Mail.MailWriter");

            using (var memoryStream = new MemoryStream())
            {
                // Get reflection info for MailWriter contructor
                var mMailWriterContructor = mMailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

                // Construct MailWriter object with our FileStream
                var mMailWriter = mMailWriterContructor.Invoke(new object[] { memoryStream });

                // Get reflection info for Send() method on MailMessage
                var mSendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call method passing in MailWriter
                mSendMethod.Invoke(pMailMessage, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mMailWriter, true, true }, null);

                // Finally get reflection info for Close() method on our MailWriter
                MethodInfo mCloseMethod = mMailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call close method
                mCloseMethod.Invoke(mMailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

                return Encoding.ASCII.GetString(memoryStream.ToArray());
            }
        }
    }
}

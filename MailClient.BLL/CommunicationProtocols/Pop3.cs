using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace MailClient.BLL
{
    public class Pop3 : MailClient.Shared.IProtocol
    {
        public string Host { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }

        public IEnumerable<MailMessage> Retrieve(string pMailAddress, string pPassword, int pOffset = 0, int pWindow = 0)
        {
            try
            {
                if (pWindow == 0)
                    return new List<MailMessage>();

                Pop3Client mPop3Client = new Pop3Client();
                mPop3Client.Connect(this.Host, this.Port, this.SSL);
                mPop3Client.Authenticate(pMailAddress, pPassword);

                if (pWindow > mPop3Client.GetMessageCount() || pWindow == -1)
                {
                    pWindow = mPop3Client.GetMessageCount();
                }

                return mPop3Client.GetMessageCount() > 0 ? 
                    Enumerable.Range(pOffset + 1, pWindow)
                        .Select(bIndex => this.ProjectToMailMessage(mPop3Client.GetMessage(bIndex)))
                        .ToList()
                    : new List<MailMessage>();
            }
            catch (Exception bException)
            {
                throw new FailOnRetrieve(Resources.Exceptions.Pop3_Retrieve_UnknownErrorException, bException);
            }
        }

        private MailMessage ProjectToMailMessage(Message pPopMessage)
        {
            MailMessage mMailMessage = pPopMessage.ToMailMessage();
            mMailMessage.Headers.Add("ExternalId", pPopMessage.Headers.MessageId);
            return mMailMessage;
        }
    }
}
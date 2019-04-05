using System;
using System.Linq;
using MailClient.Shared;

namespace MailClient.DAL
{
    [Serializable]
    public class MailServiceCollection
    {
        public MailService[] MailServices { get; set; }
    }
}

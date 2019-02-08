using System;
using System.Linq;
using MailClient.Core.Selectors;
using MailClient.Shared;

namespace MailClient.Core
{
    [Serializable]
    public class MailServiceCollection : IMailServiceCollection
    {
        public MailService[] MailServices { get; set; }

        public MailService ResolveByName(MailServiceSelector pMailServiceSelector)
        {
            return this.MailServices.AsQueryable().SingleOrDefault(pMailServiceSelector.Criteria);
        }
    }
}

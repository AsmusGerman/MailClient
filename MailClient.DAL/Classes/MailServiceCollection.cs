using System;
using System.Linq;
using MailClient.Shared;

namespace MailClient.DAL
{
    [Serializable]
    public class MailServiceCollection : IMailServiceCollection
    {
        public MailService[] MailServices { get; set; }

        public MailService ResolveByName(ISelector<MailService, bool> pMailServiceSelector)
        {
            return this.MailServices.AsQueryable().SingleOrDefault(pMailServiceSelector.Criteria);
        }
    }
}

using MailClient.Shared;
using System;
using System.Linq.Expressions;

namespace MailClient.Core.Selectors
{
	public class MailServiceSelector : ISelector<MailService>
	{
        public Expression<Func<MailService, bool>> Criteria { get; }

        public MailServiceSelector(Expression<Func<MailService, bool>> expression)
        {
            this.Criteria = expression;
        }

        /// <summary>
        /// devuelve el selector para buscar una cuenta por alias
        /// </summary>
        public static MailServiceSelector ByName(string pName)
		{
			return new MailServiceSelector(bMailService => bMailService.Name.Equals(pName));
		}
	}
}

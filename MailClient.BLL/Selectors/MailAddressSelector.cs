using MailClient.Shared;
using System;
using System.Linq.Expressions;

namespace MailClient.BLL.Selectors
{
	public class MailAddressSelector : EntitySelector<MailAddress>
	{
		public MailAddressSelector(Expression<Func<MailAddress, bool>> expression) : base(expression)
		{
		}

        public static MailAddressSelector ByMailAddress(string pMailAddress)
		{
			return new MailAddressSelector(bMailAddress => bMailAddress.Value.Equals(pMailAddress));
		}
    }
}

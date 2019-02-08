using MailClient.Shared;
using System;
using System.Linq.Expressions;

namespace MailClient.BLL.Selectors
{
	public class MailAccountSelector : EntitySelector<MailAccount>
	{
		public MailAccountSelector(Expression<Func<MailAccount, bool>> expression) : base(expression)
		{
		}

		/// <summary>
		/// devuelve el selector para buscar una cuenta por alias
		/// </summary>
		public static MailAccountSelector ByAlias(string pAlias)
		{
			return new MailAccountSelector(bMailAccount => bMailAccount.Alias.Equals(pAlias));
		}
		/// <summary>
		/// devuelve el selector para buscar una cuenta por correo electronico
		/// </summary>
		public static MailAccountSelector ByMailAddress(string pMailAddress)
		{
			return new MailAccountSelector(bMailAccount => bMailAccount.MailAddress.Value.Equals(pMailAddress));
		}
	}
}

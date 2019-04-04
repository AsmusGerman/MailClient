using MailClient.Shared;
using System;
using System.Linq.Expressions;

namespace MailClient.BLL.Selectors
{
	public class MailServiceSelector : ISelector<MailService, bool>
	{
		public MailServiceSelector(Expression<Func<MailService, bool>> expression)
		{
			this.Criteria = expression;
		}

		public Expression<Func<MailService, bool>> Criteria { get; }

		/// <summary>
		/// devuelve el selector para buscar un servicio de correo por nombre
		/// </summary>
		public static MailServiceSelector ByName(string pName)
		{
			return new MailServiceSelector(bMailService => bMailService.Name.Equals(pName));
		}

	}
}

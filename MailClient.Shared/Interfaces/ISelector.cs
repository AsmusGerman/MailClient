using System;
using System.Linq.Expressions;

namespace MailClient.Shared
{
	public interface ISelector<TEntity> where TEntity : class
	{
		Expression<Func<TEntity, bool>> Criteria { get; }
	}
}

using System.Collections.Generic;
using MailClient.Shared;

namespace MailClient.DAL.Interfaces
{
	public interface IRepository {

	}

	public interface IRepository<T> : IRepository
	{
		void Create(T pEntity);
		IEnumerable<T> List(ISelector<T, bool> pSelector);
		void Remove(T pEntity);
		T Single(ISelector<T, bool> pSelector);
	}
}
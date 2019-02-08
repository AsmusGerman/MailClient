using System.Collections.Generic;

namespace MailClient.Shared
{
	public interface IRepository
	{
		T Single<T>(ISelector<T> pSpecification) where T : BaseEntity;
		List<T> List<T>(ISelector <T> pSpecification = null) where T : BaseEntity;
		T Create<T>(T pEntity) where T : BaseEntity;
		void Create<T>(List<T> pEntities) where T : BaseEntity;
		void Remove<T>(T pEntity) where T : BaseEntity;
	}
}

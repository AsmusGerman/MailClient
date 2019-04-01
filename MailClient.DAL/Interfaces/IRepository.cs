using MailClient.Shared;
using System.Collections.Generic;

namespace MailClient.DAL
{
    public interface IRepository
    {
        T Single<T>(ISelector<T, bool> pSpecification) where T : BaseEntity;
        IEnumerable<T> List<T>(ISelector<T, bool> pSpecification = null) where T : BaseEntity;
        T Create<T>(T pEntity) where T : BaseEntity;
        void Create<T>(IEnumerable<T> pEntities) where T : BaseEntity;
        void Remove<T>(T pEntity) where T : BaseEntity;
        T2 Query<T1, T2>(ISelector<T1, T2> pSpecification) where T1 : BaseEntity;
    }
}

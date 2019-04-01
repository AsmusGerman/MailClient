using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL.Selectors
{
    public class EntitySelector<TEntity> : ISelector<TEntity, bool> where TEntity : BaseEntity
    {
        protected EntitySelector(Expression<Func<TEntity, bool>> expression)
        {
            this.Criteria = expression;
        }

        /// <summary>
        /// Devuelve un nuevo EntitySelector cuyo criterio de selección son todas las entidades
        /// </summary>
        /// <returns></returns>
        public static EntitySelector<TEntity> All()
        {
            return new EntitySelector<TEntity>(bEntity => true);
        }

        /// <summary>
        /// Devuelve un nuevo EntitySelector cuyo criterio de selección es aquella entidad cuyo Id sea igual al argumentado
        /// </summary>
        /// <returns></returns>
        public static EntitySelector<TEntity> ById(int? id)
        {
            return new EntitySelector<TEntity>(bEntity => bEntity.Id == id);
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }
    }
}

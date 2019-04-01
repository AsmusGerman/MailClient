using System;
using System.Linq.Expressions;

namespace MailClient.Shared
{
    public interface ISelector<Tin, Tout>
    {
        Expression<Func<Tin, Tout>> Criteria { get; }
    }
}

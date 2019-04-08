using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Core
{
    public abstract class AbstractServiceCommand : IServiceCommand
    {
        public AbstractServiceCommand()
        {

        }
        public abstract void Execute();
    }
}

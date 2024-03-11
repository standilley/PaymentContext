using PaymentContext.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T :ICommand
    {
        ICommandResult handle(T command);
    }
}

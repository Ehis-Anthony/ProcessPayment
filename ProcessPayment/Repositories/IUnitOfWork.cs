using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentProcessRepository PaymentProcess { get; }
        IPaymentStateRepository PaymentState { get; }

        int Complete();
    }
}

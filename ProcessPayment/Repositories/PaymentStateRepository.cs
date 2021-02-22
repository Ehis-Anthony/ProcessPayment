using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessPayment.Data;
using ProcessPayment.Models;

namespace ProcessPayment.Repositories
{
    public class PaymentStateRepository : Repository<PaymentState>, IPaymentStateRepository
    {
        public PaymentStateRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}

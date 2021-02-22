using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessPayment.Data;
using ProcessPayment.Models;

namespace ProcessPayment.Repositories
{
    public class PaymentProcessRepository : Repository<PRProcessPayment>, IPaymentProcessRepository
    {
        public PaymentProcessRepository(ApplicationDBContext context) : base(context)
        {
           
        }
    }
}

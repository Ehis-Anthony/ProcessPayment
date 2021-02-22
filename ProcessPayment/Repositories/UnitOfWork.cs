using ProcessPayment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDBContext _context;

        #region "Lock"
        private static readonly object _Instancelock = new object();
        #endregion

        #region "Interfaces"
        IPaymentProcessRepository _PaymentProcess;
        IPaymentStateRepository _PaymentState;
        #endregion


        public IPaymentProcessRepository PaymentProcess
        {
            get
            {
                if (_PaymentProcess == null)
                {
                    lock (_Instancelock)
                    {
                        if (_PaymentProcess == null)
                            _PaymentProcess = new PaymentProcessRepository(_context);
                    }
                }

                return _PaymentProcess;
            }
        }

        public IPaymentStateRepository PaymentState
        {
            get
            {
                if (_PaymentState == null)
                {
                    lock (_Instancelock)
                    {
                        if (_PaymentState == null)
                            _PaymentState = new PaymentStateRepository(_context);
                    }
                }

                return _PaymentState;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

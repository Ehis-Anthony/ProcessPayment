using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Models
{
    public class PaymentState
    {
        [Key]
        public int paymentStateID { get; set; }
        public string paymentState { get; set; }
        public int processPaymentID { get; set; }
    }
}

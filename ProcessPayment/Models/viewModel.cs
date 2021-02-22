using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Models
{
    public class ViewModel
    {
    }

    public class paymentProcessViewModel
    {
        public int processPaymentID { get; set; }
        public string creditCardNumber { get; set; }
        public string cardHolder { get; set; }
        public DateTime expirationDate { get; set; }
        public string securityCode { get; set; }
        public float amount { get; set; }

        public paymentGatewayParametersViewModel PaymentGatewayParameters { get; set; }

        public paymentProcessViewModel()
        {
            PaymentGatewayParameters = new paymentGatewayParametersViewModel();
        }
    }

    public class paymentStateViewModel
    {
        public int paymentStateID { get; set; }
        public string paymentState { get; set; }
        public int processPaymentID { get; set; }
    }

    public class paymentGatewayParametersViewModel
    {
        public string creditCardNumber { get; set; }
        public string cardHolder { get; set; }
        public DateTime expirationDate { get; set; }
        public string securityCode { get; set; }
        public float amount { get; set; }
    }
}

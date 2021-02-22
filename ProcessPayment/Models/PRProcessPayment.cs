using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ProcessPayment.HelperClass;


namespace ProcessPayment.Models
{
    public class PRProcessPayment
    {
        [Key]
        public int processPaymentID { get; set; }

        [Required(ErrorMessage = "Credit Card is Mandatory")]
        [StringLength(16)]
        public string creditCardNumber { get; set; }

        [Required(ErrorMessage = "Credit Card Holder is Mandatory")]
        public string cardHolder { get; set; }

        [Required(ErrorMessage = "Expiration Date is Mandatory")]
        [DateAttributeClass.DateLessThanOrEqualToToday]
        public DateTime expirationDate { get; set; }

        [StringLength(3)]
        public string securityCode { get; set; }

        [Required(ErrorMessage = "Amount is Mandatory")]
        [Range(0, int.MaxValue, ErrorMessage = "Amount must be a positive number")]
        public double amount { get; set; }

        
    }
}

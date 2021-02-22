using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.HelperClass
{
    public class DateAttributeClass
    {
        public class DateLessThanOrEqualToToday : ValidationAttribute
        {
            public override string FormatErrorMessage(string name)
            {
                return "DateTime - it cannot be in the past!";
            }

            protected override ValidationResult IsValid(object objValue, ValidationContext validationContext)
            {
                var dateValue = objValue as DateTime? ?? new DateTime();

                if (dateValue.Date < DateTime.Now.Date)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                return ValidationResult.Success;
            }
        }
    }
}

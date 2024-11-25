using System.ComponentModel.DataAnnotations;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Utility;

namespace Vila_WebAPI.ModelValidation
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vila = (VilaDTOs)validationContext.ObjectInstance;
            try
            {
                var date = vila.MadeDate.ToEnglishDateTime();
                if (date > DateTime.Now)
                    return new ValidationResult("تاریخ ساخت باید در گذشته باشد .");

                return ValidationResult.Success;
            }
            catch
            {
                return new ValidationResult("(ماه و روز 2 رقمی باشد .)تاریخ ساخت باید در فرمت 1396/01/12 باشد .");
            }
        }
    }
}

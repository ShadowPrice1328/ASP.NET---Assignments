using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CustomValidators
{
    public class MaximumDateValidatorAttribute(string maxDate) : ValidationAttribute
    {
        private readonly DateTime _maximumDate = DateTime.Parse(maxDate);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if ((DateTime)value < _maximumDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"Date should not be older than {_maximumDate}", [nameof(validationContext.MemberName)]);
                }
                return ValidationResult.Success;
            }
            else ArgumentNullException.ThrowIfNull(value);

            return null;
        }
    }
}

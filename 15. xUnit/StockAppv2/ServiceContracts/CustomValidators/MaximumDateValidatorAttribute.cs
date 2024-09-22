using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CustomValidators
{
    public class MaximumDateValidatorAttribute : ValidationAttribute
    {
        private readonly DateTime _maximumDate;
        public MaximumDateValidatorAttribute(string maxDate) 
        {
            _maximumDate = DateTime.Parse(maxDate);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ArgumentNullException.ThrowIfNull(value);

            if ((DateTime)value > _maximumDate)
            {
                return new ValidationResult(ErrorMessage ?? $"Date should not be older than {_maximumDate}", [nameof(validationContext.MemberName)]);
            }

            return ValidationResult.Success;
        }
    }
}

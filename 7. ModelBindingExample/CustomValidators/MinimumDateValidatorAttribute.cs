using System.ComponentModel.DataAnnotations;

namespace ModelBindingExample.CustomValidators;

public class MinimumDateValidatorAttribute(string minimumDate) : ValidationAttribute
{
    public string DefaultErrorMessage {get; set;} = "Order date should be greater than or equal to {0}";
    public DateTime MinimumDate { get; set; } = Convert.ToDateTime(minimumDate);
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            DateTime orderDate = (DateTime)value;

            if (orderDate < MinimumDate)
            {
                return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumDate.ToString("yyyy-MM-dd")), [nameof(validationContext.MemberName)]);
            }

            return ValidationResult.Success;
        }
        return null;
    }
}
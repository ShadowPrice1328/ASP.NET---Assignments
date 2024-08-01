using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ModelBindingExample.Models;

namespace ModelBindingExample.CustomValidators;

public class InvoicePriceValidatorAttribute : ValidationAttribute
{
    public InvoicePriceValidatorAttribute() {}
    public string DefaultErrorMessage {get; set;} = "Invoice Price should be equal to the total cost of all products";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            PropertyInfo? OtherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
            if (OtherProperty != null)
            {
                List<Product> products = (List<Product>)OtherProperty.GetValue(validationContext.ObjectInstance)!;

                if (products == null)
                {
                    return null;
                }

                double totalPrice = 0;
                totalPrice = products.Sum(p => p.Price * p.Quantity);
                
                double actualPrice = (double)value;

                if (totalPrice > 0)
                {
                    if (totalPrice != actualPrice)
                    {
                        return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage), [nameof(validationContext.MemberName)]);
                    }
                }
                else
                {
                    return new ValidationResult("No products found to validate invoice price", [nameof(validationContext.MemberName)]);
                }

                // No errors
                return ValidationResult.Success;
            }
            else
                return null;
        }
        else
            return null;
    }
}
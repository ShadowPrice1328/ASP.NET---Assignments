using System.ComponentModel.DataAnnotations;
using ModelBindingExample.Models;

namespace ModelBindingExample.CustomValidators;

public class ProductsListValidatorAttribute : ValidationAttribute
{
    public ProductsListValidatorAttribute() {}
    public string DefaultErrorMessage {get; set;} = "Order should have at least one product";
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        List<Product> products = (List<Product>)value!;

        if (products == null || products.Count == 0)
        {
            return new ValidationResult(ErrorMessage ?? DefaultErrorMessage, [nameof(validationContext.MemberName)]);
        }

        return ValidationResult.Success;
    }
}
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBindingExample.CustomValidators;

namespace ModelBindingExample.Models;

public class Order
{
    [BindNever]
    [Display(Name = "Order Number")]
    public int? OrderNo {get; set;}

    [Required(ErrorMessage = "{0} can't be blank")]    
    [Display(Name = "Order Date")]
    [MinimumDateValidator("2000-01-01")]
    public DateTime OrderDate{get; set;}

    [Required(ErrorMessage = "{0} can't be blank")]
    [Display(Name = "Invoice Price")]
    [InvoicePriceValidator]
    [Range(1, double.MaxValue, ErrorMessage = "{0} should be between a valid number")]
    public double InvoicePrice {get; set;}

    [ProductsListValidator]
    public required List<Product> Products {get; set;}
}
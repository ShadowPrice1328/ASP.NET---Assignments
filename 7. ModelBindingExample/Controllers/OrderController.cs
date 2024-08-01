using Microsoft.AspNetCore.Mvc;
using ModelBindingExample.Models;

namespace ModelBindingExample.Controllers;

public class OrderController : Controller
{
    [HttpPost]
    [Route("/order")]
    public IActionResult Index(Order order)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

            return BadRequest(string.Join(";\n", errors));
        }

        Random rnd = new();
        order.OrderNo = rnd.Next(1, 9999);

        return Json(order.OrderNo);
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return Content("Welcome to the Best Bank", "text/plain");
    }
    [HttpGet]
    [Route("/account-details")]
    public IActionResult GetDetails()
    {
        return Json(new {accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000});
    }
    [HttpGet]
    [Route("/account-statement")]
    public IActionResult GetStatement()
    {
        return new VirtualFileResult("Sample PDF.pdf", "application/pdf");
    }
    [HttpGet]
    [Route("/get-current-balance/{accountNumber:int}")]
    public IActionResult GetCurrentBalance(int accountNumber)
    {
        var acc = new {accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000};

        if (accountNumber == 1001)
            return Content(acc.currentBalance.ToString(), "text/plain");
        else
            return StatusCode(400, "Account Number should be 1001");
    }
    [HttpGet]
    [Route("/get-current-balance")]
    public IActionResult GetCurrentBalance()
    {
        return NotFound("Account Number should be supplied");
    }
}
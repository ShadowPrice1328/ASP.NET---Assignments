using Microsoft.AspNetCore.Mvc;

namespace StockAppv2.Controllers
{
    public class TradeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {

            return View();
        }
    }
}

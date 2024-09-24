using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDExample.Controllers
{
    public class PersonsController(IPersonsService personsService) : Controller
    {
        private readonly IPersonsService _personsService = personsService;

        [Route("/")]
        [Route("[controller]")]
        [Route("[controller]/index")]
        public IActionResult Index()
        {
            List<PersonResponse> responses = _personsService.GetAllPersons();
            return View(responses);
        }
    }
}

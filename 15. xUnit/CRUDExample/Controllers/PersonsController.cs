using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Reflection;

namespace CRUDExample.Controllers
{
    public class PersonsController(IPersonsService personsService) : Controller
    {
        private readonly IPersonsService _personsService = personsService;

        [Route("/")]
        [Route("[controller]")]
        [Route("[controller]/index")]
        public IActionResult Index(string searchBy, string? searchString)
        {
            List<PropertyInfo> properties = typeof(PersonResponse).GetProperties().Skip(1).ToList();

            List<string> personFields = [];

            for (int i = 0; i < properties.Count; i++)
            {
                //personFields.Add(properties[i].Name, properties[i].GetValue(responses[i])?.ToString() ?? "null");
                personFields.Add(properties[i].Name);
            }

            ViewBag.personFields = personFields;

            ViewBag.currentSearchBy = searchBy;
            ViewBag.currentSearchString = searchString;

            List<PersonResponse> responses = _personsService.GetFilteredPersons(searchBy, searchString);

            return View(responses);
        }
    }
}

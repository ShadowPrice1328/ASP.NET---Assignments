using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
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
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), OrderOptions order = OrderOptions.ASC)
        {
            List<PropertyInfo> properties = typeof(PersonResponse).GetProperties().Skip(1).ToList();

            List<string> personFields = [];

            for (int i = 0; i < properties.Count; i++)
            {
                //personFields.Add(properties[i].Name, properties[i].GetValue(persons[i])?.ToString() ?? "null");
                personFields.Add(properties[i].Name);
            }

            ViewBag.PersonFields = personFields;

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);

            // Sorting
            List<PersonResponse> sortedPersons = _personsService.GetSortedPersons(persons, sortBy, order);
            
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentOrderOption = order.ToString();

            return View(sortedPersons);
        }
    }
}

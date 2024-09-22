using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public partial class PersonsServices : IPersonsService
{
    private readonly List<Person> _persons;
    private readonly ICountriesService _countryService;

    public PersonsServices()
    {
        _persons = new List<Person>();
        _countryService = new CountriesService();
    }
    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.Country = _countryService.GetCountryByCountryId(person.CountryId)?.CountryName;

        return personResponse;
    }
    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        ValidationHelper.ModelValidation(personAddRequest);

        Person person = personAddRequest.ToPerson();
        person.PersonId = Guid.NewGuid();

        _persons.Add(person);

        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons()
    {
        return _persons.Select(p => p.ToPersonResponse()).ToList();
    }

    public PersonResponse? GetPersonByPersonId(Guid? guid)
    {
        if ( guid == null)
            return null;

        return _persons.FirstOrDefault(p => p.PersonId == guid)?.ToPersonResponse() ?? null; 
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        List<PersonResponse> allPeople = GetAllPersons();
        List<PersonResponse> matchingPeople = allPeople;

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
        {
            return matchingPeople;
        }

        switch (searchBy)
        {
            case nameof(Person.PersonName):
                matchingPeople = allPeople.Where(p =>
                string.IsNullOrEmpty(p.PersonName) || p.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
                break;

            case nameof(Person.Email):
                matchingPeople = allPeople.Where(p =>
                string.IsNullOrEmpty(p.Email) || p.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
                break;

            case nameof(Person.Address):
                matchingPeople = allPeople.Where(p =>
                string.IsNullOrEmpty(p.Address) || p.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
                break;

            case nameof(Person.DateOfBirth):
                matchingPeople = allPeople.Where(p =>
                (p.DateOfBirth != null) ?
                p.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)
                .ToList();
                break;

            case nameof(Person.Gender):
                matchingPeople = allPeople.Where(p =>
                string.IsNullOrEmpty(p.Gender) || p.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
                break;

            case nameof(Person.CountryId):
                matchingPeople = allPeople.Where(p =>
                string.IsNullOrEmpty(p.Country) || p.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
                break;

            default: matchingPeople = allPeople; break;
        }

        return matchingPeople;
    }
    public List<PersonResponse> GetSortedPersons(List<PersonResponse> persons, string sortBy, OrderOptions orderOption)
    {
        if (persons == null || persons.Count == 0)
        {
            throw new ArgumentNullException(nameof(persons));
        }

        if (string.IsNullOrEmpty(sortBy))
        {
            return persons;
        }

        switch (orderOption)
        {
            case OrderOptions.DESC:
            case OrderOptions.DESCENDING:
                return SortPersons(persons, sortBy, descending: true);

            case OrderOptions.ASC:
            case OrderOptions.ASCENDING:
                return SortPersons(persons, sortBy, descending: false);

            default:
                throw new ArgumentException("Wrong orderOption parameter value", nameof(orderOption));
        }
    }

    private List<PersonResponse> SortPersons(List<PersonResponse> persons, string sortBy, bool descending)
    {
        return descending
            ? persons.OrderByDescending(p => GetSortValue(p, sortBy)).ToList()
            : persons.OrderBy(p => GetSortValue(p, sortBy)).ToList();
    }

    private object GetSortValue(PersonResponse person, string sortBy)
    {
        return sortBy switch
        {
            nameof(Person.PersonName) => person.PersonName,
            nameof(Person.Address) => person.Address,
            nameof(Person.CountryId) => person.CountryId,
            nameof(Person.DateOfBirth) => person.DateOfBirth,
            nameof(Person.Email) => person.Email,
            nameof(Person.Gender) => person.Gender,
            nameof(Person.PersonId) => person.PersonId,
            nameof(Person.ReceiveNewsLetters) => person.ReceiveNewsLetters,
            _ => throw new ArgumentException("Wrong sortBy parameter value", nameof(sortBy))
        };
    }

    PersonResponse IPersonsService.UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest == null)
            throw new ArgumentNullException(nameof(Person));

        ValidationHelper.ModelValidation(personUpdateRequest);

        Person? matchingPerson = _persons.FirstOrDefault(p => p.PersonId == personUpdateRequest.PersonId);

        if (matchingPerson == null)
            throw new ArgumentNullException(nameof(matchingPerson), "Person with given Id does not exist");

        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        matchingPerson.CountryId = personUpdateRequest.CountryId;
        matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

        return matchingPerson.ToPersonResponse();
    }

    public bool DeletePerson(Guid? guid)
    {
        if (!guid.HasValue)
            throw new ArgumentNullException(nameof(guid));

        var person = _persons.FirstOrDefault(p => p.PersonId == guid);

        if (person == null)
            return false;

        _persons.RemoveAll(p => p.PersonId == guid);
        return true;
    }
}
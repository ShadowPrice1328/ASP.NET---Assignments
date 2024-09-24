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

    public PersonsServices(bool initialize = true)
    {
        _persons = new List<Person>();

        if (initialize)
        {
            _persons.AddRange(new List<Person>()
            {
                new Person() {PersonId = Guid.Parse("575b2727-4458-4d44-90e0-307415d44cc2"), PersonName = "Ardyth", Email = "awoloschinski0@cnet.com", DateOfBirth = DateTime.Parse("1999-05-08"), Gender = "Female", CountryId = Guid.Parse("81D82D26-D5BF-40C3-81F5-C1FD4EDE7B2F"),Address = "5 Drewry Drive", ReceiveNewsLetters = false },
                new Person() {PersonId = Guid.Parse("4f416b05-5255-4d92-86d6-a3d248de8d27"), PersonName = "Erinna", Email = "ezumbusch1@youtu.be", DateOfBirth = DateTime.Parse("2003-02-02"), Gender = "Female", CountryId = Guid.Parse("53423D62-AB97-49CD-95E2-62CEC28968D3"), Address = "088 Russell Terrace", ReceiveNewsLetters = true }, // Germany
                new Person() {PersonId = Guid.Parse("07bfde2a-6c50-47bf-be82-e89999e7ab66"), PersonName = "Marys", Email = "mgrimsdike2@arstechnica.com", DateOfBirth = DateTime.Parse("1995-12-19"), Gender = "Female", CountryId = Guid.Parse("E52B029D-7AB4-4006-8A86-C552D4F7EE33"), Address = "587 Onsgard Lane", ReceiveNewsLetters = true }, // Ukraine
                new Person() {PersonId = Guid.Parse("4c6bbc84-5457-4746-a24f-ce1c9c0d202e"), PersonName = "Ulrica", Email = "uledram3@china.com.cn", DateOfBirth = DateTime.Parse("2001-11-11"), Gender = "Female", CountryId = Guid.Parse("3C1B53C6-7DEF-4B0A-BCF5-7634DC0469B4"), Address = "39459 Troy Trail", ReceiveNewsLetters = true }, // Poland
                new Person() {PersonId = Guid.Parse("011319f8-796a-40b8-90be-072fc5015ccc"), PersonName = "Keith", Email = "kyerrell4@simplemachines.org", DateOfBirth = DateTime.Parse("2005-01-26"), Gender = "Male", CountryId = Guid.Parse("81D82D26-D5BF-40C3-81F5-C1FD4EDE7B2F"), Address = "39 Washington Road", ReceiveNewsLetters = true }, // USA
                new Person() {PersonId = Guid.Parse("193e9bf0-8dc4-441c-bb36-4fe87f2be954"), PersonName = "Rich", Email = "rrusson5@biglobe.ne.jp", DateOfBirth = DateTime.Parse("1995-05-02"), Gender = "Male", CountryId = Guid.Parse("53423D62-AB97-49CD-95E2-62CEC28968D3"), Address = "9 Packers Place", ReceiveNewsLetters = true }, // Germany
                new Person() {PersonId = Guid.Parse("99b4d137-f2c6-4d64-b04c-47298cdf2e8d"), PersonName = "Caryl", Email = "cledford6@usatoday.com", DateOfBirth = DateTime.Parse("1997-03-18"), Gender = "Female", CountryId = Guid.Parse("E52B029D-7AB4-4006-8A86-C552D4F7EE33"), Address = "7 Old Gate Trail", ReceiveNewsLetters = true }, // Ukraine
                new Person() {PersonId = Guid.Parse("efa20850-5aa2-4af0-bf6a-97e40e17ae0d"), PersonName = "Brooke", Email = "bstyle7@elpais.com", DateOfBirth = DateTime.Parse("2005-04-28"), Gender = "Male", CountryId = Guid.Parse("9B98BA57-466B-46D3-8F8D-ED9D7B982354"), Address = "3 Superior Court", ReceiveNewsLetters = false }, // Canada
                new Person() {PersonId = Guid.Parse("94da91d7-dbc2-4b52-9d13-3852c13a776c"), PersonName = "Teodorico", Email = "twinley8@huffingtonpost.com", DateOfBirth = DateTime.Parse("2004-12-19"), Gender = "Male", CountryId = Guid.Parse("3C1B53C6-7DEF-4B0A-BCF5-7634DC0469B4"), Address = "8 Hintze Drive", ReceiveNewsLetters = false }, // Poland
                new Person() {PersonId = Guid.Parse("b289e8aa-7d16-4060-953d-0e0eec68b113"), PersonName = "Elita", Email = "egreenrodd9@nps.gov", DateOfBirth = DateTime.Parse("1996-02-14"), Gender = "Female", CountryId = Guid.Parse("9B98BA57-466B-46D3-8F8D-ED9D7B982354"), Address = "92 Saint Paul Hill", ReceiveNewsLetters = false }, // Canada
            });
        }

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
                string.IsNullOrEmpty(p.Gender) || p.Gender.Equals(searchString, StringComparison.OrdinalIgnoreCase))
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
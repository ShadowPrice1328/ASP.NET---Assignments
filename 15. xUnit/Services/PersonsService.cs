using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class PersonsServices : IPersonsService
{
    private readonly List<Person> _persons;

    public PersonsServices()
    {
        _persons = new List<Person>();
    }
    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        throw new NotImplementedException();
    }

    public List<PersonResponse?> GetAllPersons()
    {
        throw new NotImplementedException();
    }
}
using System.Text.RegularExpressions;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;

namespace CRUDTests;

public class PersonsServiceTest
{
    private readonly IPersonsService _personsService;
    public PersonsServiceTest()
    {
        _personsService = new PersonsServices();
    }

    #region AddPerson

    [Fact]
    public void AddPerson_NullPerson()
    {
        // Arrange
        PersonAddRequest? personAddRequest = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _personsService.AddPerson(personAddRequest);
        });
    }

    [Fact]
    public void AddPerson_PersonNameIsNull()
    {
        // Arrange
        PersonAddRequest? personAddRequest = new() {PersonName = null};

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
           _personsService.AddPerson(personAddRequest); 
        });
    }

    [Fact]
    public void AddPerson_ProperPersonDetails()
    {
        // Arrange
        PersonAddRequest? personAddRequest = new()
        {
            PersonName = "Somebody Somewhere",
            Email = "mail@example.com",
            DateOfBirth = DateTime.Parse("2005-05-01"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = Guid.NewGuid(),
            ReceiveNewsLetters = false
        };

        // Act
        PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);
        List<PersonResponse> persons_list_from_get_method = _personsService.GetAllPersons();

        // Assert
        Assert.True(person_response_from_add.PersonId != Guid.Empty);
        Assert.Contains(person_response_from_add, persons_list_from_get_method);
    }

    [Fact]
    public void AddPerson_InvalidEmail()
    {
        // Arrange
        PersonAddRequest? personAddRequest = new()
        {
            PersonName = "Somebody Somewhere",
            Email = "mailexample.com",
            DateOfBirth = DateTime.Parse("2005-05-01"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = Guid.NewGuid(),
            ReceiveNewsLetters = false            
        };

        var exception = Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personsService.AddPerson(personAddRequest);
        });

        // Assert
        Assert.Contains("Invalid email format", exception.Message);
    }
    
    #endregion
}
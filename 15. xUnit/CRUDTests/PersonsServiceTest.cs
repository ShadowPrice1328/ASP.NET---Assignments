using System.Text.RegularExpressions;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CRUDTests;

public class PersonsServiceTest
{
    private readonly IPersonsService _personsService;
    private readonly ICountriesService _countriesService;
    private readonly ITestOutputHelper _testOutputHelper;
    public PersonsServiceTest(ITestOutputHelper testOutputHelper)
    {
        _personsService = new PersonsServices();
        _countriesService = new CountriesService();
        _testOutputHelper = testOutputHelper;
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

    #region GetPersonByPersonId

    [Fact]
    public void GetPersonByPersonId_NullPersonId()
    {
        // Arrange
        Guid? guid = null;

        // Act
        PersonResponse? person_response_from_get = _personsService.GetPersonByPersonId(guid);

        // Assert
        Assert.Null(person_response_from_get);
    }

    [Fact]
    public void GetPersonByPersonId_WithPersonId()
    {
        // Assert
        CountryAddRequest countryAddRequest = new()
        {
            CountryName = "USA"
        };

        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new()
        {
            PersonName = "Somebody Somewhere",
            Email = "mail@example.com",
            DateOfBirth = DateTime.Parse("2005-01-05"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = countryResponse.CountryId,
            ReceiveNewsLetters = false
        };

        PersonResponse person_response_from_add = _personsService.AddPerson(personAddRequest);

        // Act
        PersonResponse? person_response_from_get = _personsService.GetPersonByPersonId(person_response_from_add.PersonId);

        // Assert
        Assert.Equal(person_response_from_add, person_response_from_get);
    }

    #endregion

    #region GetAllPersons

    [Fact]
    public void GetAllPersons_EmptyList()
    {
        // Act
        List<PersonResponse> personResponses = _personsService.GetAllPersons();

        // Assert
        Assert.Empty(personResponses);
    }

    [Fact]
    public void GetAllPersons_AddFewPersons()
    {
        // Arrange
        List<PersonResponse> person_responses_from_add = AddTwoPersons();

        // Write expected values in test console
        foreach (PersonResponse person_response_from_add in person_responses_from_add)
        {
            _testOutputHelper.WriteLine(person_response_from_add.ToString());
        }

        // Act
        List<PersonResponse> person_responses_from_get = _personsService.GetAllPersons();

        foreach(PersonResponse person_response_from_get in person_responses_from_get)
        {
            // Write expected values in test console
            _testOutputHelper.WriteLine(person_response_from_get.ToString());
            // Assert
            Assert.Contains(person_response_from_get, person_responses_from_add);
        }
    }

    #endregion

    #region GetFilteredPersons

    [Fact]
    public void GetFilteredPersons_EmptySearchString()
    {
        // Arrange
        List<PersonResponse> person_responses_from_add = AddTwoPersons();

        // Write expected values in test console
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse person_response_from_add in person_responses_from_add)
        {
            _testOutputHelper.WriteLine(person_response_from_add.ToString());
        }

        // Act
        List<PersonResponse> person_responses_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "");

        _testOutputHelper.WriteLine("Actual:");
        foreach(PersonResponse person_response_from_search in person_responses_from_search)
        {
            // Write actual values in test console
            _testOutputHelper.WriteLine(person_response_from_search.ToString());
            
            // Assert
            Assert.Contains(person_response_from_search, person_responses_from_add);
        }
    }

    [Fact]
    public void GetFilteredPersons_GivenPersonName()
    {
        // Arrange
        List<PersonResponse> person_responses_from_add = AddTwoPersons();

        // Write expected values from Add list in test console
        foreach (PersonResponse person_response_from_add in person_responses_from_add)
        {
            _testOutputHelper.WriteLine(person_response_from_add.ToString());
        }

        // Act
        List<PersonResponse> person_responses_from_search = _personsService.GetFilteredPersons(nameof(Person.PersonName), "som");

        foreach(PersonResponse person_response_from_search in person_responses_from_search)
        {
            // Write expected values in test console
            _testOutputHelper.WriteLine(person_response_from_search.ToString());
        }

        // Assert
        foreach(PersonResponse person_response_from_add in person_responses_from_add)
        {
            if (person_response_from_add.PersonName != null)
            {
                if (person_response_from_add.PersonName.Contains("som", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(person_response_from_add, person_responses_from_search);
                }
            }
        }
    }

    private List<PersonResponse> AddTwoPersons()
    {
        PersonAddRequest person_add_request_1 = new()
        {
            PersonName = "Somebody Somewhere",
            Email = "mail@example.com",
            DateOfBirth = DateTime.Parse("2005-05-01"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = Guid.NewGuid(),
            ReceiveNewsLetters = false
        };

        PersonAddRequest person_add_request_2 = new()
        {
            PersonName = "John Doe",
            Email = "mail@example.com",
            DateOfBirth = DateTime.Parse("2002-05-01"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = Guid.NewGuid(),
            ReceiveNewsLetters = false
        };

        List<PersonResponse> person_responses_from_add =
        [
            _personsService.AddPerson(person_add_request_1),
            _personsService.AddPerson(person_add_request_2)
        ];

        return person_responses_from_add;
    }

    #endregion

    #region GetSortedPersons

    [Fact]
    public void GetSortedPersons()
    {
        // Arrange
        List<PersonResponse> person_responses_from_add = AddTwoPersons();
        
        // Act
        List<PersonResponse> person_responses_from_sort = _personsService.GetSortedPersons(person_responses_from_add, nameof(Person.PersonName), OrderOptions.DESC);

        _testOutputHelper.WriteLine("Expected: ");
        foreach (PersonResponse person_response_from_add in person_responses_from_add.OrderByDescending(p => p.PersonName))
        {
            _testOutputHelper.WriteLine(person_response_from_add.ToString());
        }
        person_responses_from_add = person_responses_from_add.OrderByDescending(p => p.PersonName).ToList();

        _testOutputHelper.WriteLine("Actual: ");
        foreach (PersonResponse person_response_from_sort in person_responses_from_sort)
        {
            _testOutputHelper.WriteLine(person_response_from_sort.ToString());
        }

        // Assert
        for (int i = 0; i < person_responses_from_sort.Count; i++)
        {
            Assert.Equal(person_responses_from_add[i], person_responses_from_sort[i]);
        }
    }

    #endregion

    #region UpdatePerson

    [Fact]
    public void UpdatePerson_NullPerson()
    {
        // Arrange
        PersonUpdateRequest person_update_request = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _personsService.UpdatePerson(person_update_request);
        });
    }

    [Fact]
    public void UpdatePerson_PersonNameIsNull()
    {
        // Arrange
        PersonAddRequest person_add_request = new PersonAddRequest()
        {
            PersonName = "Maria",
            Email = "maria@example.com",
            Gender = GenderOptions.Female
        };

        PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

        PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();
        person_update_request.PersonName = null;

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personsService.UpdatePerson(person_update_request);
        });
    }

    [Fact]
    public void UpdatePerson_ProperNameAndEmail()
    {
        // Arrange

        PersonAddRequest person_add_request = new PersonAddRequest()
        {
            PersonName = "Maria",
            Email = "maria@example.com",
            Gender = GenderOptions.Female
        };

        PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);
        PersonUpdateRequest person_update_request = person_response_from_add.ToPersonUpdateRequest();

        person_update_request.PersonName = "Yelyzaveta";
        person_update_request.Email = "yelyzaveta@example.com";

        // Act
        PersonResponse person_response_from_update = _personsService.UpdatePerson(person_update_request);
        PersonResponse? person_reponse_from_get = _personsService.GetPersonByPersonId(person_response_from_update.PersonId);

        // Assert
        Assert.Equal(person_reponse_from_get, person_response_from_update);
    }

    #endregion

    #region DeletePerson

    [Fact]
    public void DeletePerson_NullPersonId()
    {
        // Arrange
        Guid? personId = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _personsService.DeletePerson(personId);
        });
    }

    [Fact]
    public void DeletePerson_InvalidPersonId()
    {
        // Act
        bool isDeleted = _personsService.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.False(isDeleted);
    }

    [Fact]
    public void DeletePerson_ValidPersonId()
    {
        // Assert
        CountryAddRequest country_add_request = new CountryAddRequest()
        {
            CountryName = "USA"
        };

        CountryResponse country_response_from_add = _countriesService.AddCountry(country_add_request);

        PersonAddRequest person_add_request = new PersonAddRequest()
        {
            PersonName = "Somebody Somewhere",
            Email = "mail@example.com",
            DateOfBirth = DateTime.Parse("2005-05-01"),
            Gender = GenderOptions.Female,
            Address = "Some street",
            CountryId = country_response_from_add.CountryId,
            ReceiveNewsLetters = false
        };

        PersonResponse person_response_from_add = _personsService.AddPerson(person_add_request);

        // Act
        bool isDeleted = _personsService.DeletePerson(person_response_from_add.PersonId);

        // Assert
        Assert.True(isDeleted);
    }

    #endregion
}
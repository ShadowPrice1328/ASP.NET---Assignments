using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class that is used for adding request
/// </summary>
public class PersonAddRequest
{
    [Required(ErrorMessage = "Person Name cannot be blank")]
    public string? PersonName {get; set;}

    [Required(ErrorMessage = "Email adress cannot be blank")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email {get; set;}

    public DateTime? DateOfBirth {get; set;}
    public GenderOptions? Gender {get; set;}
    public Guid? CountryId {get; set;}
    public string? Address {get; set;}
    public bool ReceiveNewsLetters {get; set;}

    /// <summary>
    /// Converts PersonAddRequest object into a new Person object
    /// </summary>
    /// <returns></returns>
    public Person ToPerson()
    {
        return new Person()
        {
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = Gender.ToString(),
            CountryId = CountryId,
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetters
        };
    }
}
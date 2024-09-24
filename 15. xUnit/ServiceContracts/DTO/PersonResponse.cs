using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

/// <summary>
/// DTO class used as a return type in PersonsService
/// </summary>
public class PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsLetters { get; set; }
    public double? Age { get; set; }

    /// <summary>
    /// Compares current object with parameter object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>True of False: True if all the details are matches with the parameter object's details</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() != typeof(PersonResponse)) return false;

        PersonResponse person = (PersonResponse)obj;

        return PersonId == person.PersonId && PersonName == person.PersonName && Email == person.Email
            && DateOfBirth == person.DateOfBirth && Gender == person.Gender && CountryId == person.CountryId
            && Country == person.Country && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters
            && Age == person.Age;
    }
    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest()
        {
            PersonId = PersonId,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender, true),
            Address = Address,
            ReceiveNewsLetters = ReceiveNewsLetters,
            CountryId = CountryId
        };
    }

    public override string ToString()
    {
        return $"PersonId: {PersonId}, PersonName: {PersonName}, Email: {Email}, DateOfBirth: {DateOfBirth}, Gender: {Gender}, CountryId: {CountryId}, Country: {Country}, Address: {Address}, ReceiveNewsLetters: {ReceiveNewsLetters}, Age: {Age}";
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public static class PersonExtentions
{
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse()
        {
            PersonId = person.PersonId,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            CountryId = person.CountryId,
            Address = person.Address,
            ReceiveNewsLetters = person.ReceiveNewsLetters,
            Age = (person.DateOfBirth != null)? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null 
        };
    }
}
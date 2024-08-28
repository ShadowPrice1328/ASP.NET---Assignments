using ServiceContracts.DTO;

namespace ServiceContracts;

/// <summary>
/// Represents business logic for manipulating Person entity
/// </summary>
public interface IPersonsService
{
    /// <summary>
    /// Adds the Person object to the list of Person objects
    /// </summary>
    /// <param name="personAddRequest"></param>
    /// <returns>Returns the same details but with generated Guid</returns>
    PersonResponse AddPerson(PersonAddRequest? personAddRequest);

    /// <summary>
    /// Returns all persons
    /// </summary>
    /// <returns></returns>
    List<PersonResponse> GetAllPersons();
}
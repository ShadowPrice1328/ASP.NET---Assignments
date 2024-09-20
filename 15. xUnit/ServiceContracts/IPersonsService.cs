using ServiceContracts.DTO;
using ServiceContracts.Enums;

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
    /// <returns>Returns list of all Person Responses</returns>
    List<PersonResponse> GetAllPersons();

    /// <summary>
    /// Searches Person Response with matching Person Id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns>Returns Person Response based on given Person Id</returns>
    PersonResponse? GetPersonByPersonId(Guid? guid);
    
    /// <summary>
    /// Returns all the PersonResponse objects that match given parameters
    /// </summary>
    /// <param name="searchBy"></param>
    /// <param name="searchString"></param>
    /// <returns>Returns all the PersonResponse objects that match given parameters</returns>
    List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);

    /// <summary>
    /// Sorts the list of PersonResponses according to specified field
    /// </summary>
    /// <param name="persons">List to sort</param>
    /// <param name="sortBy">Field to sort by</param>
    /// <param name="orderOption">ASCENDING or DESCENDING</param>
    /// <returns>Sorted list of PersonResponses</returns>
    List<PersonResponse> GetSortedPersons(List<PersonResponse>  persons, string sortBy, OrderOptions orderOption);

    /// <summary>
    /// Updates the details about person
    /// </summary>
    /// <param name="personUpdateRequest">DTO for updating</param>
    /// <returns>Returns newly created PersonResponse object with updated deatils</returns>
    PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
}
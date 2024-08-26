using ServiceContracts.DTO;

namespace ServiceContracts;

/// <summary>
/// Represents business logic for manipulating Country entity
/// </summary>
public interface ICountriesService
{
    /// <summary>
    /// Adds a country object to the list of countries
    /// </summary>
    /// <param name="countryAddRequest">Country object to add</param>
    /// <returns>Returns the same details but with generated Guid</returns>
    CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

    /// <summary>
    /// Returns all countries
    /// </summary>
    /// <returns></returns>
    List<CountryResponse> GetAllCountries();

    /// <summary>
    /// Return a country object based on the id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    CountryResponse? GetCountryByCountryId(Guid? guid);
}

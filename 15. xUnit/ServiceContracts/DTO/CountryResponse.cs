using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    /// <summary>
    /// DTO class that is used for most of the CountyService methods.
    /// </summary>
    public Guid CountryId {get; set;}
    public string? CountryName {get; set;}
}

public static class CountryExtentions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        return new CountryResponse
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName
        };
    }
}
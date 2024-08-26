using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    /// <summary>
    /// DTO class that is used for most of the CountyService methods.
    /// </summary>
    public Guid CountryId {get; set;}
    public string? CountryName {get; set;}
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != typeof(CountryResponse))
        {
            return false;
        }

        CountryResponse country_to_compare = (CountryResponse)obj;

        return CountryId == country_to_compare.CountryId && CountryName == country_to_compare.CountryName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
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
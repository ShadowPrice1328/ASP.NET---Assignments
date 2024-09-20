using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    private readonly List<Country> _countries;
    public CountriesService()
    {
        _countries = new List<Country>();
    }
    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        ArgumentNullException.ThrowIfNull(countryAddRequest);

        if (countryAddRequest.CountryName == null)
        {
            throw new ArgumentException(nameof(countryAddRequest.CountryName));
        }

        if (_countries.Where(c => c.CountryName == countryAddRequest.CountryName).Count() > 0)
        {
            throw new ArgumentException("This Country already exist.");
        }

        Country country = countryAddRequest.ToCountry();
        country.CountryId = Guid.NewGuid();

        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryId(Guid? guid)
    {
        if (guid == null)
            return null;

        return _countries.FirstOrDefault(c => c.CountryId == guid)?.ToCountryResponse() ?? null;;
    }
}

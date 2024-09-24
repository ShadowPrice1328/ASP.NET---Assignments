using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    private readonly List<Country> _countries;
    public CountriesService(bool initialize = true)
    {
        _countries = new List<Country>();

        if (initialize)
        {
            _countries.AddRange(new List<Country>() {
                new Country() { CountryId = Guid.Parse("81D82D26-D5BF-40C3-81F5-C1FD4EDE7B2F"), CountryName = "USA" },
                new Country() { CountryId = Guid.Parse("53423D62-AB97-49CD-95E2-62CEC28968D3"), CountryName = "Germany" },
                new Country() { CountryId = Guid.Parse("E52B029D-7AB4-4006-8A86-C552D4F7EE33"), CountryName = "Ukraine" },
                new Country() { CountryId = Guid.Parse("3C1B53C6-7DEF-4B0A-BCF5-7634DC0469B4"), CountryName = "Poland" },
                new Country() { CountryId = Guid.Parse("9B98BA57-466B-46D3-8F8D-ED9D7B982354"), CountryName = "Canada" }
            });
        }
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

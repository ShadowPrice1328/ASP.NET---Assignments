using System.Runtime.CompilerServices;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountryService _countryService;
    public CountriesServiceTest()
    {
        _countryService = new CountryService();
    }

    [Fact]
    public void AddCountry_NullCountry()
    {
        // Arrange
        CountryAddRequest? countryAddRequest = null;

        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _countryService.AddCountry(countryAddRequest);
        });
    }

    [Fact]
    public void AddCountry_NullCountryName()
    {
        // Arrange
        CountryAddRequest? countryAddRequest = new() {CountryName = null};
        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countryService.AddCountry(countryAddRequest);
        });
    }

    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        // Arrange
        CountryAddRequest? countryAddRequest1 = new() {CountryName = "Ukraine"};
        CountryAddRequest? countryAddRequest2 = new() {CountryName = "Ukraine"};

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countryService.AddCountry(countryAddRequest1);
            _countryService.AddCountry(countryAddRequest2);
        });
    }

    [Fact]
    public void AddCountry_ProperCountryDetails()
    {
        // Arrange
        CountryAddRequest? countryAddRequest = new() {CountryName = "Ukraine"};

        // Act
        CountryResponse countryResponse = _countryService.AddCountry(countryAddRequest);

        // Assert
        Assert.True(countryResponse.CountryId != Guid.Empty);
    }
}
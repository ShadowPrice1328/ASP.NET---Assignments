using System.Runtime.CompilerServices;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountriesService _countryService;
    public CountriesServiceTest()
    {
        _countryService = new CountriesService();
    }
    
    #region AddCountry

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
        List<CountryResponse> countries_from_GetAllCountries = _countryService.GetAllCountries();

        // Assert
        Assert.True(countryResponse.CountryId != Guid.Empty);
        Assert.Contains(countryResponse, countries_from_GetAllCountries);
    }

    #endregion

    #region GetAllCounties

    [Fact]
    public void GetAllCounties_EmptyList()
    {
        // Act
        List<CountryResponse> actualCountryResponses = _countryService.GetAllCountries();

        // Assert
        Assert.Empty(actualCountryResponses);
    }

    [Fact]
    public void GetAllCounties_AddFewCountries()
    {
        // Arrange
        List<CountryAddRequest> country_AddRequest_list = new()
        {
            new() {CountryName = "Ukraine"},
            new() {CountryName = "USA"},
            new() {CountryName = "Germany"}
        };

        // Act
        List<CountryResponse> countryResponses_from_AddCountry = new();

        foreach(CountryAddRequest countryAddRequest in country_AddRequest_list)
        {
            countryResponses_from_AddCountry.Add(_countryService.AddCountry(countryAddRequest));
        }

        // Assert
        List<CountryResponse> actual_CountryResponses = _countryService.GetAllCountries();

        foreach(CountryResponse expected_CountryResponse in countryResponses_from_AddCountry)
        {
            Assert.Contains(expected_CountryResponse, actual_CountryResponses);
        }

    }
    #endregion

    #region GetCountryByCountryId
    
    [Fact]
    public void GetCountryByCountryId_NullCountryId()
    {
        // Arrange
        Guid? guid = null;

        // Act
        CountryResponse? country_response_from_get_method = _countryService.GetCountryByCountryId(guid);

        // Assert
        Assert.Null(country_response_from_get_method);
    }

    [Fact]
    public void GetCountryByCountryId_ValidCountryId()
    {
        // Arrange
        CountryAddRequest? country_add_request = new CountryAddRequest() { CountryName = "USA" };
        CountryResponse? country_response = _countryService.AddCountry(country_add_request);

        // Act
        CountryResponse? country_response_from_get_method = _countryService.GetCountryByCountryId(country_response.CountryId);

        // Assert
        Assert.Equal(country_response, country_response_from_get_method);
    }

    #endregion
}
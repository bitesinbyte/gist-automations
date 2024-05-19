using System.Net;
using ExcelDataReader;
using GistAutomation.Records;

namespace GistAutomation.Utils;

public class GeoName
{
    private const string COUNTRY_INFO_URL = "https://download.geonames.org/export/dump/countryInfo.txt";
    private const string ADMIN_CODES_URL = "https://download.geonames.org/export/dump/admin1CodesASCII.txt";
    private const string COUNTRY_FLAG_URL = "http://img.geonames.org/flags/x/{0}.gif";

    public async static Task<(bool DoesAvailable, string Url)> DoesFlagImageAvailable(string iso2Code)
    {
        var client = new HttpClient();
        var url = string.Format(COUNTRY_FLAG_URL, iso2Code.ToLower());
        var response = await client.GetAsync(url);
        return (response.StatusCode == HttpStatusCode.OK, url);
    }

    public async static Task<List<Country>> GetCountries()
    {
        var listOfCountry = new List<Country>();

        var countryInfo = await GetCountryInfo();
        var admin1 = await GetAdmin1Codes();

        foreach (var country in countryInfo)
        {
            if (string.IsNullOrEmpty(country.Name) || country.Name.Equals("Country", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var stateProvinces = admin1
            .Where(x => x.Code.StartsWith(country.ISOAlpha2 + "."))
            .Select(x => new StateProvince
            {
                Name = x.Name
            }).ToList();

            var tempCountry = new Country
            {
                CountryCode = country.ISOAlpha2,
                Currency = country.CurrencyCode,
                Name = country.Name,
                Phone = country.Phone,
                CountryCodeAlpha3 = country.ISOAlpha3,
                StateProvinces = stateProvinces
            };

            var (DoesAvailable, Url) = await DoesFlagImageAvailable(country.ISOAlpha2);
            if (DoesAvailable)
            {
                tempCountry.Flag = Url;
            }

            tempCountry.Symbol = Currency.GetCurrency(tempCountry.Currency)?.Symbol ?? "";
            listOfCountry.Add(tempCountry);
        }
        return listOfCountry;
    }
    private static async Task<List<CountryInfo>> GetCountryInfo()
    {
        var returnList = new List<CountryInfo>();
        var client = new HttpClient();
        var response = await client.GetAsync(COUNTRY_INFO_URL);
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = ExcelReaderFactory.CreateCsvReader(stream);
        do
        {
            while (reader.Read())
            {
                var countryInfo = reader.MapCountryInfo();
                returnList.Add(countryInfo);
            }
        } while (reader.NextResult());
        return returnList;
    }
    private static async Task<List<Admin1CodesAscii>> GetAdmin1Codes()
    {
        var returnList = new List<Admin1CodesAscii>();
        var client = new HttpClient();

        var response = await client.GetAsync(ADMIN_CODES_URL);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        using var reader = ExcelReaderFactory.CreateCsvReader(stream);
        do
        {
            while (reader.Read())
            {
                var adminCode = reader.MapAdmin1Codes();
                returnList.Add(adminCode);
            }
        } while (reader.NextResult());
        return returnList;
    }
}
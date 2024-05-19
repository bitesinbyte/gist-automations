using ExcelDataReader;
using GistAutomation.Records;

namespace GistAutomation.Utils;

public class GeoName
{
    private const string COUNTRY_INFO_URL = "https://download.geonames.org/export/dump/countryInfo.txt";
    private const string ADMIN_CODES_URL = "https://download.geonames.org/export/dump/admin1CodesASCII.txt";
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
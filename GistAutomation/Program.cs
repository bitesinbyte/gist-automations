using System.Text;
using System.Text.Json;
using GistAutomation.Records;
using GistAutomation.Utils;

namespace GistAutomation;

public class Program
{

    public static async Task Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid args");
            return;
        }
        var command = args[0];

        switch (command)
        {
            case "sync-country":
                await SyncCountryAndState();
                break;
            case "sync-currency":
                await SyncCurrency();
                break;
            default:
                Console.WriteLine("Invalid command");
                break;
        }
    }

    public static async Task SyncCountryAndState()
    {
        var githubAccessToken = Environment.GetEnvironmentVariable("GIT_PERSONAL_TOKEN");
        if (string.IsNullOrEmpty(githubAccessToken))
        {
            throw new Exception("githubAccessToken is null, please add GITHUB_PERSONAL_TOKEN in env");
        }

        var gistId = Environment.GetEnvironmentVariable("COUNTRY_STATE_GIST_ID");

        if (string.IsNullOrEmpty(gistId))
        {
            throw new Exception("gistId is null, please add GITHUB_COUNTRY_GIST_ID in env");
        }
        var countries = await GeoName.GetCountries();
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = JsonSerializer.Serialize(countries, options);
        await GitHub.Save("country_state.json", githubAccessToken, gistId, data);
    }

    public static async Task SyncCurrency()
    {
        var githubAccessToken = Environment.GetEnvironmentVariable("GIT_PERSONAL_TOKEN");
        if (string.IsNullOrEmpty(githubAccessToken))
        {
            throw new Exception("githubAccessToken is null, please add GITHUB_PERSONAL_TOKEN in env");
        }

        var gistId = Environment.GetEnvironmentVariable("CURRENCY_GIST_ID");

        if (string.IsNullOrEmpty(gistId))
        {
            throw new Exception("gistId is null, please add CURRENCY_GIST_ID in env");
        }
        var countries = await GeoName.GetCountries();
        var currencyDataList = new List<CurrencyData>();
        foreach (var country in countries)
        {
            var currencyData = Currency.GetCurrency(country.Currency);
            currencyDataList.Add(new CurrencyData
            {
                Flag = country.Flag,
                CountryName = country.Name,
                Currency = currencyData?.CurrencyName ?? "",
                Code = currencyData?.CurrencyCode ?? "",
                Symbol = currencyData?.Symbol ?? "",
                SymbolImage = currencyData?.SymbolImage ?? ""
            });
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = JsonSerializer.Serialize(currencyDataList, options);
        await GitHub.Save("world_currency_symbols.json", githubAccessToken, gistId, data);
    }
}



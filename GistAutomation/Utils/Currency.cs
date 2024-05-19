using System.Text.Json;
using GistAutomation.Records;

namespace GistAutomation.Utils;

public static class Currency
{
    private static readonly List<CurrencyJson>? currencyList = JsonSerializer.Deserialize<List<CurrencyJson>>(File.ReadAllText("currencies.json"));

    public static CurrencyJson? GetCurrency(string currencyCode)
    {
        if (currencyList == null)
        {
            return null;
        }
        return currencyList.Find(x => x.CurrencyCode.Equals(currencyCode, StringComparison.OrdinalIgnoreCase));
    }
}
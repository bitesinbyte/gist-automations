using System.Text.Json.Serialization;

namespace GistAutomation.Records;

public record CurrencyJson
{
    [JsonPropertyName("symbolImage")]
    public string SymbolImage { get; set; } = "";

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; } = "";

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = "";

    [JsonPropertyName("currencyName")]
    public string CurrencyName { get; set; } = "";
}
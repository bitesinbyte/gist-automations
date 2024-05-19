namespace GistAutomation.Records;
public record CurrencyData
{
    public string Flag { get; set; } = "";
    public string CountryName { get; set; } = "";
    public string Currency { get; set; } = "";
    public string Code { get; set; } = "";
    public string Symbol { get; set; } = "";
    public string SymbolImage { get; set; } = "";
}
using System.Text.Json.Serialization;

namespace GistAutomation.Records;
public record Country
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; } = "";

    [JsonPropertyName("countryCodeAlpha3")]
    public string CountryCodeAlpha3 { get; set; } = "";

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = "";

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = "";

    [JsonPropertyName("stateProvinces")]
    public List<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}

public record StateProvince
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
}
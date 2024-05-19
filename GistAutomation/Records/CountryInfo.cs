using System.Text.Json.Serialization;

namespace GistAutomation.Records;

public record CountryInfo
{
    [JsonPropertyName("isoAlpha2")]
    public string ISOAlpha2 { get; set; } = "";

    [JsonPropertyName("isoAlpha3")]
    public string ISOAlpha3 { get; set; } = "";

    [JsonPropertyName("isoNumeric")]
    public int ISONumeric { get; set; }

    [JsonPropertyName("fipsCode")]
    public string FipsCode { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("capital")]
    public string Capital { get; set; } = "";

    [JsonPropertyName("areainsqkm")]
    public double AreaInSqKM { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("continent")]
    public string Continent { get; set; } = "";

    [JsonPropertyName("tld")]
    public string TLD { get; set; } = "";

    [JsonPropertyName("currencycode")]
    public string CurrencyCode { get; set; } = "";

    [JsonPropertyName("currencyname")]
    public string CurrencyName { get; set; } = "";

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = "";

    [JsonPropertyName("postalcode")]
    public string PostalCode { get; set; } = "";

    [JsonPropertyName("postalcoderegex")]
    public string PostalCodeRegex { get; set; } = "";

    [JsonPropertyName("languages")]
    public string Languages { get; set; } = "";

    [JsonPropertyName("geonameId")]
    public int GeonameId { get; set; }

    [JsonPropertyName("neighbors")]
    public string Neighbors { get; set; } = "";

    [JsonPropertyName("equivfipscode")]
    public string EquivFIPSCode { get; set; } = "";
}
namespace GistAutomation.Records;

using System.Text.Json.Serialization;

public record Admin1CodesAscii
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("nameAscii")]
    public string NameAscii { get; set; } = "";

    [JsonPropertyName("geonameid")]
    public int GeonameId { get; set; }
}
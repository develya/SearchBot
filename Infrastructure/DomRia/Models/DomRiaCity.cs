using System.Text.Json.Serialization;

namespace Infrastructure.DomRia.Models;

public class DomRiaCity
{
    [JsonPropertyName("cityID")]
    public int CityId { get; set; }

    [JsonPropertyName("stateID")]
    public int StateId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("eng")]
    public string EnglishName { get; set; } = string.Empty;
}
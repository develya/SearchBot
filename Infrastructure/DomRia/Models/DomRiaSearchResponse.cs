using System.Text.Json.Serialization;

namespace Infrastructure.DomRia.Models;

public class DomRiaSearchResponse
{
    [JsonPropertyName("items")]
    public List<int> Items { get; set; } = [];

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
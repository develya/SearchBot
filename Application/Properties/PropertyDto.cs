using System.Text.Json.Serialization;

namespace Application;

public class PropertyDto
{
    [JsonPropertyName("realty_id")]
    public int Id { get; set; }

    [JsonPropertyName("description_uk")]
    public string? Description { get; set; }

    [JsonPropertyName("price_total")]
    public decimal Price { get; set; }

    [JsonPropertyName("currency_type")]
    public string? Currency { get; set; }

    [JsonPropertyName("rooms_count")]
    public int Rooms { get; set; }

    [JsonPropertyName("city_name")]
    public string? City { get; set; }

    [JsonPropertyName("street_name")]
    public string? Street { get; set; }

    [JsonPropertyName("building_number_str")]
    public string? BuildingNumber { get; set; }

    [JsonPropertyName("floors_count")]
    public int TotalFloors { get; set; }

    [JsonPropertyName("beautiful_url")]
    public string? Url { get; set; }
}
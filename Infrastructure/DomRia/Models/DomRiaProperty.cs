using System.Text.Json.Serialization;

namespace Infrastructure.DomRia.Models;

public class DomRiaProperty
{
    [JsonPropertyName("realty_id")] 
    public int RealtyId { get; set; }

    [JsonPropertyName("advert_type_name")]  //назва операції (продаж, оренда)
    public string AdvertTypeName { get; set; } = string.Empty;

    [JsonPropertyName("realty_type_name")] 
    public string RealtyTypeName { get; set; } = string.Empty;

    [JsonPropertyName("state_id")] 
    public int StateId { get; set; }

    [JsonPropertyName("state_name")]
    public string StateName { get; set; } = string.Empty;

    [JsonPropertyName("city_id")]
    public int CityId { get; set; }

    [JsonPropertyName("city_name")]
    public string CityName { get; set; } = string.Empty;

    [JsonPropertyName("district_id")] //район
    public int? DistrictId { get; set; }

    [JsonPropertyName("district_name")]
    public string DistrictName { get; set; } = string.Empty;

    [JsonPropertyName("street_id")]
    public int? StreetId { get; set; }

    [JsonPropertyName("street_name")]
    public string StreetName { get; set; } = string.Empty;

    [JsonPropertyName("building_number_str")]
    public string BuildingNumber { get; set; } = string.Empty;

    [JsonPropertyName("beautiful_url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("description_uk")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("price_total")]
    public decimal PriceTotal { get; set; }

    [JsonPropertyName("currency_type")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("rooms_count")]
    public int? RoomsCount { get; set; }

    [JsonPropertyName("total_square_meters")]
    public decimal? TotalArea { get; set; }

    [JsonPropertyName("living_square_meters")]
    public decimal? LivingArea { get; set; }

    [JsonPropertyName("kitchen_square_meters")]
    public decimal? KitchenArea { get; set; }

    [JsonPropertyName("floor")]
    public int? Floor { get; set; }

    [JsonPropertyName("floors_count")]
    public int? FloorsCount { get; set; }

    [JsonPropertyName("main_photo")]
    public string MainPhoto { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("publishing_date")]
    public string? PublishingDate { get; set; }

    [JsonPropertyName("is_bargain")] //торг
    public int? IsBargain { get; set; }

    [JsonPropertyName("advert_publish_type")] //статус
    public int? AdvertPublishType { get; set; }
}
using System.Text.Json.Serialization;

namespace Application;

public class PropertyDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Currency { get; set; }
    public int Rooms { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? BuildingNumber { get; set; }
    public int TotalFloors { get; set; }
    public string? Url { get; set; }
}
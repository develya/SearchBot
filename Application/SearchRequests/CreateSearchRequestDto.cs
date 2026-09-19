namespace Application.SearchRequests;

public class CreateSearchRequestDto
{
    public string CityId { get; set; } = string.Empty;
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinRooms { get; set; }
    public int? MaxRooms { get; set; }
}
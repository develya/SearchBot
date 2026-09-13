namespace Domain;

public class SearchRequest
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string CityId { get; set; }

    public int? MinPrice { get; set; }

    public int? MaxPrice { get; set; }

    public int? MinRooms { get; set; }

    public int? MaxRooms { get; set; }

    public User User { get; set; }
}
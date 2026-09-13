namespace Domain;

public class User
{
    public Guid Id { get; set; }

    public long TelegramId { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<SearchRequest> SearchRequests { get; set; } = new List<SearchRequest>();
}
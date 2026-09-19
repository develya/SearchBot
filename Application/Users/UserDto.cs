namespace Application.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public long TelegramId { get; set; }
    public DateTime CreatedAt { get; set; }
}
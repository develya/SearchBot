using MediatR;

namespace Application.Users;

public record CreateUserCommand(long TelegramId) : IRequest<UserDto>;
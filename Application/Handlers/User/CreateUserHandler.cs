using Application.Interfaces;
using Application.Users;
using Domain;
using MediatR;

namespace Application.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByTelegramIdAsync(request.TelegramId, cancellationToken);
        if (user is not null)
        {
            return new UserDto
            {
                Id = user.Id,
                TelegramId = user.TelegramId,
                CreatedAt = user.CreatedAt
            };
        }

        user = new User
        {
            Id = Guid.NewGuid(),
            TelegramId = request.TelegramId,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);

        return new UserDto
        {
            Id = user.Id,
            TelegramId = user.TelegramId,
            CreatedAt = user.CreatedAt
        };
    }
}
using Application.Interfaces;
using Application.Users;
using MediatR;

namespace Application.Handlers;

public class GetUserByTelegramIdHandler : IRequestHandler<GetUserByTelegramIdRequest, UserDto?>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByTelegramIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDto?> Handle(GetUserByTelegramIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByTelegramIdAsync(request.TelegramId, cancellationToken);
        if (user is null)
        {
            return null;
        }
        return new UserDto
        {
            Id = user.Id,
            TelegramId = user.TelegramId,
            CreatedAt = user.CreatedAt
        };
    }
}
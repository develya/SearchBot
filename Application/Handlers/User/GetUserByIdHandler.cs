using Application.Common;
using Application.Interfaces;
using Application.Users;
using MediatR;

namespace Application.Handlers;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task< Result<UserDto>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure($"User with ID '{request.Id}' was not found.");
        }
        var dto = new UserDto
        {
            Id = user.Id,
            TelegramId = user.TelegramId,
            CreatedAt = user.CreatedAt
        };
        
        return Result<UserDto>.Success(dto);
    }
}
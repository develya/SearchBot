using Application.Common;
using MediatR;

namespace Application.Users;


public record GetUserByTelegramIdRequest(long TelegramId) : IRequest<UserDto?>;
public record GetUserByIdRequest(Guid Id) : IRequest<Result<UserDto>>;
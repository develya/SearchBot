using Application.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;
    
    public UsersController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserByIdRequest(id), cancellationToken);
    
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error); //return with text
    }
    
    [HttpGet("by-telegram/{telegramId:long}")]
    public async Task<ActionResult<UserDto>> GetByTelegramId(long telegramId, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetUserByTelegramIdRequest(telegramId), cancellationToken);
        
        return result is not null ? Ok(result) : NotFound();
    }
}
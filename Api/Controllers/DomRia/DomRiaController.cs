using Infrastructure.DomRia;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/domria")]
public class DomRiaController : ControllerBase
{
    private readonly DomRiaClient _domRiaClient;

    public DomRiaController(DomRiaClient domRiaClient)
    {
        _domRiaClient = domRiaClient;
    }

    [HttpGet("cities/{stateId}")]
    public async Task<IActionResult> GetCities(int stateId, CancellationToken cancellationToken)
    {
        var cities = await _domRiaClient.GetCitiesAsync(stateId, cancellationToken);

        return Ok(cities);
    }

    [HttpGet("properties/{realtyId}")]
    public async Task<IActionResult> GetProperty(int realtyId, CancellationToken cancellationToken)
    {
        var property = await _domRiaClient.GetPropertyByIdAsync(realtyId, cancellationToken);
        return Ok(property);

    }
}
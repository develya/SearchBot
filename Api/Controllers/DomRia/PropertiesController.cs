using Application;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

    [ApiController]
    [Route("api/properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertySearchService _propertySearchService;

        public PropertiesController(PropertySearchService propertySearchService)
        {
            _propertySearchService = propertySearchService;
        }

        [HttpGet("search")]

        public async Task<ActionResult<IReadOnlyCollection<PropertyDto>>> Search([FromQuery] PropertySearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var properties = await _propertySearchService.SearchAsync(request, cancellationToken);

                return Ok(properties);
            }
            catch (PropertyProviderException ex)
            {
                var statusCode = ex.UpstreamStatusCode switch
                {
                    400 => StatusCodes.Status400BadRequest,
                    429 => StatusCodes.Status429TooManyRequests,
                    _ => StatusCodes.Status502BadGateway
                };

                return Problem(
                    title: "DomRia property search failed",
                    detail: ex.Message,
                    statusCode: statusCode);
            }
        }
    }

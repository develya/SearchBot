using Application;
using Microsoft.Extensions.Options;

namespace Infrastructure.DomRia;

public class DomRiaSearchUrlBuilder
{
    private readonly DomRiaOptions _options;

    public DomRiaSearchUrlBuilder(IOptions<DomRiaOptions> options)
    {
        _options = options.Value;
    }
    
    public string Build(PropertySearchRequest request)
    {
        var url = $"{_options.BaseUrl}/dom/search" + $"?city_id={request.CityId}" + $"&api_key={_options.ApiKey}";

        if (request.MinPrice.HasValue)
        {
            url += $"&characteristic[234][from]={request.MinPrice.Value}";
        }

        if (request.MaxPrice.HasValue)
        {
            url += $"&characteristic[234][to]={request.MaxPrice.Value}";
        }
        
        if (request.MinRooms.HasValue)
        {
            url += $"&characteristic[209][from]={request.MinRooms.Value}";
        }

        if (request.MaxRooms.HasValue)
        {
            url += $"&characteristic[209][to]={request.MaxRooms.Value}";
        }

        return url;
    }
}
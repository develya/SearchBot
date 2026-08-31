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

        return url;
    }
}
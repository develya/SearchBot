using System.Net.Http.Json;
using Application;
using Infrastructure.DomRia.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.DomRia;

public class DomRiaClient
{
    private readonly HttpClient _httpClient;
    private readonly DomRiaOptions _options;

    public DomRiaClient(HttpClient httpClient, IOptions<DomRiaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<DomRiaCity>> GetCitiesAsync(int stateId, CancellationToken cancellationToken)
    {
        var url = $"{_options.BaseUrl}/dom/cities/{stateId}?api_key={_options.ApiKey}&lang_id=4";
        var cities = await _httpClient.GetFromJsonAsync<List<DomRiaCity>>(url, cancellationToken);

        return cities ?? [];
    }
}
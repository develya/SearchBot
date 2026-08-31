using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Application;
using Infrastructure.DomRia.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.DomRia;

public class DomRiaClient
{
    private const int MaxAttempts = 3;
    private static readonly TimeSpan FallbackRetryDelay = TimeSpan.FromSeconds(2);

    private readonly HttpClient _httpClient;
    private readonly DomRiaOptions _options;
    private readonly DomRiaSearchUrlBuilder _searchUrlBuilder;

    public DomRiaClient(HttpClient httpClient, IOptions<DomRiaOptions> options, DomRiaSearchUrlBuilder searchUrlBuilder)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _searchUrlBuilder = searchUrlBuilder;
    }

    public async Task<IReadOnlyCollection<DomRiaProperty>> GetSearchResultsAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        var url = _searchUrlBuilder.Build(request);

        var response = await GetWithRetryAsync(url, cancellationToken);

        if (response is null)
        {
            return [];
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var searchResponse = JsonSerializer.Deserialize<DomRiaSearchResponse>(content);

        if (searchResponse is null)
        {
            return [];
        }

        var properties = new List<DomRiaProperty>(searchResponse.Items.Count);

        foreach (var id in searchResponse.Items.Take(5))
        {
            var property = await GetPropertyByIdAsync(id, cancellationToken);

            if (property is null)
            {
                continue;
            }

            properties.Add(property);
        }

        return properties;
    }

    public async Task<IReadOnlyCollection<DomRiaCity>> GetCitiesAsync(int stateId, CancellationToken cancellationToken)
    {
        var url = $"{_options.BaseUrl}/dom/cities/{stateId}?api_key={_options.ApiKey}&lang_id=4";
        var cities = await _httpClient.GetFromJsonAsync<List<DomRiaCity>>(url, cancellationToken);

        return cities ?? [];
    }
    
    public async Task<DomRiaProperty?> GetPropertyByIdAsync(int realtyId, CancellationToken cancellationToken)
   {
       var url = $"{_options.BaseUrl}/dom/info/{realtyId}?api_key={_options.ApiKey}";

       var response = await GetWithRetryAsync(url, cancellationToken);

       if (response is null)
       {
           return null;
       }

       return await response.Content.ReadFromJsonAsync<DomRiaProperty>(cancellationToken);
   }
   
  

   private async Task<HttpResponseMessage?> GetWithRetryAsync(string url, CancellationToken cancellationToken)
   {
       for (var attempt = 1; attempt <= MaxAttempts; attempt++)
       {
           var response = await _httpClient.GetAsync(url, cancellationToken);

           if (response.IsSuccessStatusCode)
           {
               return response;
           }

           if (response.StatusCode == HttpStatusCode.NotFound)
           {
               return null;
           }

           if (response.StatusCode == HttpStatusCode.TooManyRequests && attempt < MaxAttempts)
           {
               await Task.Delay(GetRetryDelay(response, attempt), cancellationToken);
               continue;
           }

           var body = await response.Content.ReadAsStringAsync(cancellationToken);
           throw BuildUpstreamException(response.StatusCode, body);
       }

       throw new PropertyProviderException(
           $"DomRia rate limit exceeded after {MaxAttempts} attempts.",
           (int)HttpStatusCode.TooManyRequests);
   }

   private static TimeSpan GetRetryDelay(HttpResponseMessage response, int attempt)
   {
       var retryAfter = response.Headers.RetryAfter;

       if (retryAfter?.Delta is { } delta && delta > TimeSpan.Zero)
       {
           return delta;
       }

       if (retryAfter?.Date is { } date)
       {
           var untilDate = date - DateTimeOffset.UtcNow;

           if (untilDate > TimeSpan.Zero)
           {
               return untilDate;
           }
       }

       return FallbackRetryDelay * attempt;
   }

   private static PropertyProviderException BuildUpstreamException(HttpStatusCode statusCode, string body)
   {
       var message = statusCode switch
       {
           HttpStatusCode.BadRequest => $"DomRia rejected the request as invalid: {body}",
           HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => $"DomRia rejected our API key: {body}",
           HttpStatusCode.TooManyRequests => $"DomRia rate limit exceeded: {body}",
           _ when (int)statusCode >= 500 => $"DomRia is currently unavailable ({(int)statusCode}): {body}",
           _ => $"DomRia returned an unexpected error ({(int)statusCode}): {body}"
       };

       return new PropertyProviderException(message, (int)statusCode);
   }
}
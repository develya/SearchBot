using Application;

namespace Infrastructure.DomRia;

public class DomRiaPropertySource : IPropertySource
{
    private readonly DomRiaClient _client;

    public DomRiaPropertySource(DomRiaClient client)
    {
        _client = client;
    }
    public async Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        var properties = await _client.SearchAsync(request, cancellationToken);

        return properties;
    }
}

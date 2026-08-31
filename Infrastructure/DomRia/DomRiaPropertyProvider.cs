using Application;
using Infrastructure.DomRia.Mapping;

namespace Infrastructure.DomRia;

public class DomRiaPropertyProvider : IPropertyProvider
{
    private readonly DomRiaClient _domRiaClient;
    private readonly DomRiaPropertyMapper _mapper;

    public DomRiaPropertyProvider(DomRiaClient domRiaClient, DomRiaPropertyMapper mapper)
    {
        _domRiaClient = domRiaClient;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        var properties = await _domRiaClient.GetSearchResultsAsync(request, cancellationToken);

        return properties.Select(_mapper.Map).ToArray();
    }
}

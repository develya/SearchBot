namespace Application;

public class PropertySearchService
{
    private readonly IPropertyProvider _propertyProvider;

    public PropertySearchService(IPropertyProvider propertyProvider)
    {
        _propertyProvider = propertyProvider;
    }

    public Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        return _propertyProvider.SearchAsync(request, cancellationToken);
    }
    
}
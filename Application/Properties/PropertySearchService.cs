namespace Application;

public class PropertySearchService
{
    private readonly IPropertyProvider _propertyProvider;

    public PropertySearchService(IPropertyProvider propertyProvider)
    {
        _propertyProvider = propertyProvider;
    }

    public async Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {

        if (request.MinRooms > request.MaxRooms)
        { 
            throw new InvalidPropertySearchRequestException("The minimum rooms cannot be greater than the maximum rooms.");
        }

        if (request.MinPrice > request.MaxPrice)
        {
            throw new InvalidPropertySearchRequestException("The minimum price must be less than the maximum price.");
        }
        return await _propertyProvider.SearchAsync(request, cancellationToken);
    }
    
}
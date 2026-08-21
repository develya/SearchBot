namespace Application;

public interface IPropertySource
{
    Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest searchRequest, CancellationToken cancellationToken);
    
    //any of sources can perform searching and return collection of domains
}
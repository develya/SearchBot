namespace Application;

public interface IPropertyProvider
{
    Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken);
}
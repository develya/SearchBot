using Application;
using Application.Interfaces;
using Infrastructure.DomRia.Mapping;

namespace Infrastructure.DomRia;

public class DomRiaPropertyProvider : IPropertyProvider
{
    private readonly DomRiaClient _domRiaClient;
    private readonly DomRiaPropertyMapper _mapper;
    private readonly DomRiaPropertyEntityMapper _entityMapper;
    private readonly IPropertyRepository _propertyRepository;

    public DomRiaPropertyProvider(DomRiaClient domRiaClient, DomRiaPropertyMapper mapper, DomRiaPropertyEntityMapper entityMapper, IPropertyRepository propertyRepository)
    {
        _domRiaClient = domRiaClient;
        _mapper = mapper;
        _entityMapper = entityMapper;
        _propertyRepository = propertyRepository;
    }

    public async Task<IReadOnlyCollection<PropertyDto>> SearchAsync(PropertySearchRequest request, CancellationToken cancellationToken)
    {
        var properties = await _domRiaClient.GetSearchResultsAsync(request, cancellationToken);

        foreach (var property in properties)
        {
            var existingProperty = await _propertyRepository
                .GetByExternalIdAsync(property.RealtyId, cancellationToken);

            if (existingProperty is null)
            {
                var entity = _entityMapper.Map(property);

                await _propertyRepository.AddAsync(entity, cancellationToken);
            }
        }

        return properties.Select(_mapper.Map).ToArray();
    }
}

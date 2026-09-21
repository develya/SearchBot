using System.Linq.Expressions;
using Application.Common.Specifications;
using Domain;

namespace Application.Specifications;

public class PropertySearchSpecification : Specification<Property>
{
    private readonly PropertySearchRequest _request;
    
    public PropertySearchSpecification(PropertySearchRequest request)
    {
        _request = request;
    }
    public override Expression<Func<Property, bool>> ToExpression()
    {
        return property =>
            (!_request.CityId.HasValue || property.CityId == _request.CityId.Value) &&
            (!_request.MinPrice.HasValue || property.Price >= _request.MinPrice.Value) &&
            (!_request.MaxPrice.HasValue || property.Price <= _request.MaxPrice.Value) &&
            (!_request.MinRooms.HasValue || property.Rooms >= _request.MinRooms.Value) &&
            (!_request.MaxRooms.HasValue || property.Rooms <= _request.MaxRooms.Value) &&
            (!_request.MinFloor.HasValue || property.Floor >= _request.MinFloor.Value) &&
            (!_request.MaxFloor.HasValue || property.Floor <= _request.MaxFloor.Value);
    }
}
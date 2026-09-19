using Application;
using Domain;

namespace Infrastructure.DomRia.Mapping;

public class PropertyDtoMapper
{
    public PropertyDto Map(Property property)
    {
        return new PropertyDto
        {
            Id = property.ExternalId,
            Description = property.Description,
            Price = property.Price,
            Currency = property.Currency,
            Rooms = property.Rooms ?? 0,
            City = property.City,
            Street = property.Address,
            Floor = property.Floor,
            TotalFloors = property.TotalFloors ?? 0,
            Url = property.Url
        };
    }
}
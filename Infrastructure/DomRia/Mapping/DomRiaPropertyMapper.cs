using Application;
using Infrastructure.DomRia.Models;

namespace Infrastructure.DomRia.Mapping;

public class DomRiaPropertyMapper
{
    public PropertyDto Map(DomRiaProperty property)
    {
        return new PropertyDto
        {
            Id = property.RealtyId,
            City = property.CityName,
            Street = property.StreetName,
            BuildingNumber = property.BuildingNumber,
            Price = property.PriceTotal,
            Currency = property.Currency,
            Rooms = property.RoomsCount ?? 0,
            Description = property.Description,
            TotalFloors = property.FloorsCount ?? 0,
            Url = property.Url
        };
    }
}

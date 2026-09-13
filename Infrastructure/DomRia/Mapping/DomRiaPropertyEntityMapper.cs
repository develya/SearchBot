using Domain;
using Infrastructure.DomRia.Models;

namespace Infrastructure.DomRia.Mapping;

public class DomRiaPropertyEntityMapper
{
    public Property Map(DomRiaProperty property)
    {
        return new Property
        {
            ExternalId = property.RealtyId,
            Description = property.Description,
            Price = property.PriceTotal,
            Currency = property.Currency,
            Rooms = property.RoomsCount,
            City = property.CityName,
            Address = $"{property.StreetName}, {property.BuildingNumber}",
            TotalFloors = property.FloorsCount,
            Url = property.Url
        };
    }
}
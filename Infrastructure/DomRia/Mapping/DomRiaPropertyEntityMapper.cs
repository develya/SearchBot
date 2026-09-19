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
            Title = string.IsNullOrWhiteSpace(property.RealtyTypeName)
                ? $"{property.CityName}, {property.RoomsCount} кімн."
                : $"{property.RealtyTypeName}, {property.RoomsCount} кімн.",
            Description = property.Description,
            Price = property.PriceTotal,
            Currency = property.Currency,
            Rooms = property.RoomsCount,
            City = property.CityName,
            CityId = property.CityId,
            Address = $"{property.StreetName}, {property.BuildingNumber}",
            Floor = property.Floor,
            TotalFloors = property.FloorsCount,
            Url = property.Url
        };
    }
}
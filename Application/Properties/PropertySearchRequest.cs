namespace Application;

public class PropertySearchRequest
{
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinRooms { get; set; }
    public int? MaxRooms { get; set; }
    public int? CityId { get; set; }
    //public int?MinFloor { get; set; }
    //public int? MaxFloor { get; set; }
    //public bool? AllowedPet { get; set; }
   
}
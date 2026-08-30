namespace Domain;

public class Property
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Currency { get; set; }
    public int Rooms { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int TotalFloors { get; set; }
    //public bool AllowedPet { get; set; }
    public string Url {get; set;}
}
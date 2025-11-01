public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
}

public class HotelDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Rating { get; set; }
}
using System.ComponentModel.DataAnnotations;

public class HotelDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public double Rating { get; set; }
}
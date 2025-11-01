using System.Drawing;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private static readonly List<Hotel> hotels = new List<Hotel>
    {
        new Hotel { Id = 1, Name = "Hotel A", Address = "Address A", Rating = 4.5 },
        new Hotel { Id = 2, Name = "Hotel B", Address = "Address B", Rating = 4.0 },
        new Hotel { Id = 3, Name = "Hotel C", Address = "Address C", Rating = 3.5 }
    };

    [HttpGet]
    public IEnumerable<Hotel> GetHotels()
    {
        return hotels;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotel(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }
    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateHotel(HotelDto hotelDto)
    {
        var newHotel = new Hotel
        {
            Id = hotels.Max(h => h.Id) + 1,
            Name = hotelDto.Name,
            Address = hotelDto.Address,
            Rating = hotelDto.Rating
        };
        hotels.Add(newHotel);
        return CreatedAtAction(nameof(GetHotel), new { id = newHotel.Id }, newHotel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateHotel(int id, HotelDto hotelDto)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        hotel.Name = hotelDto.Name;
        hotel.Address = hotelDto.Address;
        hotel.Rating = hotelDto.Rating;
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        hotels.Remove(hotel);
        return NoContent();
    }
}
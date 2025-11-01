using System.Drawing;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private AppDbContext _db;
    public HotelsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Hotel>>> GetHotels()
    {
        var hotels = await _db.Hotels.ToListAsync();
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotelById(int id)
    {
        var hotel = await _db.Hotels.FindAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }

     [HttpPost]
    public async Task<IActionResult> CreateHotel(HotelDto model)
    {
        if (model == null) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var newHotel = new Hotel
        {
            Name = model.Name,
            Address = model.Address,
            Rating = model.Rating
        };
        _db.Hotels.Add(newHotel);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetHotelById),new {id = newHotel.Id},newHotel);
    }

}
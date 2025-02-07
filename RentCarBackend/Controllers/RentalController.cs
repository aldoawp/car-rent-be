using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarBackend.Data;
using RentCarBackend.Models;
using RentCarBackend.Models.Requests;
using RentCarBackend.Models.Results;
using RentCarBackend.Models.Results;

namespace RentCarBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : Controller
{
  private readonly AppDbContext _context;

  public RentalController(AppDbContext context)
  {
    _context = context;
  }

  // GET: As a checker on the rental ID making process
  [HttpGet("GetAllRentalID")]
  public async Task<ActionResult<List<string>>> Get()
  {
    var rentalIds = await _context.Rental
                    .Select(x => x.RentalID)
                    .ToListAsync();
                    
    if (rentalIds == null) {
      return NotFound("User is empty");
    }

    return Ok(rentalIds);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<IEnumerable<GetRentalResult>>> Get(string id)
  {
    var rentals = await _context.Rental
                    .Include(x => x.Customer)
                    .Include(x => x.Car)
                    .OrderByDescending(x => x.RentalDate)
                    .Where(x => x.CustomerID == id)
                    .Select(x => new GetRentalResult
                    {
                      RentalID = x.RentalID,
                      RentalDate = x.RentalDate,
                      ReturnDate = x.ReturnDate,
                      TotalPrice = x.TotalPrice,
                      PaymentStatus = x.PaymentStatus,
                      CustomerID = x.CustomerID,
                      CarID = x.CarID,
                      Customer = x.Customer,
                      Car = x.Car,
                    })
                    .ToListAsync();
    
    if (!rentals.Any())
    {
      return NotFound("Rental history not found.");
    }

    var response = new ApiResponse<IEnumerable<GetRentalResult>>
    {
      StatusCode = StatusCodes.Status200OK,
      RequestMethod = HttpContext.Request.Method,
      Payload = rentals,
    };

    return Ok(response);
  }


  [HttpPost]
  public async Task<ActionResult<ApiResponse<string>>> Post([FromBody] CreateRentalRequest createRentalRequest)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest();
    }

    var checkRental = await _context.Rental
                        .Include(x => x.Car)
                        .Where(x => x.CarID == createRentalRequest.CarID && x.Car.Status == true)
                        .FirstOrDefaultAsync();
    
    if (checkRental == null)
    {
      return NotFound("Car is not available to rent.");
    }

    var rental = new Rental
    {
      RentalID = createRentalRequest.RentalID,
      RentalDate = createRentalRequest.RentalDate,
      ReturnDate = createRentalRequest.ReturnDate,
      TotalPrice = createRentalRequest.TotalPrice,
      PaymentStatus = createRentalRequest.PaymentStatus,
      CustomerID = createRentalRequest.CustomerID,
      CarID = createRentalRequest.CarID,
    };
    
    _context.Add(rental);
 
    // Change the selected car status to unavailable
    var car = await _context.Car.FindAsync(createRentalRequest.CarID);
    if (car == null) return NotFound("Car is not found.");
    car.Status = false;

    await _context.SaveChangesAsync();

    var response = new ApiResponse<string>
    {
      StatusCode = StatusCodes.Status201Created,
      RequestMethod = HttpContext.Request.Method,
      Payload = "New rental successfuly created"
    };

    return Ok(response);
  }
}

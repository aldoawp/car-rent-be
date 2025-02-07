using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarBackend.Data;
using RentCarBackend.Models.Results;

namespace RentCarBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : Controller
{
  private readonly AppDbContext _context;

  public CarController(AppDbContext context) 
  {
    _context = context;
  }

  // GET: all car list
  [HttpGet("GetAllCars")]
  public async Task<ActionResult<IEnumerable<GetCarResult>>> Get (int year) 
  {
    var cars = _context.Car.AsQueryable();
    
    if (year != 0) cars = cars.Where(x => x.Year == year);

    var carResults = await cars
              .Select(x => new GetCarResult()
              {
                CarID = x.CarID,
                Name = x.Name ?? "Unknown",
                Model = x.Model ?? "Unknown",
                Year = x.Year,
                LicensePlate = x.LicensePlate ?? "Unknown",
                NumberOfCarSeats = x.NumberOfCarSeats,
                Transmission = x.Transmission ?? "Unknown",
                PricePerDay = x.PricePerDay,
                Status = x.Status,
              })
              .ToListAsync();
    
    if (cars == null || !cars.Any()) {
      return NotFound("No car found.");
    }

    var response = new ApiResponse<IEnumerable<GetCarResult>>
    {
      StatusCode = StatusCodes.Status200OK,
      RequestMethod = HttpContext.Request.Method,
      Payload = carResults,
    };

    return Ok(response);
  }

  // GET: all car list per page
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetCarResult>>> Get(int pageNumber, int pageContent, string sort, int year) 
  {
    var cars = _context.Car.AsQueryable();
    
    if (year != 0) cars = cars.Where(x => x.Year == year); 
    
    if (sort == "asc") {
      cars = cars.OrderBy(x => x.PricePerDay);
    } else if (sort == "desc") {
      cars = cars.OrderByDescending(x => x.PricePerDay);
    }

    var carResults = await cars.Skip((pageNumber - 1) * pageContent)
              .Take(pageContent)
              .Select(x => new GetCarResult()
              {
                CarID = x.CarID,
                Name = x.Name ?? "Unknown",
                Model = x.Model ?? "Unknown",
                Year = x.Year,
                LicensePlate = x.LicensePlate ?? "Unknown",
                NumberOfCarSeats = x.NumberOfCarSeats,
                Transmission = x.Transmission ?? "Unknown",
                PricePerDay = x.PricePerDay,
                Status = x.Status,
              })
              .ToListAsync();
    
    if (cars == null || !cars.Any()) {
      return NotFound("No car found.");
    }

    var response = new ApiResponse<IEnumerable<GetCarResult>>
    {
      StatusCode = StatusCodes.Status200OK,
      RequestMethod = HttpContext.Request.Method,
      Payload = carResults,
    };

    return Ok(response);
  }

  // GET: specific car
  [HttpGet("{id}")]
  public async Task<ActionResult<GetCarResult>> Get(string id)
  {
    var car = await _context.Car
                .OrderBy(x => x.PricePerDay)
                .Where(x => x.CarID == id)
                .Select(x => new GetCarResult()
                {
                  CarID = x.CarID,
                  Name = x.Name ?? "Unknown",
                  Model = x.Model ?? "Unknown",
                  Year = x.Year,
                  LicensePlate = x.LicensePlate ?? "Unknown",
                  NumberOfCarSeats = x.NumberOfCarSeats,
                  Transmission = x.Transmission ?? "Unknown",
                  PricePerDay = x.PricePerDay,
                  Status = x.Status,
                })
                .FirstOrDefaultAsync();

    if (car == null) {
      return NotFound("Car not found.");
    }

    var response = new ApiResponse<GetCarResult>
    {
      StatusCode = StatusCodes.Status200OK,
      RequestMethod = HttpContext.Request.Method,
      Payload = car,
    };

    return Ok(response);
  }
  
  [HttpGet("GetYears")]
  public async Task<ActionResult<IEnumerable<GetYearResult>>> GetYears()
  {
    var years = await _context.Car
                  .GroupBy(x => x.Year)
                  .Select(x => new GetYearResult()
                  {
                    Year = x.Key,
                  })
                  .ToListAsync();
    
    if (years == null)
    {
      return NotFound("Years not found.");
    }

    var response = new ApiResponse<IEnumerable<GetYearResult>>
    {
      StatusCode = StatusCodes.Status200OK,
      RequestMethod = HttpContext.Request.Method,
      Payload = years
    };

    return Ok(response);
  }
}

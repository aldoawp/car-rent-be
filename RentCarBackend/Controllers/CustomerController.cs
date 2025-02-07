using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCarBackend.Data;
using RentCarBackend.Models;
using RentCarBackend.Models.Requests;
using RentCarBackend.Models.Results;

namespace RentCarBackend.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
  private readonly AppDbContext _context;

  public CustomerController(AppDbContext context)
  {
    _context = context;
  }

  // GET: As a checker on the customer ID making process
  [HttpGet("GetAllCustomerID")]
  public async Task<ActionResult<List<string>>> Get()
  {
    var customerIds = await _context.Customer
                    .Select(x => x.CustomerID)
                    .ToListAsync();
                    
    if (customerIds == null) {
      return NotFound("User is empty");
    }

    return Ok(customerIds);
  }

  // Put: Login
  [HttpPut("Login")]
  public async Task<ActionResult<ApiResponse<GetCustomerResult>>> Put([FromBody] CreateLoginRequest loginRequest)
  {
    var customer = await _context.Customer
      .FirstOrDefaultAsync(x => x.Email == loginRequest.Email && x.Password == loginRequest.Password);

    if (customer == null)
    {
        return NotFound("Invalid email or password.");
    }

    var isAuthExist = await _context.Customer
                        .Where(x => x.IsAuthenticated == $"{loginRequest.Email}{loginRequest.Password}")
                        .ToListAsync();

    if (isAuthExist.Any())
    {
      return BadRequest("User is authenticated");
    }

    customer.IsAuthenticated = $"{loginRequest.Email}{loginRequest.Password}";

    await _context.SaveChangesAsync();

    var result = new GetCustomerResult
    {
      CustomerID = customer.CustomerID,
      Email = customer.Email,
      Name = customer.Name,
      Address = customer.Address,
      PhoneNumber = customer.PhoneNumber,
      DriverLicenseNumber = customer.DriverLicenseNumber,
      IsAuthenticated = customer.IsAuthenticated
    };

    var response = new ApiResponse<GetCustomerResult>
    {
        StatusCode = StatusCodes.Status202Accepted,
        RequestMethod = HttpContext.Request.Method,
        Payload = result,
    };

    return Ok(response);
  }

  // PUT: Logout
  [HttpPut("Logout")]
  public async Task<ActionResult<ApiResponse<string>>> Put([FromBody] CreateLogoutRequest logoutRequest)
  {
    var customer = await _context.Customer
      .FirstOrDefaultAsync(x => x.IsAuthenticated == logoutRequest.IsAuthenticated);

    if (customer == null)
    {
        return BadRequest("No user is authenticated");
    }

    customer.IsAuthenticated = "0";

    await _context.SaveChangesAsync();

    var response = new ApiResponse<string>
    {
        StatusCode = StatusCodes.Status202Accepted,
        RequestMethod = HttpContext.Request.Method,
        Payload = "Logout successful"
    };

    return Ok(response);
  }

  // POST: New customer user
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateCustomerRequest createCustomerRequest)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var checkCustomer = _context.Customer.Where(x => x.Email == createCustomerRequest.Email).Count();

    if (checkCustomer != 0)
    {
      return BadRequest("Customer already exist.");
    }

    var response = new ApiResponse<string>
    {
      StatusCode = StatusCodes.Status201Created,
      RequestMethod = HttpContext.Request.Method,
      Payload = "Successfuly created a customer",
    };

    var customer = new Customer
    {
      CustomerID = createCustomerRequest.CustomerID,
      Email = createCustomerRequest.Email,
      Name = createCustomerRequest.Name,
      Password = createCustomerRequest.Password,
      Address = createCustomerRequest.Address,
      PhoneNumber= createCustomerRequest.PhoneNumber,
      DriverLicenseNumber = createCustomerRequest.DriverLicenseNumber,
      IsAuthenticated = createCustomerRequest.IsAuthenticated,
    };

    _context.Add(customer);
    await _context.SaveChangesAsync();

    return Ok(response);
  }
}



// GET: Specific customer user
  // [HttpGet]
  // public async Task<ActionResult<GetCustomerResult>> Get(string customerID)
  // {
  //   var customer = await _context.Customer
  //                   .Where(x => x.CustomerID == customerID)
  //                   .Select(x => new GetCustomerResult
  //                   {
  //                     Email = x.Email,
  //                     Name = x.Name,
  //                     PhoneNumber = x.PhoneNumber,
  //                     Address = x.Address,
  //                     DriverLicenseNumber = x.DriverLicenseNumber,
  //                   })
  //                   .FirstOrDefaultAsync();
                    
  //   if (customer == null) {
  //     return NotFound("User not found.");
  //   }

  //   return Ok(customer);
  // }
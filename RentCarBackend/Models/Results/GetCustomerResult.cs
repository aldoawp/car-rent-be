using System;

namespace RentCarBackend.Models.Results;

public class GetCustomerResult
{
  public string CustomerID { get; set; }
  public string Email { get; set; }
  public string Name { get; set; }
  public string? PhoneNumber { get; set; }
  public string? Address { get; set; }
  public string? DriverLicenseNumber { get; set; }
  public string? IsAuthenticated { get; set; }
}

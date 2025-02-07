using System;
using System.ComponentModel.DataAnnotations;

namespace RentCarBackend.Models.Requests;

public class CreateCustomerRequest
{
  [Required]
  public string CustomerID { get; set; }

  [Required]
  public string Email { get; set; }

  [Required]
  public string Password { get; set; }

  [Required]
  public string Name { get; set; }

  public string PhoneNumber { get; set; }

  public string Address { get; set; }

  public string DriverLicenseNumber { get; set; }

  public string IsAuthenticated { get; set; }
}

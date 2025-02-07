using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace RentCarBackend.Models;

[Table("MsCustomer")]
public class Customer
{
  [Key]
  [MaxLength(36)]
  [Column("Customer_id")]
  public string CustomerID { get; set; }

  [MaxLength(100)]
  public string Email { get; set; }

  [MaxLength(100)]
  public string Password { get; set; }

  [MaxLength(200)]
  public string Name { get; set; }

  [MaxLength(50)]
  [Column("phone_number")]
  public string? PhoneNumber { get; set; }

  [MaxLength(500)]
  public string? Address { get; set; }
  
  [MaxLength(100)]
  [Column("driver_license_number")]
  public string? DriverLicenseNumber { get; set; }

  [MaxLength(1000)]
  [Column("is_authenticated")]
  public string IsAuthenticated { get; set; }
}

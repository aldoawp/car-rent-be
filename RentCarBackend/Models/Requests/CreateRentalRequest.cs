using System;
using System.ComponentModel.DataAnnotations;

namespace RentCarBackend.Models.Requests;

public class CreateRentalRequest
{
  [Required]
  public string RentalID { get; set; }

  [Required]
  public DateTime RentalDate { get; set; }

  [Required]
  public DateTime ReturnDate { get; set; }

  [Required]
  public decimal TotalPrice { get; set; }

  [Required]
  public bool PaymentStatus { get; set; }

  [Required]
  public string CustomerID { get; set; }

  [Required]
  public string CarID { get; set; }
}

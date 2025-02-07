using System;

namespace RentCarBackend.Models.Results;

public class GetRentalResult
{
  public string RentalID { get; set; }
  public DateTime RentalDate { get; set; }
  public DateTime ReturnDate { get; set; }
  public decimal TotalPrice { get; set; }
  public bool PaymentStatus { get; set; }
  public string CustomerID { get; set; }
  public string CarID { get; set; }
  public Customer Customer { get; set; }
  public Car Car { get; set; }
}

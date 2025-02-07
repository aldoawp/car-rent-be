using System;

namespace RentCarBackend.Models.Results;

public class GetCarResult
{
  public string CarID { get; set; }
  public string? Name { get; set; }
  public string? Model { get; set; }
  public int? Year { get; set; }
  public string? LicensePlate { get; set; }
  public int? NumberOfCarSeats { get; set; }
  public string? Transmission { get; set; }
  public decimal? PricePerDay { get; set; }
  public bool? Status { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarBackend.Models;

[Table("MsCar")]
public class Car
{ 
  [Key]
  [MaxLength(36)]
  [RegularExpression("CAR[0-9][0-9][0-9]")]
  [Column("Car_id")]
  public string CarID { get; set; }

  [MaxLength(200)]
  public string Name { get; set; }

  [MaxLength(100)]
  public string Model { get; set; }

  public int Year { get; set; }

  [MaxLength(50)]
  [Column("license_plate")]
  public string LicensePlate { get; set; }

  [Column("number_of_car_seats")]
  public int NumberOfCarSeats { get; set; }

  [MaxLength(100)]
  public string Transmission { get; set; }

  [Column("price_per_day")]
  public decimal PricePerDay { get; set; }

  public bool Status { get; set; }
}

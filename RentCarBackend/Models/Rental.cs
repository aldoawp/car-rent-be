using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarBackend.Models;

[Table("TrRental")]
public class Rental
{
  [Key]
  [MaxLength(36)]
  [Column("Rental_id")]
  public string RentalID { get; set; }

  [Column("rental_date")]
  public DateTime RentalDate { get; set; }

  [Column("return_date")]
  public DateTime ReturnDate { get; set; }

  [Column("total_price")]
  public decimal TotalPrice { get; set; }

  [Column("payment_status")]
  public bool PaymentStatus { get; set; }

  [MaxLength(36)]
  [ForeignKey(nameof(Customer))]
  [Column("customer_id")]
  public string CustomerID { get; set; }

  [MaxLength(36)]
  [ForeignKey(nameof(Car))]
  [Column("Car_id")]
  public string CarID { get; set; }

  public Customer Customer { get; set; }

  public Car Car { get; set; }
}

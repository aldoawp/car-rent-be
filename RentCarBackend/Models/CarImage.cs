using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCarBackend.Models;

[Table("MsCarImages")]
public class CarImage
{
  [Key]
  [MaxLength(36)]
   public string ImageCarID { get; set; }

  [ForeignKey(nameof(Car))]
  [MaxLength(36)]
  public string CarID { get; set; }

  [MaxLength(2000)]
  public string ImageLink { get; set; }

  public Car Car { get; set; }
}

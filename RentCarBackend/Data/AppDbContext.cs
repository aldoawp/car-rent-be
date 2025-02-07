using System;
using Microsoft.EntityFrameworkCore;
using RentCarBackend.Models;

namespace RentCarBackend.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  public DbSet<Car> Car { get; set; }
  public DbSet<CarImage> CarImage { get; set; }
  public DbSet<Customer> Customer { get; set; }
  public DbSet<Rental> Rental { get; set; }
} 

using System;

namespace RentCarBackend.Models.Requests;

public class CreateLoginRequest
{
  public string Email { get; set; }
  public string Password { get; set; }
}

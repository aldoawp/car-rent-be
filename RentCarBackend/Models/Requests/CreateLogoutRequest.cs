using System;

namespace RentCarBackend.Models.Requests;

public class CreateLogoutRequest
{
  public string IsAuthenticated { get; set; }
}

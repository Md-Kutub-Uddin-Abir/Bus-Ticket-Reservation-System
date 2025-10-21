using System;

namespace Application.Contracts.DTOs;

public class CreateBusDto
{
    public string CompanyName { get; set; } = string.Empty;
    public string BusName { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public int TotalSeats { get; set; }
}

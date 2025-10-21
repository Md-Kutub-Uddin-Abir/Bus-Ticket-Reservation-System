using System;

namespace Domain.Entities;

public class Bus
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string BusName { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public int TotalSeats { get; set; }
}

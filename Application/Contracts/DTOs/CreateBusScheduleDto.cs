using System;

namespace Application.Contracts.DTOs;

public class CreateBusScheduleDto
{
    public int BusId { get; set; }
    public DateTime JourneyDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public decimal Price { get; set; }
}

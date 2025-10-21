using System;

namespace Domain.Entities;

public class BusSchedule
{
    public int Id { get; set; }
    public int BusId { get; set; }
    public Bus? Bus { get; set; }   // Relation (EF Core Navigation)
    public DateTime JourneyDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public decimal Price { get; set; }
}

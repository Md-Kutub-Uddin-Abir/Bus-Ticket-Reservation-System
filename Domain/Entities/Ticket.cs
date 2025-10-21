using System;

namespace Domain.Entities;

public enum SeatStatus
{
    Available,
    Booked,
    Sold
}

public class Ticket
{
    public int Id { get; set; }
    public int BusScheduleId { get; set; }
    public BusSchedule? BusSchedule { get; set; }  // Relation
    public int SeatNo { get; set; }
    public string PassengerName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public SeatStatus Status { get; set; } = SeatStatus.Booked;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


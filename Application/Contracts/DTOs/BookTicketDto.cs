using System;

namespace Application.Contracts.DTOs;

public class BookTicketDto
{
    public int BusScheduleId { get; set; }
    public List<int> SeatNumbers { get; set; } = new();
    public string PassengerName { get; set; } = string.Empty;
    public string PassengerMobile { get; set; } = string.Empty;
}

using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class BusService : IBusService
{
    private readonly IBusRepository _busRepository;
    private readonly ITicketRepository _ticketRepository;

    public BusService(IBusRepository busRepository, ITicketRepository ticketRepository)
    {
        _busRepository = busRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<Bus> CreateBusAsync(CreateBusDto dto)
    {
        var bus = new Bus
        {
            CompanyName = dto.CompanyName,
            BusName = dto.BusName,
            From = dto.From,
            To = dto.To,
            TotalSeats = dto.TotalSeats
        };

        return await _busRepository.AddBusAsync(bus);
    }

    public async Task<BusSchedule> CreateBusScheduleAsync(CreateBusScheduleDto dto)
    {
        var bus = await _busRepository.GetBusByIdAsync(dto.BusId);
        if (bus == null)
            throw new Exception("Bus not found.");

        var schedule = new BusSchedule
        {
            BusId = bus.Id,
            JourneyDate = dto.JourneyDate.ToUniversalTime(),
            StartTime = dto.StartTime,
            ArrivalTime = dto.ArrivalTime,
            Price = dto.Price
        };

        //  Schedule save
        var createdSchedule = await _busRepository.AddScheduleAsync(schedule);

        // seat generating
        for (int i = 1; i <= bus.TotalSeats; i++)
        {
            var ticket = new Ticket
            {
                BusScheduleId = createdSchedule.Id,
                SeatNo = i,
                Status = SeatStatus.Available,
                PassengerName = string.Empty,
                Mobile = string.Empty,
                CreatedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddTicketAsync(ticket);
        }

        return createdSchedule;
    }
}

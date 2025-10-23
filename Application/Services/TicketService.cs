using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IBusRepository _busRepository;

    public TicketService(ITicketRepository ticketRepository, IBusRepository busRepository)
    {
        _ticketRepository = ticketRepository;
        _busRepository = busRepository;
    }

    
    public async Task<List<Ticket>> BookTicketAsync(BookTicketDto dto)
    {
        var schedule = await _busRepository.GetBusByIdAsync(dto.BusScheduleId);
        if (schedule == null)
            throw new Exception("Bus Schedule not found.");

        var bookedTickets = new List<Ticket>();

        foreach (var seatNo in dto.SeatNumbers)
        {
            var ticket = await _ticketRepository.GetTicketBySeatAsync(dto.BusScheduleId, seatNo);
            if (ticket == null)
                throw new Exception($"Seat {seatNo} not found.");

            if (ticket.Status == SeatStatus.Booked)
                throw new Exception($"Seat {seatNo} is already booked.");

            ticket.Status = SeatStatus.Booked;
            ticket.PassengerName = dto.PassengerName;
            ticket.Mobile = dto.PassengerMobile;

            await _ticketRepository.UpdateTicketAsync(ticket);
            bookedTickets.Add(ticket);
        }

        return bookedTickets;
    }

    
    public async Task<bool> CancelTicketAsync(int ticketId)
    {
        var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
        if (ticket == null)
            return false;

        if (ticket.Status != SeatStatus.Booked)
            throw new Exception("Seat is not booked yet.");

        ticket.Status = SeatStatus.Available;
        ticket.PassengerName = string.Empty;
        ticket.Mobile = string.Empty;

        await _ticketRepository.UpdateTicketAsync(ticket);
        return true;
    }
}

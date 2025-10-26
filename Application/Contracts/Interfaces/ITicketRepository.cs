using System;

using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface ITicketRepository
{
    Task<Ticket?> GetTicketBySeatAsync(int busScheduleId, int seatNo);
    Task<Ticket?> GetTicketByIdAsync(int ticketId);
    Task<List<Ticket>> GetTicketsByScheduleAsync(int busScheduleId);
    Task UpdateTicketAsync(Ticket ticket);
    Task AddTicketAsync(Ticket ticket);
    Task<List<Ticket>> GetTicketsByScheduleIdAsync(int busScheduleId);


}


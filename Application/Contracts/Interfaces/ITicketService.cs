using System;

using Application.Contracts.DTOs;
using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface ITicketService
{
    Task<Ticket> BookTicketAsync(BookTicketDto dto);
    Task<bool> CancelTicketAsync(int ticketId);
}


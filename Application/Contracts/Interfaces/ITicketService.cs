using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts.DTOs;
using Domain.Entities;

namespace Application.Contracts.Interfaces
{
    public interface ITicketService
    {

        Task<List<Ticket>> BookTicketAsync(BookTicketDto dto);

        Task<bool> CancelTicketAsync(int ticketId);
        
        Task<List<Ticket>> GetSeatsByScheduleIdAsync(int busScheduleId);

    }
}

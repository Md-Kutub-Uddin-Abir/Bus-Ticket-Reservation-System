using System;

using Application.Contracts.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetTicketBySeatAsync(int busScheduleId, int seatNo)
    {
        return await _context.Tickets
            .FirstOrDefaultAsync(t => t.BusScheduleId == busScheduleId && t.SeatNo == seatNo);
    }

    public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
    {
        return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
    }

    public async Task<List<Ticket>> GetTicketsByScheduleAsync(int busScheduleId)
    {
        return await _context.Tickets
            .Where(t => t.BusScheduleId == busScheduleId)
            .ToListAsync();
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }
    public async Task AddTicketAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Ticket>> GetTicketsByScheduleIdAsync(int busScheduleId)
    {
        return await _context.Tickets
            .Where(t => t.BusScheduleId == busScheduleId)
            .OrderBy(t => t.SeatNo)
            .ToListAsync();
    }


}

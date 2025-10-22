using Application.Contracts.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BusRepository : IBusRepository
{
    private readonly ApplicationDbContext _context;

    public BusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public async Task<Bus> AddBusAsync(Bus bus)
    {
        _context.Buses.Add(bus);
        await _context.SaveChangesAsync();
        return bus;
    }

    public async Task<Bus?> GetBusByIdAsync(int id)
    {
        return await _context.Buses.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<BusSchedule> AddScheduleAsync(BusSchedule schedule)
    {
        _context.BusSchedules.Add(schedule);
        await _context.SaveChangesAsync();
        return schedule;
    }

    //  SEARCH methods
    public async Task<List<BusSchedule>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate)
    {
        return await _context.BusSchedules
            .Include(bs => bs.Bus)
            .Where(bs => bs.Bus.From.ToLower() == from.ToLower()
                      && bs.Bus.To.ToLower() == to.ToLower()
                      && bs.JourneyDate.Date == journeyDate.Date)
            .ToListAsync();
    }

    public async Task<int> CountBookedSeatsAsync(int busScheduleId)
    {
        return await _context.Tickets
            .CountAsync(t => t.BusScheduleId == busScheduleId && t.Status == SeatStatus.Booked);
    }
}

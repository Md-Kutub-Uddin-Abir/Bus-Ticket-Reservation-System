using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface IBusRepository
{
     //Search
    Task<List<BusSchedule>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate);
    Task<int> CountBookedSeatsAsync(int busScheduleId);

    //Create
    Task<Bus> AddBusAsync(Bus bus);
    Task<Bus?> GetBusByIdAsync(int id);
    Task<BusSchedule> AddScheduleAsync(BusSchedule schedule);
}

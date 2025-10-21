using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class SearchService : ISearchService
{
    private readonly IBusRepository _repository;

    public SearchService(IBusRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AvailableBusDto>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate)
    {
        var schedules = await _repository.SearchAvailableBusesAsync(from, to, journeyDate);
        var result = new List<AvailableBusDto>();

        foreach (var s in schedules)
        {
            var bookedSeats = await _repository.CountBookedSeatsAsync(s.Id);

            result.Add(new AvailableBusDto
            {
                BusScheduleId = s.Id,
                CompanyName = s.Bus!.CompanyName,
                BusName = s.Bus.BusName,
                StartTime = s.StartTime,
                ArrivalTime = s.ArrivalTime,
                SeatsLeft = s.Bus.TotalSeats - bookedSeats,
                Price = s.Price
            });
        }

        return result;
    }
}

using System;

using Application.Contracts.DTOs;
using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface IBusService
{
    Task<Bus> CreateBusAsync(CreateBusDto dto);
    Task<BusSchedule> CreateBusScheduleAsync(CreateBusScheduleDto dto);
}

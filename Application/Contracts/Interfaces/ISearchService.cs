using System;

using Application.Contracts.DTOs;

namespace Application.Contracts.Interfaces;

public interface ISearchService
{
    Task<List<AvailableBusDto>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate);
}

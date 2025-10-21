using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusController : ControllerBase
{
    private readonly IBusService _busService;

    public BusController(IBusService busService)
    {
        _busService = busService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBus([FromBody] CreateBusDto dto)
    {
        var bus = await _busService.CreateBusAsync(dto);
        return Ok(bus);
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateBusScheduleDto dto)

    {
        var schedule = await _busService.CreateBusScheduleAsync(dto);
        return Ok(schedule);
    }
}

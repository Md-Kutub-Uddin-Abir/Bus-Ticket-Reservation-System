using Microsoft.AspNetCore.Http;
using Application.Contracts.DTOs;
using Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    //  POST /api/ticket/book
    [HttpPost("book")]
    public async Task<IActionResult> BookTicket([FromBody] BookTicketDto dto)
    {
        var ticket = await _ticketService.BookTicketAsync(dto);
        return Ok(ticket);
    }

    //  PUT /api/ticket/cancel/{ticketId}
    [HttpPut("cancel/{ticketId}")]
    public async Task<IActionResult> CancelTicket(int ticketId)
    {
        var result = await _ticketService.CancelTicketAsync(ticketId);
        if (!result)
            return NotFound(new { message = "Ticket not found" });

        return Ok(new { message = "Ticket cancelled successfully" });
    }
}

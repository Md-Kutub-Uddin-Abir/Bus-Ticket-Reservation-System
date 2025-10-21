using Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }


    /// Search available buses by from, to, and journey date
    
 
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string from, [FromQuery] string to, [FromQuery] DateTime journeyDate)
    {
        if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
        {
            return BadRequest("From and To are required fields.");
        }

        var result = await _searchService.SearchAvailableBusesAsync(from, to, journeyDate);

        if (result == null || result.Count == 0)
        {
            return NotFound("No buses found for your search criteria.");
        }

        return Ok(result);
    }
}

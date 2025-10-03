using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    private readonly GalaxyMapContext _context;
    private readonly ILogger<SystemController> _logger;

    public SystemController(ILogger<SystemController> logger, GalaxyMapContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("instance/{instanceId}/date/{date}/system/{systemId}")]
    public async Task<ActionResult<Models.Info.System>> Get(
        string instanceId,
        int date,
        string systemId
    )
    {
        // Adjust the date to be in the middle of the year
        date += (368 / 2);
        Data.SystemsRepository systemsRepo = new Data.SystemsRepository(_context.Systems);
        Models.System? system =
            await systemsRepo.GetSystemForInstanceDateWithOrbitingBodyGovernments(
                instanceId,
                date,
                systemId
            );
        if (system == null)
        {
            return NotFound();
        }
        return new Models.Info.System(system, new Date(date));
    }
}

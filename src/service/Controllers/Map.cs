using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Controllers;

public struct Planet
{
    public Planet(string name, int x, int y, string color, int focusLevel)
    {
        Name = name;
        X = x;
        Y = y;
        Color = color;
        FocusLevel = focusLevel;
    }

    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Color { get; set; }
    public int FocusLevel { get; set; }
}

[ApiController]
[Route("[controller]")]
public class MapController : ControllerBase
{
    private readonly GalaxyMapContext _context;
    private readonly ILogger<MapController> _logger;

    public MapController(ILogger<MapController> logger, GalaxyMapContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("instance/{instanceId}/date/{date}")]
    public async Task<ActionResult<Models.Map.Map>> Get(string instanceId, int date)
    {
        // Adjust the date to be in the middle of the year
        date += (368 / 2);
        Data.SystemsRepository systemsRepo = new Data.SystemsRepository(_context.Systems);
        Data.SpacelaneSegmentsRepository segmentsRepo = new Data.SpacelaneSegmentsRepository(
            _context.SpacelaneSegments
        );
        List<Models.System> systems =
            await systemsRepo.GetAllSystemsForInstanceDateWithPlanetGovernments(instanceId, date);
        List<Models.SpacelaneSegment> spacelanes =
            await segmentsRepo.GetSpacelaneSegmentsForInstanceDateWithSpacelaneOriginDestination(
                instanceId,
                date
            );
        Models.Map.Map data = new Models.Map.Map(systems, spacelanes);
        return data;
    }
}

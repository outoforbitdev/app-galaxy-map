using GalaxyMapSiteApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Controllers;

public struct Planet{
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

    [HttpGet("{instanceId}")]
    public async Task<ActionResult<Models.Map.Map>> Get(string instanceId)
    {
        List<Models.System> systems = await _context
            .Systems.Where(s => s.InstanceId == instanceId)
            .Include(s => s.Planets)
            .ThenInclude(p => p.ParentGovernments)
            .ThenInclude(p => p.Government)
            .ToListAsync();
        List<Models.SpacelaneSegment> spacelanes = await _context
            .SpacelaneSegments.Where(s => s.InstanceId == instanceId)
            .Include(spacelane => spacelane.Spacelane)
            .Include(spacelane => spacelane.Origin)
            .Include(spacelane => spacelane.Destination)
            .ToListAsync();
        Models.Map.Map data = new Models.Map.Map(systems, spacelanes);
        return data;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalaxyMapSiteApi.Data;

namespace GalaxyMapSiteApi.Controllers;

public struct Planet{
    public Planet(string name, int x, int y, string color, int focusLevel){
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
public class PlanetsController : ControllerBase
{
    private readonly GalaxyMapContext _context;
    private readonly ILogger<PlanetsController> _logger;

    public PlanetsController(ILogger<PlanetsController> logger, GalaxyMapContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetPlanets")]
    public async Task<ActionResult<IEnumerable<Models.Map.System>>> Get()
    {
        List<Models.System> systems = 
            await _context.Systems
                .Include(s => s.Planets)
                    .ThenInclude(p => p.ParentGovernments)
                        .ThenInclude(p => p.Government)
                .ToListAsync();
        List<Models.Map.System> data = systems.ConvertAll(s => new Models.Map.System(s));
        List<Models.Planet> planets = await _context.Planets.ToListAsync();
        // planetList.Sort((a, b) => a.FocusLevel - b.FocusLevel);
        return data;
    }
}

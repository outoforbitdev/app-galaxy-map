using GalaxyMapSiteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Data;

public class SystemsRepository
{
    public DbSet<Models.System> Systems { get; private set; }

    public SystemsRepository(DbSet<Models.System> systems)
    {
        Systems = systems;
    }

    public async Task<List<Models.System>> GetAllSystemsForInstanceWithPlanetGovernments(
        string instanceId
    )
    {
        return await Systems
            .Where(s => s.InstanceId == instanceId)
            .Include(s => s.Planets)
            .ThenInclude(p => p.ParentGovernments)
            .ThenInclude(p => p.Government)
            .ToListAsync();
    }

    public async Task<List<Models.System>> GetAllSystemsForInstanceDateWithPlanetGovernments(
        string instanceId,
        int date
    )
    {
        return await Systems
            .Where(s =>
                s.InstanceId == instanceId
                && (s.StartDate == null || s.StartDate < new Date(date))
                && (s.EndDate == null || s.EndDate > new Date(date))
            )
            .Include(s => s.Planets)
            .ThenInclude(p => p.ParentGovernments.Where(pg => pg.Government.Id == "red"))
            .ThenInclude(p => p.Government)
            .ToListAsync();
    }

    public async Task<Models.System?> GetSystemByName(string instanceId, string name)
    {
        return await Systems
            .Where(s =>
                s.InstanceId == instanceId
                && (s.Name == name || s.OtherNames != null && s.OtherNames.Contains(name))
            )
            .FirstOrDefaultAsync();
    }
}

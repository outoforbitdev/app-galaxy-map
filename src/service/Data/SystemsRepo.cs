using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Data;

public class SystemsRepo
{
    public DbSet<Models.System> Systems { get; private set; }

    public SystemsRepo(DbSet<Models.System> systems)
    {
        Systems = systems;
    }

    public async Task<List<Models.System>> GetAllSystemsForInstanceWithPlanetsAndGovernments(
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
}

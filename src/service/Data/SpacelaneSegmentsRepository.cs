using GalaxyMapSiteApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Data;

public class SpacelaneSegmentsRepository
{
    public DbSet<Models.SpacelaneSegment> SpacelaneSegments { get; private set; }

    public SpacelaneSegmentsRepository(DbSet<Models.SpacelaneSegment> spacelaneSegments)
    {
        SpacelaneSegments = spacelaneSegments;
    }

    public async Task<
        List<Models.SpacelaneSegment>
    > GetAllSpacelaneSegmentsForInstanceWithSpacelaneOriginDestination(string instanceId)
    {
        return await SpacelaneSegments
            .Where(s => s.InstanceId == instanceId)
            .Include(s => s.Spacelane)
            .Include(s => s.Origin)
            .Include(s => s.Destination)
            .ToListAsync();
    }

    public async Task<
        List<Models.SpacelaneSegment>
    > GetAllSpacelaneSegmentsForInstanceDateWithSpacelaneOriginDestination(
        string instanceId,
        int date
    )
    {
        return await SpacelaneSegments
            .Where(s =>
                s.InstanceId == instanceId
                && (s.Origin.StartDate == null || s.Origin.StartDate < new Date(date))
                && (s.Origin.EndDate == null || s.Origin.EndDate > new Date(date))
                && (s.Destination.StartDate == null || s.Destination.StartDate < new Date(date))
                && (s.Destination.EndDate == null || s.Destination.EndDate > new Date(date))
            )
            .Include(s => s.Spacelane)
            .Include(s => s.Origin)
            .ThenInclude(s => s.Planets)
            .ThenInclude(p => p.Governments)
            .ThenInclude(g => g.Organization)
            .ThenInclude(o => o.ParentOrganizationRelationships)
            .ThenInclude(por => por.Parent)
            .Include(s => s.Destination)
            .ThenInclude(s => s.Planets)
            .ThenInclude(p => p.Governments)
            .ThenInclude(g => g.Organization)
            .ThenInclude(o => o.ParentOrganizationRelationships)
            .ThenInclude(por => por.Parent)
            .ToListAsync();
    }
}

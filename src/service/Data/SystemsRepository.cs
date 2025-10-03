using System.Collections.Generic;
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

    public async Task<List<Models.System>> GetAllSystemsForInstanceWithOrbitingBodyGovernments(
        string instanceId
    )
    {
        return await Systems
            .Where(s => s.InstanceId == instanceId)
            .Include(s => s.OrbitingBodies)
            .ThenInclude(p => p.Governments)
            .ThenInclude(g => g.Organization)
            .ThenInclude(o => o.ParentOrganizations)
            .ToListAsync();
    }

    public async Task<List<Models.System>> GetAllSystemsForInstanceDateWithOrbitingBodyGovernments(
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
            .Include(s =>
                s.OrbitingBodies.Where(p =>
                    (p.StartDate == null || p.StartDate < new Date(date))
                    && (p.EndDate == null || p.EndDate > new Date(date))
                )
            )
            .ThenInclude(p =>
                p.Governments.Where(g =>
                    (g.StartDate == null || g.StartDate < new Date(date))
                    && (g.EndDate == null || g.EndDate > new Date(date))
                )
            )
            .ThenInclude(g => g.Organization)
            .ThenInclude(o =>
                o.ParentOrganizationRelationships.Where(por =>
                    (por.StartDate == null || por.StartDate < new Date(date))
                    && (por.EndDate == null || por.EndDate > new Date(date))
                    && por.Parent.OrganizationType == OrganizationType.Government
                )
            )
            .ThenInclude(por => por.Parent)
            .ToListAsync();
    }

    public async Task<Models.System?> GetSystemForInstanceDateWithOrbitingBodyGovernments(
        string instanceId,
        int date,
        string systemId
    )
    {
        List<Models.System> systems = await Systems
            .Where(s => s.InstanceId == instanceId && s.Id == systemId)
            .Include(s =>
                s.OrbitingBodies.Where(p =>
                    (p.StartDate == null || p.StartDate < new Date(date))
                    && (p.EndDate == null || p.EndDate > new Date(date))
                )
            )
            .ThenInclude(p =>
                p.Governments.Where(g =>
                    (g.StartDate == null || g.StartDate < new Date(date))
                    && (g.EndDate == null || g.EndDate > new Date(date))
                )
            )
            .ThenInclude(g => g.Organization)
            .ThenInclude(o =>
                o.ParentOrganizationRelationships.Where(por =>
                    (por.StartDate == null || por.StartDate < new Date(date))
                    && (por.EndDate == null || por.EndDate > new Date(date))
                    && por.Parent.OrganizationType == OrganizationType.Government
                )
            )
            .ThenInclude(por => por.Parent)
            .ToListAsync();
        return systems.Count > 0 ? systems[0] : null;
    }
}

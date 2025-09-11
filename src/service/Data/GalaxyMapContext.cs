using System.Collections.Generic;
using EFCore.NamingConventions;
using GalaxyMapSiteApi.Models;
using GalaxyMapSiteApi.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Data;

public class GalaxyMapContext : DbContext
{
    public GalaxyMapContext(DbContextOptions<GalaxyMapContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSnakeCaseNamingConvention();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Date>().HaveConversion<DateConverter>();
        configurationBuilder
            .Properties<OrganizationType>()
            .HaveConversion<EnumConverter<OrganizationType>>();
        configurationBuilder.Properties<MapColor>().HaveConversion<EnumConverter<MapColor>>();
        configurationBuilder.Properties<FocusLevel>().HaveConversion<EnumConverter<FocusLevel>>();
    }

    public DbSet<Models.System> Systems { get; set; }
    public DbSet<Models.Spacelane> Spacelanes { get; set; }
    public DbSet<Models.SpacelaneSegment> SpacelaneSegments { get; set; }
    public DbSet<Models.OrbitingBody> OrbitingBodies { get; set; }
    public DbSet<Models.Government> Governments { get; set; }
    public DbSet<Models.OrganizationOrganization> OrganizationOrganizations { get; set; }
    public DbSet<Models.Instance> Instances { get; set; }
    public DbSet<Models.Calendar> Calendars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Organization Relationships
        modelBuilder
            .Entity<Models.Organization>()
            .HasMany(o => o.ParentOrganizationRelationships)
            .WithOne(o => o.Child);
        modelBuilder
            .Entity<Models.Organization>()
            .HasMany(o => o.ChildOrganizationRelationships)
            .WithOne(o => o.Parent);
        #endregion Organization Relationships
        #region OrbitingBody-Government Relationships
        modelBuilder
            .Entity<Models.OrbitingBody>()
            .HasMany(p => p.Governments)
            .WithMany(g => g.OrbitingBodies)
            .UsingEntity<Models.OrbitingBodyGovernment>(
                j =>
                    j.HasOne(pg => pg.Parent)
                        .WithMany()
                        .HasForeignKey(pg => new { pg.InstanceId, pg.ParentId }),
                j =>
                    j.HasOne(pg => pg.Child)
                        .WithMany()
                        .HasForeignKey(pg => new { pg.InstanceId, pg.ChildId }),
                j =>
                {
                    j.ToTable("orbiting_body_governments");
                    j.HasKey(pg => new
                    {
                        pg.InstanceId,
                        pg.ChildId,
                        pg.ParentId,
                    });
                }
            );
        #endregion OrbitingBody-Government Relationships
    }
}

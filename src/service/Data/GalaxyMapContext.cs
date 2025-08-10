using EFCore.NamingConventions;
using GalaxyMapSiteApi.Models;
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
    }

    public DbSet<Models.System> Systems { get; set; }
    public DbSet<Models.Spacelane> Spacelanes { get; set; }
    public DbSet<Models.SpacelaneSegment> SpacelaneSegments { get; set; }
    public DbSet<Models.Planet> Planets { get; set; }
    public DbSet<Models.Government> Governments { get; set; }
    public DbSet<Models.GovernmentGovernment> GovernmentGovernments { get; set; }
    public DbSet<Models.Instance> Instances { get; set; }
    public DbSet<Models.Calendar> Calendars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Models.Government>()
            .HasMany(g => g.ParentGovernments)
            .WithMany(g => g.ChildGovernments)
            .UsingEntity<Models.GovernmentGovernment>(
                j =>
                    j.HasOne(gg => gg.Parent)
                        .WithMany()
                        .HasForeignKey(gg => new { gg.InstanceId, gg.ParentId }),
                j =>
                    j.HasOne(gg => gg.Child)
                        .WithMany()
                        .HasForeignKey(gg => new { gg.InstanceId, gg.ChildId }),
                j =>
                {
                    j.ToTable("government_governments");
                    j.HasKey(gg => new
                    {
                        gg.InstanceId,
                        gg.ChildId,
                        gg.ParentId,
                    });
                }
            );

        modelBuilder
            .Entity<Models.Planet>()
            .HasMany(p => p.Governments)
            .WithMany(g => g.Planets)
            .UsingEntity<Models.PlanetGovernment>(
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
                    j.ToTable("planet_governments");
                    j.HasKey(pg => new
                    {
                        pg.InstanceId,
                        pg.ChildId,
                        pg.ParentId,
                    });
                }
            );
    }
}

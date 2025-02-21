using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Data;

public class GalaxyMapContext : DbContext {
    public GalaxyMapContext(DbContextOptions<GalaxyMapContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
    public DbSet<Models.System> Systems { get; set;}
    public DbSet<Models.Spacelane> Spacelanes { get; set;}
    public DbSet<Models.Planet> Planets {get; set; }
    public DbSet<Models.Government> Governments {get; set; }
    public DbSet<Models.GovernmentGovernment> GovernmentGovernments {get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Government>()
            .HasMany(g => g.ParentGovernments)
            .WithOne(g => g.ChildGovernment)
            .HasForeignKey(g => g.ChildGovernmentId)
            .IsRequired();
    }

}

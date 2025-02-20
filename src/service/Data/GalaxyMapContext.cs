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
    //     #region System
    //     modelBuilder.Entity<Models.System>()
    //         .HasKey(s => s.Name);
    //     #endregion System
    //     #region Spacelane
    //     modelBuilder.Entity<Models.Spacelane>()
    //         .HasOne(s => s.Origin)
    //         .WithMany()
    //         .HasForeignKey(s => s.OriginId)
    //         .IsRequired();
    //     modelBuilder.Entity<Models.Spacelane>()
    //         .HasOne(s => s.Destination)
    //         .WithMany()
    //         .HasForeignKey(s => s.DestinationId)
    //         .IsRequired();
    //     modelBuilder.Entity<Models.Spacelane>()
    //         .HasNoKey();
    //     #endregion Spacelane
    //     #region Planet
    //     modelBuilder.Entity<Models.Planet>()
    //         .HasKey(p => p.Name);
    //     modelBuilder.Entity<Models.Planet>()
    //         .HasMany(p => p.ParentGovernments)
    //         .WithOne(p => p.Planet)
    //         .HasForeignKey(p => p.PlanetId)
    //         .IsRequired();
    //     #endregion Planet
    //     #region Government
    //     modelBuilder.Entity<Models.Government>()
    //         .HasKey(g => g.Name);
    //     modelBuilder.Entity<Models.Government>()
    //         .Property(g => g.Color)
    //             .HasConversion(
    //                 v => v.ToString(),
    //                 v => (MapColor)Enum.Parse(typeof(MapColor), v));
    //     #endregion Government
    //     #region GovernmentGovernment
    //     modelBuilder.Entity<Models.GovernmentGovernment>()
    //         .HasOne(g => g.ChildGovernment)
    //         .WithMany()
    //         .HasForeignKey(g => g.ChildGovernmentId)
    //         .IsRequired();
    //     modelBuilder.Entity<Models.GovernmentGovernment>()
    //         .HasOne(g => g.ParentGovernment)
    //         .WithMany()
    //         .HasForeignKey(g => g.ParentGovernmentId)
    //         .IsRequired();
    //     modelBuilder.Entity<Models.GovernmentGovernment>()
    //         .HasNoKey()
    //         .Property(g => g.Relationship)
    //             .HasConversion(
    //                 v => v.ToString(),
    //                 v => (GovernmentRelationship)Enum.Parse(typeof(GovernmentRelationship), v));
    //     #endregion GovernmentGovernment
    //     #region PlanetGovernment
    //     modelBuilder.Entity<Models.PlanetGovernment>()
    //         .HasOne(p => p.Government)
    //         .WithMany()
    //         .HasForeignKey(p => p.GovernmentId)
    //         .IsRequired();
    //     modelBuilder.Entity<Models.PlanetGovernment>()
    //         .HasNoKey()
    //         .Property(p => p.Relationship)
    //             .HasConversion(
    //                 v => v.ToString(),
    //                 v => (GovernmentRelationship)Enum.Parse(typeof(GovernmentRelationship), v));
    //     #endregion PlanetGovernment
    }

}
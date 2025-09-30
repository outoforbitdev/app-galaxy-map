using System.Collections.Generic;

namespace GalaxyMapSiteApi.Models.Info;

public struct System
{
    #region Properties
    public string Id { get; set; }
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Sector { get; set; }
    public string Region { get; set; }
    public List<string> Planets { get; set; }
    public string Government { get; set; }
    #endregion Properties
    #region Constructors
    public System(Models.System system, Date date)
    {
        Id = system.Id;
        Name = system.Name;
        Planets = system
            .OrbitingBodies.Where(p =>
                (p.StartDate == null || p.StartDate < date)
                && (p.EndDate == null || p.EndDate > date)
            )
            .Select(p => p.Name)
            .ToList();
        X = system.Coordinates.X;
        Y = system.Coordinates.Y;
        Sector = system.Sector;
        Region = system.Region;
        Government = system.GetGovernment()?.GetGalacticGovernment()?.Name ?? "";
    }
    #endregion Constructors
}

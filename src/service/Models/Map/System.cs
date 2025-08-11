namespace GalaxyMapSiteApi.Models.Map;

public struct System
{
    #region Properties
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Color { get; set; }
    public string FocusLevel { get; set; }
    #endregion Properties
    #region Constructors
    public System(Models.System system)
    {
        if (system.Planets.Count > 0)
        {
            Planet primaryPlanet = system.Planets.First();
            Name = primaryPlanet.Name;
            Color = Map.GetColorFromEnum(
                primaryPlanet.CurrentGovernment is not null
                    ? primaryPlanet.CurrentGovernment.GetGalacticGovernment().Color
                    : MapColor.Gray
            );
            Console.WriteLine(
                $"System: {Name}, Governments: {string.Join(", ", primaryPlanet.CurrentGovernment.Organization.ParentGovernments.Select(g => g.ToString()))}, Organizations: {string.Join(", ", primaryPlanet.CurrentGovernment.ParentOrganizations.Select(g => g.ToString()))}"
            );
        }
        else
        {
            Name = system.Name;
            Color = Map.GetColorFromEnum(MapColor.Gray);
        }
        X = system.Coordinates.X;
        Y = system.Coordinates.Y;
        FocusLevel = Map.GetFocusLevelFromEnum(system.Focus);
    }
    #endregion Constructors
}

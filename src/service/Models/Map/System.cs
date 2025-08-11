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

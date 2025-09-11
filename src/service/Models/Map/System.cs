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
        if (system.OrbitingBodies.Count > 0)
        {
            OrbitingBody primaryBody = system.OrbitingBodies.First();
            Name = primaryBody.Name;
            Color = Map.GetColorFromEnum(
                primaryBody.CurrentGovernment is not null
                    ? primaryBody.CurrentGovernment.GetGalacticGovernment()?.Color
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

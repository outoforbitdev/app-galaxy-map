namespace GalaxyMapSiteApi.Models.Map;

public struct System
{
    #region Properties
    public string Id { get; set; }
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Color { get; set; }
    public string FocusLevel { get; set; }
    #endregion Properties
    #region Constructors
    public System(Models.System system)
    {
        Id = system.Id;
        Government? government = system.GetGovernment()?.GetGalacticGovernment();
        if (government != null)
        {
            Color = Map.GetColorFromEnum(government.Color);
        }
        else
        {
            Color = Map.GetColorFromEnum(MapColor.Gray);
        }
        OrbitingBody? primaryBody = system.GetPrimaryOrbitingBody();
        if (primaryBody != null)
        {
            Name = primaryBody.Name;
        }
        else
        {
            Name = system.Name;
        }
        X = system.Coordinates.X;
        Y = system.Coordinates.Y;
        FocusLevel = Map.GetFocusLevelFromEnum(system.Focus);
    }
    #endregion Constructors
}

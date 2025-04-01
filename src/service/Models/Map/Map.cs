namespace GalaxyMapSiteApi.Models.Map;

public struct Map
{
    #region Properties
    public List<System> Systems { get; set; }
    public List<SpacelaneSegment> Spacelanes { get; set; }
    #endregion Properties
    #region Constructors
    public Map(List<Models.System> systems, List<Models.SpacelaneSegment> spacelanes)
    {
        Systems = systems.ConvertAll(s => new System(s));
        Spacelanes = spacelanes.ConvertAll(s => new Models.Map.SpacelaneSegment(s));
    }
    #endregion Constructors
    #region Static Methods
    public static string GetFocusLevelFromEnum(FocusLevel? focusLevel)
    {
        FocusLevel focusLevelOrDefault = focusLevel ?? FocusLevel.Quaternary;
        return Enum.GetName(typeof(FocusLevel), focusLevelOrDefault) ?? "Quaternary";
    }

    public static string GetColorFromEnum(MapColor? color)
    {
        MapColor colorOrDefault = color ?? MapColor.Gray;
        return Enum.GetName(typeof(MapColor), colorOrDefault) ?? "Gray";
    }
    #endregion StaticMethods
}

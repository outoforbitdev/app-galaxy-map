using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GalaxyMapSiteApi.Models.Map;

public struct Map
{
    #region Properties
    public List<System> Systems { get; set; }
    public List<SpacelaneSegment> Spacelanes { get; set; }
    public List<LegendEntry> Legend { get; set; }
    #endregion Properties
    #region Constructors
    public Map(List<Models.System> systems, List<Models.SpacelaneSegment> spacelanes)
    {
        Systems = systems.ConvertAll(s => new System(s));
        Systems.Sort((a, b) => b.FocusLevel.CompareTo(a.FocusLevel));
        Spacelanes = spacelanes.ConvertAll(s => new Models.Map.SpacelaneSegment(s));
        Dictionary<string, LegendEntry> legendDict = new Dictionary<string, LegendEntry>();
        foreach (Models.System system in systems)
        {
            Government? government = system.GetGovernment()?.GetGalacticGovernment();
            if (government != null)
            {
                legendDict[government.Name] = new LegendEntry(
                    government.Id,
                    government.Name,
                    GetColorFromEnum(government.Color)
                );
            }
        }
        Legend = legendDict.Values.ToList();
        Legend.Sort((a, b) => a.Label.CompareTo(b.Label));
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

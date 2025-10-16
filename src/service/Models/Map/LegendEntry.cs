namespace GalaxyMapSiteApi.Models.Map;

public class LegendEntry
{
    #region Properties
    public string Id { get; set; }
    public string Label { get; set; }
    public string Color { get; set; }
    #endregion Properties
    #region Constructors
    public LegendEntry(string id, string label, string color)
    {
        Id = id;
        Label = label;
        Color = color;
    }
    #endregion Constructors
}

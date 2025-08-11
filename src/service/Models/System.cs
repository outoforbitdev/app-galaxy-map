using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Models.Map;

namespace GalaxyMapSiteApi.Models;

[Table("solar_systems")]
public class System : InstanceEntity
{
    #region Properties
    public string Name { get; set; } = "";

    [NotMapped]
    public Coordinates Coordinates { get; set; }
    public int X
    {
        get { return Coordinates.X; }
        set { Coordinates = new Coordinates() { X = value, Y = Coordinates.Y }; }
    }
    public int Y
    {
        get { return Coordinates.Y; }
        set { Coordinates = new Coordinates() { X = Coordinates.X, Y = value }; }
    }
    public string? Sector { get; set; }
    public string? Region { get; set; }

    [NotMapped]
    public FocusLevel? Focus { get; set; }
    public string? FocusString
    {
        get { return Focus.ToString(); }
        set
        {
            Focus = value is not null ? (FocusLevel)Enum.Parse(typeof(FocusLevel), value) : null;
        }
    }
    public virtual ICollection<Planet> Planets { get; } = [];
    #endregion Properties
    public Government? GetGovernment()
    {
        return Planets.Select(p => p.CurrentGovernment).FirstOrDefault();
    }
}

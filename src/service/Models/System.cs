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
    /// <summary>
    /// Gets the government that currently controls this system.
    /// </summary>
    /// <returns></returns>
    public Government? GetGovernment()
    {
        // @TODO(jmirecki): This just gets the government of the first planet
        // in the system with a government. This should be updated to find the
        // common government among all planets in the system.
        return Planets.Select(p => p.CurrentGovernment).FirstOrDefault();
    }
}

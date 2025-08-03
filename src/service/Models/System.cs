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
    public Date? StartDate { get; set; }
    public Date? EndDate { get; set; }

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
    #region Constructors
    // public System(string name, Coordinates coordinates) {
    //     Name = name;
    //     Coordinates = coordinates;
    // }
    // public System(string name, int x, int y, string sector, string region, int focus): this(name, new Coordinates(){ X = x, Y = y }) {}
    #endregion Constructors
}

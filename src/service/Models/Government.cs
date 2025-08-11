using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("governments")]
public class Government : OrganizationEntity
{
    #region Properties

    [NotMapped]
    public MapColor Color { get; set; } = MapColor.Gray;
    public string ColorString
    {
        get { return Color.ToString(); }
        set { Color = (MapColor)Enum.Parse(typeof(MapColor), value); }
    }
    public virtual ICollection<Planet> Planets { get; set; } = [];
    #endregion Properties
    #region Constructors
    public Government(string name, string colorString)
    {
        Name = name;
        ColorString = colorString;
    }
    #endregion Constructors

    public Government GetGalacticGovernment()
    {
        Government? parent = GetParentGovernment();
        // @TODO(jaymirecki): replace this comparison with an IEquatable comparison
        if (parent is null)
        {
            return this;
        }
        return parent.GetGalacticGovernment();
    }
}

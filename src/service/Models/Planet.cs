using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyMapSiteApi.Models;

[Table("planets")]
public class Planet : InstanceEntity
{
    #region Properties
    public string Name { get; set; }

    [ForeignKey("InstanceId, SystemId")]
    public virtual System System { get; set; } = null!;
    public string SystemId { get; set; }
    public virtual ICollection<PlanetGovernment> ParentGovernments { get; set; } = [];

    [NotMapped]
    public Government? CurrentGovernment
    {
        get { return ParentGovernments.Count > 0 ? ParentGovernments.First().Government : null; }
    }
    #endregion Properties
    #region Constructors
    public Planet(string name, string systemId)
    {
        Name = name;
        SystemId = systemId;
    }
    #endregion Constructors
    #region Overrides
    public override string ToString()
    {
        return $"{{Name: '{Name}', SystemId: '{SystemId}'}}";
    }
    #endregion Overrides
}

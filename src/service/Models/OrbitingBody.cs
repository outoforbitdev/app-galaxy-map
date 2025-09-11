using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyMapSiteApi.Models;

[Table("orbiting_bodies")]
public class OrbitingBody : InstanceEntity
{
    #region Properties
    public string Name { get; set; }

    [ForeignKey("InstanceId, SystemId")]
    public virtual required System System { get; set; }
    public required string SystemId { get; set; }
    public OrbitingBodyType? Type { get; set; }

    [ForeignKey("InstanceId, OrbitedBodyId")]
    public virtual OrbitingBody? OrbitedBody { get; set; }
    public string? OrbitedBodyId { get; set; }
    public virtual ICollection<Government> Governments { get; set; } = [];

    [NotMapped]
    public Government? CurrentGovernment
    {
        get { return Governments.Count > 0 ? Governments.First() : null; }
    }
    #endregion Properties
}

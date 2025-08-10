using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[PrimaryKey(nameof(InstanceId), nameof(ChildId), nameof(ParentId))]
public abstract class InstanceRelationship<ChildType, ParentType>
    where ChildType : InstanceEntity
    where ParentType : InstanceEntity
{
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }

    [ForeignKey("InstanceId, ChildId")]
    public virtual ChildType Child { get; set; } = null!;

    [Key, Column(Order = 1)]
    public string ChildId { get; set; }

    [ForeignKey("InstanceId, ParentId")]
    public virtual ParentType Parent { get; set; } = null!;

    [Key, Column(Order = 2)]
    public string ParentId { get; set; }
    #endregion Properties
}

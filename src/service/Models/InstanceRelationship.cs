using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[PrimaryKey(nameof(InstanceId), nameof(ChildId), nameof(ParentId), nameof(StartDate))]
public abstract class InstanceRelationship<ChildType, ParentType> : IDatedInstanceItem
    where ChildType : InstanceEntity
    where ParentType : InstanceEntity
{
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }

    public Date? StartDate { get; set; }
    public Date? EndDate { get; set; }

    [ForeignKey("InstanceId, ChildId")]
    public virtual ChildType Child { get; set; } = null!;

    [Key, Column(Order = 1)]
    public required string ChildId { get; set; }

    [ForeignKey("InstanceId, ParentId")]
    public virtual ParentType Parent { get; set; } = null!;

    [Key, Column(Order = 2)]
    public required string ParentId { get; set; }
    #endregion Properties
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[PrimaryKey(
    nameof(InstanceId),
    nameof(ChildId),
    nameof(ParentId),
    nameof(StartDate),
    nameof(Relationship)
)]
// [Keyless]
public abstract class InstanceRelationship<ChildType, ParentType, RelationshipType>
    : IDatedInstanceItem
    where ChildType : InstanceEntity
    where ParentType : InstanceEntity
    where RelationshipType : Enum
{
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }

    [Key, Column(Order = 3)]
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

    [Key, Column(Order = 4)]
    public required RelationshipType Relationship { get; set; }
    #endregion Properties
}

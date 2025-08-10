using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[PrimaryKey(nameof(InstanceId), nameof(Id))]
public abstract class InstanceEntity : IDatedInstanceItem
{
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }

    [Key, Column(Order = 1)]
    public required string Id { get; set; }

    public Date? StartDate { get; set; }
    public Date? EndDate { get; set; }
    #endregion Properties
}

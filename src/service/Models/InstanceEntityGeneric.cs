using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Keyless]
public abstract class InstanceEntityGeneric : IDatedInstanceItem
{
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }

    public Date? StartDate { get; set; }
    public Date? EndDate { get; set; }
    #endregion Properties
}

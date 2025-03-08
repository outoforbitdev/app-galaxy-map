using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[PrimaryKey(nameof(InstanceId), nameof(Id))]
public abstract class InstanceEntity: KeylessInstanceEntity {
    #region Properties
    [Key, Column(Order = 1)]
    public required string Id { get; set; }
    #endregion Properties
}

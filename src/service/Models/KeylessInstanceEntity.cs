using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public abstract class KeylessInstanceEntity {
    #region Properties
    [ForeignKey(nameof(InstanceId))]
    // [Key, Column(Order = 0)]
    public virtual required Instance Instance { get; set; }
    public virtual required string InstanceId { get; set; }
    #endregion Properties
}

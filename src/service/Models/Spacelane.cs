using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
[Keyless]
public class Spacelane: KeylessInstanceEntity {
    #region Properties
    public string Name { get; set; }
    [ForeignKey("InstanceId, OriginId")]
    public virtual System Origin { get; set; } = null!;
    public string OriginId { get; set; }
    [ForeignKey("InstanceId, DestinationId")]
    public virtual System Destination { get; set; } = null!;
    public string DestinationId { get; set; }
    public int Focus { get; set; }
    #endregion Properties
    #region Constructors
    public Spacelane(string name, string originId, string destinationId) {
        Name = name;
        OriginId = originId;
        DestinationId = destinationId;
    }
    #endregion Constructors
}

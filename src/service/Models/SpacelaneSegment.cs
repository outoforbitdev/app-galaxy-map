using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("spacelane_segments")]
[Keyless]
public class SpacelaneSegment: KeylessInstanceEntity {
    #region Properties
    [ForeignKey("InstanceId, SpacelaneId")]
    public virtual Spacelane? Spacelane {get; set; }
    public string? SpacelaneId { get; set; }
    [ForeignKey("InstanceId, OriginId")]
    public virtual System Origin { get; set; } = null!;
    public string OriginId { get; set; }
    [ForeignKey("InstanceId, DestinationId")]
    public virtual System Destination { get; set; } = null!;
    public string DestinationId { get; set; }
    #endregion Properties

    #region Constructors
    public SpacelaneSegment(string spacelaneId, string originId, string destinationId) {
        SpacelaneId = spacelaneId;
        OriginId = originId;
        DestinationId = destinationId;
    }
    #endregion Constructors
}

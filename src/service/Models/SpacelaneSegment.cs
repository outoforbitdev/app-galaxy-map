using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("spacelane_segments")]
[Keyless]
public class SpacelaneSegment : KeylessInstanceEntity
{
    #region Properties
    [ForeignKey("InstanceId, SpacelaneId")]
    public virtual Spacelane? Spacelane { get; set; }
    public string? SpacelaneId { get; set; }

    [ForeignKey("InstanceId, OriginId")]
    public virtual System Origin { get; set; } = null!;
    public string OriginId { get; set; }

    [ForeignKey("InstanceId, DestinationId")]
    public virtual System Destination { get; set; } = null!;
    public string DestinationId { get; set; }
    [NotMapped]
    public Date? StartDate { get; set; }
    public int? StartDateValue { get { return StartDate?.Days; } set { StartDate = value is not null ? new Date((int)value) : null; } }
    [NotMapped]
    public Date? EndDate { get; set; }
    public int? EndDateValue { get { return EndDate?.Days; } set { EndDate = value is not null ? new Date((int)value) : null; } }
    #endregion Properties

    #region Constructors
    public SpacelaneSegment(string spacelaneId, string originId, string destinationId)
    {
        SpacelaneId = spacelaneId;
        OriginId = originId;
        DestinationId = destinationId;
    }
    #endregion Constructors
}

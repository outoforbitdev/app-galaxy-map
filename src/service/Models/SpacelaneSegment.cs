using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public enum SpacelaneStartReason
{
    Discovered,
    Created,
}

public enum SpacelaneEndReason
{
    Collapsed,
    Lost,
}

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

    #region Dates
    [NotMapped]
    public Date? StartDate { get; set; }
    public int? StartDateValue
    {
        get { return StartDate?.Days; }
        set { StartDate = value is not null ? new Date((int)value) : null; }
    }

    [NotMapped]
    public Date? EndDate { get; set; }
    public int? EndDateValue
    {
        get { return EndDate?.Days; }
        set { EndDate = value is not null ? new Date((int)value) : null; }
    }

    [NotMapped]
    public SpacelaneStartReason? StartReason { get; set; }
    public string? StartReasonString
    {
        get { return StartReason?.ToString(); }
        set { StartReason = EnumConverter.ConvertToEnumOrNull<SpacelaneStartReason>(value); }
    }

    [NotMapped]
    public SpacelaneEndReason? EndReason { get; set; }
    public string? EndReasonString
    {
        get { return EndReason?.ToString(); }
        set { EndReason = EnumConverter.ConvertToEnumOrNull<SpacelaneEndReason>(value); }
    }
    #endregion Dates
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

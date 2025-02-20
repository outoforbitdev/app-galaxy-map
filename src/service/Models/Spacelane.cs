using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
[Keyless]
public class Spacelane {
    #region Properties
    public string Name { get; set; }
    public virtual System Origin { get; set; } = null!;
    [ForeignKey(nameof(Origin))]
    public string OriginId { get; set; }
    public virtual System Destination { get; set; } = null!;
    [ForeignKey(nameof(Destination))]
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

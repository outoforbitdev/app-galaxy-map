using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
public class Spacelane {
    #region Properties
    public string Name { get; set; }
    public System Origin { get; set; } = null!;
    public string OriginId { get; set; }
    public System Destination { get; set; } = null!;
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

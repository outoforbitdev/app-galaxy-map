using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("planet_governments")]
[PrimaryKey(nameof(PlanetId), nameof(GovernmentId))]
public class PlanetGovernment {
    #region Properties
    public virtual Planet Planet { get; set; } = null!;
    [Key, Column(Order = 0)]
    [ForeignKey(nameof(Planet))]
    public string PlanetId { get; set; }
    public virtual Government Government { get; set; } = null!;
    [Key, Column(Order = 1)]
    [ForeignKey(nameof(Government))]
    public string GovernmentId { get; set; }
    [NotMapped]
    public GovernmentRelationship Relationship { get; set; }
    public string RelationshipString {
        get { return Relationship.ToString(); }
        set { Relationship = (GovernmentRelationship)Enum.Parse(typeof(GovernmentRelationship), value); }
    }
    #endregion Properties
    #region Constructors
    public PlanetGovernment(string planetId, string governmentId, string relationshipString) {
        PlanetId = planetId;
        GovernmentId = governmentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("planet_governments")]
[PrimaryKey(nameof(InstanceId), nameof(PlanetId), nameof(GovernmentId))]
public class PlanetGovernment : KeylessInstanceEntity
{
    #region Properties
    [ForeignKey("InstanceId, PlanetId")]
    public virtual Planet Planet { get; set; } = null!;

    [Key, Column(Order = 1)]
    public string PlanetId { get; set; }

    [ForeignKey("InstanceId, GovernmentId")]
    public virtual Government Government { get; set; } = null!;

    [Key, Column(Order = 2)]
    public string GovernmentId { get; set; }

    [NotMapped]
    public GovernmentRelationship Relationship { get; set; }
    public string RelationshipString
    {
        get { return Relationship.ToString(); }
        set
        {
            Relationship = (GovernmentRelationship)
                Enum.Parse(typeof(GovernmentRelationship), value);
        }
    }
    #endregion Properties
    #region Constructors
    public PlanetGovernment(string planetId, string governmentId, string relationshipString)
    {
        PlanetId = planetId;
        GovernmentId = governmentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

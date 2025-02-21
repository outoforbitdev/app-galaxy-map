using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("government_governments")]
[PrimaryKey(nameof(ChildGovernmentId), nameof(ParentGovernmentId))]
public class GovernmentGovernment {
    #region Properties
    public virtual Government ChildGovernment { get; set; } = null!;
    [Key, Column(Order = 0)]
    [ForeignKey(nameof(ChildGovernment))]
    public string ChildGovernmentId { get; set; }
    public virtual Government ParentGovernment { get; set; } = null!;
    [Key, Column(Order = 1)]
    [ForeignKey(nameof(ParentGovernment))]
    public string ParentGovernmentId { get; set; }
    [NotMapped]
    public virtual GovernmentRelationship Relationship { get; set; }
    public string RelationshipString {
        get { return Relationship.ToString(); }
        set { Relationship = (GovernmentRelationship)Enum.Parse(typeof(GovernmentRelationship), value); }
    }
    #endregion Properties
    #region Constructors
    public GovernmentGovernment(string childGovernmentId, string parentGovernmentId, string relationshipString) {
        ChildGovernmentId = childGovernmentId;
        ParentGovernmentId = parentGovernmentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

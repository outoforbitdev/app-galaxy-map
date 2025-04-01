using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("government_governments")]
[PrimaryKey(nameof(InstanceId), nameof(ChildGovernmentId), nameof(ParentGovernmentId))]
public class GovernmentGovernment : KeylessInstanceEntity
{
    #region Properties
    [ForeignKey("InstanceId, ChildGovernmentId")]
    public virtual Government ChildGovernment { get; set; } = null!;

    [Key, Column(Order = 1)]
    public string ChildGovernmentId { get; set; }

    [ForeignKey("InstanceId, ParentGovernmentId")]
    public virtual Government ParentGovernment { get; set; } = null!;

    [Key, Column(Order = 2)]
    public string ParentGovernmentId { get; set; }

    [NotMapped]
    public virtual GovernmentRelationship Relationship { get; set; }
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
    public GovernmentGovernment(
        string childGovernmentId,
        string parentGovernmentId,
        string relationshipString
    )
    {
        ChildGovernmentId = childGovernmentId;
        ParentGovernmentId = parentGovernmentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public class GovernmentGovernment : InstanceRelationship<Government, Government>
{
    #region Properties

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
    public GovernmentGovernment(string childId, string parentId, string relationshipString)
    {
        ChildId = childId;
        ParentId = parentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

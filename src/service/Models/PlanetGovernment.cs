using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("planet_governments")]
public class PlanetGovernment : InstanceRelationship<Planet, Government>
{
    #region Properties

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
    public PlanetGovernment(string childId, string parentId, string relationshipString)
    {
        ChildId = childId;
        ParentId = parentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public class PlanetGovernment : InstanceRelationship<Planet, Government>
{
    #region Properties

    [NotMapped]
    public OrganizationRelationship Relationship { get; set; }
    public string RelationshipString
    {
        get { return Relationship.ToString(); }
        set
        {
            Relationship = (OrganizationRelationship)
                Enum.Parse(typeof(OrganizationRelationship), value);
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

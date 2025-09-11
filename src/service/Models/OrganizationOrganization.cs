using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public class OrganizationOrganization : InstanceRelationship<Organization, Organization>
{
    #region Properties

    [NotMapped]
    public virtual OrganizationRelationship Relationship { get; set; }
    public string RelationshipString
    {
        get { return Relationship.ToString(); }
        set
        {
            Relationship = EnumConverter.ConvertToEnumOrDefault<OrganizationRelationship>(
                value,
                OrganizationRelationship.Member
            );
        }
    }
    #endregion Properties
    #region Constructors
    public OrganizationOrganization(string childId, string parentId, string relationshipString)
    {
        ChildId = childId;
        ParentId = parentId;
        RelationshipString = relationshipString;
    }
    #endregion Constructors
}

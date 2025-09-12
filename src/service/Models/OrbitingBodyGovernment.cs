using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public class OrbitingBodyGovernment : InstanceRelationship<OrbitingBody, Government>
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
}

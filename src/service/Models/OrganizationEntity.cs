using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public abstract class OrganizationEntity : InstanceEntity
{
    #region Properties
    public required string Name { get; set; }

    [ForeignKey("InstanceId, OrganizationId")]
    public virtual required Organization Organization { get; set; } = null!;
    public required string OrganizationId { get; set; }

    [NotMapped]
    public ICollection<Organization> ParentOrganizations
    {
        get { return Organization.ParentOrganizations; }
    }

    [NotMapped]
    public ICollection<Organization> ChildOrganizations
    {
        get { return Organization.ChildOrganizations; }
    }

    [NotMapped]
    public ICollection<Organization> ParentGovernments
    {
        get { return Organization.ParentGovernments; }
    }

    [NotMapped]
    public ICollection<Organization> ChildGovernments
    {
        get { return Organization.ChildGovernments; }
    }

    [NotMapped]
    public ICollection<Organization> ParentCorporations
    {
        get { return Organization.ParentCorporations; }
    }

    [NotMapped]
    public ICollection<Organization> ChildCorporations
    {
        get { return Organization.ChildCorporations; }
    }

    public Government? GetParentGovernment()
    {
        if (ParentGovernments.Count > 0)
        {
            return ParentGovernments.First().Government;
        }
        return null;
    }
    #endregion Properties
}

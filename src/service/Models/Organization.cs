using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("organizations")]
public class Organization : InstanceEntity
{
    #region Properties
    public OrganizationType OrganizationType { get; set; }
    public virtual Government? Government { get; set; }
    public virtual Corporation? Corporation { get; set; }
    public virtual ICollection<OrganizationOrganization> ParentOrganizationRelationships { get; set; } =
    [];
    public virtual ICollection<OrganizationOrganization> ChildOrganizationRelationships { get; set; } =
    [];
    public virtual ICollection<Organization> ParentOrganizations
    {
        get { return ParentOrganizationRelationships.Select(r => r.Parent).ToList(); }
    }
    public virtual ICollection<Organization> ChildOrganizations
    {
        get { return ChildOrganizationRelationships.Select(r => r.Child).ToList(); }
    }
    public virtual ICollection<Organization> ParentGovernments
    {
        get
        {
            return ParentOrganizations
                .Where(po => po.OrganizationType == OrganizationType.Government)
                .ToList();
        }
    }
    public virtual ICollection<Organization> ChildGovernments
    {
        get
        {
            return ChildOrganizations
                .Where(co => co.OrganizationType == OrganizationType.Government)
                .ToList();
        }
    }
    public virtual ICollection<Organization> ParentCorporations
    {
        get
        {
            return ParentOrganizations
                .Where(po => po.OrganizationType == OrganizationType.Corporation)
                .ToList();
        }
    }
    public virtual ICollection<Organization> ChildCorporations
    {
        get
        {
            return ChildOrganizations
                .Where(co => co.OrganizationType == OrganizationType.Corporation)
                .ToList();
        }
    }

    [NotMapped]
    public OrganizationEntity? OrganizationEntity
    {
        get
        {
            switch (OrganizationType)
            {
                case OrganizationType.Government:
                    return Government;
                case OrganizationType.Corporation:
                    return Corporation;
            }
            return null;
        }
    }

    public string ToString()
    {
        return $"{Id} - {OrganizationType}";
    }
    #endregion Properties
    #region Constructors
    #endregion Constructors
}

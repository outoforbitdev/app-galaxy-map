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
    public virtual ICollection<Organization> ParentOrganizations { get; set; } = [];
    public virtual ICollection<Organization> ChildOrganizations { get; set; } = [];
    public virtual ICollection<Organization> ParentGovernments { get; set; } = [];
    public virtual ICollection<Organization> ChildGovernments { get; set; } = [];

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
    #endregion Properties
    #region Constructors
    #endregion Constructors
}

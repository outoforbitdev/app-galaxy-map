using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

public class OrganizationOrganization
    : InstanceRelationship<Organization, Organization, OrganizationRelationship> { }

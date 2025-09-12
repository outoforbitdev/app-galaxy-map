using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Data;
using GalaxyMapSiteApi.Models.Map;
using Microsoft.EntityFrameworkCore;

namespace GalaxyMapSiteApi.Models;

[Table("governments")]
public class Government : OrganizationEntity, IEquatable<Government>
{
    #region Properties
    public MapColor? Color { get; set; } = MapColor.Gray;
    public virtual ICollection<OrbitingBody> OrbitingBodies { get; set; } = [];
    #endregion Properties
    #region Constructors
    #endregion Constructors

    public Government? GetGalacticGovernment()
    {
        Government? parent = GetParentGovernment();
        // @TODO(jaymirecki): replace this comparison with an IEquatable comparison
        if (parent is null)
        {
            return this;
        }
        return parent.GetGalacticGovernment();
    }

    /// <summary>
    /// Gets the common government between this government and another.
    /// This is an O(n^2) operation.
    /// </summary>
    /// <param name="other">The other government</param>
    /// <returns></returns>
    public Government? GetCommonGovernment(Government? other)
    {
        if (other is null)
            return null;
        if (other == this)
            return this;

        Government? otherParent = other;
        Government? thisParent;
        // This loop is O(n^2). Perhaps we should short-circuit it by first
        // checking if the galactic government is the same for both
        // governments.
        while (otherParent is not null)
        {
            thisParent = this;
            while (thisParent is not null)
            {
                if (thisParent == otherParent)
                {
                    return thisParent;
                }
                thisParent = thisParent.GetParentGovernment();
            }
            otherParent = otherParent.GetParentGovernment();
        }
        return null;
    }

    #region IEquatable
    public bool Equals(Government? other)
    {
        if (other is null)
            return false;
        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj == this)
            return true;

        return obj is Government other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Government? left, Government? right)
    {
        if (left is null)
            return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Government? left, Government? right)
    {
        return !(left == right);
    }
    #endregion IEquatable
}

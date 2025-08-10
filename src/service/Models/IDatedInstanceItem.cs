namespace GalaxyMapSiteApi.Models;

public interface IDatedInstanceItem : IInstanceItem
{
    public Date? StartDate { get; }
    public Date? EndDate { get; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Models.Map;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
public class Spacelane : InstanceEntity
{
    #region Properties
    public string Name { get; set; } = "";
    public FocusLevel? Focus { get; set; } = FocusLevel.Quaternary;
    #endregion Properties
    #region Constructors
    #endregion Constructors
}

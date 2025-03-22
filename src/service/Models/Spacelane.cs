using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GalaxyMapSiteApi.Models.Map;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
public class Spacelane: InstanceEntity {
    #region Properties
    public string Name { get; set; } = "";
    [NotMapped]
    public FocusLevel? Focus { get; set; } = FocusLevel.Quaternary;
    public string? FocusString {
        get { return Focus.ToString(); }
        set { Focus = value is not null ? (FocusLevel)Enum.Parse(typeof(FocusLevel), value) : null; }
    }
    #endregion Properties
    #region Constructors
    #endregion Constructors
}

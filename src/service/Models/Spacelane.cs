using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyMapSiteApi.Models;

[Table("spacelanes")]
public class Spacelane: InstanceEntity {
    #region Properties
    public string Name { get; set; }
    public int Focus { get; set; }
    #endregion Properties
    #region Constructors
    public Spacelane(string name, int focus) {
        Name = name;
        Focus = focus;
    }
    #endregion Constructors
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyMapSiteApi.Models;
[Table("instances")]
public class Instance {
    #region Properties
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    #endregion Properties
    #region Constructors
    public Instance(string id, string name) {
        Id = id;
        Name = name;
    }
    #endregion Constructors
}

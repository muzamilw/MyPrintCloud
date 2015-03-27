using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Variable Type Domain Model
    /// </summary>
    public class CostCentreVariableType
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        
        [NotMapped]
        public virtual ICollection<CostCentreVariable> VariablesList { get; set; }
    }
}

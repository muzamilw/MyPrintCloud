
namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cost Centre WebApi Model
    /// </summary>
    public class CostCentre
    {
        public long CostCentreId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
    }
}
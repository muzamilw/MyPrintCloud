namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreType
    {
        public int TypeId { get; set; }
        public short? IsSystem { get; set; }
        public string TypeName { get; set; }
        public short? IsExternal { get; set; }
    }
}
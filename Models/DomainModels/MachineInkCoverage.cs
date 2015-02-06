namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Machine Ink Coverage Domain Model
    /// </summary>
    public class MachineInkCoverage
    {
        public long Id { get; set; }
        public int? SideInkOrder { get; set; }
        public int? SideInkOrderCoverage { get; set; }
        public int? MachineId { get; set; }

        public virtual Machine Machine { get; set; }
    }
}

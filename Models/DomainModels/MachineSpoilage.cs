namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Machine Spoilage Domain Model
    /// </summary>
    public class MachineSpoilage
    {
        public int MachineSpoilageId { get; set; }
        public int? MachineId { get; set; }
        public int? SetupSpoilage { get; set; }
        public float? RunningSpoilage { get; set; }
        public int? NoOfColors { get; set; }
    }
}

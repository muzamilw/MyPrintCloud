namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Machine Lookup Method Domain Model
    /// </summary>
    public class MachineLookupMethod
    {
        public long Id { get; set; }
        public int? MachineId { get; set; }
        public long? MethodId { get; set; }
        public bool? DefaultMethod { get; set; }
        public Machine Machine { get; set; }
    }
}

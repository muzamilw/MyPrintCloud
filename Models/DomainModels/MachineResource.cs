﻿namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Machine Resource Domain Model
    /// </summary>
    public class MachineResource
    {
        public long Id { get; set; }
        public int? MachineId { get; set; }
        public int? ResourceId { get; set; }

        public virtual Machine Machine { get; set; }
    }
}

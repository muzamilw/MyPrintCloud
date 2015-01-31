using System;
namespace MPC.Models.DomainModels
{
     [Serializable()]
    /// <summary>
    /// Cost Centre Matrix Detail Domain Model
    /// </summary>
    public class CostCentreMatrixDetail
    {
        public long Id { get; set; }
        public int MatrixId { get; set; }
        public string Value { get; set; }
    }
}

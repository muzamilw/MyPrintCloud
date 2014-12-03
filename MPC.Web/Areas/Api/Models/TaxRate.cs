namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Tax Rate Web Model
    /// </summary>
    public class TaxRate
    {

        /// <summary>
        /// Tax id
        /// </summary>
        public int TaxId { get; set; }

        /// <summary>
        /// Tax Name
        /// </summary>
        public string TaxName { get; set; }

        /// <summary>
        /// Tax1
        /// </summary>
        public double? Tax1 { get; set; }
    }
}
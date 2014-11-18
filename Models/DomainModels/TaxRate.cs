namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Tax Rate Domain Model
    /// </summary>
    public class TaxRate
    {
        #region Persisted Properties

        /// <summary>
        /// Tax id
        /// </summary>
        public int TaxId { get; set; }

        /// <summary>
        /// TaxCode
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Tax Name
        /// </summary>
        public string TaxName { get; set; }

        /// <summary>
        /// Source State Id
        /// </summary>
        public int? SourceStateId { get; set; }

        /// <summary>
        /// Destination State Id
        /// </summary>
        public int? DestinationStateId { get; set; }

        /// <summary>
        /// Tax 1
        /// </summary>
        public double? Tax1 { get; set; }

        /// <summary>
        /// Tax 2
        /// </summary>
        public double? Tax2 { get; set; }

        /// <summary>
        /// Tax 3
        /// </summary>
        public double? Tax3 { get; set; }

        /// <summary>
        /// Is Fixed
        /// </summary>
        public bool? IsFixed { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public int UserDomainKey { get; set; }

        #endregion
    }
}

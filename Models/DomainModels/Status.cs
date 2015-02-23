using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Status
    /// </summary>
    public class Status
    {
        #region Persisted Properties

        /// <summary>
        /// Status Id
        /// </summary>
        public short StatusId { get; set; }

        /// <summary>
        /// Status Name
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status Type
        /// </summary>
        public int? StatusType { get; set; }

        #endregion

        #region Reference Properties

        /// <summary>
        /// Items 
        /// </summary>
        public virtual ICollection<Item> Items { get; set; }

        /// <summary>
        /// Estimates
        /// </summary>
        public virtual ICollection<Estimate> Estimates { get; set; }

        /// <summary>
        /// Invoices
        /// </summary>
        public virtual ICollection<Invoice> Invoices { get; set; }

        #endregion
    }
}

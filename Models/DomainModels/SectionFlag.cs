using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Section Flag Domain Model
    /// </summary>
    public class SectionFlag
    {
        #region Persisted Properties
        /// <summary>
        /// Section Id
        /// </summary>
        public int SectionFlagId { get; set; }

        /// <summary>
        /// Section Id
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Flag Name
        /// </summary>
        public string FlagName { get; set; }

        /// <summary>
        /// Flag Color
        /// </summary>
        public string FlagColor { get; set; }

        /// <summary>
        /// Flag Description
        /// </summary>
        public string flagDescription { get; set; }

        /// <summary>
        /// Organisation Id
        /// </summary>
        public long? OrganisationId { get; set; }

        /// <summary>
        /// Flag Column
        /// </summary>
        public string FlagColumn { get; set; }

        /// <summary>
        /// Is Default
        /// </summary>
        public bool? isDefault { get; set; }

        #endregion

        #region Reference Properties
        /// <summary>
        /// Section
        /// </summary>
        public virtual Section Section { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<DeliveryNote> DeliveryNotes { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        #endregion
    }
}

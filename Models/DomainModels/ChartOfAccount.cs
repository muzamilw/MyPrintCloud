using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Chart Of Account
    /// </summary>
    public class ChartOfAccount
    {
        #region Persisted Properties

        /// <summary>
        /// Unique Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Account No
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Opening Balance
        /// </summary>
        public double OpeningBalance { get; set; }

        /// <summary>
        /// Opening Balance Type
        /// </summary>
        public short? OpeningBalanceType { get; set; }

        /// <summary>
        /// Type Id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Sub Type Id
        /// </summary>
        public int? SubTypeId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Nature
        /// </summary>
        public short? Nature { get; set; }

        /// <summary>
        /// Is Active
        /// </summary>
        public short? IsActive { get; set; }

        /// <summary>
        /// Is Fixed
        /// </summary>
        public short? IsFixed { get; set; }

        /// <summary>
        /// Last Activity Date
        /// </summary>
        public DateTime? LastActivityDate { get; set; }

        /// <summary>
        /// Is For Reconciliation
        /// </summary>
        public short IsForReconciliation { get; set; }

        /// <summary>
        /// Balance
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Is Read
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>
        /// System Site Id
        /// </summary>
        public int? SystemSiteId { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public int UserDomainKey { get; set; }

        #endregion
    }
}

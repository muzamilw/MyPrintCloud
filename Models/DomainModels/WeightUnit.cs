using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Weight Unit Domain Model
    /// </summary>
    public class WeightUnit
    {
        #region Persisted Properties

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Unit Name
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Pound
        /// </summary>
        public double? Pound { get; set; }

        /// <summary>
        /// GSM
        /// </summary>
        public double? GSM { get; set; }

        /// <summary>
        /// KG
        /// </summary>
        public double? KG { get; set; }

        /// <summary>
        /// Organisations using this weight unit
        /// </summary>
        public virtual ICollection<Organisation> Organisations { get; set; } 

        #endregion
    }
}

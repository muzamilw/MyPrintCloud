﻿namespace MPC.Models.DomainModels
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
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }

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
        #endregion
    }
}

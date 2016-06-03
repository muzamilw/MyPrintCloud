using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Section Ink Coverage Domain Model
    /// </summary>
   [Serializable]
    public class SectionInkCoverage
    {
        public int Id { get; set; }
        public long? SectionId { get; set; }
        public int? InkOrder { get; set; }
        public int? InkId { get; set; }
        public int? CoverageGroupId { get; set; }
        public int? Side { get; set; }
        public double? CoverageRate { get; set; }

        public virtual ItemSection ItemSection { get; set; }

        #region Public 

        /// <summary>
        /// Creates Copy of Section Ink Coverage
        /// </summary>
        public void Clone(SectionInkCoverage target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemSectionInkCoverageClone_InvalidInkCoverage, "target");
            }

            target.CoverageGroupId = CoverageGroupId;
            target.Side = Side;
            target.InkOrder = InkOrder;
            target.InkId = InkId;
        }

        #endregion
    }
}

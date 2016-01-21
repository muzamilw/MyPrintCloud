using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Section Ink Coverage mapper
    /// </summary>
    public static class SectionInkCoverageMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this SectionInkCoverage source, SectionInkCoverage target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.Id = source.Id;
            target.InkId = source.InkId;
            target.InkOrder = source.InkOrder;
            target.SectionId = source.SectionId;
            target.Side = source.Side;
            target.CoverageRate = source.CoverageRate;
            target.CoverageGroupId = source.CoverageGroupId;
        }

        #endregion
    }
}

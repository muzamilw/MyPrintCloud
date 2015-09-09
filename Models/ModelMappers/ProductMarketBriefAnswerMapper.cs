using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Product MarketBrief Answer mapper
    /// </summary>
    public static class ProductMarketBriefAnswerMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ProductMarketBriefAnswer source, ProductMarketBriefAnswer target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.MarketBriefAnswerId = source.MarketBriefAnswerId;
            target.MarketBriefQuestionId = source.MarketBriefQuestionId;
            target.AnswerDetail = source.AnswerDetail;
        }

        #endregion
    }
}

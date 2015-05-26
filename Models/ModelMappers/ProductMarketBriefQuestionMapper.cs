using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Product MarketBrief Question mapper
    /// </summary>
    public static class ProductMarketBriefQuestionMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ProductMarketBriefQuestion source, ProductMarketBriefQuestion target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.MarketBriefQuestionId = source.MarketBriefQuestionId;
            target.ItemId = source.ItemId;
            target.IsMultipleSelection = source.IsMultipleSelection;
            target.QuestionDetail = source.QuestionDetail;
            target.SortOrder = source.SortOrder;
        }

        #endregion
    }
}

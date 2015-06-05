using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Product Market Brief Answer Mapper
    /// </summary>
    public static class ProductMarketBriefAnswerMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ProductMarketBriefAnswer CreateFrom(this DomainModels.ProductMarketBriefAnswer source)
        {
            return new ProductMarketBriefAnswer
            {
                MarketBriefAnswerId = source.MarketBriefAnswerId,
                MarketBriefQuestionId = source.MarketBriefQuestionId,
                AnswerDetail = source.AnswerDetail
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ProductMarketBriefAnswer CreateFrom(this ProductMarketBriefAnswer source)
        {
            return new DomainModels.ProductMarketBriefAnswer
            {
                MarketBriefAnswerId = source.MarketBriefAnswerId,
                MarketBriefQuestionId = source.MarketBriefQuestionId,
                AnswerDetail = source.AnswerDetail
            };
        }

    }
}
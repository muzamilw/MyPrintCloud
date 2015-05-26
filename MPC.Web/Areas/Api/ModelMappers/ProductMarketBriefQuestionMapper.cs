using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Product Market Brief Question Mapper
    /// </summary>
    public static class ProductMarketBriefQuestionMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ProductMarketBriefQuestion CreateFrom(this DomainModels.ProductMarketBriefQuestion source)
        {
            return new ProductMarketBriefQuestion
            {
                MarketBriefQuestionId = source.MarketBriefQuestionId,
                ItemId = source.ItemId,
                QuestionDetail = source.QuestionDetail,
                SortOrder = source.SortOrder,
                IsMultipleSelection = source.IsMultipleSelection,
                ProductMarketBriefAnswers = source.ProductMarketBriefAnswers != null ? source.ProductMarketBriefAnswers.Select(answer => answer.CreateFrom()).ToList() :
                new List<ProductMarketBriefAnswer>()
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ProductMarketBriefQuestion CreateFrom(this ProductMarketBriefQuestion source)
        {
            return new DomainModels.ProductMarketBriefQuestion
            {
                MarketBriefQuestionId = source.MarketBriefQuestionId,
                ItemId = source.ItemId,
                QuestionDetail = source.QuestionDetail,
                SortOrder = source.SortOrder,
                IsMultipleSelection = source.IsMultipleSelection,
                ProductMarketBriefAnswers = source.ProductMarketBriefAnswers != null ? source.ProductMarketBriefAnswers.Select(answer => answer.CreateFrom()).ToList() :
                new List<DomainModels.ProductMarketBriefAnswer>()
            };
        }

    }
}
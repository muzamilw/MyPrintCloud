using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Product Market Brief Question Api Model
    /// </summary>
    public class ProductMarketBriefQuestion
    {
        public int MarketBriefQuestionId { get; set; }
        public long? ItemId { get; set; }
        public string QuestionDetail { get; set; }
        public int SortOrder { get; set; }
        public bool? IsMultipleSelection { get; set; }
        public IEnumerable<ProductMarketBriefAnswer> ProductMarketBriefAnswers { get; set; }
    }
}
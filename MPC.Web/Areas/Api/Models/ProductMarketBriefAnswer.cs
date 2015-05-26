namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Product Market Brief Answer Api Model
    /// </summary>
    public class ProductMarketBriefAnswer
    {
        public int MarketBriefAnswerId { get; set; }
        public int? MarketBriefQuestionId { get; set; }
        public string AnswerDetail { get; set; }

    }
}
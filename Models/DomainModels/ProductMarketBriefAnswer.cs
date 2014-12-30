namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Market Brief Answer
    /// </summary>
    public class ProductMarketBriefAnswer
    {
        public int MarketBriefAnswerId { get; set; }
        public int? MarketBriefQuestionId { get; set; }
        public string AnswerDetail { get; set; }

        public virtual ProductMarketBriefQuestion ProductMarketBriefQuestion { get; set; }
    }
}

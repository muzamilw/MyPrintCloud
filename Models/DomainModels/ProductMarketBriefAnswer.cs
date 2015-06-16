using System;

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

        #region Public

        /// <summary>
        /// Makes a copy of Market Brief Answer
        /// </summary>
        public void Clone(ProductMarketBriefAnswer target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidProductMarketBriefAnswer, "target");
            }

            target.AnswerDetail = AnswerDetail;
        }

        #endregion
    }
}

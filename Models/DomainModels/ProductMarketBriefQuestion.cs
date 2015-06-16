using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Market Brief Question Domain Model
    /// </summary>
    public class ProductMarketBriefQuestion
    {
        public int MarketBriefQuestionId { get; set; }
        public long? ItemId { get; set; }
        public string QuestionDetail { get; set; }
        public int SortOrder { get; set; }
        public bool? IsMultipleSelection { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<ProductMarketBriefAnswer> ProductMarketBriefAnswers { get; set; }

        #region Public

        /// <summary>
        /// Makes a copy of Market Brief Question
        /// </summary>
        public void Clone(ProductMarketBriefQuestion target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidProductMarketBriefQuestion, "target");
            }

            target.QuestionDetail = QuestionDetail;
            target.IsMultipleSelection = IsMultipleSelection;
            target.SortOrder = SortOrder;
        }

        #endregion
    }
}

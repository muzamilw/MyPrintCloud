//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductMarketBriefQuestion
    {
        public ProductMarketBriefQuestion()
        {
            this.ProductMarketBriefAnswers = new HashSet<ProductMarketBriefAnswer>();
        }
    
        public int MarketBriefQuestionId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string QuestionDetail { get; set; }
        public int SortOrder { get; set; }
        public Nullable<bool> isMultipleSelction { get; set; }
    
        public virtual ICollection<ProductMarketBriefAnswer> ProductMarketBriefAnswers { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ProductMarketBriefQuestions
    {
        public tbl_ProductMarketBriefQuestions()
        {
            this.tbl_ProductMarketBriefAnswers = new HashSet<tbl_ProductMarketBriefAnswers>();
        }
    
        public int MarketBriefQuestionID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string QuestionDetail { get; set; }
        public int SortOrder { get; set; }
        public Nullable<bool> isMultipleSelction { get; set; }
    
        public virtual ICollection<tbl_ProductMarketBriefAnswers> tbl_ProductMarketBriefAnswers { get; set; }
    }
}

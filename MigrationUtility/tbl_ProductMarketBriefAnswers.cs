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
    
    public partial class tbl_ProductMarketBriefAnswers
    {
        public int MarketBriefAnswerID { get; set; }
        public Nullable<int> MarketBriefQuestionID { get; set; }
        public string AnswerDetail { get; set; }
    
        public virtual tbl_ProductMarketBriefQuestions tbl_ProductMarketBriefQuestions { get; set; }
    }
}
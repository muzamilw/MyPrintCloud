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
    
    public partial class CampaignClickThrough
    {
        public int ClickThroughId { get; set; }
        public int CampaignId { get; set; }
        public Nullable<int> ClickThruCountTotal { get; set; }
        public Nullable<int> ClickThruCount { get; set; }
        public string ClickThruUpdateField { get; set; }
        public string ClickThruText { get; set; }
        public string ClickThruURL { get; set; }
        public Nullable<int> AutoResponseId { get; set; }
        public string StoredProcedure { get; set; }
    }
}
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
    
    public partial class Widget
    {
        public Widget()
        {
            this.CmsSkinPageWidgets = new HashSet<CmsSkinPageWidget>();
        }
    
        public long WidgetId { get; set; }
        public string WidgetCode { get; set; }
        public string WidgetName { get; set; }
        public string WidgetControlName { get; set; }
    
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
    }
}

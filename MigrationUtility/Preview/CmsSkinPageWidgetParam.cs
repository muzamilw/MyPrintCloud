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
    
    public partial class CmsSkinPageWidgetParam
    {
        public long PageWidgetParamId { get; set; }
        public Nullable<long> PageWidgetId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
    
        public virtual CmsSkinPageWidget CmsSkinPageWidget { get; set; }
    }
}

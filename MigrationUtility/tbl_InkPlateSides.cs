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
    
    public partial class tbl_InkPlateSides
    {
        public int PlateInkID { get; set; }
        public string InkTitle { get; set; }
        public string PlateInkDescription { get; set; }
        public bool isDoubleSided { get; set; }
        public int PlateInkSide1 { get; set; }
        public int PlateInkSide2 { get; set; }
    }
}
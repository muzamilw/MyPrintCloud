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
    
    public partial class tbl_enquiry_options
    {
        public int ID { get; set; }
        public int OptionNo { get; set; }
        public string ItemTitle { get; set; }
        public int CoverPages { get; set; }
        public int TextPages { get; set; }
        public int OtherPages { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int OrientationID { get; set; }
        public int EnquiryID { get; set; }
        public Nullable<int> NominalCode { get; set; }
    }
}
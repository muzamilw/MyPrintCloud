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
    
    public partial class ListingImage
    {
        public long ListingImageId { get; set; }
        public Nullable<long> ListingId { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public Nullable<int> ImageOrder { get; set; }
        public Nullable<System.DateTime> LastMode { get; set; }
        public string ImageRef { get; set; }
        public string PropertyRef { get; set; }
        public string ClientImageId { get; set; }
    }
}

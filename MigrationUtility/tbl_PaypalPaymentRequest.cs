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
    
    public partial class tbl_PaypalPaymentRequest
    {
        public int Request_ID { get; set; }
        public int Order_ID { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int Status { get; set; }
    }
}

using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    [DelimitedRecord("|")]
    [IgnoreFirst()]
    public class ImportProducts
    {

          public string ProductCode { get; set; }

         public string Product_Name { get; set; }
         public string Category { get; set; }
         public string FinishedSize { get; set; }

         public string PrintedPages { get; set; }
         public string isQtyRanged { get; set; }
         public string quantity { get; set; }
         public string QtyRangeFrom { get; set; }
         public string QtyRangeTo { get; set; }
         public string StockLabel1 { get; set; }
         public string Price1 { get; set; }
         public string StockLabel2 { get; set; }
         public string Price2 { get; set; }
         public string StockLabel3 { get; set; }
         public string Price3 { get; set; }
         public string StockLabel4 { get; set; }
         public string Price4 { get; set; }
         public string StockLabel5 { get; set; }
        public string Price5 { get; set; }

        public string StockLabel6 { get; set; }

          public string Price6 { get; set; }
         public string StockLabel7 { get; set; }
         public string Price7 { get; set; }
         public string StockLabel8 { get; set; }
         public string Price8 { get; set; }

         public string StockLabel9 { get; set; }

         public string Price9 { get; set; }

         public string SupplierPrice1 { get; set; }

         public string SupplierPrice2 { get; set; }

         public string SupplierPrice3 { get; set; }

         public string SupplierPrice4 { get; set; }

         public string SupplierPrice5 { get; set; }

         public string SupplierPrice6 { get; set; }

         public string SupplierPrice7 { get; set; }

         public string SupplierPrice8 { get; set; }

         public string SupplierPrice9 { get; set; }

         public string JobDescriptionTitle1 { get; set; }

         public string JobDescription1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
         public string JobDescription7 { get; set; }

      
           
           
         
           

    }
}
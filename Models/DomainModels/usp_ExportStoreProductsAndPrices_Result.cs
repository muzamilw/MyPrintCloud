using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ExportStoreProductsAndPrices_Result
    {
        public string ProductCode { get; set; }
        public string Product_Name { get; set; }
        public string Category { get; set; }
        public string FinishedSize { get; set; }
        public string PrintedPages { get; set; }
        public Nullable<bool> isQtyRanged { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> QtyRangeFrom { get; set; }
        public Nullable<int> QtyRangeTo { get; set; }
        public string StockLabel1 { get; set; }
        public Nullable<double> Price1 { get; set; }
        public string StockLabel2 { get; set; }
        public Nullable<double> Price2 { get; set; }
        public string StockLabel3 { get; set; }
        public Nullable<double> Price3 { get; set; }
        public string StockLabel4 { get; set; }
        public Nullable<double> Price4 { get; set; }
        public string StockLabel5 { get; set; }
        public Nullable<double> Price5 { get; set; }
        public string StockLabel6 { get; set; }
        public Nullable<double> Price6 { get; set; }
        public string StockLabel7 { get; set; }
        public Nullable<double> Price7 { get; set; }
        public string StockLabel8 { get; set; }
        public Nullable<double> Price8 { get; set; }
        public string StockLabel9 { get; set; }
        public Nullable<double> Price9 { get; set; }
        public int Supplier1 { get; set; }
        public int Supplier2 { get; set; }
        public int Supplier3 { get; set; }
        public int Supplier4 { get; set; }
        public int Supplier5 { get; set; }
        public int Supplier6 { get; set; }
        public int Supplier7 { get; set; }
        public int Supplier8 { get; set; }
        public int Supplier9 { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescription7 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
    }
}

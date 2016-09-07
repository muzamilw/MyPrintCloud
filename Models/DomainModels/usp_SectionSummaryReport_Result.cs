using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_SectionSummaryReport_Result
    {
        public long EstimateID { get; set; }
        public long ItemID { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ContactFullName { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public string ProductCode { get; set; }
        public string WebDescription { get; set; }
        public string JobDescription { get; set; }
        public string SectionName { get; set; }
        public Nullable<int> SectionNo { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public string Qty1WorkInstructions { get; set; }
        public string CostCenterName { get; set; }
        public string ItemNotes { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty1Tax1Value { get; set; }
        public Nullable<double> GrossTotal { get; set; }
        public string FullProductName { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<double> CostCenterChargeQty1 { get; set; }
        public Nullable<bool> IsDoubleSided { get; set; }
        public Nullable<bool> IsBooklet { get; set; }
        public Nullable<bool> isWorknTurn { get; set; }
        public Nullable<int> SectionQty1 { get; set; }
        public long ItemSectionId { get; set; }
        public string ItemSize { get; set; }
        public double InkTotal { get; set; }
        public double PressTotal { get; set; }
        public double StockTotal { get; set; }
        public double PlatesTotal { get; set; }
        public double MakeReadyTotal { get; set; }
        public double WashupTotal { get; set; }
        public double GuillotineTotal { get; set; }
        public double OtherCostCenterTotal { get; set; }
    }
}

using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Model For Calender Dialog
    /// </summary>
    public class CompanyForCalender
    {
        public long CompanyId { get; set; }
        public long? StoreId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public short IsCustomer { get; set; }
        public DateTime? CreationDate { get; set; }
        public double? TaxRate { get; set; }
    }
}
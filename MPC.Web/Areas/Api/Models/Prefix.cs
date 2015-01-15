using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class Prefix
    {
        #region Persisted Properties

        /// <summary>
        /// Prefix id
        /// </summary>
        public long PrefixId { get; set; }

        /// <summary>
        /// Estimate Prefix
        /// </summary>
        public string EstimatePrefix { get; set; }

        /// <summary>
        /// Estimate Start
        /// </summary>
        public long? EstimateStart { get; set; }

        /// <summary>
        /// Estimate Next
        /// </summary>
        public long? EstimateNext { get; set; }

        /// <summary>
        /// Invoice Prefix
        /// </summary>
        public string InvoicePrefix { get; set; }

        /// <summary>
        /// Invoice Start
        /// </summary>
        public long? InvoiceStart { get; set; }

        /// <summary>
        /// Invoice Next
        /// </summary>
        public long? InvoiceNext { get; set; }

        /// <summary>
        /// Job Prefix
        /// </summary>
        public string JobPrefix { get; set; }

        /// <summary>
        /// Job Start
        /// </summary>
        public long? JobStart { get; set; }

        /// <summary>
        /// Job Next
        /// </summary>
        public long? JobNext { get; set; }

        /// <summary>
        /// PO Prefix
        /// </summary>
        public string PoPrefix { get; set; }

        /// <summary>
        /// PO Start
        /// </summary>
        public long? PoStart { get; set; }

        /// <summary>
        /// PO Next
        /// </summary>
        public long? PoNext { get; set; }

        /// <summary>
        /// Gof Prefix
        /// </summary>
        public string GofPrefix { get; set; }

        /// <summary>
        /// Gof Start
        /// </summary>
        public long? GofStart { get; set; }

        /// <summary>
        /// Gof Next
        /// </summary>
        public long? GofNext { get; set; }

        /// <summary>
        /// Item Prefix
        /// </summary>
        public string ItemPrefix { get; set; }

        /// <summary>
        /// Item Start
        /// </summary>
        public long? ItemStart { get; set; }

        /// <summary>
        /// Item Next
        /// </summary>
        public long? ItemNext { get; set; }

        /// <summary>
        /// DeliveryN Prefix
        /// </summary>
        public string DeliveryNPrefix { get; set; }

        /// <summary>
        /// DeliveryN Start
        /// </summary>
        public long? DeliveryNStart { get; set; }

        /// <summary>
        /// DeliveryN Next
        /// </summary>
        public long? DeliveryNNext { get; set; }

        /// <summary>
        /// JobCard Prefix
        /// </summary>
        public string JobCardPrefix { get; set; }

        /// <summary>
        /// JobCard Start
        /// </summary>
        public long? JobCardStart { get; set; }

        /// <summary>
        /// JobCard Next
        /// </summary>
        public long? JobCardNext { get; set; }

        /// <summary>
        /// Grn Prefix
        /// </summary>
        public string GrnPrefix { get; set; }

        /// <summary>
        /// Grn Start
        /// </summary>
        public long? GrnStart { get; set; }

        /// <summary>
        /// Grn Next
        /// </summary>
        public long? GrnNext { get; set; }

        /// <summary>
        /// Product Prefix
        /// </summary>
        public string ProductPrefix { get; set; }

        /// <summary>
        /// Product Start
        /// </summary>
        public long? ProductStart { get; set; }

        /// <summary>
        /// Product Next
        /// </summary>
        public long? ProductNext { get; set; }

        /// <summary>
        /// FinishedGoods Prefix
        /// </summary>
        public string FinishedGoodsPrefix { get; set; }

        /// <summary>
        /// FinishedGoods Start
        /// </summary>
        public long? FinishedGoodsStart { get; set; }

        /// <summary>
        /// FinishedGoods Next
        /// </summary>
        public long? FinishedGoodsNext { get; set; }

        /// <summary>
        /// Order Prefix
        /// </summary>
        public string OrderPrefix { get; set; }

        /// <summary>
        /// Order Start
        /// </summary>
        public long? OrderStart { get; set; }

        /// <summary>
        /// Order Next
        /// </summary>
        public long? OrderNext { get; set; }

        /// <summary>
        /// Enquiry Prefix
        /// </summary>
        public string EnquiryPrefix { get; set; }

        /// <summary>
        /// Enquiry Start
        /// </summary>
        public long? EnquiryStart { get; set; }

        /// <summary>
        /// Enquiry Next
        /// </summary>
        public long? EnquiryNext { get; set; }

        /// <summary>
        /// StockItem Prefix
        /// </summary>
        public string StockItemPrefix { get; set; }

        /// <summary>
        /// StockItem Start
        /// </summary>
        public long? StockItemStart { get; set; }

        /// <summary>
        /// StockItem Next
        /// </summary>
        public long? StockItemNext { get; set; }

        /// <summary>
        /// System Site Id
        /// </summary>
        public int? SystemSiteId { get; set; }

        /// <summary>
        /// Markup Id
        /// </summary>
        public long? MarkupId { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Job Manager Id
        /// </summary>
        public int? JobManagerId { get; set; }

        /// <summary>
        /// Order Manager Id
        /// </summary>
        public int? OrderManagerId { get; set; }

        /// <summary>
        /// Organisation Id
        /// </summary>
        public long? OrganisationId { get; set; }

        #endregion

        #region Reference Properties

        /// <summary>
        /// Markup
        /// </summary>
        public virtual Markup Markup { get; set; }

        #endregion
    }
}
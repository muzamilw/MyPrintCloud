using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ReportNote
    {
        public int Id { get; set; }
        public string FootNotes { get; set; }
        public string HeadNotes { get; set; }
        public string AdvertitorialNotes { get; set; }
        public int? UserId { get; set; }
        public int? ReportCategoryId { get; set; }
        public int SystemSiteId { get; set; }
        public string ReportBanner { get; set; }
        public string ReportTitle { get; set; }
        public string BannerAbsolutePath { get; set; }
        public string EstimateBannerBytes { get; set; }

        public string OrderBannerBytes { get; set; }

        public string InvoiceBannerBytes { get; set; }

        public string PurchaseBannerBytes { get; set; }

        public string DeliveryBannerBytes { get; set; }
        public bool? isDefault { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }
    }
}
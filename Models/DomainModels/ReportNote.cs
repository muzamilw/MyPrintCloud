using System.ComponentModel.DataAnnotations.Schema;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Report Note Domain Model
    /// </summary>
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
        public bool? isDefault { get; set; }
        public long? OrganisationId { get; set; }
        public long? CompanyId { get; set; }
        [NotMapped]
        public string EstimateBannerBytes { get; set; }
         [NotMapped]
        public string OrderBannerBytes { get; set; }
         [NotMapped]
        public string InvoiceBannerBytes { get; set; }
         [NotMapped]
        public string PurchaseBannerBytes { get; set; }
         [NotMapped]
        public string DeliveryBannerBytes { get; set; }

        public virtual ReportCategory ReportCategory { get; set; }
    }
}

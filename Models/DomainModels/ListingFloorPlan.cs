using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Floor Plan Domain Model
    /// </summary>
    public class ListingFloorPlan
    {
        public long FloorPlanId { get; set; }
        public long? ListingId { get; set; }
        public string ImageURL { get; set; }
        public string PDFURL { get; set; }
        public DateTime? LastMode { get; set; }
        public string ClientFloorplanID { get; set; }
    }
}

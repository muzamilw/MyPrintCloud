﻿namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cms Offer Domain Model
    /// </summary>
    public class CmsOffer
    {
        public int OfferId { get; set; }
        public int? ItemId { get; set; }
        public int? OfferType { get; set; }
        public string Description { get; set; }
        public string ItemName { get; set; }
        public int? SortOrder { get; set; }
        public long? CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
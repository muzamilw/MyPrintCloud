using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    public class CampaignImage
    {
        public long CampaignImageId { get; set; }
        public long? CampaignId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }

        public virtual Campaign Campaign { get; set; }

        #region Additional Properties
        [NotMapped]
        public string ImageByteSource { get; set; }
        #endregion
    }
}

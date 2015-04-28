using System.IO;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Campaign Image Mapper
    /// </summary>
    public static class CampaignImageMapper
    {
        public static CampaignImage CreateFrom(this DomainModels.CampaignImage source)
        {
            byte[] bytes = null;
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                string imagePath = HttpContext.Current.Server.MapPath("~/" + source.ImagePath);
                if (File.Exists(imagePath))
                {
                    bytes = File.ReadAllBytes(imagePath);
                }
            }
            return new CampaignImage()
             {
                 CampaignImageId = source.CampaignImageId,
                 CampaignId = source.CampaignId,
                 ImageName = source.ImageName,
                 ImagePath = source.ImagePath,
                 Image = bytes
             };
        }

        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.CampaignImage CreateFrom(this CampaignImage source)
        {
            return new DomainModels.CampaignImage()
            {
                CampaignImageId = source.CampaignImageId,
                CampaignId = source.CampaignId,
                ImageByteSource = source.ImageByteSource,
                ImageName = source.ImageName
            };
        }

    }
}
using MPC.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface ITemplateBackgroundImagesRepository : IBaseRepository<TemplateBackgroundImage, int>
    {
        void DeleteTemplateBackgroundImages(long productID, out List<TemplateBackgroundImage> objTemplateImages);
        List<TemplateBackgroundImage> GetProductBackgroundImages(long productId);

        TemplateBackgroundImage DeleteBackgroundImage(long imageId);
        void UpdateCropedImage(int mode, string contentString, string newImgPath, string newBkImgPath, long objectID, long productID, int imgHeight, int imgWidth);
        void DownloadImageLocally(long productId, string ImgName, int imageType, int ImageWidth, int ImageHeight);
        List<sp_GetImagesResponseModel> getImages(int isCalledFrom, int imageSetType, long productId, long contactCompanyId, long contactId, long territoryId, int pageNumber, string SearchKeyword, out int imageCount);
        TemplateBackgroundImage getImage(long imgID);
        TemplateBackgroundImage UpdateImage(long imageID, string imgTitle, string imgDescription, string imgKeywords, int imType);
        List<CompanyTerritory> getCompanyTerritories(long companyId);
        long insertImageRecord(List<TemplateBackgroundImage> listImages);

        bool UpdateImgTerritories(long imgID, string territory);
        List<ImagePermission> getImgTerritories(long imgID);

        List<RealEstateImage> getPropertyImages(long propertyId);

    }
}

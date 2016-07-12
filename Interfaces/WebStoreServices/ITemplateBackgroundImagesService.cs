using MPC.Common;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateBackgroundImagesService
    {
        void DeleteTemplateBackgroundImages(long productID, long OrganisationID);
        List<TemplateBackgroundImage> GetProductBackgroundImages(long productId, long organisationID);
        bool DeleteProductBackgroundImage(long productID, long ImageID, long organisationID);
        string CropImage(string ImgName, int ImgX1, int ImgY1, int ImWidth1, int ImHeight1, string ImProductName, int mode, long objectID, long organisationID);
        string DownloadImageLocally(string ImgName, long TemplateID, string imgType, long organisationID, bool addRecord);
        DesignerDamImageWrapper getImages(int isCalledFrom, int imageSetType, long productId, long contactCompanyID, long contactID, long territoryId, int pageNumner, string SearchKeyword, long OrganisationID);
        TemplateBackgroundImage getImage(long imgID, long OrganisationID);
        TemplateBackgroundImage UpdateImage(long imageID, int imType, string imgTitle, string imgDescription, string imgKeywords);
        TemplateBackgroundImage InsertUploadedImageRecord(string imageName, long productId, int uploadedFrom, long contactId, long organisationId, int imageType, long contactCompanyID);
        List<CompanyTerritory> getCompanyTerritories(long companyId);

        List<ImagePermission> getImgTerritories(long imgID);

        bool UpdateImgTerritories(long imgID, string territory);

        List<RealEstateImage> getPropertyImages(long propertyId);
        bool generateClippingPath(string path);


        int generatePdfAsBackgroundDesigner(string physicalPath, long TemplateID, long organisationId);
    }
}

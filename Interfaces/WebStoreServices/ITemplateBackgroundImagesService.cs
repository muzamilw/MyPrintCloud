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
        string DownloadImageLocally(string ImgName, long TemplateID, string imgType, long organisationID);
    }
}

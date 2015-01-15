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
    }
}

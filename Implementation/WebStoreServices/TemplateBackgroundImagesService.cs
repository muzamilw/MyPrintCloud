using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateBackgroundImagesService : ITemplateBackgroundImagesService
    {
        #region private
        public readonly ITemplateBackgroundImagesRepository _templateImagesRepository;
        #endregion
        #region constructor
        public TemplateBackgroundImagesService(ITemplateBackgroundImagesRepository templateImagesRepository, IProductCategoryRepository ProductCategoryRepository)
        {
            this._templateImagesRepository = templateImagesRepository;
        }
        #endregion
        //delete all template images of the given tempalte // added by saqib
        public void DeleteTemplateBackgroundImages(long productID, long OrganisationID)
        {
            List<TemplateBackgroundImage> listImages ;
            _templateImagesRepository.DeleteTemplateBackgroundImages(productID, out listImages);
            var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/Templates/" + productID.ToString());
                
            foreach (var obj in listImages)
            {
                string sfilePath = drURL + "/"+  obj.ImageName;
                if (File.Exists(sfilePath))
                {
                    File.Delete(sfilePath);

                }
            }
        }
    }
}

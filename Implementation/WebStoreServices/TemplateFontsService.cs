using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateFontsService : ITemplateFontsService
    {
        #region private
        public readonly ITemplateFontsRepository _templateFontRepository;
        #endregion
        #region constructor
        public TemplateFontsService(ITemplateFontsRepository templateFontRepository)
        {
            this._templateFontRepository = templateFontRepository;
        }
        #endregion

        #region public
        // get fonts list ,called from designer // added by saqib ali
        public List<TemplateFont> GetFontList(long productId, long customerId)
        {
            var fonts = _templateFontRepository.GetFontList(productId, customerId);
            List<TemplateFont> objToReturn = new List<TemplateFont>();
            foreach (var objFonts in fonts)
            {
                string path = "";
                if (objFonts.FontPath != null)
                {
                    path = objFonts.FontPath;
                }
                else
                {
                    path = "PrivateFonts/FontFace/";
                }

                objFonts.FontFile = "Designer/" + path + objFonts.FontFile;
                objToReturn.Add(objFonts);
            }
            return objToReturn;
        }

        // called from mis to delete template fonts against company(Store) and organization // added by saqib ali
        public void DeleteTemplateFonts(long Companyid, long organizationID)
        {
            _templateFontRepository.DeleteTemplateFonts(Companyid);
            string drUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/WebFonts/" + Companyid.ToString());
            if (System.IO.Directory.Exists(drUrl))
            {

                foreach (string item in System.IO.Directory.GetFiles(drUrl))
                {
                    System.IO.File.Delete(item);
                }

                System.IO.Directory.Delete(drUrl);
            }
        }
        public List<TemplateFont> GetFontList()
        {
           return _templateFontRepository.GetFontList();
        }

        #endregion
    }
}

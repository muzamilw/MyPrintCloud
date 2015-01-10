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

       

     

        #endregion
    }
}

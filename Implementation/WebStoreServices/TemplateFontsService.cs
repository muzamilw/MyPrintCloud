using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
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
        public readonly IItemRepository _itemRepository;
        #endregion
        #region constructor
        public TemplateFontsService(ITemplateFontsRepository templateFontRepository, IItemRepository itemRepository)
        {
            this._templateFontRepository = templateFontRepository;
            this._itemRepository = itemRepository;
        }
        #endregion

        #region public
        // get fonts list ,called from designer // added by saqib ali
        public List<TemplateFontResponseModel> GetFontList(long productId, long customerId, long OrganisationID, long territoryId,long itemID)
        {
           
            //if not zero then its a retail store case
            if  ( itemID != 0)
            {
                var item = this._itemRepository.GetItemByItemID(itemID);

                //replacing the customerid in this case as its not needed and we'llk pull the store fonts instead
                customerId = item.CompanyId.Value;

                
            }

            var fonts = _templateFontRepository.GetFontList(productId, customerId,territoryId);
            List<TemplateFontResponseModel> objToReturn = new List<TemplateFontResponseModel>();
            foreach (var objFonts in fonts)
            {
                string path = "";
                if (objFonts.FontPath != null)
                {
                    path = objFonts.FontPath;
                    if(path.Contains("Organisation" + OrganisationID.ToString() + "/WebFonts/")) {
                        path = path.Replace("Organisation" + OrganisationID.ToString() + "/WebFonts/", "");
                    }
                }
                var drURL = "MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/" ;

                objFonts.FontFile = drURL + path + objFonts.FontFile;
                objToReturn.Add(objFonts);
            }
            return objToReturn;
        }

        // called from mis to delete template fonts against company(Store) and Organisation // added by saqib ali
        public void DeleteTemplateFonts(long Companyid, long OrganisationID)
        {
            _templateFontRepository.DeleteTemplateFonts(Companyid);
            string drUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + OrganisationID.ToString() + "/WebFonts/" + Companyid.ToString());
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
        public List<TemplateFont> GetFontListForTemplate(long templateId)
        {
            return _templateFontRepository.GetFontListForTemplate(templateId);
        }
        public void InsertFontFile(long customerId, long organisationId, string FontName,string fontDisplayName)
        {
            try
            {
                string path = "Organisation" + organisationId + "/WebFonts/" + customerId + "/";
                string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(FontName);
                var FontObj = new TemplateFont();
                FontObj.FontName = fontDisplayName;
                FontObj.FontFile = fileNameWithoutExt;
                FontObj.FontDisplayName = fontDisplayName;
                FontObj.CustomerId = customerId;
                FontObj.IsPrivateFont = true;
                FontObj.IsEnable = true;
                FontObj.FontPath = path;
                _templateFontRepository.InsertFontFile(FontObj);


                
            }
            catch (Exception ex)
            {
                //AppCommon.LogException(ex);
                throw ex;
            }
        }
        #endregion
    }
}

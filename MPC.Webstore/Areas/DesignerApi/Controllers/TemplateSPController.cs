using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Models.DomainModels;
using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class TemplateSPController : ApiController
    {
        #region Private

        private readonly ITemplateService templateService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateSPController(ITemplateService templateService)
        {
            this.templateService = templateService;
        }

        #endregion
        #region public

        // old function name GetTemplate

        public HttpResponseMessage GetTemplate(int id)
        {

            var template = templateService.GetTemplate(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
        }

        // public MatchingSets GetMatchingSetbyID(int MatchingSetID)  //not used in webstore called from v2
        //public List<TemplateColorStyles> GetColorStyle(int ProductId) // moved to TemplateColorStyleController
        // public string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo)//not used in webstore called from v2
        //public string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo) // not implemented as not used in webstore 
        // public Templates GetTemplateWebStore(int TemplateID) //not implemented as we call this function from v2
        // public List<TemplateFonts> GetTemplateFonts(int TemplateID)//not implemented as we call this function from v2
        //public List<TemplatePages> GetTemplatePages(int TemplateID) // not implemented as we call this function from v2
        //public List<TemplatePages> GetTemplatePages(int TemplateID) // moved to template page controller new function name = GetTemplatePagesSP
        //public List<TemplatePages> UpdateTemplatePages(int TemplateID, int pageId, string operation) // not immplemented
        //public bool UpdateTemplatePage(int pageId, string PageName, string Orientation)// not implemented 
        //public bool UpdateTemplatePageBackground(int productID, int PageID, string path, string backgroundtype)// not implemented
        //public void AddNewPage(int templateID)// not implemented
        //public List<TemplateObjects> GetTemplateObjects(int TemplateID)
        //public List<TemplateBackgroundImages> GettemplateImages(int TemplateID)
        //public List<tbl_ProductCategoryFoldLines> GetFoldLinesByProductCategoryID(int ProductCategoryID, out bool ApplyFoldLines)
        //public string GenerateTemplateThumbnail(byte[] PDFDoc, string savePath, string ThumbnailFileName, double CuttingMargin)
        //public string GenerateProductBackground(string PDFPath, int index, string savePath, string BKImg, int PageNo)
        // public string getNextFileName(string dirPath)
        //public bool IsNumeric(object value)
        //public List<tbl_ProductCategory> GetCategoriesByMatchingSetID(int MatchingSetID)
        //public List<Templates> GetTemplates(string keywords, int ProductCategoryID, int PageNo, int PageSize, bool callbind, int status, int UserID, string Role, out int PageCount, int TemplateOwnerID, string userType)
        //public List<BaseColors> GetBaseColors()
        //public List<String> GetMatchingSetTheme()
        //public List<sp_GetTemplateThemeTags_Result> GetTemplateThemeTags(int? ProductID)
        //public List<sp_GetTemplateIndustryTags_Result> GetTemplateIndustryTags(int? ProductID)
        //private TemplateObjects ReturnObject(string Name, string Content, double PositionX, double PositionY, int ProductID, int DisplayOrder, int CtrlID, double FontSize, bool IsBold, int ProductPageID, int QtextOrder)
        //public int SaveTemplates(Templates oTemplate, List<TemplatePages> lstTemplatePages, List<TemplateIndustryTags> lstIndustryTags, List<TemplateThemeTags> lstThemeTags, bool IsAdd, out int NewTemplateID, bool IsCatChanged)
        // public bool DeleteCategory(int productCatID) 
        //private bool UpdateBackgroundPDF(int productPageID)
        //public bool CropImage(string ImgName, int ImgX, int ImgY, int ImgWidth, int ImgHeight)
        //public Bitmap CropImg1(System.Drawing.Image img, int xSize, int ySize, int wd, int ht)
        //public Bitmap CropImg2(System.Drawing.Image img, int xSize, int ySize, int wd, int ht)
        //public List<Tags> GetTags()
        //public List<Templates> GetTemplatesbyCategory(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, int IndustryID, int ThemeStyleID, int[] BaseColors, out int PageCount, out int SearchCount)
        //public List<sp_SearchTemplate_Result> GetTemplatesbyCategoryAndMultipleIndustryIds(string GlobalCategoryName, int PageNo, int PageSize, string Keywords, string IndustryID, int ThemeStyleID, string BaseColors, out int PageCount, out int SearchCount)
        //ublic List<Templates> GetTemplatesbyProductIds(int[] ProductIds, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        //public List<vw_WebStore_MatchingSets> GetTemplatesbyTemplateName(string TemplateName, string[] CategoryNames, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        //public List<vw_WebStore_MatchingSets> GetEditorsChoiceTemplates(string[] CategoryNames, int CustomerID, int PageNo, int PageSize, out int PageCount, out int SearchCount)
        // public List<vw_ProductCategoriesLeafNodes> GetCategories() // only used from v2 service
        //public List<vw_ProductCategoriesLeafNodesWithRes> GetCategoriesWithResolution()// only used from v2 service
        // public List<CategoryTypes> getCategoryTypes()// only used from v2 service
        //public List<CategoryRegions> getCategoryRegions()// only used from v2 service

        #endregion
   
    }
}

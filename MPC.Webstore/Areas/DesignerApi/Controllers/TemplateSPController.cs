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
        #endregion
   
    }
}

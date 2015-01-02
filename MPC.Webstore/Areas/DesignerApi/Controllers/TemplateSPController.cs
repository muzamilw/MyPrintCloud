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

        // old function name GetTemplateWebstore

        public HttpResponseMessage GetTemplate(int id)
        {
            var template = templateService.GetTemplate(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, template, formatter);
        }

        // public MatchingSets GetMatchingSetbyID(int MatchingSetID)
        //public List<TemplateColorStyles> GetColorStyle(int ProductId)
        // public string GetProductBackgroundImg(int ProductId, string BkImg, bool IsSide2, int PageNo)

        #endregion
   
    }
}

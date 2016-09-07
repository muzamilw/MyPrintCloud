using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class TemplateFontController : ApiController
    {
        #region Private

        private readonly ITemplateFontService _templateFontService;
        #endregion

        #region Constructor

        public TemplateFontController(ITemplateFontService templateFontService)
        {
            if(templateFontService == null)
                throw new ArgumentNullException("templateFontService");
            _templateFontService = templateFontService;
        }
        
        #endregion
        #region Public
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilter]
        public TemplateFontSearchResponse Get(long id, bool isTerritory)
        {
            var response = isTerritory
                ? _templateFontService.GetTemplateFontsByTerritory(id)
                : _templateFontService.GetTemplateFontsByStore(id);
            return new TemplateFontSearchResponse
            {
                TemplateFonts = response != null ? response.Select(t => t.CreateFrom()).ToList() : null
            };
        }

        // GET api/<controller>/5
        public TemplateFont Get(long id)
        {
            return _templateFontService.GetTemplateFontById(id).CreateFrom();
        }

        // POST api/<controller>
        public TemplateFont Post(TemplateFont value)
        {
            if (value == null || !ModelState.IsValid)
            {
                throw new MPCException("Invalid Font Data ", 0);
            }
            return _templateFontService.SaveTemplateFont(value.CreateFrom()).CreateFrom();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public bool Delete(TemplateFont font)
        {
            return _templateFontService.DeleteTemplateFont(font.ProductFontId);
        }
        #endregion
        
    }
}
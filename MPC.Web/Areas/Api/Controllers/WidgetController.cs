using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class WidgetController : ApiController
    {
        #region Private
        private readonly IWidgetService _widgetService;
        #endregion

        #region Constructor

        public WidgetController(IWidgetService widgetService)
        {
            if (widgetService == null)
            {
                throw new ArgumentNullException("widgetService");
            }

            this._widgetService = widgetService;
        }
        #endregion
        #region Public

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public WidgetSearchResponse Get()
        {
            var response = _widgetService.GetWidgetsByOrganisation();
            return new WidgetSearchResponse
            {
                OrganisationWidgets = response != null ? response.Select(widget => widget.CreateFrom()).ToList() : null
            };
        }
        
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Widget Get(long id)
        {
            return _widgetService.GetWidgetById(id).CreateFrom();
        }

        // POST api/<controller>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Widget Post(Widget widget)
        {
            if (widget == null || !ModelState.IsValid)
            {
                //throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
                throw new MPCException("Invalid Data ", 0);
            }
            return _widgetService.SaveWidget(widget.CreateFrom()).CreateFrom();
        }


        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public bool Delete(Widget widget)
        {
            _widgetService.DeleteWidget(widget.WidgetId);
            return true;
        }
        #endregion
        

        
    }
}
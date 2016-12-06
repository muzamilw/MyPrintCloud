﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.WebStoreServices;
using MPC.WebBase.Mvc;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class DownloadArtworkController : ApiController
    {

        #region Private
        
        private readonly ITemplateService _templateService;
        #endregion
        #region Constructor

        public DownloadArtworkController(ITemplateService templateService)
        {
            this._templateService = templateService;
        }
        #endregion

         [CompressFilterAttribute]
         [ApiException]
        public string Get(int itemId)
        {
            if (itemId <= 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            string path = _templateService.GetGemplateWithoutCropMarks(itemId);
            if (!string.IsNullOrEmpty(path))
            {
                string sFilePath = Request.RequestUri.Scheme + "://" + Request.RequestUri.Host + "/" + path;
                //var response = Request.CreateResponse(HttpStatusCode.Found);
                //response.Headers.Location = new Uri(sFilePath);
                return sFilePath;
            }
            else
            {
                return string.Empty;
            } 
           //  return _templateService.GetGemplateWithoutCropMarks(itemId);
        }
    }
}

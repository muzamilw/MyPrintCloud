using MPC.Interfaces.WebStoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class TemplateBackgroundImageController : ApiController
    {
        #region Private

        private readonly ITemplateBackgroundImagesService templateBackgroundImages;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public TemplateBackgroundImageController(ITemplateBackgroundImagesService templateBackgroundImages)
        {
            this.templateBackgroundImages = templateBackgroundImages;
        }

        #endregion
        #region public
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = template ID , parameter 2 = organisationID
        public HttpResponseMessage GetProductBackgroundImages(long parameter1, long parameter2)
        {
            var result = templateBackgroundImages.GetProductBackgroundImages(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = template ID , parameter 2 = imageid, parameter3 = organisationID
        public HttpResponseMessage DeleteProductBackgroundImage(long parameter1, long parameter2, long parameter3)
        {
            var result = templateBackgroundImages.DeleteProductBackgroundImage(parameter1, parameter2,parameter3);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = imageName , parameter 2 = imageX1, parameter3 = imageY1, parameter4 =imageWidth1 , parameter5 = imageHeight1,parameter6 = Image productName
            //parameter 77 = mode , parameter 8 = objectID, parameter 9 = organisationId
        public HttpResponseMessage CropImage(string parameter1, int parameter2, int parameter3, int parameter4, int parameter5, string parameter6, int parameter7, long parameter8, long parameter9)
        {
            var result = templateBackgroundImages.CropImage(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // parameter 1 = imagename ID , parameter 2 = templateID, parameter3 = image type, parameter4 = organisationID 
        public HttpResponseMessage DownloadImageLocally(string parameter1, long parameter2, string parameter3,long parameter4)
        {
            var result = templateBackgroundImages.DownloadImageLocally(parameter1, parameter2, parameter3,parameter4,true);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
   
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // int isCalledFrom, int imageSetType, long productId, long contactCompanyID, long contactID, long territoryId, int pageNumner, string SearchKeyword, long OrganisationID
        public HttpResponseMessage getImages(int parameter1, int parameter2, long parameter3, long parameter4, long parameter5, long parameter6, int parameter7, string parameter8, long parameter9)
        {
            var result = templateBackgroundImages.getImages(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // long imgID, long OrganisationID
        public HttpResponseMessage getImage(long parameter1, long parameter2)
        {
            var result = templateBackgroundImages.getImage(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // long imageID, int imType, string imgTitle, string imgDescription, string imgKeywords
        public HttpResponseMessage UpdateImage(long parameter1, int parameter2, string parameter3, string parameter4, string parameter5)
        {
            var result = templateBackgroundImages.UpdateImage(parameter1, parameter2, parameter3, parameter4, parameter5);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // string filepath, long productID, int uploaded from,contactId,organisationId,imageSetType,contactCompanyId
        public HttpResponseMessage UploadImageRecord(string parameter1,long parameter2, int parameter3, long parameter4, int parameter5, int parameter6, long parameter7)
        {
            var result = templateBackgroundImages.InsertUploadedImageRecord(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //contactCompanyID
        public HttpResponseMessage GetTerritories(long id)
        {
            var result = templateBackgroundImages.getCompanyTerritories(id) ;
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //imageID
        public HttpResponseMessage getImgTerritories(long id)
        {
            var result = templateBackgroundImages.getImgTerritories(id);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //contactCompanyID
        public HttpResponseMessage updateImgTerritories(long parameter1,string parameter2)
        {
            var result = templateBackgroundImages.UpdateImgTerritories(parameter1, parameter2);
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        //contactCompanyID
        public HttpResponseMessage getPropertyImages(long id)
        {
            var result = templateBackgroundImages.getPropertyImages(id) ;
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return Request.CreateResponse(HttpStatusCode.OK, result, formatter);

        }


       

        #endregion
    }
}

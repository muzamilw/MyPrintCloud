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
    public class ItemController : ApiController
    {
       #region Private
        private readonly ISmartFormService smartFormService;
        private readonly IItemService itemService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ItemController(IItemService itemService, ISmartFormService smartFormService)
        {
            this.itemService = itemService;
            this.smartFormService = smartFormService;
        }

        #endregion
        #region public
        //parameter1 = itemID , parameter2 = contactID
        public HttpResponseMessage GetItem(long parameter1,long parameter2)
        {
            var item = itemService.GetItemByIdDesigner(parameter1);
            long parentTemplateID = itemService.getParentTemplateID(parameter1);
            string[] images = smartFormService.GetContactImageAndCompanyLogo(parameter2);
            var result = new
            {
                ItemId = item.ItemId,
                SmartFormId = item.SmartFormId,
                allowPdfDownload = item.allowPdfDownload,
                allowImageDownload = item.allowImageDownload,
                drawWaterMarkTxt = item.drawWaterMarkTxt,
                drawBleedArea = item.drawBleedArea,
                printCropMarks = item.printCropMarks,
                isMultipagePDF = item.isMultipagePDF,
                IsTemplateDesignMode = item.IsTemplateDesignMode.HasValue ?  item.IsTemplateDesignMode.Value: 1,
                userImage = images[1],
                companyImage = images[0],
                ScaleFactor = item.Scalar,
                ParentTemplateId = parentTemplateID
            };

            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }
        #endregion
    }
}

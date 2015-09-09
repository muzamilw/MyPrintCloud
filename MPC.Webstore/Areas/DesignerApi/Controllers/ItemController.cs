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
        private readonly ITemplateService templateService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ItemController(IItemService itemService, ISmartFormService smartFormService, ITemplateService templateService)
        {
            this.templateService = templateService;
            this.itemService = itemService;
            this.smartFormService = smartFormService;
        }

        #endregion
        #region public
        //parameter1 = itemID , parameter2 = contactID, parameter3 = organisationId
        public HttpResponseMessage GetItem(long parameter1, long parameter2, long parameter3)
        {
            var item = itemService.GetItemByIdDesigner(parameter1);
            long parentTemplateID = itemService.getParentTemplateID(parameter1);
            long templateId = 0;
            if(item.TemplateId.HasValue)
                templateId = item.TemplateId.Value;
            string conversionRation = templateService.GetConvertedSizeWithUnits(templateId, parameter3, parameter1);
            string[] images = smartFormService.GetContactImageAndCompanyLogo(parameter2);
            long parentItemID = 0;
            if (item.RefItemId.HasValue)
                parentItemID = item.RefItemId.Value;
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
                companyImageHeight = images[2],
                companyImageWidth = images[3],
                contactImageHeight = images[4],
                contactImageWidth = images[5],
                ScaleFactor = item.Scalar,
                ParentTemplateId = parentTemplateID,
                RefItemId = parentItemID,
                IsUploadImage = item.IsUploadImage,
                ZoomFactor = item.ZoomFactor,
                TemplateDimensionConvertionRatio = conversionRation
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

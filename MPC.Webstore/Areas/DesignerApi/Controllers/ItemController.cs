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

        private readonly IItemService itemService;
        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        #endregion
        #region public
        public HttpResponseMessage GetItem(long id)
        {
            var item = itemService.GetItemByIdDesigner(id);
            //not needed in designer 
            //result.ProductSpecification = "";
            //result.CompleteSpecification = "";
            //result.WebDescription = "";
            //result.TipsAndHints = "";
            var result = new
            {
                ItemId = item.ItemId,
                SmartFormId = item.SmartFormId,
                allowPdfDownload = item.allowPdfDownload,
                allowImageDownload = item.allowImageDownload,
                drawWaterMarkTxt = item.drawWaterMarkTxt,
                drawBleedArea = item.drawBleedArea,
                printCropMarks = item.printCropMarks,
                isMultipagePDF = item.isMultipagePDF
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

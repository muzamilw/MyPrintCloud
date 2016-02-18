﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class DeleteAttachmentController : ApiController
    {
         #region Private

        private readonly IItemService _ItemService;
     
        #endregion
        #region Constructor
        public DeleteAttachmentController(IItemService ItemService)
        {
            
            this._ItemService = ItemService;
        }

        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DeleteArtworkAttachment(long AttachmentId, long ItemId, string pageType)
        {
            List<string> messages = new List<string>();
                   
            _ItemService.DeleteItemAttachment(AttachmentId);

            List<ItemAttachment> ListOfAttachments = _ItemService.GetItemAttactchments(ItemId);
            if (ListOfAttachments == null || ListOfAttachments.Count == 0)
            {
                _ItemService.UpdateUploadFlagInItem(ItemId, null);
                messages.Add("NoFiles");
                JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                return Request.CreateResponse(HttpStatusCode.OK, messages);
            }
            else
            {
                messages.Add("Success");
                string ArtworkHtml = "";
                if (pageType == "ProductOptionsAndDetails")
                {
                    foreach (var attach in ListOfAttachments)
                    {

                        ArtworkHtml = ArtworkHtml + "<div class='artwork_sides_container rounded_corners'><div class='artwork_image_sides_container float_left_simple'><img class='artwork_image_thumbnail' src='/" + attach.FolderPath + "/" + attach.FileName + "Thumb.png' /></div><div class='artwork_filedetail_container float_left_simple'><button type='button' class='delete_icon_img' onclick='ConfirmDeleteArtWorkPopUP(" + attach.ItemAttachmentId + "," + attach.ItemId + ",1);'></button></div><div class='clearBoth'>&nbsp;</div></div>";

                    }
                }
                else
                {
                    foreach (var attach in ListOfAttachments)
                    {
                        ArtworkHtml = ArtworkHtml + "<div class='LGBC BD_PCS rounded_corners'><div class='DeleteIconPP'><button type='button' class='delete_icon_img' onclick='ConfirmDeleteArtWorkPopUP(" + attach.ItemAttachmentId + "," + attach.ItemId + ");'</button></div><a><div class='PDTC_LP FI_PCS'><img class='full_img_ThumbnailPath_LP' src='/" + attach.FolderPath + "/" + attach.FileName + "Thumb.png' /></div></a><div class='confirm_design LGBC height40_LP '><label>" + attach.FileName + "</label></div></div>";


                    }
                }
                messages.Add(ArtworkHtml);
                JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

                return Request.CreateResponse(HttpStatusCode.OK, messages);
            }
                    
        }
    }
}

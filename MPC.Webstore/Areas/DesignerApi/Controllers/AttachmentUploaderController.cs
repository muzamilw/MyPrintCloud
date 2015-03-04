﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class AttachmentUploaderController : ApiController
    {
       #region Private

        private readonly IItemService itemService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public AttachmentUploaderController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        #endregion
        public async Task<List<string>> PostAsync(string id, string name, string ItemId, string ContactId, string CompanyId)
        {
            try
            {
                List<ItemAttachment> ListOfAttachments = ListOfAttachments = new List<ItemAttachment>(); //itemService.GetArtwork(ItemId);
                
                id = id.Replace("__", "/");
                name = name.Replace("__", "/");
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/" + id);
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);
                    //  string _idOfObject1 = HttpRequest.Content.Headers["IDofObject1"].ToString();
                    //string _idOfObject2 = Request.Content.Headers.GetValues("IDofObject2").ToString(); ;// Headers["IDofObject2"].ToString();
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    // string _param1 = streamProvider.FormData["pid"];
                    //  string _param2 = streamProvider.FormData["ItemID"];
                    List<string> messages = new List<string>();
                    ItemAttachment attachment = null;
                    foreach (var file in streamProvider.FileData)
                    {
                        
                        FileInfo fi = new FileInfo(file.LocalFileName);
                        messages.Add(fi.Name);
                        string srcPath = uploadPath + "/" + fi.Name;
                        string fileExt = Path.GetExtension(fi.Name);
                        string desPath = uploadPath + "/" + name  + fileExt;
                        File.Copy(srcPath, desPath, true);
                        File.Delete(srcPath);
                        if (fileExt == ".pdf" || fileExt == ".TIF" || fileExt == ".TIFF")
                        {
                            itemService.GenerateThumbnailForPdf(desPath, false);

                        }
                        else 
                        {
                            itemService.CreatAndSaveThumnail(null,desPath);
                        }
                        attachment = new ItemAttachment();
                        attachment.ContactId = Convert.ToInt64(ContactId);
                        attachment.Type = UploadFileTypes.Artwork.ToString();
                        attachment.ItemId = Convert.ToInt64(ItemId);
                        attachment.CompanyId = Convert.ToInt64(CompanyId);
                        attachment.FileName = name;
                        attachment.FileType = fileExt;
                        attachment.FolderPath = id;
                        attachment.FileTitle = "User Uploaded ArtWork";
                        attachment.IsApproved = 1;
                        attachment.isFromCustomer = 1;
                        attachment.UploadDate = DateTime.Now;
                        attachment.UploadTime = DateTime.Now;

                        ListOfAttachments.Add(attachment);
                    }

                    ListOfAttachments = itemService.SaveArtworkAttachments(ListOfAttachments);
                    if (ListOfAttachments == null)
                    {
                        messages.Add("Artwork not uploaded");
                        return messages;
                    }
                    else 
                    {
                        foreach(var attach in ListOfAttachments)
                        {
                            messages.Add(attach.FolderPath + attach.FileName);
                        }

                        return messages;
                    }
                    
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                    throw new HttpResponseException(response);
                }


            }
            catch (Exception ex)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
            finally
            {

            }
        }

    
    }
}
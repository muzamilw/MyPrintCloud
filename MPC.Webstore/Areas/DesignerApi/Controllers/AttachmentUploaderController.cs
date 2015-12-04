using MPC.Interfaces.WebStoreServices;
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
        public async Task<List<string>> PostAsync(string parameter1, string parameter2, string parameter3, string parameter4, string parameter5, string parameter6)
        {
            MyStreamProvider streamProvider = null;
            try
            {
                List<ItemAttachment> ListOfAttachments = ListOfAttachments = new List<ItemAttachment>(); //itemService.GetArtwork(ItemId);

                parameter1 = parameter1.Replace("__", "/");
                parameter2 = parameter2.Replace("__", "/");
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/" + parameter1);
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    streamProvider = new MyStreamProvider(uploadPath);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    List<string> messages = new List<string>();
                   
                    ItemAttachment attachment = null;
                    foreach (var file in streamProvider.FileData)
                    {
                        FileInfo fi = new FileInfo(file.LocalFileName);
                       // messages.Add(fi.Name);
                        string srcPath = uploadPath + "/" + fi.Name;
                        string fileExt = Path.GetExtension(fi.Name);
                        
                        string desPath = uploadPath + "/" + parameter2 + fileExt;
                        File.Copy(srcPath, desPath, true);
                        File.Delete(srcPath);
                        if (fileExt == ".pdf" || fileExt == ".TIF" || fileExt == ".TIFF")
                        {
                            itemService.GenerateThumbnailForPdf(desPath, false, Convert.ToInt64(parameter3));

                        }
                        else 
                        {
                            itemService.CreatAndSaveThumnail(null, desPath, parameter3 + "/");
                        }
                        attachment = new ItemAttachment();
                        attachment.ContactId = Convert.ToInt64(parameter4);
                        attachment.Type = UploadFileTypes.Artwork.ToString();
                        attachment.ItemId = Convert.ToInt64(parameter3);
                        attachment.CompanyId = Convert.ToInt64(parameter4);
                        attachment.FileName = parameter2;
                        attachment.FileType = fileExt;
                        attachment.FolderPath = parameter1;
                        attachment.FileTitle = "User Uploaded ArtWork";
                        attachment.IsApproved = 1;
                        attachment.isFromCustomer = 1;
                        attachment.UploadDate = DateTime.Now;
                        attachment.UploadTime = DateTime.Now;

                        ListOfAttachments.Add(attachment);
                    }
                    streamProvider.Contents.Clear();
                    ListOfAttachments = itemService.SaveArtworkAttachments(ListOfAttachments);
                    itemService.UpdateUploadFlagInItem(Convert.ToInt64(parameter3), 1);
                    if (ListOfAttachments == null)
                    {
                        messages.Add("Artwork not uploaded");
                        return messages;
                    }
                    else 
                    {
                        messages.Add("Success");
                        string ArtworkHtml = "";
                        if(parameter6 == "ProductOptionsAndDetails")
                        {
                             foreach(var attach in ListOfAttachments)
                            {
                               
                                ArtworkHtml = ArtworkHtml +   "<div class='artwork_sides_container rounded_corners'><div class='artwork_image_sides_container float_left_simple'><img class='artwork_image_thumbnail' src='/" + attach.FolderPath + "/" + attach.FileName + "Thumb.png' /></div><div class='artwork_filedetail_container float_left_simple'><button type='button' class='delete_icon_img' onclick='ConfirmDeleteArtWorkPopUP(" + attach.ItemAttachmentId + "," + attach.ItemId + ",1);'></button></div><div class='clearBoth'>&nbsp;</div></div>";
                                
                            }
                        }
                        else
                        {
                             foreach(var attach in ListOfAttachments)
                            {
                                ArtworkHtml = ArtworkHtml + "<div class='LGBC BD_PCS rounded_corners'><div class='DeleteIconPP'><button type='button' class='delete_icon_img' onclick='ConfirmDeleteArtWorkPopUP(" + attach.ItemAttachmentId + "," + attach.ItemId + ");'</button></div><a><div class='PDTC_LP FI_PCS'><img class='full_img_ThumbnailPath_LP' src='/" + attach.FolderPath + "/" + attach.FileName + "Thumb.png' /></div></a><div class='confirm_design LGBC height40_LP '><label>" + attach.FileName + "</label></div></div>";
                            

                            }
                        }
                       
                        messages.Add(ArtworkHtml);
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
                streamProvider = null;
            }
        }

    
    }
}

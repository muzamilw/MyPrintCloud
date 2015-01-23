using MPC.Interfaces.WebStoreServices;
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
        public async Task<List<string>> PostAsync(string id, string name)
        {
            try
            {
                id = id.Replace("__", "/");
                name = name.Replace("__", "/");
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/MPC_Content/" + id);
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);
                    //  string _idOfObject1 = HttpRequest.Content.Headers["IDofObject1"].ToString();
                    //string _idOfObject2 = Request.Content.Headers.GetValues("IDofObject2").ToString(); ;// Headers["IDofObject2"].ToString();
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    // string _param1 = streamProvider.FormData["pid"];
                    //  string _param2 = streamProvider.FormData["ItemID"];
                    List<string> messages = new List<string>();
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
                        

                    }

                    return messages;
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

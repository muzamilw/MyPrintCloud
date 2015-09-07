using MPC.Webstore.Areas.DesignerApi.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi.Controllers
{
    public class FileUploadController : ApiController
    {
        public async Task<List<string>> PostAsync()
        {
            try
            {
                
                if (Request.Content.IsMimeMultipartContent())
                {
                    //string[] fileList = files.Split(new string[] { "____" }, StringSplitOptions.None);
                    string uploadPath = HttpContext.Current.Server.MapPath("~/MPC_Content/EmailAttachments/" );
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    List<string> messages = new List<string>();
                    foreach (var file in streamProvider.FileData)
                    {
                       

                        FileInfo fi = new FileInfo(file.LocalFileName);
                        string newfile = Guid.NewGuid().ToString() + fi.Name;
                        string srcPath = uploadPath + "/" + fi.Name;

                        string desPath = uploadPath + "/" + newfile;

                        File.Copy(srcPath, desPath, true);
                        File.Delete(srcPath);
                        messages.Add(newfile);

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

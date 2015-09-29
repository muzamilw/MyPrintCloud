using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Net.Http.Headers;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class UploadController : ApiController
    {
        //parameter1 = filepath
        public async Task<List<string>> PostAsync(string id)
        {
            try
            {
                id = id.Replace("__", "/");
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/MPC_Content/"+ id);
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    List<string> messages = new List<string>();
                    foreach (var file in streamProvider.FileData)
                    {
                        FileInfo fi = new FileInfo(file.LocalFileName);
                        System.IO.File.Move( file.LocalFileName, file.LocalFileName.Replace("&",""));
                        messages.Add( fi.Name.Replace("&",""));

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
    public class MyStreamProvider : MultipartFormDataStreamProvider
    {
        public MyStreamProvider(string uploadPath)
            : base(uploadPath)
        {

        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string fileName = headers.ContentDisposition.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Guid.NewGuid().ToString() + ".data";
            }
            return fileName.Replace("\"", string.Empty);
        }
    }
}

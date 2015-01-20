using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MPC.Webstore.Areas.DesignerApi.Controllers
{
    public class UploadController : ApiController
    {
        //parameter1 = filepath
        public async Task<List<string>> PostAsync(string parameter1)
        {
            try
            {
                parameter1 = parameter1.Replace("__", "/");
                if (Request.Content.IsMimeMultipartContent())
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/MPC_Content/" + parameter1);


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
                        messages.Add(fi.FullName );
                        //string srcPath = uploadPath + "/" + fi.Name;
                        //string desPath = uploadPath + "/new/" + fi.Name;
                        //File.Copy(srcPath, desPath, true);
                        //File.Delete(srcPath);

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

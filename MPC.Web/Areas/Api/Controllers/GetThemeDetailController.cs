using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Castle.Core.Internal;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Theme Detail From Themeing Project
    /// </summary>
    public class GetThemeDetailController : ApiController
    {
        #region
        #endregion
        private readonly ICompanyService companyService;
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public GetThemeDetailController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        public void Get([FromUri] int themeId, string fullZipPath, long companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (!fullZipPath.IsNullOrEmpty())
            {
                //string[] str = fullZipPath.Split('/');
                //string themeName = str[str.Length - 1].Split('.')[0];
                ////Theme Already exist in MIS
                //if (Directory.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName)))
                //{
                //    companyService.ApplyTheme(themeId, themeName, companyId);
                //}
                //else
                //{
                //    GetZipFile(themeId, fullZipPath, companyId);
                //}

                GetZipFile(themeId, fullZipPath, companyId);
                // Get List of Skins 
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string url = "WebstoreApi/StoreCache/Get?id=" + companyId;
                    var response = client.GetAsync(url);
                }
            }


        }

        #endregion

        #region Private
        /// <summary>
        /// Get Zip File Of Theme
        /// </summary>
        private void GetZipFile(int themeId, string fullZipPath, long companyId)
        {
            WebClient webClient = new WebClient();
            Stream data = null;
            using (WebClient client = new WebClient())
            {
                try
                {
                    data = webClient.OpenRead(fullZipPath);
                }
                catch (WebException e)
                {
                    //How do I capture this from the UI to show the error in a message box?
                    throw e;
                }
            }

            WriteZipFile(themeId, data, fullZipPath, companyId);

        }

        /// <summary>
        /// Use For Write
        /// </summary>
        private void WriteZipFile(int themeId, Stream fileStream, string fullZipPath, long companyId)
        {
            string baseFolder = HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/");
            if (!Directory.Exists(baseFolder))
            {
                Directory.CreateDirectory(baseFolder);
            }
            // FileStream fr = fileStream;
            Stream st = fileStream;
            ZipInputStream ins = new ZipInputStream(st);
            ZipEntry ze = ins.GetNextEntry();
            while (ze != null)
            {
                if (ze.IsDirectory)
                {
                    Directory.CreateDirectory(baseFolder + "\\" + ze.Name);
                }
                else if (ze.IsFile)
                {
                    if (!Directory.Exists(baseFolder + Path.GetDirectoryName(ze.Name)))
                    {
                        Directory.CreateDirectory(baseFolder + Path.GetDirectoryName(ze.Name));
                    }

                    FileStream fs = File.Create(baseFolder + "\\" + ze.Name);

                    byte[] writeData = new byte[ze.Size];
                    int iteration = 0;
                    while (true)
                    {
                        int size = ins.Read(writeData, (int)Math.Min(ze.Size, (iteration * 2048)), (int)Math.Min(ze.Size - (int)Math.Min(ze.Size, (iteration * 2048)), 2048));
                        if (size > 0)
                        {
                            fs.Write(writeData, (int)Math.Min(ze.Size, (iteration * 2048)), size);
                        }
                        else
                        {
                            break;
                        }
                        iteration++;
                    }
                    fs.Close();
                }
                ze = ins.GetNextEntry();
            }
            ins.Close();
            st.Close();
            ApplyTheme(themeId, fullZipPath, companyId);
        }

        private void ApplyTheme(int themeId, string fullZipPath, long companyId)
        {
            string[] str = fullZipPath.Split('/');
            string themeName = str[str.Length - 1].Split('.')[0];
            //Theme Already exist in MIS
            if (Directory.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName)))
            {
                companyService.ApplyTheme(themeId, themeName, companyId);

            }
        }

        #endregion
    }
}
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Castle.Core.Internal;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using MPC.Interfaces.MISServices;


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
        public void Get([FromUri] string fullZipPath, long companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            if (!fullZipPath.IsNullOrEmpty())
            {
                string[] str = fullZipPath.Split('/');
                string themeName = str[str.Length - 1].Split('.')[0];
                //Theme Already exist in MIS
                if (Directory.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName)))
                {
                    companyService.ApplyTheme(themeName, companyId);

                }
                else
                {
                    GetZipFile(fullZipPath, companyId);
                }
            }


        }

        #endregion

        #region Private
        /// <summary>
        /// Get Zip File Of Theme
        /// </summary>
        private void GetZipFile(string fullZipPath, long companyId)
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

            WriteZipFile(data, fullZipPath, companyId);

        }

        /// <summary>
        /// Use For Write
        /// </summary>
        private void WriteZipFile(Stream fileStream, string fullZipPath, long companyId)
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
            ApplyTheme(fullZipPath, companyId);
        }

        private void ApplyTheme(string fullZipPath, long companyId)
        {
            string[] str = fullZipPath.Split('/');
            string themeName = str[str.Length - 1].Split('.')[0];
            //Theme Already exist in MIS
            if (Directory.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/Themes/" + themeName)))
            {
                companyService.ApplyTheme(themeName, companyId);

            }
        }

        #endregion
    }
}
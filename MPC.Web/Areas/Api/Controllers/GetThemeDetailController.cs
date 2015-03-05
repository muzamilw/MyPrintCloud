using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ICSharpCode.SharpZipLib.Zip;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Theme Detail From Themeing Project
    /// </summary>
    public class GetThemeDetailController : ApiController
    {
        #region Public
        public async void Get([FromUri] string fullZipPath)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            await GetZipFile(fullZipPath);
        }

        #endregion

        #region Private
        /// <summary>
        /// Get Zip File Of Theme
        /// </summary>
        private static async Task GetZipFile(string fullZipPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MPCThemingPath"]);

                string url = "ApplyTheme?fullZipPath=" + fullZipPath;
                using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                    if (response.IsSuccessStatusCode)
                    {
                        Stream resultStream = response.Content.ReadAsStreamAsync().Result;
                        WriteZipFile(resultStream);

                    }
            }
        }
        /// <summary>
        /// Use For Write
        /// </summary>
        private static void WriteZipFile(Stream fileStream)
        {
            string baseFolder = HttpContext.Current.Server.MapPath("~/MPC_Themes/");
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
        }
        #endregion
    }
}
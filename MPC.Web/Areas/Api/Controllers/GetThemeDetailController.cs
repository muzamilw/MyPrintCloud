using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MPC.MIS.Areas.Api.Models;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Get Theme Detail From Themeing Project
    /// </summary>
    public class GetThemeDetailController : ApiController
    {
        public void Get([FromUri] string fullZipPath)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            // Get List of Skins 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MPCThemingPath"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "ApplyTheme?fullZipPath=" + fullZipPath;
                string responsestr = "";
                var response = client.GetAsync(url);

                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr = response.Result.Content.ReadAsStringAsync().Result;
                    //themes = JsonConvert.DeserializeObject<List<SkinForTheme>>(responsestr);
                }
            }
        }
    }
}
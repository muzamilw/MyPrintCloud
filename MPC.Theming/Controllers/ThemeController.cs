using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MPC.Theming.Controllers
{
    /// <summary>
    /// Theme Api Controller 
    /// </summary>
    public class ThemeController : ApiController
    {
        #region Public
        // GET api/values
        public ArrayList GetThemesByOrganisationId(long organisationId)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            string queryString = "Select * From Skin Where (OrganisationId=" + organisationId + " or OrganisationId is null)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    var rdr = command.ExecuteReader();
                    ArrayList skins = new ArrayList();

                    //get the data reader, etc.
                    while (rdr.Read())
                    {
                        skins.Add(new
                        {
                            SkinId = rdr[0],
                            Name = rdr[1],
                            Type = rdr[3],
                            FullZipPath = rdr[4],
                            Thumbnail = rdr[2]
                        });
                    }

                    connection.Close();
                    return skins;
                }
                catch (Exception ex)
                {
                    // return "Please contact support@myprintcloud.com . There were errors in setting up your account : " + ex.ToString();
                }

            }

            return null;
        }

        // GET api/values
        [HttpGet]
        public HttpResponseMessage ApplyTheme([FromUri] string fullZipPath)
        {
            fullZipPath = "http://themes.myprintcloud.com/mpc_themes/classic.zip";
            HttpResponseMessage result = null;
            if (File.Exists(fullZipPath))
            {
                result = new HttpResponseMessage(HttpStatusCode.OK);
                FileStream stream = new FileStream(fullZipPath, FileMode.Open);
                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Theme.zip"
                };
            }

            return result;
        }
        #endregion
    }
}
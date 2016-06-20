using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace MPC.Provisioning.Controllers
{
    public class ProvisioningController : ApiController
    {
        // GET: Api/Provisioning
        //public string Get1()
        //{

        //    string result ="";
        //    try
        //    {




        //        string scriptfile = HttpContext.Current.Server.MapPath("~/scripts/provisionNew.ps1");

        //        //using (PowerShell PowerShellInstance = PowerShell.Create())
        //        //{


        //        //    PowerShellInstance.AddCommand("Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Force -Scope Process", true);

        //        //    PowerShellInstance.Commands.AddScript(scriptfile);


        //        //     use "AddParameter" to add a single parameter to the last command/script on the pipeline.
        //        //    PowerShellInstance.AddParameter("param1", "parameter 1 value!");

        //        //    var ressults = PowerShellInstance.Invoke();

        //        //}



        //        RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();

        //        Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
        //        runspace.Open();



        //        RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);
        //        //scriptInvoker.Invoke("Set-ExecutionPolicy Unrestricted");

        //        Pipeline pipeline = runspace.CreatePipeline();

        //        //pipeline.Commands.Add("Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted");

        //        //Here's how you add a new script with arguments
        //        //Command myCommand = new Command(scriptfile);
        //        //CommandParameter testParam = new CommandParameter("key", "value");
        //        //myCommand.Parameters.Add(testParam);

        //        pipeline.Commands.AddScript("Start-Job -FilePath "+ scriptfile +" -Name MyCommandJob -RunAs32");

        //        //pipeline.Commands.Add(myCommand);

        //        // Execute PowerShell script
        //        var results = pipeline.Invoke();

        //        foreach (var item in results)
        //        {
        //            result += item.ToString();
        //        }

        //        runspace.Close();

        //        return result;



        //    }
        //    catch (Exception e)
        //    {

        //        throw e;
        //    }

        //}

        /// <summary>
        /// http://us1.myprintcloud.com/api/provisioning?subdomain=printgiant.saleflow.com&sitePhysicalPath=F:\wwwroot\preview&siteOrganisationId=2288&ContactFullName=Ilias stoilas&userId=db7ac3ed-1fca-483d-845f-6c7453601099&username=lou@printgiant.com&Email=lou@printgiant.com&hash=hash&mpcContentFolder=F:\wwwroot\preview\mis\MPC_Content&isCorp=false
        /// </summary>
        /// <param name="subdomain"></param>
        /// <param name="sitePhysicalPath"></param>
        /// <param name="siteOrganisationId"></param>
        /// <param name="ContactFullName"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="Email"></param>
        /// <param name="hash"></param>
        /// <param name="mpcContentFolder"></param>
        /// <param name="isCorp"></param>
        /// <returns></returns>

        public string Get(string subdomain, string sitePhysicalPath, string siteOrganisationId, string ContactFullName, string userId, string username, string Email, string hash, string mpcContentFolder,string isCorp)
        {
            try
            {


                string misFolder = sitePhysicalPath + "\\mis";
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"powershell.exe";
                startInfo.Arguments = @"-File " + HttpContext.Current.Server.MapPath("~/scripts/provisionNew.ps1") + " " + subdomain + " " + sitePhysicalPath + " " + siteOrganisationId + " " + mpcContentFolder + " " + misFolder;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
               //string output = "App Created";
            //Assert.IsTrue(output.Contains("StringToBeVerifiedInAUnitTest"));

            //string errors = process.StandardError.ReadToEnd();
            //Assert.IsTrue(string.IsNullOrEmpty(errors));

            if (output.Contains( "App Created"))
            {
                string connectionString = ConfigurationManager.AppSettings["connectionString"];
                        

                //inserting the default Organisation
                string queryString =
                   "INSERT INTO Organisation (OrganisationId,OrganisationName) VALUES(" + siteOrganisationId + ",'" + ContactFullName + "')";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                   
                      
                    try
                    {
                        connection.Open();

                        var result = command.ExecuteNonQuery();


                       Guid ID = Guid.Parse(userId);
                       
                        //creating default user
                        //must save the user ID as userid coming from core
                        command.CommandText = "INSERT INTO [SystemUser] ([SystemUserId],[UserName],[OrganizationId],[FullName],[RoleId],[CostPerHour],[IsSystemUser],[Email])";
                        command.CommandText += " values ('" + ID + "','" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','1',0,0,'" + Email + "')";


                        result = command.ExecuteNonQuery();


                        connection.Close();
                         

                        // import organisation
                  
                        string sCurrentServer = ConfigurationManager.AppSettings["instanceUrl"];
                       
                       // Uri uri = new Uri(sCurrentServer + "/mis/Api/ImportExportOrganisation/" + siteOrganisationId + "/" + isCorp);
                        //WebClient oClient = new WebClient();
                        //oClient.OpenRead(uri);

                        isCorp = "true";
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sCurrentServer + "/mis/Api/ImportExportOrganisation/" + siteOrganisationId + "/" + subdomain + "/" + isCorp);
                        //request.Method = "GET";
                        ////request.Credentials = new NetworkCredential("xxx", "xxx");
                        //var iTask = request.GetRequestStreamAsync();
                        //Task.WaitAll(iTask);
                        request.Method = "GET";
                        request.Timeout = 500000;
                        using (WebResponse response = request.GetResponse())
                        {
                            using (Stream stream = response.GetResponseStream())
                            {
                                StreamReader reader = new StreamReader(stream);
                                string text = reader.ReadToEnd();
                                if (text != "true")
                                    throw new Exception("Failed to import store");

                               
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        return "Please contact support@myprintcloud.com . There were errors in setting up your account : " + ex.ToString();
                    }

                }
                return "true";
            }
            else
            {
                return "Please contact support@myprintcloud.com . There were errors in setting up your account : " + output;
            }

            }
            catch (Exception ex)
            {

                return "Please contact support@myprintcloud.com . There were errors in setting up your account : " + ex.ToString();
            }
           
        }

          // get requested domain name
        public string CurrentServerPath()
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;
        }

        /// <summary>
        /// //POST: Api/CreateNewDomain
        ///   Method is used to add New Binding for a site in IIS
        /// </summary>
        /// <param name="siteName"> Site Name represents parent site name eg "mpc"</param>
        /// <param name="domainName">Domain Name Represents new binding in IIS to be created</param>
        /// <param name="isRemoving"></param>
        /// <returns>return 'true' if successfully adds binding else return 'false'</returns>
        [System.Web.Http.HttpGet]
        public string AddDomain(string siteName, string domainName, bool isRemoving)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"powershell.exe";
                if (!isRemoving)
                {
                    startInfo.Arguments = @"-File " + HttpContext.Current.Server.MapPath("~/scripts/AddDomain.ps1") + " " + siteName + " " + domainName;
                }
                else
                {
                    startInfo.Arguments = @"-File " + HttpContext.Current.Server.MapPath("~/scripts/RemoveDomain.ps1") + " " + siteName + " " + domainName;
                }
                
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                if (!isRemoving && output.Contains("Domain Created"))
                {
                    return "true";
                }
                if (isRemoving && output.Contains("Domain Removed"))
                {
                    return "true";
                }
                return "Please contact support@myprintcloud.com . There were errors in setting up domain bindings" + output;
            }
            catch (Exception ex)
            {
                throw new Exception("Please contact support@myprintcloud.com . There were errors in setting up domain bindings: " + ex);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

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

        public string Get(string subdomain, string sitePhysicalPath, string siteOrganisationId, string ContactFullName, string userId, string username, string Email, string hash, string mpcContentFolder)
        {

            string misFolder = sitePhysicalPath + "\\mis";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"powershell.exe";
            startInfo.Arguments = @"-File " + HttpContext.Current.Server.MapPath("~/scripts/provisionNew.ps1") + " " + subdomain + " " + sitePhysicalPath + " " + siteOrganisationId + " " + mpcContentFolder  +  " " + misFolder;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            //Assert.IsTrue(output.Contains("StringToBeVerifiedInAUnitTest"));

            //string errors = process.StandardError.ReadToEnd();
            //Assert.IsTrue(string.IsNullOrEmpty(errors));

            if (output == "App Created")
            {
                string connectionString = ConfigurationManager.AppSettings["connectionString"];
                        

                //inserting the default Organisation
                string queryString =
                   "INSERT INTO Organisation VALUES(" + siteOrganisationId + ",'" + ContactFullName + "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,NULL,NULL,NULL)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);

                    try
                    {
                        connection.Open();

                        var result = command.ExecuteNonQuery();

                        //creating default user
                        //must save the user ID as userid coming from core
                        command.CommandText = "INSERT INTO [SystemUser] ([UserName],[OrganizationId],[FullName],[Email],[RoleId],[CostPerHour],[ReplyEmail],[IsSystemUser],[CreatedBy],[CreatedDate]";
                        command.CommandText += "values ('" + username + "'," + siteOrganisationId + ",'" + ContactFullName + "','" + Email + "','1',0,'" + Email + "',0,'Auto Provisioned','" + DateTime.Now + "')";

                        result = command.ExecuteNonQuery();

                        connection.Close();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            
            }

            
            return output;
        }
    }
}
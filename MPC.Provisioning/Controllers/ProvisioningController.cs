﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Diagnostics;

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

        public string Post(string siteName, string sitePhysicalPath)
        {

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"powershell.exe";
            startInfo.Arguments = @"-File " + HttpContext.Current.Server.MapPath("~/scripts/provisionNew.ps1") + " " + siteName + " " + sitePhysicalPath;
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
            return output;
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace MPC.Theming.Controllers
{
    /// <summary>
    /// Theme Controller Api
    /// </summary>
    public class ThemeController : ApiController
    {
        // GET api/values
        public ArrayList Get()
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            string queryString = "Select * From Skin";
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
                            FullZipPath = rdr[4]
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
        public string ApplyTheme([FromUri] string fullZipPath)
        {

            return fullZipPath;
        }
    }
}
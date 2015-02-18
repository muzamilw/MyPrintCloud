﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using MPC.Implementation.MISServices;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;

namespace MPC.MIS.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewData["Heading"] = "Setting";
        //    return View();
        //}

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [Dependency]
        public IClaimsSecurityService ClaimsSecurityService { get; set; }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Redirect()
        {

            // Uri url = new Uri("http://site.myprintcloud.com/dashboard");
            Uri url = new Uri("http://localhost:56090/Home/LoginFull");
            if (Request.Url == url)
                return RedirectToRoute("Dashboard");
            else
                return RedirectToRoute("Login");
        }

        public async Task<ActionResult> LoginFull(string token)
        {
            /*
             * Entry Point For MIS
             * Get User Id, DomainKey/OrganisationId From Url
             * Call WebStore Service to Authenticate User
             * On Call back, if user is authenticated then add Claims
             */

            ValidationInfo validationInfo = null;
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MPCLoginAPIPath"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "login?token=" + token;
                string responsestr = "";
                var response = client.GetAsync(url);
                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr =  response.Result.Content.ReadAsStringAsync().Result;
                    validationInfo =  JsonConvert.DeserializeObject<ValidationInfo>(responsestr);
                }
               
            }

            long organisationId = 0; //Request.QueryString["OrganisationId"];
            string userId = ""; //Request.QueryString["UserId"];
            string fullName = "";
            string Plan = "";
            string email = "";
            Boolean isTrial = false;
            int trialCount = 0;

            if ( validationInfo != null)
            {
                organisationId = Convert.ToInt64( validationInfo.CustomerID);
                userId = validationInfo.userId;
                fullName = validationInfo.FullName;
                Plan = validationInfo.Plan;
                email = validationInfo.Email;
                isTrial = validationInfo.IsTrial;
                trialCount = validationInfo.TrialCount;
            }
            else
            {
                return Redirect(ConfigurationManager.AppSettings["MPCDashboardPath"]);
            }


            // Authenticate User For this Site
            // TODO: AuthenticateUser() // will return user
            var user = new UserIdentityModel
            {
                IsAuthenticated = true,
                User = new MisUser
                {
                    Id = userId,
                    FullName = fullName,
                    UserName = fullName,
                    Email = email,
                    SecurityStamp = "123",
                    Role = "Admin",
                    RoleId = 1,
                    IsTrial = isTrial,
                    TrialCount = trialCount,
                    OrganisationId = organisationId,
                    RoleSections = new List<RoleSection>
                    {
                        new RoleSection
                        {  
                            RoleId = 1, 
                            SectionId = 1, 
                            Section = new Section
                            {
                                SectionName = "Security",
                                AccessRights = new List<AccessRight>
                                {
                                    new AccessRight
                                    {
                                        RightName = "CanViewSecurity",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewOrganisation",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewPaperSheet",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewInventory",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewInventoryCategory",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewProduct",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewOrder",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewDashboard",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewCRM",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewProspect",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewSupplier",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewCalendar",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewContact",
                                        RightId = 1,
                                        SectionId = 1
                                    },
                                    new AccessRight
                                    {
                                        RightName = "CanViewStore",
                                        RightId = 1,
                                        SectionId = 1
                                    }
                                }
                        
                            }
                        }
                    }
                }
            };

            // Add Claims 
            if (ClaimsSecurityService != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                ClaimsSecurityService.AddClaimsToIdentity(user, identity);
                HttpContext.User = new ClaimsPrincipal(identity);
                // Make sure the Principal's are in sync
                Thread.CurrentPrincipal = HttpContext.User;
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<ActionResult> Logout()
        {
            Thread.CurrentPrincipal = null;
            HttpContext.User = null;
            AuthenticationManager.SignOut(new[] { DefaultAuthenticationTypes.ApplicationCookie });
            return Redirect(ConfigurationManager.AppSettings["MPCDashboardPath"]);
        }

        /// <summary>
        /// Page Under Construction
        /// </summary>
        public ActionResult PageUnAvailable()
        {
            return View();
        }
    }
}

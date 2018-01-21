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
using MPC.Models.DomainModels;
using Newtonsoft.Json;
using System.Configuration;
using GrapeCity.ActiveReports;
using MPC.Web.Reports;
using MPC.MIS.Models;
using FluentScheduler;
using AccessRight = MPC.Models.Common.AccessRight;
using Section = MPC.Models.Common.Section;

namespace MPC.MIS.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewData["Heading"] = "Setting";
        //    return View();
        //}
        private readonly IReportService IReportService;
        private readonly IRoleService _roleService;
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        private IOrderService orderService{ get; set; }
        private readonly MPC.Interfaces.MISServices.IListingService _listingService;

        public HomeController(IOrderService orderService, IReportService reportService, IListingService listingService, IRoleService roleService)
        {
            this.orderService = orderService;
            this.IReportService = reportService;
            this._listingService = listingService;
            this._roleService = roleService;
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

            //|| System.Web.HttpContext.Current.Request.Url.Authority.Contains("ngrok")
            //For Development environment Set these values and comment code above starting from using...
            if (System.Web.HttpContext.Current.Request.Url.Authority == "mpc" || System.Web.HttpContext.Current.Request.Url.Authority == "localhost" || System.Web.HttpContext.Current.Request.Url.Authority.Contains("ngrok"))
            {
                validationInfo = new ValidationInfo();
                validationInfo.CustomerID = "1";
                validationInfo.userId = "84B1BA93-BC52-4988-A8AB-C5C69A06E007";
                validationInfo.FullName = "Simu";
                validationInfo.Plan = "light";
                validationInfo.Email = "muzamilw@hotmail.com";// "naveedmnz@hotmail.com";
                validationInfo.IsTrial = true;
                validationInfo.TrialCount = 9;

              
            } 
            else
            {
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
                        responsestr = response.Result.Content.ReadAsStringAsync().Result;
                        validationInfo = JsonConvert.DeserializeObject<ValidationInfo>(responsestr);
                    }

                }
            }
        

            

            long organisationId = 0; //Request.QueryString["OrganisationId"];
            string userId = ""; //Request.QueryString["UserId"];
            string fullName = "";
            string Plan = "";
            string email = "";
            Boolean isTrial = false;
            int trialCount = 0;
            Role userRole = null;

            if (validationInfo != null)
            {
                organisationId = Convert.ToInt64(validationInfo.CustomerID);
                Guid newGuid = Guid.Parse(validationInfo.userId);
                if (validationInfo.Email == "muzamilw@hotmail.com" || validationInfo.Email == "muzamilw@gmail.com")
                {
                    userRole = _roleService.GetSystemAdminRole();
                }
                else
                {
                    userRole = _roleService.GetRoleByUserId(newGuid);
                }
                

                userId = validationInfo.userId;
                fullName = validationInfo.FullName;
                Plan = validationInfo.Plan;
                email = validationInfo.Email;
                isTrial = validationInfo.IsTrial;
                trialCount = validationInfo.TrialCount;

                // for lisitng temporary commented while Xms holidays to review
                if (organisationId == 1682)
                {
                    string sUrl = string.Format("{0}://{1}/mis/", System.Web.HttpContext.Current.Request.Url.Scheme,
                        System.Web.HttpContext.Current.Request.Url.Authority);
                    TaskManager.Initialize(new ListingBackgroundTask(sUrl));
                }
                     
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
                    Role = userRole != null? userRole.RoleName : "",
                    RoleId = userRole.RoleId,
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
                                AccessRights = SetAccessRights(userRole)
                                //AccessRights = new List<AccessRight>
                                //{
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewSecurity",
                                //        RightId = 1,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewOrganisation",
                                //        RightId = 2,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewPaperSheet",
                                //        RightId = 4,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewInventory",
                                //        RightId = 5,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewInventoryCategory",
                                //        RightId = 6,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewProduct",
                                //        RightId = 7,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewOrder",
                                //        RightId = 8,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewDashboard",
                                //        RightId = 9,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewCRM",
                                //        RightId = 10,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewProspect",
                                //        RightId = 11,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewSupplier",
                                //        RightId = 12,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewCalendar",
                                //        RightId = 13,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewContact",
                                //        RightId = 14,
                                //        SectionId = 1
                                //    },
                                //    new AccessRight
                                //    {
                                //        RightName = "CanViewStore",
                                //        RightId = 15,
                                //        SectionId = 1
                                //    }
                                //}
                        
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
            return Redirect(ConfigurationManager.AppSettings["MPCDashboardPath"] + "/logout");

        }

        /// <summary>
        /// Page Under Construction
        /// </summary>
        public ActionResult PageUnAvailable()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult OrderMenuItems()
        {
            var OrderStatusCount = orderService.GetOrderScreenMenuItemCount();
            ViewBag.AllOrdersCount = OrderStatusCount.AllOrdersCount;
            ViewBag.PendingOrders = OrderStatusCount.PendingOrders;
            ViewBag.ConfirmedStarts = OrderStatusCount.ConfirmedStarts;
            ViewBag.InProduction = OrderStatusCount.InProduction;
            ViewBag.ReadyForShipping = OrderStatusCount.ReadyForShipping;
            ViewBag.Invoiced = OrderStatusCount.Invoiced;
            ViewBag.CancelledOrders = OrderStatusCount.CancelledOrders;
            return PartialView();
        }

        public ActionResult Viewer(int? id, int? itemId,int? ComboValue,string Datefrom,string DateTo,string ParamTextValue,int? ComboValue2)
        {

            ReportDescriptor model = new ReportDescriptor() { Id = id ?? 0, ItemId = itemId ?? 0,ComboValue = ComboValue ?? 0,Datefrom = Datefrom,DateTo = DateTo,ParamTextValue = ParamTextValue, ComboValue2 = ComboValue2 ?? 0 };


            return View(model);
        }


        // GET: Common/Report
        public ActionResult GetReport(ReportDescriptor req)
        {

            SectionReport report = IReportService.GetReport(req.Id, req.ItemId,req.ComboValue,req.Datefrom,req.DateTo,req.ParamTextValue,req.ComboValue2);
           
            
            ViewBag.Report = report;

            return PartialView("WebViewer");

        }

        private List<AccessRight> SetAccessRights(Role role)
        {
            List<AccessRight> rightsList = new List<AccessRight>();
            if (role != null)
            {
                role.Rolerights.ToList().ForEach(a => rightsList.Add(new AccessRight{RightId = a.RightId, RightName = a.AccessRight.RightName, SectionId = 1}));
            }
            return  rightsList;
        }
    }
}

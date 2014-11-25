using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.Models.Common;
using Microsoft.Practices.Unity;
using Microsoft.Owin;
using MicrosoftClaims = Microsoft.IdentityModel.Claims;

namespace MPC.MIS.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    ViewData["Heading"] = "Setting";
        //    return View();
        //}

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

        public ActionResult LoginFull()
        {
            /*
             * Entry Point For MIS
             * Get User Id, DomainKey/OrganisationId From Url
             * Call WebStore Service to Authenticate User
             * On Call back, if user is authenticated then add Claims
             */

            var organisationId = 1;//Request.QueryString["OrganisationId"];
            var userId = "khurram";//Request.QueryString["UserId"];

            // Authenticate User For this Site
            // TODO: AuthenticateUser() // will return user
            var user = new UserIdentityModel
            { 
                IsAuthenticated = true, 
                User = new MisUser
                {
                    FullName = "khurram@innostark.com",
                    Email = "khurram@innostark.com",
                    Role = "Admin",
                    RoleId = 1,
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
                MicrosoftClaims.IClaimsIdentity identity = new MicrosoftClaims.ClaimsIdentity(user.User.FullName);
                System.Web.HttpContext.Current.User = new MicrosoftClaims.ClaimsPrincipal(new MicrosoftClaims.ClaimsIdentityCollection{ identity });
                // Make sure the Principal's are in sync
                Thread.CurrentPrincipal = System.Web.HttpContext.Current.User;
                ClaimsSecurityService.AddClaimsToIdentity(user, identity);    
            }

            return View();
        }


    }
}

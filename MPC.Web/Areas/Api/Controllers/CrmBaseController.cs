using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CrmBaseController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion
        #region Constructor

        public CrmBaseController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        [CompressFilterAttribute]
        public CrmBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var result = companyService.GetBaseDataForCrm();
            byte[] bytes = null;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png")))
            {
                bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png"));
            }
            string defaultCss = string.Empty;
            if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultCss/Default_CompanyStyles.css")))
            {
                defaultCss = File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultCss/Default_CompanyStyles.css"));
            }


            return new CrmBaseResponse
            {
                SystemUsers = result.SystemUsers != null ? result.SystemUsers.Select(x => x.CreateFrom()) : new List<SystemUserDropDown>(),
                CompanyContactRoles = result.CompanyContactRoles != null ? result.CompanyContactRoles.Select(x => x.CreateFrom()) : new List<CompanyContactRole>(),
                RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) :
                new List<RegistrationQuestionDropDown>(),
                DefaultSpriteImage = bytes,
                DefaultCompanyCss = defaultCss,
                Countries = result.Countries != null ? result.Countries.Select(x => x.CreateFromDropDown()) : new List<CountryDropDown>(),
                States = result.States != null ? result.States.Select(x => x.CreateFromDropDown()) : new List<StateDropDown>(),
                SectionFlags = result.SectionFlags != null ? result.SectionFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                StoresListDropDown = result.Companies != null? result.Companies.Select(x=>x.CreateFromForDropDown()): new List<StoresListDropDown>(),
                DefaultCountryId = result.DefaultCountryId
            };
        }
    }
}
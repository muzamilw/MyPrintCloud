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
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using Newtonsoft.Json;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class StoreBaseController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region constructor

        public StoreBaseController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        #endregion

        #region Public
        public CompanyBaseResponse Get(long companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = companyService.GetBaseData(companyId);
            return new CompanyBaseResponse
                   {
                       //SystemUsers = result.SystemUsers.Select(x => x.CreateFrom()),
                       CompanyTerritories = result.CompanyTerritories.Select(x => x.CreateFrom()),
                       //CompanyContactRoles = result.CompanyContactRoles.Select(x => x.CreateFrom()),
                      // PageCategories = result.PageCategories != null ? result.PageCategories.Select(x => x.CreateFromDropDown()) : null,
                      // RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) : null,
                       Addresses = result.Addresses != null ? result.Addresses.Select(x => x.CreateFrom()) : null,
                      // EmailEvents = result.EmailEvents != null ? result.EmailEvents.Select(x => x.CreateFrom()) : null,
                      // Widgets = result.Widgets != null ? result.Widgets.Select(x => x.CreateFrom()) : null,
                      // CostCenterDropDownList = result.CostCentres != null ? result.CostCentres.Select(x => x.CostCentreDropDownCreateFrom()) : null,
                      // Countries = result.Countries != null ? result.Countries.Select(x => x.CreateFromDropDown()) : null,
                      // States = result.States != null ? result.States.Select(x => x.CreateFromDropDown()) : null,
                       FieldVariableResponse = result.FieldVariableResponse.CreateFrom(),
                       SmartFormResponse = result.SmartFormResponse.CreateFrom(),
                       FieldVariableForSmartForms = result.FieldVariablesForSmartForm.Select(fv => fv.CreateFromForSmartForm()),
                      // CmsPageDropDownList = result.CmsPages != null ? result.CmsPages.Select(x => x.CreateFromForDropDown()) : null
                   };
        }
        public CompanyBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = companyService.GetBaseDataForNewCompany();
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
            List<SkinForTheme> themes = new List<SkinForTheme>();
            // Get List of Skins 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MPCThemingPath"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "GET/";
                string responsestr = "";
                var response = client.GetAsync(url);

                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr = response.Result.Content.ReadAsStringAsync().Result;
                    themes = JsonConvert.DeserializeObject<List<SkinForTheme>>(responsestr);
                }

            }

            return new CompanyBaseResponse
            {
                SystemUsers = result.SystemUsers != null ? result.SystemUsers.Select(x => x.CreateFrom()) : null,
                CompanyContactRoles = result.CompanyContactRoles != null ? result.CompanyContactRoles.Select(x => x.CreateFrom()) : null,
                PageCategories = result.PageCategories != null ? result.PageCategories.Select(x => x.CreateFromDropDown()) : null,
                RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) : null,
                Addresses = result.Addresses != null ? result.Addresses.Select(x => x.CreateFrom()) : null,
                EmailEvents = result.EmailEvents != null ? result.EmailEvents.Select(x => x.CreateFrom()) : null,
                Widgets = result.Widgets != null ? result.Widgets.Select(x => x.CreateFrom()) : null,
                DefaultSpriteImage = bytes,
                DefaultCompanyCss = defaultCss,
                CostCenterDropDownList = result.CostCentres != null ? result.CostCentres.Select(x => x.CostCentreDropDownCreateFrom()) : null,
                Countries = result.Countries != null ? result.Countries.Select(x => x.CreateFromDropDown()) : null,
                States = result.States != null ? result.States.Select(x => x.CreateFromDropDown()) : null,
                SectionFlags = result.SectionFlags.Select(flag => flag.CreateFromDropDown()),
                CmsPageDropDownList = result.CmsPages.Select(cmspage => cmspage.CreateFromForDropDown()),
                Themes = themes
            };
        }
        #endregion

    }
}
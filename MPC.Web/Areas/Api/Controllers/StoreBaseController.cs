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
using MPC.Models.DomainModels;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;
using Address = MPC.MIS.Areas.Api.Models.Address;
using CompanyContactRole = MPC.MIS.Areas.Api.Models.CompanyContactRole;
using CompanyTerritory = MPC.MIS.Areas.Api.Models.CompanyTerritory;
using EmailEvent = MPC.MIS.Areas.Api.Models.EmailEvent;
using PaymentMethod = MPC.MIS.Areas.Api.Models.PaymentMethod;
using Widget = MPC.MIS.Areas.Api.Models.Widget;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class StoreBaseController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;
        private readonly ICompanyRepository companyRepository;

        #endregion

        #region constructor

        public StoreBaseController(ICompanyService companyService, ICompanyRepository companyRepository)
        {
            this.companyService = companyService;
            this.companyRepository = companyRepository;
        }
        #endregion

        #region Public
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyBaseResponse Get(long companyId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var result = companyService.GetBaseData(companyId);
            List<SkinForTheme> themes = new List<SkinForTheme>();
            // Get List of Skins 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MPCThemingPath"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "GetThemesByOrganisationId?organisationId=" + companyRepository.OrganisationId;
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
                       CompanyTerritories = result.CompanyTerritories != null ? result.CompanyTerritories.Select(x => x.CreateFrom()) : new List<CompanyTerritory>(),
                       Addresses = result.Addresses != null ? result.Addresses.Select(x => x.CreateFrom()) : new List<Address>(),
                       FieldVariableResponse = result.FieldVariableResponse.CreateFrom(),
                       SmartFormResponse = result.SmartFormResponse.CreateFrom(),
                       FieldVariableForSmartForms = result.FieldVariablesForSmartForm != null ? result.FieldVariablesForSmartForm.Select(fv => fv.CreateFromForSmartForm()) : new List<FieldVariableForSmartForm>(),
                       CmsPageDropDownList = result.CmsPages != null ? result.CmsPages.Select(x => x.CreateFromForDropDown()) : new List<CmsPageDropDown>(),
                       Themes = themes ?? new List<SkinForTheme>(),
                       PriceFlags = result.PriceFlags != null ? result.PriceFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>()
                   };
        }

        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            var result = companyService.GetBaseDataForNewCompany();
            byte[] bytes = null;
            //Commented by Naveed on 20150827
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png")))
            //{
            //    bytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultSprite/sprite.bakup.png"));
            //}
            string defaultCss = string.Empty;
            //Commented by Naveed on 20150827
            //if (File.Exists(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultCss/Default_CompanyStyles.css")))
            //{
            //    defaultCss = File.ReadAllText(HttpContext.Current.Server.MapPath("~/MPC_Content/DefaultCss/Default_CompanyStyles.css"));
            //}
            string corporateStoreName = ConfigurationManager.AppSettings["CorporateStoreNameWOP"];
            string retailStoreName = ConfigurationManager.AppSettings["RetailStoreName"];

            return new CompanyBaseResponse
            {
                SystemUsers = result.SystemUsers != null ? result.SystemUsers.Select(x => x.CreateFrom()) : new List<SystemUserDropDown>(),
                CompanyContactRoles = result.CompanyContactRoles != null ? result.CompanyContactRoles.Select(x => x.CreateFrom()) : new List<CompanyContactRole>(),
                PageCategories = result.PageCategories != null ? result.PageCategories.Select(x => x.CreateFromDropDown()) : new List<PageCategoryDropDown>(),
                RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) :
                new List<RegistrationQuestionDropDown>(),
                EmailEvents = result.EmailEvents != null ? result.EmailEvents.Select(x => x.CreateFrom()) : new List<EmailEvent>(),
                Widgets = result.Widgets != null ? result.Widgets.Select(x => x.CreateFrom()) : new List<Widget>(),
                DefaultSpriteImage = bytes,
                DefaultCompanyCss = defaultCss,
                CostCenterDropDownList = result.CostCentres != null ? result.CostCentres.Select(x => x.CostCentreDropDownCreateFrom()) : new List<CostCentreDropDown>(),
                Countries = result.Countries != null ? result.Countries.Select(x => x.CreateFromDropDown()) : new List<CountryDropDown>(),
                States = result.States != null ? result.States.Select(x => x.CreateFromDropDown()) : new List<StateDropDown>(),
                SectionFlags = result.SectionFlags != null ? result.SectionFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                PaymentMethods = result.PaymentMethods != null ? result.PaymentMethods.Select(pm => pm.CreateFrom()) : new List<PaymentMethod>(),
                SystemVariablesForSmartForms = result.SystemVariablesForSmartForms != null ? result.SystemVariablesForSmartForms.Select(pm => pm.CreateFromForSmartForm()) : new List<FieldVariableForSmartForm>(),
                PriceFlags = result.PriceFlags != null ? result.PriceFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                OrganisationId = result.OrganisationId,
                CorporateStoreNameWebConfigValue = corporateStoreName,
                RetailStoreNameWebConfigValue = retailStoreName,
                CurrencySymbol = result.Currency
            };
        }
        #endregion

    }
}
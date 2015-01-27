using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

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
                       SystemUsers = result.SystemUsers.Select(x => x.CreateFrom()),
                       CompanyTerritories = result.CompanyTerritories.Select(x => x.CreateFrom()),
                       CompanyContactRoles = result.CompanyContactRoles.Select(x => x.CreateFrom()),
                       PageCategories = result.PageCategories != null ? result.PageCategories.Select(x => x.CreateFromDropDown()) : null,
                       RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) : null,
                       Addresses = result.Addresses != null ? result.Addresses.Select(x => x.CreateFrom()) : null,
                       //PaymentMethods = result.PaymentMethods.Select(x=>x.CreateFrom()),
                       EmailEvents = result.EmailEvents != null ? result.EmailEvents.Select(x => x.CreateFrom()) : null,
                       Widgets = result.Widgets != null ? result.Widgets.Select(x => x.CreateFrom()) : null,
                       //CmsPageDropDownList = result.CmsPages.Select(x => x.CreateFromForDropDown())
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
                DefaultCompanyCss = defaultCss
            };
        }
        #endregion

    }
}
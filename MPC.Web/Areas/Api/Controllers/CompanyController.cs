using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.MIS.ModelMappers;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CompanyController : ApiController
    {
        #region Private

        private readonly ICompanyService companyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        #endregion
        /// <summary>
        /// Get All Companies Of Organisation
        /// </summary>
        /// <returns></returns>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyResponse Get([FromUri] CompanyRequestModel request)
        {
            var result = companyService.GetAllCompaniesOfOrganisation(request);
            return new CompanyResponse
            {
                Companies = result.Companies.Select(x => x.ListViewModelCreateFrom()),
                RowCount = result.RowCount
            };
        }

        /// <summary>
        /// Get Company By Id
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public CompanyResponse Get([FromUri]int companyId)
        {
            CompanyResponse companyResponse = companyService.GetCompanyById(companyId).CreateFrom();

            return companyResponse;
        }

        /// <summary>
        /// Add/Update Company
        /// </summary>
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Company Post(Company company)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            // ReSharper disable SuggestUseVarKeywordEvident
            CompanySavingModel companySavingModel = new CompanySavingModel
            // ReSharper restore SuggestUseVarKeywordEvident
            {
                Company = company.CreateFrom(),
                NewAddedCompanyTerritories =
                    company.NewAddedCompanyTerritories != null
                        ? company.NewAddedCompanyTerritories.Select(
                            x => x.CreateFrom())
                        : null,
                EdittedCompanyTerritories =
                    company.EdittedCompanyTerritories != null
                        ? company.EdittedCompanyTerritories.Select(
                            x => x.CreateFrom())
                        : null,
                DeletedCompanyTerritories =
                    company.DeletedCompanyTerritories != null
                        ? company.DeletedCompanyTerritories.Select(
                            x => x.CreateFrom())
                        : null,
                NewAddedAddresses =
                    company.NewAddedAddresses != null
                        ? company.NewAddedAddresses.Select(x => x.CreateFrom())
                        : null,
                EdittedAddresses =
                    company.EdittedAddresses != null
                        ? company.EdittedAddresses.Select(x => x.CreateFrom())
                        : null,
                DeletedAddresses =
                    company.DeletedAddresses != null
                        ? company.DeletedAddresses.Select(x => x.CreateFrom())
                        : null,
                NewAddedCompanyContacts =
                    company.NewAddedCompanyContacts != null
                        ? company.NewAddedCompanyContacts.Select(
                            x => x.Createfrom())
                        : null,
                EdittedCompanyContacts = company.EdittedCompanyContacts != null
                    ? company.EdittedCompanyContacts.Select(x => x.Createfrom())
                    : null,
                DeletedCompanyContacts = company.DeletedCompanyContacts != null
                    ? company.DeletedCompanyContacts.Select(x => x.Createfrom())
                    : null,
                NewAddedCmsPages =
                    company.NewAddedCmsPages != null
                        ? company.NewAddedCmsPages.Select(x => x.CreateFrom())
                    .ToList()
                        : null,
                EditCmsPages =
                    company.EditCmsPages != null
                        ? company.EditCmsPages.Select(x => x.CreateFrom())
                    .ToList()
                        : null,
                DeletedCmsPages =
                    company.DeletedCmsPages != null
                        ? company.DeletedCmsPages.Select(x => x.CreateFrom())
                    .ToList()
                        : null,
                PageCategories =
                    company.PageCategories != null
                        ? company.PageCategories.Select(x => x.CreateFrom())
                    .ToList()
                        : null,
                CmsPageWithWidgetList =
                    company.CmsPageWithWidgetList != null
                        ? company.CmsPageWithWidgetList.Select(
                            x => x.CreateFrom()).ToList()
                        : null,
                NewAddedCampaigns = company.NewAddedCampaigns != null
                ? company.NewAddedCampaigns.Select(
                    x => x.CreateFrom()).ToList()
                : null,
                EdittedCampaigns = company.EdittedCampaigns != null
             ? company.EdittedCampaigns.Select(
                 x => x.CreateFrom()).ToList()
             : null,
                DeletedCampaigns = company.DeletedCampaigns != null
                ? company.DeletedCampaigns.Select(
                    x => x.CreateFrom()).ToList()
                : null,
            };

            return companyService.SaveCompany(companySavingModel).CreateFrom();
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public Company Delete(CompanyRequestModel model)
        {
            return companyService.DeleteCompany(model.CompanyId).CreateFrom();
        }

    }
}
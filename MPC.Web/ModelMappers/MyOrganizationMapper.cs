using System.Linq;
using DomainResponse = MPC.Models.ResponseModels;
using ApiResponse = MPC.MIS.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    /// <summary>
    /// My Organization Mapper
    /// </summary>
    public static class MyOrganizationMapper
    {
        #region Base Reposne Mapper
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.MyOrganizationBaseResponse CreateFrom(this DomainResponse.MyOrganizationBaseResponse source)
        {
            return new ApiResponse.MyOrganizationBaseResponse
            {
                ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                //TaxRates = source.Markups != null ? source.TaxRates.Select(taxRate => taxRate.CreateFrom()).ToList() : null,
            };
        }

        #endregion

        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.Organisation CreateFrom(this DomainModels.Organisation source)
        {
            return new ApiModels.Organisation
            {
                OrganisationId = source.OrganisationId,
                OrganisationName = source.OrganisationName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                State = source.State,
                Country = source.Country,
                ZipCode = source.ZipCode,
                Tel = source.Tel,
                Fax = source.Fax,
                Email = source.Email,
                Mobile = source.Mobile,
                Url = source.URL,
                MisLogo = source.MISLogo,
                TaxRegistrationNo = source.TaxRegistrationNo,

            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Organisation CreateFrom(this ApiModels.Organisation source)
        {
            return new DomainModels.Organisation
            {
                OrganisationId = source.OrganisationId,
                OrganisationName = source.OrganisationName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                State = source.State,
                Country = source.Country,
                ZipCode = source.ZipCode,
                Tel = source.Tel,
                Fax = source.Fax,
                Email = source.Email,
                Mobile = source.Mobile,
                URL = source.Url,
                MISLogo = source.MisLogo,
                TaxRegistrationNo = source.TaxRegistrationNo,
                //MarkupId =source.MarkupId,
                //TaxRates = source.TaxRates.Select(taxRate => taxRate.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                ChartOfAccounts = source.ChartOfAccounts != null ? source.ChartOfAccounts.Select(chartOfAcc => chartOfAcc.CreateFrom()).ToList() : null,
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiResponse.MyOrganizationSaveResponse CreateFrom(this DomainResponse.MyOrganizationSaveResponse source)
        {
            return new ApiResponse.MyOrganizationSaveResponse
            {
                OrganizationId = source.OrganizationId,
                ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                //TaxRates = source.Markups != null ? source.TaxRates.Select(taxRate => taxRate.CreateFrom()).ToList() : null,
            };
        }
        #endregion

    }
}
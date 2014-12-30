using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
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
        public static MyOrganizationBaseResponse CreateFrom(this MPC.Models.ResponseModels.MyOrganizationBaseResponse source)
        {
            return new MyOrganizationBaseResponse
            {

                ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                Countries = source.Countries != null ? source.Countries.Select(c => c.CreateFromDropDown()).ToList() : null,
                States = source.States != null ? source.States.Select(s => s.CreateFromDropDown()).ToList() : null,
            };
        }

        #endregion

        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Organisation CreateFrom(this DomainModels.Organisation source)
        {
            return new Organisation
           {
               OrganisationId = source.OrganisationId,
               OrganisationName = source.OrganisationName,
               Address1 = source.Address1,
               Address2 = source.Address2,
               Address3 = source.Address3,
               City = source.City,
               StateId = source.StateId,
               CountryId = source.CountryId,
               ZipCode = source.ZipCode,
               Tel = source.Tel,
               Fax = source.Fax,
               MarkupId = source.MarkupId,
               LanguageId = source.LanguageId,
               CurrencyId = source.CurrencyId,
               SystemLengthUnit = source.SystemLengthUnit,
               SystemWeightUnit = source.SystemWeightUnit,
               Email = source.Email,
               Mobile = source.Mobile,
               Url = source.URL,
               MisLogo = source.MISLogo,
               TaxRegistrationNo = source.TaxRegistrationNo,
               Image = source.MisLogoBytes
           };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Organisation CreateFrom(this Organisation source)
        {
            return new DomainModels.Organisation
            {
                OrganisationId = source.OrganisationId,
                OrganisationName = source.OrganisationName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                Address3 = source.Address3,
                City = source.City,
                StateId = source.StateId,
                CountryId = source.CountryId,
                ZipCode = source.ZipCode,
                Tel = source.Tel,
                Fax = source.Fax,
                Email = source.Email,
                Mobile = source.Mobile,
                URL = source.Url,
                MISLogo = source.MisLogo,
                TaxRegistrationNo = source.TaxRegistrationNo,
                MarkupId = source.MarkupId,
                LanguageId = source.LanguageId,
                CurrencyId = source.CurrencyId,
                SystemLengthUnit = source.SystemLengthUnit,
                SystemWeightUnit = source.SystemWeightUnit,
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                ChartOfAccounts = source.ChartOfAccounts != null ? source.ChartOfAccounts.Select(chartOfAcc => chartOfAcc.CreateFrom()).ToList() : null,
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static MyOrganizationSaveResponse CreateFrom(this MPC.Models.ResponseModels.MyOrganizationSaveResponse source)
        {
            return new MyOrganizationSaveResponse
            {
                OrganizationId = source.OrganizationId,
                ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
            };
        }
        #endregion

    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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

                //ChartOfAccounts = source.ChartOfAccounts.Select(coa => coa.CreateFrom()).ToList(),
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : new List<Markup>(),
                Countries = source.Countries != null ? source.Countries.Select(c => c.CreateFromDropDown()).ToList() : new List<CountryDropDown>(),
                States = source.States != null ? source.States.Select(s => s.CreateFromDropDown()).ToList() : new List<StateDropDown>(),
                Currencies = source.Currencies != null ? source.Currencies.Select(s => s.CreateFromDropDown()).ToList() : new List<CurrencyDropDown>(),
                LengthUnits = source.LengthUnits != null ? source.LengthUnits.Select(s => s.CreateFromDropDown()).ToList() : new List<LengthUnitDropDown>(),
                WeightUnits = source.WeightUnits != null ? source.WeightUnits.Select(s => s.CreateFromDropDown()).ToList() : new List<WeightUnitDropDown>(),
                GlobalLanguages = source.GlobalLanguages != null ? source.GlobalLanguages.Select(s => s.CreateFromDropDown()).ToList() : new List<GlobalLanguageDropDown>(),
            };
        }

        #endregion

        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Organisation CreateFrom(this DomainModels.Organisation source)
        {
            byte[] bytes = null;
            string imagePath = HttpContext.Current.Server.MapPath("~/" + source.MISLogo);
            if (imagePath != null && File.Exists(imagePath))
            {
                bytes = File.ReadAllBytes(imagePath);
            }

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
               IsImperical = source.IsImperical,
               AgileApiKey =  source.AgileApiKey,
               AgileApiUrl = source.AgileApiUrl,
               isAgileActive = source.isAgileActive,
               XeroApiId = source.XeroApiId,
               XeroApiKey = source.XeroApiKey,
               isXeroIntegrationRequired = source.isXeroIntegrationRequired,
               TaxRegistrationNo = source.TaxRegistrationNo,
               IsZapierEnable = source.IsZapierEnable,
               DefaultPOTax = source.DefaultPOTax,
               ShowBleedArea = source.ShowBleedArea,
               BleedAreaSize = source.BleedAreaSize,
               Image = bytes,
               LanguageEditors = source.LanguageEditors != null ? source.LanguageEditors.Select(le => le.CreateFrom()).ToList() : new List<LanguageEditor>(),
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
                IsImperical = source.IsImperical,
                AgileApiUrl = source.AgileApiUrl,
                AgileApiKey = source.AgileApiKey,
                isAgileActive = source.isAgileActive,
                XeroApiId = source.XeroApiId,
                XeroApiKey = source.XeroApiKey,
                isXeroIntegrationRequired = source.isXeroIntegrationRequired,
                IsZapierEnable = source.IsZapierEnable,
                DefaultPOTax = source.DefaultPOTax,
                BleedAreaSize = source.BleedAreaSize,
                ShowBleedArea = source.ShowBleedArea,
                Markups = source.Markups != null ? source.Markups.Select(markup => markup.CreateFrom()).ToList() : null,
                ChartOfAccounts = source.ChartOfAccounts != null ? source.ChartOfAccounts.Select(chartOfAcc => chartOfAcc.CreateFrom()).ToList() : null,
                LanguageEditors = source.LanguageEditors != null ? source.LanguageEditors.Select(le => le.CreateFrom()).ToList() : null,
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MPC.Models.ResponseModels;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Sites
    /// </summary>
    public class Organisation
    {
        #region Persisted Properties

        /// <summary>
        /// Unique Id
        /// </summary>
        public long OrganisationId { get; set; }

        /// <summary>
        /// Company Site Name
        /// </summary>
        public string OrganisationName { get; set; }

        /// <summary>
        /// Address 1
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Address 2
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Address 3
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public long? CountryId { get; set; }

        /// <summary>
        /// Zip Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Tel
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Website Logo
        /// </summary>
        public string WebsiteLogo { get; set; }

        /// <summary>
        /// Mis Logo
        /// </summary>
        public string MISLogo { get; set; }

        /// <summary>
        /// Tax Registration No
        /// </summary>
        public string TaxRegistrationNo { get; set; }
        /// <summary>
        /// License Level
        /// </summary>
        public int? LicenseLevel { get; set; }

        /// <summary>
        /// Customer Account Number
        /// </summary>
        public string CustomerAccountNumber { get; set; }

        /// <summary>
        /// Smtp Server
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Smtp User Name
        /// </summary>
        public string SmtpUserName { get; set; }

        /// <summary>
        /// Smtp Password
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Vat reg number
        /// </summary>
        public string VATRegNumber { get; set; }

        /// <summary>
        /// System Length Unit
        /// </summary>
        public long? SystemLengthUnit { get; set; }

        /// <summary>
        /// System Weight Unit
        /// </summary>
        public long? SystemWeightUnit { get; set; }

        /// <summary>
        /// Currency Id
        /// </summary>
        public long? CurrencyId { get; set; }

        /// <summary>
        /// Language Id
        /// </summary>
        public long? LanguageId { get; set; }

        /// <summary>
        /// Bleed Area Size
        /// </summary>
        public double? BleedAreaSize { get; set; }

        /// <summary>
        /// Show Bleed Area
        /// </summary>
        public bool? ShowBleedArea { get; set; }

        /// <summary>
        /// is Xero Integration Required
        /// </summary>
        public bool? isXeroIntegrationRequired { get; set; }

        /// <summary>
        /// XeroApiId
        /// </summary>
        public string XeroApiId { get; set; }

        /// <summary>
        /// XeroApiKey
        /// </summary>
        public string XeroApiKey { get; set; }

        /// <summary>
        /// TaxServiceUrl
        /// </summary>
        public string TaxServiceUrl { get; set; }

        /// <summary>
        /// TaxServiceKey
        /// </summary>
        public string TaxServiceKey { get; set; }

        /// <summary>
        /// is Imperical
        /// </summary>
        public bool? IsImperical { get; set; }

        /// <summary>
        /// Agile Api Key
        /// </summary>
        public string AgileApiKey { get; set; }

        /// <summary>
        /// Agile Api Url
        /// </summary>
        public string AgileApiUrl { get; set; }
       /// <summary>
       /// Agile Active Flag
       /// </summary>
        public bool? isAgileActive { get; set; }
        /// <summary>
        /// Trial Flag
        /// </summary>
        public bool? isTrial { get; set; }
        /// <summary>
        /// Live Stores Count
        /// </summary>
        public int? LiveStoresCount { get; set; }

        #endregion
        
        #region Reference Properties

        /// <summary>
        /// Country
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public virtual State State { get; set; }

        /// <summary>
        /// Global Language
        /// </summary>
        public virtual GlobalLanguage GlobalLanguage { get; set; }

        /// <summary>
        /// Length Unit
        /// </summary>
        public virtual LengthUnit LengthUnit { get; set; }

        /// <summary>
        /// Weight Unit
        /// </summary>
        public virtual WeightUnit WeightUnit { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Cms Skin Page Widgets
        /// </summary>
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }

        /// <summary>
        /// Estimates
        /// </summary>
        public virtual ICollection<Estimate> Estimates { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public virtual ICollection<SystemUser> SystemUsers { get; set; }

        #endregion
        
        #region Additional Properties

        // <summary>
        // Markup List
        // </summary>
        [NotMapped]
        public List<Markup> Markups { get; set; }

        // <summary>
        /// Chart Of Accounts List
        // </summary>
        [NotMapped]
        public List<ChartOfAccount> ChartOfAccounts { get; set; }

        /// <summary>
        /// Markup Id
        /// </summary>
        [NotMapped]
        public long? MarkupId { get; set; }

        /// <summary>
        /// MIS Logo Bytes
        /// </summary>
        [NotMapped]
        public byte[] MisLogoBytes { get; set; }

        /// <summary>
        /// Language Editor
        /// </summary>
        [NotMapped]
        public List<LanguageEditor> LanguageEditors { get; set; }

        #endregion
    }
}

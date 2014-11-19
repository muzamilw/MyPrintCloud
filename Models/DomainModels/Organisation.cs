using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string State { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }

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
        /// User Domain Key
        /// </summary>
        public int UserDomainKey { get; set; }

        ///// <summary>
        ///// Payment Business Email
        ///// </summary>
        //public string PaymentBusinessEmail { get; set; }

        ///// <summary>
        ///// Payment Identity Token
        ///// </summary>
        //public string PaymentIdentityToken { get; set; }

        ///// <summary>
        ///// Payment Gateway Id
        ///// </summary>
        //public int? PaymentGatewayId { get; set; }

        ///// <summary>
        ///// Customer Id
        ///// </summary>
        //public int? CustomerId { get; set; }


        ///// <summary>
        ///// Company Id
        ///// </summary>
        //public int? CompanyId { get; set; }

        ///// <summary>
        ///// State Tax Id
        ///// </summary>
        //public int? StateTaxId { get; set; }

        ///// <summary>
        ///// Tax 1
        ///// </summary>
        //public string Tax1 { get; set; }

        ///// <summary>
        ///// Tax 2
        ///// </summary>
        //public string Tax2 { get; set; }

        ///// <summary>
        ///// Tax 3
        ///// </summary>
        //public string Tax3 { get; set; }
        ///// <summary>
        ///// Tax2 Id
        ///// </summary>
        //public int? Tax2Id { get; set; }

        ///// <summary>
        ///// Tax3 Id
        ///// </summary>
        //public int? Tax3Id { get; set; }

        ///// <summary>
        ///// Production Manager Id
        ///// </summary>
        //public int? ProductionManagerId { get; set; }

        ///// <summary>
        ///// Markup Id
        ///// </summary>
        //public int? MarkupId { get; set; }

        ///// <summary>
        ///// Order Manager Id
        ///// </summary>
        //public int? OrderManagerId { get; set; }



        #endregion

        #region Reference Properties
        ///// <summary>
        ///// Tax Rate List
        ///// </summary>
        //[NotMapped]
        //public List<TaxRate> TaxRates { get; set; }

        ///// <summary>
        ///// Markup List
        ///// </summary>
        //[NotMapped]
        //public List<Markup> Markups { get; set; }

        ///// <summary>
        ///// Chart Of Accounts List
        ///// </summary>
        //[NotMapped]
        //public List<ChartOfAccount> ChartOfAccounts { get; set; }

        #endregion
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class Organisation
    {
        public Organisation()
        {
            this.CmsSkinPageWidgets = new HashSet<CmsSkinPageWidget>();
        }
    
        public long OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string ZipCode { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string WebsiteLogo { get; set; }
        public string MISLogo { get; set; }
        public string TaxRegistrationNo { get; set; }
        public Nullable<int> LicenseLevel { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string VATRegNumber { get; set; }
        public Nullable<long> SystemLengthUnit { get; set; }
        public Nullable<long> SystemWeightUnit { get; set; }
        public Nullable<long> CurrencyId { get; set; }
        public Nullable<long> LanguageId { get; set; }
        public Nullable<double> BleedAreaSize { get; set; }
        public Nullable<bool> ShowBleedArea { get; set; }
        public Nullable<bool> isXeroIntegrationRequired { get; set; }
        public string XeroApiId { get; set; }
        public string XeroApiKey { get; set; }
        public string TaxServiceUrl { get; set; }
        public string TaxServiceKey { get; set; }
    
        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
        public virtual Country Country { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual GlobalLanguage GlobalLanguage { get; set; }
        public virtual LengthUnit LengthUnit { get; set; }
        public virtual State State { get; set; }
        public virtual WeightUnit WeightUnit { get; set; }
    }
}

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Staging Import Company Contact Address Domain Model
    /// </summary>
    public class StagingImportCompanyContactAddress
    {
        public long StagingId { get; set; }
        public string CompanyName { get; set; }
        public long? CompanyId { get; set; }
        public long? AddressId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long? StateId { get; set; }
        public string Country { get; set; }
        public long? CountryId { get; set; }
        public string Postcode { get; set; }
        public long? TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        public string AddressPhone { get; set; }
        public string AddressFax { get; set; }
        public long? ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string Mobile { get; set; }
        public long? RoleId { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string AddInfo1 { get; set; }
        public string AddInfo2 { get; set; }
        public string AddInfo3 { get; set; }
        public string AddInfo4 { get; set; }
        public string AddInfo5 { get; set; }
        public long? OrganisationId { get; set; }
        public string SkypeId { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public bool? CanEditProfile { get; set; }
        public bool? CanPlaceOrderWithoutApproval { get; set; }
        public bool? CanPlaceDirectOrder { get; set; }
        public bool? CanPayByPersonalCreditCard { get; set; }
        public bool? CanSeePrices { get; set; }
        public bool? HasWebAccess { get; set; }
        public bool? CanPlaceOrder { get; set; }


        //public string DirectLine { get; set; }

        //public string UserRole { get; set; }

        //public string UserName { get; set; }

        //public string POBoxAddress { get; set; }

        //public string CorporateUnit { get; set; }

        //public string TradingName { get; set; }

        //public string BPayCRN { get; set; }

        //public string ACN { get; set; }


        //public string ContractorName { get; set; }

        //public string ABN { get; set; }

        //public string Notes { get; set; }

        //public string CreditLimit { get; set; }

        //public string SubscribedtoNewsletter { get; set; }

        //public string SubscribedtoEmails { get; set; }

        //public string isDefaultContact { get; set; }
    }
}

using System;
using FileHelpers;

namespace MPC.MIS.Areas.Api.Models
{
    [DelimitedRecord("|")]
    [IgnoreFirst()]
    public class ImportCompanyContact
    {
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string AddressPhone { get; set; }
        public string AddressFax { get; set; }
        public string TerritoryName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string JobTitle { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ContactFax { get; set; }
        public string AddInfo1 { get; set; }
        public string AddInfo2 { get; set; }
        public string AddInfo3 { get; set; }
        public string AddInfo4 { get; set; }
        public string AddInfo5 { get; set; }
        public string SkypeId { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string CanEditProfile { get; set; }
        public string CanPlaceOrderWithoutApproval { get; set; }
        public string CanPlaceDirectOrder { get; set; }
        public string CanPayByPersonalCreditCard { get; set; }
        public string CanSeePrices { get; set; }
        public string HasWebAccess { get; set; }
        public string CanPlaceOrder { get; set; }

        public string DirectLine { get; set; }

        public string UserRole { get; set; }

      

        public string POBoxAddress { get; set; }

        public string CorporateUnit { get; set; }



        public string TradingName { get; set; }
        public string BPayCRN { get; set; }

        public string ACN { get; set; }


        public string ContractorName { get; set; }

        public string ABN { get; set; }

        public string Notes { get; set; }

        public string CreditLimit { get; set; }

        public string IsNewsLetterSubscription { get; set; }

        public string IsEmailSubscription { get; set; }

        public string IsDefaultContact { get; set; }


    }

   
}
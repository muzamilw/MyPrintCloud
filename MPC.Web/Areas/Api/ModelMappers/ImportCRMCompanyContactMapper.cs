using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ImportCRMCompanyContactMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static StagingImportCompanyContactAddress Createfrom(this ImportCRMCompanyContact source, long companyId)
        {
            return new StagingImportCompanyContactAddress
            {
                CompanyId = companyId,
                AddressName = source.AddressName,
                Address1 = source.Address1,
                Address2 = source.Address2,
                City = source.City,
                State = source.State,
                Country = source.Country,
                Postcode = source.Postcode,
                TerritoryName = source.TerritoryName,
                AddressPhone = source.AddressPhone,
                AddressFax = source.AddressFax,
                ContactFirstName = source.ContactFirstName,
                ContactLastName = source.ContactLastName,
                JobTitle = source.JobTitle,
                Email = source.Email,
                Mobile = source.Mobile,
                ContactPhone = source.ContactPhone,
                ContactFax = source.ContactFax,
                AddInfo1 = source.AddInfo1,
                AddInfo2 = source.AddInfo2,
                AddInfo3 = source.AddInfo3,
                AddInfo4 = source.AddInfo4,
                AddInfo5 = source.AddInfo5,
                SkypeId = source.SkypeId,
                LinkedInUrl = source.LinkedInUrl,
                FacebookUrl = source.FacebookUrl,
                TwitterUrl = source.TwitterUrl,
                CanEditProfile = Convert.ToBoolean(source.CanEditProfile == "" ? "false" : source.CanEditProfile),
                CanPlaceOrderWithoutApproval = Convert.ToBoolean(source.CanPlaceOrderWithoutApproval == "" ? "false" : source.CanPlaceOrderWithoutApproval),
                CanPlaceDirectOrder = Convert.ToBoolean(source.CanPlaceDirectOrder == "" ? "false" : source.CanPlaceDirectOrder),
                CanPayByPersonalCreditCard = Convert.ToBoolean(source.CanPayByPersonalCreditCard == "" ? "false" : source.CanPayByPersonalCreditCard),
                CanSeePrices = Convert.ToBoolean(source.CanSeePrices == "" ? "false" : source.CanSeePrices),
                HasWebAccess = Convert.ToBoolean(source.HasWebAccess == "" ? "false" : source.HasWebAccess),
                CanPlaceOrder = Convert.ToBoolean(source.CanPlaceOrder == "" ? "false" : source.CanPlaceOrder),

                RoleId = source.UserRole == "A" ? 1
                       : source.UserRole == "M" ? 2
                       : source.UserRole == "U" ? 3
                       : 3,

                POAddress = source.POBoxAddress,
                CorporateUnit = source.CorporateUnit,
                TradingName = source.TradingName,
                BPayCRN = source.BPayCRN,
                ACN = source.ACN,
                ContractorName = source.ContractorName,
                ABN = source.ABN,

                CreditLimit = Convert.ToDecimal(source.CreditLimit == "" ? "0" : source.CreditLimit),
                IsNewsLetterSubscription = Convert.ToBoolean(source.IsNewsLetterSubscription == "" ? "false" : source.IsNewsLetterSubscription),

                IsEmailSubscription = Convert.ToBoolean(source.IsEmailSubscription == "" ? "false" : source.IsEmailSubscription),


            };
        }
    }
}
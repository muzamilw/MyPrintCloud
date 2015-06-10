using System;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ImportCompanyContactMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static StagingImportCompanyContactAddress Createfrom(this ImportCompanyContact source, long companyId)
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
                CanEditProfile = Convert.ToBoolean(source.CanEditProfile == ""? "false": source.CanEditProfile),
                CanPlaceOrderWithoutApproval = Convert.ToBoolean(source.CanPlaceOrderWithoutApproval == "" ? "false" : source.CanPlaceOrderWithoutApproval),
                CanPlaceDirectOrder = Convert.ToBoolean(source.CanPlaceDirectOrder == "" ? "false" : source.CanPlaceDirectOrder),
                CanPayByPersonalCreditCard = Convert.ToBoolean(source.CanPayByPersonalCreditCard == "" ? "false" : source.CanPayByPersonalCreditCard),
                CanSeePrices = Convert.ToBoolean(source.CanSeePrices == "" ? "false" : source.CanSeePrices),
                HasWebAccess = Convert.ToBoolean(source.HasWebAccess == "" ? "false" : source.HasWebAccess),
                CanPlaceOrder = Convert.ToBoolean(source.CanPlaceOrder == "" ? "false" : source.CanPlaceOrder) 
            };
        }
    }
}
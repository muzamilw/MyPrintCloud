using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ImportCompanyContactMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static CompanyContact Createfrom(this ImportCompanyContact source)
        {
            return new CompanyContact
            {
                //ContactId = source.ContactId,
                AddressId = source.AddressId,
                CompanyId = source.CompanyId,
                FirstName = source.FirstName,
                MiddleName = source.MiddleName,
                LastName = source.LastName,
                Title = source.Title,
                HomeTel1 = source.HomeTel1,
                HomeTel2 = source.HomeTel2,
                HomeExtension1 = source.HomeExtension1,
                HomeExtension2 = source.HomeExtension2,
                Mobile = source.Mobile,
                Email = source.Email,
                FAX = source.FAX,
                JobTitle = source.JobTitle,
                DOB = source.DOB,
                Notes = source.Notes,
                IsDefaultContact = source.IsDefaultContact,
                HomeAddress1 = source.HomeAddress1,
                HomeAddress2 = source.HomeAddress2,
                HomeCity = source.HomeCity,
                HomeState = source.HomeState,
                HomePostCode = source.HomePostCode,
                HomeCountry = source.HomeCountry,
                SecretQuestion = source.SecretQuestion,
                SecretAnswer = source.SecretAnswer,
                Password = source.Password,
                URL = source.URL,
                IsEmailSubscription = source.IsEmailSubscription,
                IsNewsLetterSubscription = source.IsNewsLetterSubscription,
                //ContactProfileImage = source.ImageBytes,
                quickFullName = source.quickFullName,
                quickTitle = source.quickTitle,
                quickCompanyName = source.quickCompanyName,
                quickAddress1 = source.quickAddress1,
                quickAddress2 = source.quickAddress2,
                quickAddress3 = source.quickAddress3,
                quickPhone = source.quickPhone,
                quickFax = source.quickFax,
                quickEmail = source.quickEmail,
                quickWebsite = source.quickWebsite,
                quickCompMessage = source.quickCompMessage,
                QuestionId = source.QuestionId,
                IsApprover = source.IsApprover,
                isWebAccess = source.isWebAccess,
                isPlaceOrder = source.isPlaceOrder,
                CreditLimit = source.CreditLimit,
                isArchived = source.isArchived,
                ContactRoleId = source.ContactRoleId,
                TerritoryId = source.TerritoryId,
                ClaimIdentifer = source.ClaimIdentifer,
                AuthentifiedBy = source.AuthentifiedBy,
                IsPayByPersonalCreditCard = source.IsPayByPersonalCreditCard,
                IsPricingshown = source.IsPricingshown,
                SkypeId = source.SkypeId,
                LinkedinURL = source.LinkedinURL,
                FacebookURL = source.FacebookURL,
                TwitterURL = source.TwitterURL,
                authenticationToken = source.authenticationToken,
                twitterScreenName = source.twitterScreenName,
                ShippingAddressId = source.ShippingAddressId,
                isUserLoginFirstTime = source.isUserLoginFirstTime,
                quickMobileNumber = source.quickMobileNumber,
                quickTwitterId = source.quickTwitterId,
                quickFacebookId = source.quickFacebookId,
                quickLinkedInId = source.quickLinkedInId,
                quickOtherId = source.quickOtherId,
                POBoxAddress = source.POBoxAddress,
                CorporateUnit = source.CorporateUnit,
                OfficeTradingName = source.OfficeTradingName,
                ContractorName = source.ContractorName,
                BPayCRN = source.BPayCRN,
                ABN = source.ABN,
                ACN = source.ACN,
                AdditionalField1 = source.AdditionalField1,
                AdditionalField2 = source.AdditionalField2,
                AdditionalField3 = source.AdditionalField3,
                AdditionalField4 = source.AdditionalField4,
                AdditionalField5 = source.AdditionalField5,
                canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval,
                CanUserEditProfile = source.CanUserEditProfile,
                canPlaceDirectOrder = source.canPlaceDirectOrder,
                OrganisationId = source.OrganisationId,
                FileName = source.FileName,
                //ScopeVariables = source.ScopVariables != null ? source.ScopVariables.Select(ccv => ccv.CreateFrom()).ToList() : null


            };
        }
    }
}
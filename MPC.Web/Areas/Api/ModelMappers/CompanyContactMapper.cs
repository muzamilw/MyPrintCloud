using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{

    public static class CompanyContactMapper
    {
        /// <summary>
        /// Company Contact Mapper Web Model -> Domain Model
        /// </summary>
        public static DomainModels.CompanyContact Createfrom(this CompanyContact source)
        {
            return new DomainModels.CompanyContact
                   {
                       ContactId = source.ContactId,
                       AddressId = source.AddressId,
                       TerritoryId = source.TerritoryId,
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
                       ContactProfileImage = source.ImageBytes,
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
                       //TerritoryId = source.TerritoryId,
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
                       SecondaryEmail = source.SecondaryEmail,
                       ScopeVariables = source.ScopVariables != null ? source.ScopVariables.Select(ccv => ccv.CreateFrom()).ToList() : null,
                       HasUserDamRights = source.HasUserDamRights

                       //CompanyTerritory = source.BussinessAddress.Territory.CreateFrom(),
                       //Address = source.BussinessAddress != null? source.BussinessAddress.CreateFrom() : null,

                   };
        }

        /// <summary>
        /// Company Contact Mapper Domain Model -> Web Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static CompanyContact CreateFrom(this DomainModels.CompanyContact source)
        {
            byte[] bytes = null;
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(source.image))
            {
                string path =
                    HttpContext.Current.Server.MapPath("~/" + source.image);
                if (File.Exists(path))
                {
                    bytes = File.ReadAllBytes(path);

                }
            }
            return new CompanyContact
                   {
                       ContactId = source.ContactId,
                       AddressId = source.AddressId,
                       CompanyId = source.CompanyId,
                       CompanyName = source.Company != null ? source.Company.Name :string.Empty,
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
                       Image = bytes,
                       //Bytes = source.image,
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
                       RoleName = source.CompanyContactRole != null ? source.CompanyContactRole.ContactRoleName : string.Empty,
                       FileName = fileName,
                       ImageBytes = source.image,
                       SecondaryEmail = source.SecondaryEmail,
                       StoreName = source.Company != null ? source.Company.StoreName: string.Empty,
                       HasUserDamRights = source.HasUserDamRights

                   };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CompanyContact CreateFromSupplier(this CompanyContact source)
        {
            return new DomainModels.CompanyContact
            {
                ContactId = 0,
                FirstName = source.FirstName,
                LastName = source.LastName,
                HomeAddress1 = source.HomeAddress1,
                HomeCity = source.HomeCity,
                HomeState = source.HomeState,
                Mobile = source.Mobile,
                Notes = source.Notes,
                HomeTel1 = source.HomeTel1,
                HomeTel2 = source.HomeTel2,
                URL = source.URL,
                HomeExtension1 = source.HomeExtension1,
                HomeExtension2 = source.HomeExtension2,
                Email = source.Email,
                Password = source.Password,
                SecretQuestion = source.SecretQuestion,
                SecretAnswer = source.SecretAnswer,
                IsEmailSubscription = source.IsEmailSubscription,
                IsNewsLetterSubscription = source.IsNewsLetterSubscription,
                HasUserDamRights = source.HasUserDamRights
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyContactDropDown CreateFromDropDown(this DomainModels.CompanyContact source)
        {
            return new CompanyContactDropDown
            {
                ContactId = source.ContactId,
                Name = source.FirstName
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyContactDropDownForOrder CreateFromDropDownForOrder(this DomainModels.CompanyContact source)
        {
            return new CompanyContactDropDownForOrder
            {
                ContactId = source.ContactId,
                Name = string.Format(CultureInfo.InvariantCulture, "{0} {1}", source.FirstName, source.LastName),
                Email = source.Email,
                IsDefaultContact = source.IsDefaultContact
            };
        }

        public static CompanyContactResponse CreateFrom(this MPC.Models.ResponseModels.CompanyContactResponse response)
        {
            return new CompanyContactResponse
            {
                CompanyContacts = response.CompanyContacts.Select(contact => contact.CreateFrom()),
                RowCount = response.RowCount
            };
        }


        /// <summary>
        /// Base Data Mapper
        /// </summary>
        public static CompanyBaseResponse CreateFrom(this MPC.Models.ResponseModels.CompanyBaseResponse result)
        {
            return new CompanyBaseResponse
            {
                CompanyContactRoles = result.CompanyContactRoles != null ? result.CompanyContactRoles.Select(x => x.CreateFrom()) : null,
                RegistrationQuestions = result.RegistrationQuestions != null ? result.RegistrationQuestions.Select(x => x.CreateFromDropDown()) : null,
                Addresses = result.Addresses != null ? result.Addresses.Select(address => address.CreateFrom()) : null,
                CompanyTerritories = result.CompanyTerritories != null ? result.CompanyTerritories.Select(territory => territory.CreateFrom()) : null,
                StateDropDowns = result.States != null ? result.States.Select(state => state.CreateFromDropDown()) : null,
            };
        }

        public static CompanyContactResponseForCalendar CreateFromForCalendar(this MPC.Models.ResponseModels.CompanyContactResponse response)
        {
            return new CompanyContactResponseForCalendar
            {
                CompanyContacts = response.CompanyContacts.Select(contact => contact.CreateFromContactForCalendar()),
                RowCount = response.RowCount
            };
        }
        public static CompanyContactForCalendar CreateFromContactForCalendar(this DomainModels.CompanyContact source)
        {
            return new CompanyContactForCalendar
            {
                ContactId = source.ContactId,
                CompanyId = source.CompanyId,
                Name = source.FirstName,
                CompanyName = source.Company != null ? source.Company.Name : string.Empty
            };
        }

        /// <summary>
        /// Mapper for Order screen
        /// </summary>
        public static ContactResponseForOrder CreateFrom(this MPC.Models.ResponseModels.ContactsResponseForOrder response)
        {
            return new ContactResponseForOrder
            {
                CompanyContacts = response.CompanyContacts.Select(contact => contact.CreateFromForOrder()),
                RowCount = response.RowCount
            };
        }

        /// <summary>
        /// Create From for Order
        /// </summary>
        public static CompanyContactForOrder CreateFromForOrder(this DomainModels.CompanyContact source)
        {
            return new CompanyContactForOrder
            {
                ContactId = source.ContactId,
                AddressId = source.AddressId,
                CompanyId = source.CompanyId,
                CompanyName = source.Company.Name,
                StoreName = source.Company.StoreName,
                StoreId = source.Company.StoreId,
                Name = string.Format(CultureInfo.InvariantCulture, "{0} {1}", source.FirstName, source.LastName),
                IsCustomer = source.Company.IsCustomer
            };
        }

    }
}
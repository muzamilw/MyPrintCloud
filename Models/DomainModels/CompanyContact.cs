using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyContact
    {
        public long ContactId { get; set; }
        public long AddressId { get; set; }
        public long CompanyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string HomeTel1 { get; set; }
        public string HomeTel2 { get; set; }
        public string HomeExtension1 { get; set; }
        public string HomeExtension2 { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FAX { get; set; }
        public string JobTitle { get; set; }
        public DateTime? DOB { get; set; }
        public string Notes { get; set; }
        public int? IsDefaultContact { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public string HomeCity { get; set; }
        public string HomeState { get; set; }
        public string HomePostCode { get; set; }
        public string HomeCountry { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public bool? IsEmailSubscription { get; set; }
        public bool? IsNewsLetterSubscription { get; set; }
        public string image { get; set; }
        public string quickFullName { get; set; }
        public string quickTitle { get; set; }
        public string quickCompanyName { get; set; }
        public string quickAddress1 { get; set; }
        public string quickAddress2 { get; set; }
        public string quickAddress3 { get; set; }
        public string quickPhone { get; set; }
        public string quickFax { get; set; }
        public string quickEmail { get; set; }
        public string quickWebsite { get; set; }
        public string quickCompMessage { get; set; }
        public int? QuestionId { get; set; }
        public bool? IsApprover { get; set; }
        public bool? isWebAccess { get; set; }
        public bool? isPlaceOrder { get; set; }
        public decimal? CreditLimit { get; set; }
        public bool? isArchived { get; set; }
        public int? ContactRoleId { get; set; }
        public long? TerritoryId { get; set; }
        public string ClaimIdentifer { get; set; }
        public string AuthentifiedBy { get; set; }
        public bool? IsPayByPersonalCreditCard { get; set; }
        public bool? IsPricingshown { get; set; }
        public string SkypeId { get; set; }
        public string LinkedinURL { get; set; }
        public string FacebookURL { get; set; }
        public string TwitterURL { get; set; }
        public string authenticationToken { get; set; }
        public string twitterScreenName { get; set; }
        public long? ShippingAddressId { get; set; }
        public bool? isUserLoginFirstTime { get; set; }
        public string quickMobileNumber { get; set; }
        public string quickTwitterId { get; set; }
        public string quickFacebookId { get; set; }
        public string quickLinkedInId { get; set; }
        public string quickOtherId { get; set; }
        public string POBoxAddress { get; set; }
        public string CorporateUnit { get; set; }
        public string OfficeTradingName { get; set; }
        public string ContractorName { get; set; }
        public string BPayCRN { get; set; }
        public string ABN { get; set; }
        public string ACN { get; set; }
        public string AdditionalField1 { get; set; }
        public string AdditionalField2 { get; set; }
        public string AdditionalField3 { get; set; }
        public string AdditionalField4 { get; set; }
        public string AdditionalField5 { get; set; }
        public bool? canUserPlaceOrderWithoutApproval { get; set; }
        public bool? CanUserEditProfile { get; set; }
        public bool? canPlaceDirectOrder { get; set; }
        public long? OrganisationId { get; set; }
        public string SecondaryEmail { get; set; }
        public DateTime? RegistrationDate { get; set; }
        [NotMapped]
        public string FileName { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyTerritory CompanyTerritory { get; set; }
        public virtual CompanyContactRole CompanyContactRole { get; set; }
        public virtual RegistrationQuestion RegistrationQuestion { get; set; }
        public virtual ICollection<Estimate> Estimates { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<FavoriteDesign> FavoriteDesigns { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }

        [NotMapped]
        public string ContactProfileImage { get; set; }
        [NotMapped]
        public IEnumerable<ScopeVariable> ScopeVariables { get; set; }



        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(CompanyContact target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.FirstName = FirstName;
            target.MiddleName = MiddleName;
            target.LastName = LastName;
            target.Title = Title;
            target.HomeTel1 = HomeTel1;
            target.HomeTel2 = HomeTel2;
            target.HomeExtension1 = HomeExtension1;
            target.HomeExtension2 = HomeExtension2;
            target.Mobile = Mobile;
            target.Email = Email;
            target.FAX = FAX;
            target.JobTitle = JobTitle;
            target.DOB = DOB;
            target.Notes = Notes;
            target.IsDefaultContact = IsDefaultContact;
            target.HomeAddress1 = HomeAddress1;
            target.HomeAddress2 = HomeAddress2;
            target.HomeCity = HomeCity;
            target.HomeState = HomeState;
            target.HomePostCode = HomePostCode;
            target.HomeCountry = HomeCountry;
            target.SecretQuestion = SecretQuestion;
            target.SecretAnswer = SecretAnswer;
            target.Password = Password;
            target.URL = URL;
            target.IsEmailSubscription = IsEmailSubscription;
            target.IsNewsLetterSubscription = IsNewsLetterSubscription;
            target.image = image;
            target.quickFullName = quickFullName;
            target.quickTitle = quickTitle;
            target.quickCompanyName = quickCompanyName;
            target.quickAddress1 = quickAddress1;
            target.quickAddress2 = quickAddress2;
            target.quickAddress3 = quickAddress3;
            target.quickPhone = quickPhone;
            target.quickFax = quickFax;
            target.quickEmail = quickEmail;
            target.quickWebsite = quickWebsite;
            target.quickCompMessage = quickCompMessage;
            target.quickCompanyName = quickCompanyName;
            target.QuestionId = QuestionId;
            target.IsApprover = IsApprover;
            target.isWebAccess = isWebAccess;
            target.isPlaceOrder = isPlaceOrder;
            target.CreditLimit = CreditLimit;
            target.isArchived = isArchived;
            target.ContactRoleId = ContactRoleId;
          
            target.ClaimIdentifer = ClaimIdentifer;
            target.AuthentifiedBy = AuthentifiedBy;
            target.IsPayByPersonalCreditCard = IsPayByPersonalCreditCard;
            target.IsPricingshown = IsPricingshown;
            target.SkypeId = SkypeId;

            target.LinkedinURL = LinkedinURL;
            target.FacebookURL = FacebookURL;
            target.TwitterURL = TwitterURL;
            target.authenticationToken = authenticationToken;
            target.twitterScreenName = twitterScreenName;
           

            target.isUserLoginFirstTime = isUserLoginFirstTime;
            target.quickMobileNumber = quickMobileNumber;
            target.quickTwitterId = quickTwitterId;
            target.quickFacebookId = quickFacebookId;
            target.quickLinkedInId = quickLinkedInId;
            target.quickOtherId = quickOtherId;

            target.POBoxAddress = POBoxAddress;
            target.CorporateUnit = CorporateUnit;
            target.OfficeTradingName = OfficeTradingName;
            target.ContractorName = ContractorName;
            target.BPayCRN = BPayCRN;
            target.ABN = ABN;

            target.ACN = ACN;
            target.AdditionalField1 = AdditionalField1;
            target.AdditionalField2 = AdditionalField2;
            target.AdditionalField3 = AdditionalField3;
            target.AdditionalField4 = AdditionalField4;
            target.AdditionalField5 = AdditionalField5;

            target.canUserPlaceOrderWithoutApproval = canUserPlaceOrderWithoutApproval;
            target.CanUserEditProfile = CanUserEditProfile;
            target.canPlaceDirectOrder = canPlaceDirectOrder;
            target.OrganisationId = OrganisationId;
            target.SecondaryEmail = SecondaryEmail;
            target.RegistrationDate = RegistrationDate;
            target.FileName = FileName;

        }
        #endregion
    }
}
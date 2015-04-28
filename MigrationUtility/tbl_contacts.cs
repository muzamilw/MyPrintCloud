//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_contacts
    {
        public tbl_contacts()
        {
            this.tbl_estimates = new HashSet<tbl_estimates>();
            this.tbl_FavoriteDesign = new HashSet<tbl_FavoriteDesign>();
            this.tbl_Inquiry = new HashSet<tbl_Inquiry>();
            this.tbl_NewsLetterSubscribers = new HashSet<tbl_NewsLetterSubscribers>();
        }
    
        public int ContactID { get; set; }
        public int AddressID { get; set; }
        public int ContactCompanyID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string HomeTel1 { get; set; }
        public string HomeTel2 { get; set; }
        public string HomeExtension1 { get; set; }
        public string HomeExtension2 { get; set; }
        public string Mobile { get; set; }
        public string Pager { get; set; }
        public string Email { get; set; }
        public string FAX { get; set; }
        public string JobTitle { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Notes { get; set; }
        public Nullable<int> IsDefaultContact { get; set; }
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
        public Nullable<bool> IsEmailSubscription { get; set; }
        public Nullable<bool> IsNewsLetterSubscription { get; set; }
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
        public Nullable<int> QuestionID { get; set; }
        public Nullable<bool> IsApprover { get; set; }
        public Nullable<bool> isWebAccess { get; set; }
        public Nullable<bool> isPlaceOrder { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<bool> isArchived { get; set; }
        public Nullable<int> ContactRoleID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> TerritoryID { get; set; }
        public string ClaimIdentifer { get; set; }
        public string AuthentifiedBy { get; set; }
        public Nullable<bool> IsPayByPersonalCreditCard { get; set; }
        public Nullable<bool> IsPricingshown { get; set; }
        public string SkypeID { get; set; }
        public string LinkedinURL { get; set; }
        public string FacebookURL { get; set; }
        public string TwitterURL { get; set; }
        public string authenticationToken { get; set; }
        public string twitterScreenName { get; set; }
        public Nullable<int> ShippingAddressID { get; set; }
        public Nullable<bool> isUserLoginFirstTime { get; set; }
        public string quickMobileNumber { get; set; }
        public string quickTwitterID { get; set; }
        public string quickFacebookID { get; set; }
        public string quickLinkedInID { get; set; }
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
        public Nullable<bool> canUserPlaceOrderWithoutApproval { get; set; }
        public Nullable<bool> CanUserEditProfile { get; set; }
        public Nullable<bool> canPlaceDirectOrder { get; set; }
    
        public virtual tbl_addresses tbl_addresses { get; set; }
        public virtual tbl_contactcompanies tbl_contactcompanies { get; set; }
        public virtual tbl_ContactCompanyTerritories tbl_ContactCompanyTerritories { get; set; }
        public virtual tbl_ContactDepartments tbl_ContactDepartments { get; set; }
        public virtual tbl_ContactRoles tbl_ContactRoles { get; set; }
        public virtual tbl_Registration_Questions tbl_Registration_Questions { get; set; }
        public virtual ICollection<tbl_estimates> tbl_estimates { get; set; }
        public virtual ICollection<tbl_FavoriteDesign> tbl_FavoriteDesign { get; set; }
        public virtual ICollection<tbl_Inquiry> tbl_Inquiry { get; set; }
        public virtual ICollection<tbl_NewsLetterSubscribers> tbl_NewsLetterSubscribers { get; set; }
    }
}

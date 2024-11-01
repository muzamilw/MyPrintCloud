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
    
    public partial class tbl_PC_Partners
    {
        public int RegistrationID { get; set; }
        public Nullable<int> RegistrationType { get; set; }
        public string PartnerCode { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string PostCode { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<System.DateTime> registrationDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string DesiredCities { get; set; }
        public string WebsiteURL { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyLogoPath { get; set; }
        public string ShopFrontImagePath { get; set; }
        public string ProductionContactFirstName { get; set; }
        public string ProductionContactLastName { get; set; }
        public string ProductionContactEmail { get; set; }
        public string ProductionContactPhone { get; set; }
        public Nullable<bool> IsduplexPrinting { get; set; }
        public Nullable<bool> IsLaminate { get; set; }
        public Nullable<bool> IsRoundCorner { get; set; }
        public Nullable<int> CompanyType { get; set; }
        public string CourierName { get; set; }
        public Nullable<bool> IsLocalDeliveryVehicle { get; set; }
        public Nullable<bool> IsNonOilBasedPrinter { get; set; }
        public Nullable<int> CompanyEmployees { get; set; }
        public Nullable<int> ServiceYears { get; set; }
        public string PostCode1 { get; set; }
        public string PostCode2 { get; set; }
        public string PostCode3 { get; set; }
        public string PayPalEmail { get; set; }
        public string PayPayPaymentStatus { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> VATVal { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> PaypalAmount { get; set; }
        public string Comments { get; set; }
        public string PayPalPayerFirstName { get; set; }
        public string PayPalPayerLastName { get; set; }
        public Nullable<bool> ConvertedToBroker { get; set; }
    }
}

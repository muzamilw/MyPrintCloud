using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Common
{
    public class ListingProperty
    {
        public ListingOffice Office { get; set; }
        public Listing Listing { get; set; }
        public List<ListingStaffMember> StaffMember { get; set; }
        public List<ListingOFIs> ListingOFIs { get; set; }
        public List<ListingImages> ListingImages { get; set; }
        public List<ListingFloorplans> ListingFloorplans { get; set; }
        public List<ListingLinks> ListingLinks { get; set; }
        public List<ListingAgents> ListingAgents { get; set; }
        public List<ListingConjunctionalAgents> ListingConjunctionalAgents { get; set; }
        public List<ListingVendors> ListingVendors { get; set; }
    }
    public class ListingImages
    {
        public string ImageID { get; set; }
        public string ListingID { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public int ImageOrder { get; set; }
        public string LastMod { get; set; }
        public string ImageRef { get; set; }
        public string PropertyRef { get; set; }
    }
    public class ListingOffice
    {
        public string OfficeID { get; set; }
        public string OfficeName { get; set; }
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public string ABN { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string MailAddress { get; set; }
        public string MailSuburb { get; set; }
        public string MailState { get; set; }
        public string MailPostCode { get; set; }
        public string Image { get; set; }
        public string StoreCode { get; set; }
    }
    public class ListingCustomCopy
    {
        public int ListingID { get; set; } //added for listing record
        public string SignboardHeadline { get; set; }
        public string SignboardDescription { get; set; }
        public string BrochureHeadline { get; set; }
        public string BrochureDescription { get; set; }
        public string BrochureFeature1 { get; set; }
        public string BrochureFeature2 { get; set; }
        public string BrochureFeature3 { get; set; }
        public string BrochureFeature4 { get; set; }
        public string BrochureLifeStyle1 { get; set; }
        public string BrochureLifeStyle2 { get; set; }
        public string BrochureLifeStyle3 { get; set; }
    }
    public class Listing
    {
        public string ListingID { get; set; }
        public string WebID { get; set; }
        public string WebLink { get; set; }
        public string AddressDisplay { get; set; }
        public string StreetAddress { get; set; }
        public string LevelNum { get; set; }
        public string LotNum { get; set; }
        public string UnitNum { get; set; }
        public string StreetNum { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyCategory { get; set; }
        public string ListingType { get; set; }
        public string ListingDate { get; set; }
        public string ListingExpiryDate { get; set; }
        public string ListingStatus { get; set; }
        public string ListingMethod { get; set; }
        public string ListingAuthority { get; set; }
        public string InspectionType { get; set; }
        public string AuctionDate { get; set; }
        public string AuctionVenue { get; set; }
        public string EOIClosingDate { get; set; }
        public string DisplayPrice { get; set; }
        public string SearchPrice { get; set; }
        public string RentPeriod { get; set; }
        public string AvailableDate { get; set; }
        public string SoldDate { get; set; }
        public string SoldPrice { get; set; }
        public string SoldPriceConfidential { get; set; }
        public string MainHeadline { get; set; }
        public string MainDescription { get; set; }
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string LoungeRooms { get; set; }
        public string Toilets { get; set; }
        public string Studies { get; set; }
        public string Pools { get; set; }
        public string Garages { get; set; }
        public string Carports { get; set; }
        public string CarSpaces { get; set; }
        public string TotalParking { get; set; }
        public string LandArea { get; set; }
        public string LandAreaUnit { get; set; }
        public string BuildingAreaSqm { get; set; }
        public string ExternalAreaSqm { get; set; }
        public string FrontageM { get; set; }
        public string Aspect { get; set; }
        public string YearBuilt { get; set; }
        public string YearRenovated { get; set; }
        public string Construction { get; set; }
        public string PropertyCondition { get; set; }
        public string EnergyRating { get; set; }
        public string Features { get; set; }

        //double?
        public string WaterRates { get; set; }
        public string LandTax { get; set; }
        public string CounsilRates { get; set; }
        public string StrataAdmin { get; set; }
        public string StrataSinking { get; set; }
        public string OtherOutgoings { get; set; }
        public string TotalOutgoings { get; set; }

        public string LegalDescription { get; set; }
        public string LegalLot { get; set; }
        public string LegalDP { get; set; }
        public string LegalVol { get; set; }
        public string LegalFolio { get; set; }
        public string Zoning { get; set; }
        public ListingCustomCopy CustomCopy { get; set; }

        public string ContactCompanyID { get; set; }
        public string StoreCode { get; set; }

    }
    public class ListingStaffMember
    {
        public string MemberID { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
    public class ListingOFIs
    {
        public string ListingOFIID { get; set; }
        public string ListingID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string OFIRef { get; set; }
        public string ThirdPartyRef { get; set; }
        public string PropertyRef { get; set; }
    }

    public class ListingFloorplans
    {
        public string ListingID { get; set; }
        public string FloorplanID { get; set; }
        public string ImageURL { get; set; }
        public string PDFURL { get; set; }
        public string ImageOrder { get; set; }
        public string LastMod { get; set; }
    }
    public class ListingLinks
    {
        public string ListingID { get; set; }
        public string LinkType { get; set; }
        public string LinkTitle { get; set; }
        public string LinkURL { get; set; }
    }
    public class ListingAgents
    {
        public string ListingID { get; set; }
        public string MemberID { get; set; }
        public int AgentOrder { get; set; }

        //bellow properties are not inculed in json
        public string AgentID { get; set; }
        public string UserRef { get; set; }
        public string Name { get; set; }
        public bool? Admin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public bool? Deleted { get; set; }
    }
    public class ListingConjunctionalAgents
    {
        public string ListingID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
    }
    public class ListingVendors
    {
        public string ListingID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string MailingSalutation { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
    }
}

using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Domain Model
    /// </summary>
    public class Listing
    {
        public long ListingId { get; set; }
        public string WebID { get; set; }
        public string WebLink { get; set; }
        public string AddressDisplay { get; set; }
        public string StreetAddress { get; set; }
        public int? LevelNumber { get; set; }
        public int? LotNumber { get; set; }
        public int? UnitNumber { get; set; }
        public int? StreetNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyCategory { get; set; }
        public string ListingType { get; set; }
        public DateTime? ListingDate { get; set; }
        public DateTime? ListingExpiryDate { get; set; }
        public string ListingStatus { get; set; }
        public string ListingMethod { get; set; }
        public string ListingAuthority { get; set; }
        public string InspectionTypye { get; set; }
        public DateTime? AuctionDate { get; set; }
        public string AutionVenue { get; set; }
        public DateTime? EOIClosingDate { get; set; }
        public double? DisplayPrice { get; set; }
        public double? SearchPrice { get; set; }
        public int? RendPeriod { get; set; }
        public DateTime? AvailableDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public double? SoldPrice { get; set; }
        public bool? IsSoldPriceConfidential { get; set; }
        public string MainHeadLine { get; set; }
        public string MainDescription { get; set; }
        public string CustomCopy { get; set; }
        public int? BedRooms { get; set; }
        public int? BathRooms { get; set; }
        public int? LoungeRooms { get; set; }
        public int? Toilets { get; set; }
        public int? Studies { get; set; }
        public int? Pools { get; set; }
        public int? Garages { get; set; }
        public int? Carports { get; set; }
        public int? CarSpaces { get; set; }
        public int? TotalParking { get; set; }
        public double? LandArea { get; set; }
        public string LandAreaUnit { get; set; }
        public int? BuildingAreaSqm { get; set; }
        public int? ExternalAreaSqm { get; set; }
        public int? FrontageM { get; set; }
        public string Aspect { get; set; }
        public string YearBuilt { get; set; }
        public string YearRenovated { get; set; }
        public string Construction { get; set; }
        public string PropertyCondition { get; set; }
        public double? EnergyRating { get; set; }
        public string Features { get; set; }
        public double? WaterRates { get; set; }
        public double? LandTax { get; set; }
        public double? CounsilRates { get; set; }
        public double? StrataAdmin { get; set; }
        public double? StrataSinking { get; set; }
        public double? OtherOutgoings { get; set; }
        public string LegalDescription { get; set; }
        public string LegalLot { get; set; }
        public string LegalDP { get; set; }
        public string LegalVol { get; set; }
        public string LegalFolio { get; set; }
        public string Zoning { get; set; }
        public string ClientListingId { get; set; }
        public double? TotalOutgoings { get; set; }
        public long? CompanyId { get; set; }
    }
}

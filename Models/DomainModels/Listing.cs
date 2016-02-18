using System;
using System.Collections.Generic;

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
        public string LevelNumber { get; set; }
        public string LotNumber { get; set; }
        public string UnitNumber { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyCategory { get; set; }
        public string ListingType { get; set; }
        public Nullable<System.DateTime> ListingDate { get; set; }
        public Nullable<System.DateTime> ListingExpiryDate { get; set; }
        public string ListingStatus { get; set; }
        public string ListingMethod { get; set; }
        public string ListingAuthority { get; set; }
        public string InspectionTypye { get; set; }
        public Nullable<System.DateTime> AuctionDate { get; set; }
        public string AutionVenue { get; set; }
        public Nullable<System.DateTime> EOIClosingDate { get; set; }
        public string DisplayPrice { get; set; }
        public string SearchPrice { get; set; }
        public Nullable<int> RendPeriod { get; set; }
        public Nullable<System.DateTime> AvailableDate { get; set; }
        public Nullable<System.DateTime> SoldDate { get; set; }
        public string SoldPrice { get; set; }
        public Nullable<bool> IsSoldPriceConfidential { get; set; }
        public string MainHeadLine { get; set; }
        public string MainDescription { get; set; }
        public string CustomCopy { get; set; }
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string LoungeRooms { get; set; }
        public string Toilets { get; set; }
        public string Studies { get; set; }
        public string Pools { get; set; }
        public string Garages { get; set; }
        public string Carports { get; set; }
        public string CarSpaces { get; set; }
        public Nullable<int> TotalParking { get; set; }
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
        public string WaterRates { get; set; }
        public string LandTax { get; set; }
        public string CounsilRates { get; set; }
        public string StrataAdmin { get; set; }
        public string StrataSinking { get; set; }
        public string OtherOutgoings { get; set; }
        public string LegalDescription { get; set; }
        public string LegalLot { get; set; }
        public string LegalDP { get; set; }
        public string LegalVol { get; set; }
        public string LegalFolio { get; set; }
        public string Zoning { get; set; }
        public string ClientListingId { get; set; }
        public string TotalOutgoings { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string AirConditioning { get; set; }
        public string AlarmSystem { get; set; }
        public string Intercom { get; set; }
        public string OpenFirePlace { get; set; }
        public string TennisCourt { get; set; }
        public string RempoteGarage { get; set; }
        public string DishWasher { get; set; }
        public string BuiltinRaboes { get; set; }
        public string Gym { get; set; }
        public string WorkShop { get; set; }
        public string RumpusRoom { get; set; }
        public string FloorBoards { get; set; }
        public string BroadBand { get; set; }
        public string PayTV { get; set; }
        public string DuctedHeating { get; set; }
        public string DuctedCooling { get; set; }
        public string SplitSystemHeating { get; set; }
        public string ReverseCycleAircon { get; set; }
        public string EvaporateCooling { get; set; }
        public string VacuumSystem { get; set; }
        public string PoolInGround { get; set; }
        public string PoolAboveGround { get; set; }
        public string Balcony { get; set; }
        public string Deck { get; set; }
        public string CourtYard { get; set; }
        public string OutDoorEnt { get; set; }
        public string Shed { get; set; }
        public string FullyFenced { get; set; }
        public string InsideSPA { get; set; }
        public string OutSideSPA { get; set; }
        public string HydronicHeating { get; set; }
        public string SplitSystemAircon { get; set; }
        public string PriceView { get; set; }
        public string AuctionTime { get; set; }
        public string InspectionDate1 { get; set; }
        public string InspectionTimeFrom1 { get; set; }
        public string InspectionTimeTo1 { get; set; }
        public string InspectionDate2 { get; set; }
        public string InspectionTimeFrom2 { get; set; }
        public string InspectionTimeTo2 { get; set; }
        public string BrochureMainHeadLine { get; set; }
        public string BrochureSummary { get; set; }
        public string BrochureDescription { get; set; }
        public string SignBoardMainHeadLine { get; set; }
        public string SignBoardSummary { get; set; }
        public string SignBoardDescription { get; set; }
        public string SignBoardInstallInstruction { get; set; }
        public string AdvertsDescription { get; set; }
        public string AdvertsSummary { get; set; }
        public string AdvertsMainHeadLine { get; set; }
        public string AuctionEndTime { get; set; }

         public virtual ICollection<ListingBulletPoint> ListingBulletPoints { get; set; }

        

        
    }
}

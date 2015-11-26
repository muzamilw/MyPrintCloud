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
        public Nullable<int> LevelNumber { get; set; }
        public Nullable<int> LotNumber { get; set; }
        public Nullable<int> UnitNumber { get; set; }
        public Nullable<int> StreetNumber { get; set; }
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
        public Nullable<double> DisplayPrice { get; set; }
        public Nullable<double> SearchPrice { get; set; }
        public Nullable<int> RendPeriod { get; set; }
        public Nullable<System.DateTime> AvailableDate { get; set; }
        public Nullable<System.DateTime> SoldDate { get; set; }
        public Nullable<double> SoldPrice { get; set; }
        public Nullable<bool> IsSoldPriceConfidential { get; set; }
        public string MainHeadLine { get; set; }
        public string MainDescription { get; set; }
        public string CustomCopy { get; set; }
        public Nullable<int> BedRooms { get; set; }
        public Nullable<int> BathRooms { get; set; }
        public Nullable<int> LoungeRooms { get; set; }
        public Nullable<int> Toilets { get; set; }
        public Nullable<int> Studies { get; set; }
        public Nullable<int> Pools { get; set; }
        public Nullable<int> Garages { get; set; }
        public Nullable<int> Carports { get; set; }
        public Nullable<int> CarSpaces { get; set; }
        public Nullable<int> TotalParking { get; set; }
        public Nullable<double> LandArea { get; set; }
        public string LandAreaUnit { get; set; }
        public Nullable<int> BuildingAreaSqm { get; set; }
        public Nullable<int> ExternalAreaSqm { get; set; }
        public Nullable<int> FrontageM { get; set; }
        public string Aspect { get; set; }
        public string YearBuilt { get; set; }
        public string YearRenovated { get; set; }
        public string Construction { get; set; }
        public string PropertyCondition { get; set; }
        public Nullable<double> EnergyRating { get; set; }
        public string Features { get; set; }
        public Nullable<double> WaterRates { get; set; }
        public Nullable<double> LandTax { get; set; }
        public Nullable<double> CounsilRates { get; set; }
        public Nullable<double> StrataAdmin { get; set; }
        public Nullable<double> StrataSinking { get; set; }
        public Nullable<double> OtherOutgoings { get; set; }
        public string LegalDescription { get; set; }
        public string LegalLot { get; set; }
        public string LegalDP { get; set; }
        public string LegalVol { get; set; }
        public string LegalFolio { get; set; }
        public string Zoning { get; set; }
        public string ClientListingId { get; set; }
        public Nullable<double> TotalOutgoings { get; set; }
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


    }
}

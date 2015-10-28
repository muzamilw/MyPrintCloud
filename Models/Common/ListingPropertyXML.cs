using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MPC.Models.Common
{
    [XmlRoot("propertyList")]
    public class ListingPropertyXML
    {
        [XmlAttribute("MpcLoginEmail")]
        public string MpcLoginEmail { get; set; }
        [XmlAttribute("storecode")]
        public string StoreCode { get; set; }
        [XmlAttribute("date")]
        public string Date { get; set; }
        [XmlElement("residential")]
        public ListingXML Listing { get; set; }
       
    }

    public class ListingOfficeXML
    {
        [XmlAttribute("display")]
        public string Display { get; set; }

         [XmlElement("subNumber")]
        public string SubNumber { get; set; }
         [XmlElement("streetNumber")]
         public string StreetNumber { get; set; }
         [XmlElement("street")]
         public string Street { get; set; }
         [XmlElement("suburb")]
        public string Suburb { get; set; }
        [XmlElement("state")]
         public string State { get; set; }
        [XmlElement("postcode")]
         public string PostCode { get; set; }

    }

    public class ListingXML
    {
        [XmlAttribute("status")]
        public string Status { get; set; }
        [XmlAttribute("modeTime")]
        public string ModeTime { get; set; }
      

        [XmlElement("agentID")]
        public string AgentId { get; set; }

        [XmlElement("uniqueID")]
        public string ClientListingId { get; set; }
        [XmlElement("authority ")]
        public Authority ListingAuthority { get; set; }
     
        [XmlElement("underOffer")]
        public UnderOffer UnderOffer { get; set; }
         [XmlElement("listingAgent")]
        public List<ListingAgentsXML> ListingAgents { get; set; }
          [XmlElement("price")]
         public string Price { get; set; }
          [XmlElement("priceView")]
         public string PriceView { get; set; }
        [XmlElement("address")]
        public ListingOfficeXML Office { get; set; }
         [XmlElement("category")]
        public CategoryName PropertyCategory { get; set; }
         [XmlElement("headline")]
        public string MainHeadline { get; set; }
         [XmlElement("description")]   
        public string MainDescription { get; set; }
        [XmlElement("features")]   
         public FeaturesListing features { get; set; }
         [XmlElement("inspectionTimes")]   
        public string InspectionTimes { get; set; }
         [XmlElement("isHomeLandPackage")]  
        public IsHomeLandPackage IsHomeLandPackage { get; set; }
         [XmlElement("images")]  
        public ListingImagesXML ListingImages { get; set; }
         [XmlElement("objects")]  
        public ListingFloorplansXML ListingFloorplans { get; set; }
         [XmlElement("videoLink ")]  
        public VideoLink VideoLink { get; set; }
         [XmlElement("externalLink")]  
        public ExternalLink ExternalLink { get; set; }

         public string CompanyId { get; set; }
    }

    public class ListingAgentsXML
    {

        [XmlAttribute("id")]
        public string ID { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
         [XmlElement("telephone")]
        public string Mobile { get; set; }
     
    }

    public class FeaturesListing
    {
        [XmlElement("bedrooms")]  
        public string BedRooms { get; set; }
          [XmlElement("bathrooms")]  
        public string BathRooms { get; set; }
          [XmlElement("garages")]  
        public string Garages { get; set; }
          [XmlElement("carports")]  
        public string Carports { get; set; }
          [XmlElement("airConditioning")]  
        public string AirConditioning { get; set; }
          [XmlElement("alarmSystem")]  
        public string AlarmSystem { get; set; }
          [XmlElement("intercom")]  
        public string Intercom { get; set; }
          [XmlElement("openFirePlace")]  
       public string OpenFirePlace { get; set; }
          [XmlElement("tennisCourt")]  
       public string TennisCourt { get; set; }
          [XmlElement("toilets")]  
       public string Toilets { get; set; }
          [XmlElement("remoteGarage")]  
       public string RempoteGarage { get; set; }
       [XmlElement("secureParking")]  
       public string SecureParking { get; set; }
       [XmlElement("study")]  
       public string Study { get; set; }
         [XmlElement("dishwasher")]  
       public string DishWasher { get; set; }
       [XmlElement("builtInRobes")]  
       public string BuiltinRaboes { get; set; }
       [XmlElement("gym")]  
       public string Gym { get; set; }
       [XmlElement("workshop")]  
       public string WorkShop { get; set; }
      [XmlElement("rumpusRoom")]  
      public string RumpusRoom { get; set; }
       [XmlElement("floorboards")]  
      public string FloorBoards { get; set; }
      [XmlElement("broadband")]  
      public string BroadBand { get; set; }
      [XmlElement("payTV")]  
      public string PayTV { get; set; }
      [XmlElement("ductedHeating")]  
      public string DuctedHeating { get; set; }
       [XmlElement("ductedCooling")]  
     public string DuctedCooling { get; set; }
       [XmlElement("splitsystemHeating")]  
     public string SplitSystemHeating { get; set; }
       [XmlElement("hydronicHeating")]  
     public string HydronicHeating { get; set; }
        [XmlElement("splitsystemAircon")]  
    public string SplitSystemAircon { get; set; }
       [XmlElement("reverseCycleAircon")]  
    public string ReverseCycleAircon { get; set; }
       [XmlElement("evaporativeCooling")]  
    public string EvaporateCooling { get; set; }
  [XmlElement("vacuumSystem")]  
    public string VacuumSystem { get; set; }
      [XmlElement("poolInGround")]  
   public string PoolInGround { get; set; }
      [XmlElement("poolAboveGround")]  
   public string PoolAboveGround { get; set; }
     [XmlElement("balcony")]  
   public string Balcony { get; set; }
      [XmlElement("deck")]  
   public string Deck { get; set; }
     [XmlElement("courtyard")]  
   public string CourtYard{ get; set; }
      [XmlElement("outdoorEnt")]  
   public string OutDoorEnt{ get; set; }
     [XmlElement("shed")]  
   public string Shed{ get; set; }
  [XmlElement("fullyFenced")]  
   public string FullyFenced{ get; set; }
    [XmlElement("insideSpa")]  
   public string InsideSPA{ get; set; }
  [XmlElement("outsideSpa")]  
   public string OutSideSPA{ get; set; }

    }

    public class ListingImagesXML
    {
        [XmlElement("img")]  
        public List<PropertyImage> image { get; set; }
    }
    public class PropertyImage
    {
         [XmlAttribute("id")]
        public string ImageID { get; set; }
    
         [XmlAttribute("url")]
        public string ImageURL { get; set; }
         [XmlAttribute("modTime")]
        public string LastMod { get; set; }
    }
    public class ListingFloorplansXML
    {
        [XmlElement("floorplan")]
        public List<FloorPlan> floorplans { get; set; }
       
    }

    public class FloorPlan
    {
         [XmlAttribute("modTime")]
        public string LastMod { get; set; }
         [XmlAttribute("id")]
        public string FloorplanID { get; set; }
         [XmlAttribute("url")]
        public string ImageURL { get; set; }
    }
    public class Authority
    {
        [XmlAttribute("value")]
        public string value { get; set; }

    }

    public class UnderOffer
    {
        [XmlAttribute("value")]
        public string value { get; set; }

    }
    public class CategoryName
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

    }
    public class IsHomeLandPackage
    {
        [XmlAttribute("value")]
        public string value { get; set; }

    }
    public class VideoLink
    {
        [XmlAttribute("href")]
        public string href { get; set; }

    }

    public class ExternalLink
    {
        [XmlAttribute("href")]
        public string href { get; set; }

    }

}

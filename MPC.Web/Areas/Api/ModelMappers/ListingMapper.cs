using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ListingMapper
    {

        public static vw_RealEstateProperties CreateFrom(this MPC.Models.DomainModels.vw_RealEstateProperties source)
        {
            return new vw_RealEstateProperties
            {
                ListingID = source.ListingID,
                WebLink = source.WebLink,
                AddressDisplay = source.AddressDisplay,
                StreetAddress = source.StreetAddress,
                StreetNumber = source.StreetNumber,
                Street = source.Street,
                Suburb = source.Suburb,
                State = source.State,
                PropertyName = source.PropertyName,
                PropertyType = source.PropertyType,
                PropertyCategory = source.PropertyCategory,
                DisplayPrice = source.DisplayPrice,
                MainHeadLine = source.MainHeadLine,
                MainDescription = source.MainDescription,
                BedRooms = source.BedRooms,
                BathRooms = source.BathRooms,
                LoungeRooms = source.LoungeRooms,
                Toilets = source.Toilets,
                Studies = source.Studies,
                Pools = source.Pools,
                Garages = source.Garages,
                Features = source.Features,
                CompanyId = source.CompanyId,
                ListingImage = source.ListingImage,
                ListingAgent = source.ListingAgent

            };
        }

        public static MPC.Models.DomainModels.vw_RealEstateProperties CreateFrom(this vw_RealEstateProperties source)
        {
            return new MPC.Models.DomainModels.vw_RealEstateProperties
            {
               ListingID = source.ListingID,
               WebLink = source.WebLink,
               AddressDisplay = source.AddressDisplay,
               StreetAddress = source.StreetAddress,
               StreetNumber = source.StreetNumber,
               Street = source.Street,
               Suburb = source.Suburb,
               State = source.State,
               PropertyName = source.PropertyName,
               PropertyType = source.PropertyType,
               PropertyCategory = source.PropertyCategory,
               DisplayPrice = source.DisplayPrice,
               MainHeadLine = source.MainHeadLine,
               MainDescription = source.MainDescription,
               BedRooms = source.BedRooms,
               BathRooms = source.BathRooms,
               LoungeRooms = source.LoungeRooms,
               Toilets = source.Toilets,
               Studies = source.Studies,
               Pools = source.Pools,
               Garages = source.Garages,
               Features = source.Features,
               CompanyId = source.CompanyId,
               ListingImage = source.ListingImage,
               ListingAgent =  source.ListingAgent

            };
        }

     

    }
}
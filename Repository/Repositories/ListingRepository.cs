using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    class ListingRepository : BaseRepository<Listing>, IListingRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ListingRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Listing> DbSet
        {
            get
            {
                return db.Listings;
            }
        }

        #endregion

        #region public

        public List<Listing> GetRealEstateProperties()
        {
            List<Listing> lstListings = db.Listings.ToList();

            return lstListings;
        }


        public string GetImageURLByListingId(long listingId)
        {
            string imageURL = string.Empty;
            ListingImage listingImage = db.ListingImages.Where(i => i.ListingId == listingId).FirstOrDefault();

            if (listingImage != null)
            {
                imageURL = listingImage.ImageURL;
            }

            return imageURL;
        }

        public List<FieldVariable> GeyFieldVariablesByItemID(long itemId)
        {

            var tempID = (from i in db.Items
                          where i.ItemId == itemId
                          select i.TemplateId).FirstOrDefault();

            long templateID = Convert.ToInt64(tempID);

            var IDs = (from v in db.TemplateVariables
                       where v.TemplateId == templateID
                       select v.VariableId).ToList();

            List<FieldVariable> lstFieldVariables = new List<FieldVariable>();

            foreach (long item in IDs)
            {
                FieldVariable objFieldVariable = (from FV in db.FieldVariables
                                                  where FV.VariableId == item
                                                  orderby FV.VariableSectionId
                                                  select FV).FirstOrDefault();

                lstFieldVariables.Add(objFieldVariable);
            }

            List<FieldVariable> finalList = (List<FieldVariable>)lstFieldVariables.OrderBy(item => item.VariableSectionId).ToList();

            return finalList;
        }

        public Listing GetListingByListingId(long listingId)
        {
            return (from Listing in db.Listings
                    where Listing.ListingId == listingId
                    select Listing).FirstOrDefault();
        }

        public List<ListingOFI> GetListingOFIsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingOFIs
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingImage> GetListingImagesByListingId(long listingId)
        {
            return (from listingImage in db.ListingImages
                    where listingImage.ListingId == listingId
                    select listingImage).ToList();
        }
        public List<ListingFloorPlan> GetListingFloorPlansByListingId(long listingId)
        {
            return (from listingOFID in db.ListingFloorPlans
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingLink> GetListingLinksByListingId(long listingId)
        {
            return (from listingOFID in db.ListingLinks
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingAgent> GetListingAgentsByListingId(long listingId)
        {
            return (from listingAgents in db.ListingAgents
                    where listingAgents.ListingId == listingId
                    select listingAgents).ToList();
        }
        public List<ListingConjunctionAgent> GetListingConjunctionalAgentsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingConjunctionAgents
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }
        public List<ListingVendor> GetListingVendorsByListingId(long listingId)
        {
            return (from listingOFID in db.ListingVendors
                    where listingOFID.ListingId == listingId
                    select listingOFID).ToList();
        }

        #endregion

    }
}

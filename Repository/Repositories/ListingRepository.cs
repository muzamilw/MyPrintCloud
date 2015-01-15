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

        #endregion

    }
}

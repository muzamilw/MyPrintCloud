using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class RaveReview
    {
        public long ReviewId { get; set; }
        public string ReviewBy { get; set; }
        public string Review { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<bool> isDisplay { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<long> CompanyId { get; set; }

        public virtual Company Company { get; set; }

        #region public


        /// <summary>
        /// Makes a copy of Entity
        /// </summary>
        ///   

        public void Clone(RaveReview target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemProductDetailClone_InvalidItemProductDetail, "target");
            }


            target.ReviewBy = ReviewBy;
            target.Review = Review;
            target.ReviewDate = ReviewDate;
            target.isDisplay = isDisplay;
            target.SortOrder = SortOrder;
            target.OrganisationId = OrganisationId;
           


        }

        #endregion
    }
}

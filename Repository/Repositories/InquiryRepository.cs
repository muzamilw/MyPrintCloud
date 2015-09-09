using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    public class InquiryRepository : BaseRepository<Inquiry>, IInquiryRepository
    {
        public InquiryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Inquiry> DbSet
        {
            get
            {
                return db.Inquiries;
            }
        }
        public long AddInquiryAndItems(Inquiry Inquiry, List<InquiryItem> InquiryItems)
        {
            
            try
            {
                Prefix oPrefix =  db.Prefixes.Where(c => c.SystemSiteId == 1).FirstOrDefault();
                // Get order prefix and update the order next number

                if (oPrefix != null)
                {
                    Inquiry.InquiryCode = oPrefix.EnquiryPrefix + "-001-" + oPrefix.EnquiryNext.ToString();
                    oPrefix.EnquiryNext = oPrefix.EnquiryNext + 1;
                }

                db.Inquiries.Add(Inquiry);
                if(db.SaveChanges() > 0)
                {
                    foreach (var item in InquiryItems) 
                    {
                        item.InquiryId = Inquiry.InquiryId;
                        db.InquiryItems.Add(item);
                    }
                }

                if (db.SaveChanges() > 0)
                {
                    return Inquiry.InquiryId;
                }
                else 
                {
                    return Inquiry.InquiryId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Inquieries Screen From Estimate
        /// <summary>
        /// Get Inquiries For Specified Search
        /// </summary>
        public GetInquiryResponse GetInquiries(GetInquiryRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;//if true get all then get by status
            bool filterFlagSpecified = request.FilterFlag == 0;
            //Order Type Filter , 2-> all, 0 -> Direct  Order, 1 -> Online Order
            bool orderTypeFilterSpecified = request.OrderTypeFilter == 2;
            Expression<Func<Inquiry, bool>> query =


                item =>
                    ((
                    string.IsNullOrEmpty(request.SearchString) ||
                    ((item.Company != null && item.Company.Name.Contains(request.SearchString)) || (item.InquiryCode.Contains(request.SearchString)) ||
                    (item.Title.Contains(request.SearchString)) 
                    )) &&
                    item.OrganisationId == OrganisationId);

            IEnumerable<Inquiry> items = DbSet.Where(query)
                .Skip(fromRow)
                .Take(toRow)
                .ToList();

            return new GetInquiryResponse { Inquiries = items, TotalCount = DbSet.Count(query) };
        }
        #endregion

    }
}

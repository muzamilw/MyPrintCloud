using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class EstimateInquiryRepository : BaseRepository<Inquiry>, IEstimateInquiryRepository
    {
        public EstimateInquiryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Inquiry> DbSet
        {
            get { return db.Inquiries; }
        }
        
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

            IEnumerable<Inquiry> items = request.IsAsc
               ? DbSet.Where(query)
                     .OrderByDescending(x => x.InquiryId)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                     .OrderByDescending(x => x.InquiryId)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
         
            return new GetInquiryResponse { Inquiries = items, TotalCount = DbSet.Count(query) };
        }

    }
}

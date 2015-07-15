using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    ///  Repository
    /// </summary>
    public class DiscountVoucherRepository : BaseRepository<DiscountVoucher>, IDiscountVoucherRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DiscountVoucherRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<DiscountVoucher> DbSet
        {
            get
            {
                return db.DiscountVouchers;
            }
        }

        #endregion
        #region Public

        /// <summary>
        /// Discount Voucher List view 
        /// </summary>
        public DiscountVoucherListViewResponse GetDiscountVoucherListView(DiscountVoucherRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isString = !string.IsNullOrEmpty(request.SearchString);

            Expression<Func<DiscountVoucher, bool>> query =
                voucher =>
                    ((!isString || voucher.VoucherName.Contains(request.SearchString) ||
                      voucher.CouponCode.Contains(request.SearchString)
                      || voucher.DiscountRate.ToString() == request.SearchString) && voucher.CompanyId == request.CompanyId

                        );

            IEnumerable<DiscountVoucher> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(voucher => voucher.VoucherName)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(voucher => voucher.VoucherName)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new DiscountVoucherListViewResponse { DiscountVouchers = items, RowCount = DbSet.Count(query) };
        }
        #endregion
    }
}

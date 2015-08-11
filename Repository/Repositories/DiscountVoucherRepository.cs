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


        public DiscountVoucher GetStoreDefaultDiscountRate(long StoreId, long OrganisationId)
        {
            try
            {
                return db.DiscountVouchers.Where(d => d.CompanyId == StoreId && d.CouponCode == null && d.IsEnabled == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DiscountVoucher GetDiscountVoucherById(long DiscountVoucherId)
        {
            try
            {
                return db.DiscountVouchers.Where(d => d.DiscountVoucherId == DiscountVoucherId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DiscountVoucher GetDiscountVoucherByVoucherId(long DVId)
        {
            try
            {
                DiscountVoucher dv = db.DiscountVouchers.Where(c => c.DiscountVoucherId == DVId).FirstOrDefault();

                if(dv.ProductCategoryVouchers != null && dv.ProductCategoryVouchers.Count > 0)
                {
                    foreach(var objDV in dv.ProductCategoryVouchers)
                    {
                        objDV.CategoryName = db.ProductCategories.Where(c => c.ProductCategoryId == objDV.ProductCategoryId).Select(v => v.CategoryName).FirstOrDefault();


                    }
                }

                return dv;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DiscountVoucher GetDiscountVoucherByCouponCode(string DiscountVoucherName, long StoreId, long OrganisationId)
        {
            try
            {
                return db.DiscountVouchers.Where(d => d.CouponCode == DiscountVoucherName && d.CompanyId == StoreId && d.OrganisationId == OrganisationId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DiscountVoucher AddCategoryVoucher(DiscountVoucher discountVoucher)
        {
            try
            {

                DiscountVoucher discountVoucherDbVersion = db.DiscountVouchers.Where(c => c.DiscountVoucherId == discountVoucher.DiscountVoucherId).FirstOrDefault();
                if (discountVoucherDbVersion != null)
                {
                    discountVoucherDbVersion.VoucherName = discountVoucher.VoucherName;
                    discountVoucherDbVersion.DiscountRate = discountVoucher.DiscountRate;
                    discountVoucherDbVersion.DiscountType = discountVoucher.DiscountType;
                    discountVoucherDbVersion.HasCoupon = discountVoucher.HasCoupon;
                    discountVoucherDbVersion.CouponCode = discountVoucher.CouponCode;
                    discountVoucherDbVersion.CouponUseType = discountVoucher.CouponUseType;
                    discountVoucherDbVersion.IsUseWithOtherCoupon = discountVoucher.IsUseWithOtherCoupon;
                    discountVoucherDbVersion.CompanyId = discountVoucher.CompanyId;

                    discountVoucherDbVersion.IsTimeLimit = discountVoucher.IsTimeLimit;
                    discountVoucherDbVersion.ValidFromDate = discountVoucher.ValidFromDate;
                    discountVoucherDbVersion.ValidUptoDate = discountVoucher.ValidUptoDate;

                    discountVoucherDbVersion.IsQtyRequirement = discountVoucher.IsQtyRequirement;
                    discountVoucherDbVersion.MinRequiredQty = discountVoucher.MinRequiredQty;
                    discountVoucherDbVersion.MaxRequiredQty = discountVoucher.MaxRequiredQty;
                    discountVoucherDbVersion.IsQtySpan = discountVoucher.IsQtySpan;

                    discountVoucherDbVersion.IsOrderPriceRequirement = discountVoucher.IsOrderPriceRequirement;
                    discountVoucherDbVersion.MinRequiredOrderPrice = discountVoucher.MinRequiredOrderPrice;
                    discountVoucherDbVersion.MaxRequiredOrderPrice = discountVoucher.MaxRequiredOrderPrice;
                    discountVoucherDbVersion.IsEnabled = discountVoucher.IsEnabled;



                    if (discountVoucher.ProductCategoryVouchers != null && discountVoucher.ProductCategoryVouchers.Count() > 0)
                    {
                        foreach (var obj in discountVoucher.ProductCategoryVouchers)
                        {
                            if (obj.CategoryVoucherId == 0)
                            {
                                ProductCategoryVoucher objVoucher = new ProductCategoryVoucher();
                                objVoucher.ProductCategoryId = obj.ProductCategoryId;
                                objVoucher.VoucherId = discountVoucherDbVersion.DiscountVoucherId;
                               // objVoucher.DiscountVoucher = null;
                                db.ProductCategoryVouchers.Add(objVoucher);
                                List<Item> CategoryProducts = GetItemsByCategoryId(obj.ProductCategoryId ?? 0);

                                foreach (var itm in CategoryProducts)
                                {
                                    ItemsVoucher ObjItemVoucher = new ItemsVoucher();
                                    ObjItemVoucher.ItemId = itm.ItemId;
                                    ObjItemVoucher.VoucherId = discountVoucherDbVersion.DiscountVoucherId;
                                    db.ItemsVouchers.Add(ObjItemVoucher);
                                }

                            }

                        }
                        
                    }

                    
                }
                db.SaveChanges();
                return discountVoucher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public List<Item> GetItemsByCategoryId(long CategoryId)
        {
            db.Configuration.LazyLoadingEnabled = false;

            var qry = from items in db.Items
                      join pci in db.ProductCategoryItems on items.ItemId equals pci.ItemId
                      join pc in db.ProductCategories on pci.CategoryId equals pc.ProductCategoryId

                      where pc.ProductCategoryId == CategoryId && (items.IsPublished == true || items.IsPublished == null) && (items.IsArchived == false || items.IsArchived == null)

                      select items;

            return qry.ToList();
        }
        #endregion
    }
}

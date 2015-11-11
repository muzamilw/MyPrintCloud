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
using MPC.ExceptionHandling;

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


        public List<DiscountVoucher> GetStoreDefaultDiscountVouchers(long StoreId, long OrganisationId)
        {
            try
            {
                //&& ((d.IsTimeLimit == null || d.IsTimeLimit == false) || (d.IsTimeLimit == true && d.ValidUptoDate <= DateTime.Now)
                return db.DiscountVouchers.Where(d => d.CompanyId == StoreId && (d.HasCoupon == null || d.HasCoupon == false) && d.IsEnabled == true).ToList();
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

                if (dv.ProductCategoryVouchers != null && dv.ProductCategoryVouchers.Count > 0)
                {
                    foreach (var objDV in dv.ProductCategoryVouchers)
                    {
                        objDV.CategoryName = db.ProductCategories.Where(c => c.ProductCategoryId == objDV.ProductCategoryId).Select(v => v.CategoryName).FirstOrDefault();


                    }
                }
                if (dv.ItemsVouchers != null && dv.ItemsVouchers.Count > 0)
                {
                    foreach (var objDV in dv.ItemsVouchers)
                    {
                        objDV.ProductName = db.Items.Where(c => c.ItemId == objDV.ItemId).Select(v => v.ProductName).FirstOrDefault();


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
                return db.DiscountVouchers.Where(d => d.CouponCode == DiscountVoucherName && d.CompanyId == StoreId && d.OrganisationId == OrganisationId && d.IsEnabled == true && d.HasCoupon == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public long IsStoreHaveFreeShippingDiscountVoucher(long StoreId, long OrganisationId, double OrderTotal)
        {
            try
            {
                List<DiscountVoucher> freeShippingV = db.DiscountVouchers.Where(d => d.CompanyId == StoreId && (d.HasCoupon == null || d.HasCoupon == false) && d.IsEnabled == true && d.DiscountType == (int)DiscountTypes.FreeShippingonEntireorder).ToList();
                DiscountVoucher dVToReturn = null;
                bool isSetVoucher = true;
                if (freeShippingV != null && freeShippingV.Count > 0)
                {
                    foreach (DiscountVoucher freeShipping in freeShippingV)
                    {
                        if (freeShipping.IsTimeLimit == true)
                        {
                            DateTime? ValidFromDate = freeShipping.ValidFromDate;
                            DateTime? ValidUptoDate = freeShipping.ValidUptoDate;
                            DateTime TodayDate = DateTime.Now;
                            if (ValidFromDate != null)
                            {
                                if (TodayDate < ValidFromDate)
                                {
                                    isSetVoucher = false;
                                }
                            }
                            if (ValidUptoDate != null)
                            {
                                if (TodayDate > ValidUptoDate)
                                {
                                    isSetVoucher = false;
                                }
                            }
                        }
                        if (freeShipping.IsOrderPriceRequirement.HasValue && freeShipping.IsOrderPriceRequirement.Value == true)
                        {
                            if (freeShipping.MinRequiredOrderPrice.HasValue && freeShipping.MinRequiredOrderPrice.Value > 0)
                            {
                                if (OrderTotal < freeShipping.MinRequiredOrderPrice.Value)
                                {
                                    isSetVoucher = false;
                                }
                            }

                            if (freeShipping.MaxRequiredOrderPrice.HasValue && freeShipping.MaxRequiredOrderPrice.Value > 0)
                            {
                                if (OrderTotal > freeShipping.MaxRequiredOrderPrice.Value)
                                {
                                    isSetVoucher = false;
                                }
                            }
                        }
                        if (isSetVoucher == true)
                        {
                            dVToReturn = freeShipping;
                            return freeShipping.DiscountVoucherId;
                        }
                    }


                    return 0;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DiscountVoucher UpdateVoucher(DiscountVoucher discountVoucher)
        {
            try
            {
                if (discountVoucher.HasCoupon == true)
                {
                    // Check for Code Duplication
                    bool isDuplicateCode = IsDuplicateCouponCode(discountVoucher.CouponCode, discountVoucher.CompanyId, discountVoucher.DiscountVoucherId);
                    if (isDuplicateCode)
                    {
                        throw new MPCException("Coupon Code already exist.", OrganisationId);
                    }

                }

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


                    // logic to delete product category which is not selected now
                    List<long> DeleteCategoryVoucherIds = new List<long>();

                    if (discountVoucherDbVersion.ProductCategoryVouchers != null && discountVoucherDbVersion.ProductCategoryVouchers.Count > 0)
                    {

                        foreach (var pcv in discountVoucherDbVersion.ProductCategoryVouchers)
                        {
                            if (discountVoucher.ProductCategoryVouchers != null)
                            {
                                ProductCategoryVoucher dbRecord = discountVoucher.ProductCategoryVouchers.Where(c => c.CategoryVoucherId == pcv.CategoryVoucherId).FirstOrDefault();
                                if (dbRecord == null)
                                {
                                    DeleteCategoryVoucherIds.Add(pcv.CategoryVoucherId);

                                    // this category voucher will delete becox its not in updated product category list
                                }
                            }



                        }
                        if (DeleteCategoryVoucherIds != null && DeleteCategoryVoucherIds.Count > 0)
                        {
                            foreach (var DCVI in DeleteCategoryVoucherIds)
                            {
                                ProductCategoryVoucher deleteVoucherCat = db.ProductCategoryVouchers.Where(c => c.CategoryVoucherId == DCVI).FirstOrDefault();
                                discountVoucherDbVersion.ProductCategoryVouchers.Remove(deleteVoucherCat);
                            }
                        }
                    }

                    // logic to delete item voucher which is not selected now
                    List<long> DeleteItemVoucherIds = new List<long>();

                    if (discountVoucherDbVersion.ItemsVouchers != null && discountVoucherDbVersion.ItemsVouchers.Count > 0)
                    {

                        foreach (var pcv in discountVoucherDbVersion.ItemsVouchers)
                        {
                            if (discountVoucher.ItemsVouchers != null)
                            {
                                ItemsVoucher dbRecord = discountVoucher.ItemsVouchers.Where(c => c.ItemVoucherId == pcv.ItemVoucherId).FirstOrDefault();
                                if (dbRecord == null)
                                {
                                    DeleteItemVoucherIds.Add(pcv.ItemVoucherId);

                                    // this category voucher will delete becox its not in updated product category list
                                }
                            }



                        }
                        if (DeleteItemVoucherIds != null && DeleteItemVoucherIds.Count > 0)
                        {
                            foreach (var DCVI in DeleteItemVoucherIds)
                            {
                                ItemsVoucher deleteVoucherItems = db.ItemsVouchers.Where(c => c.ItemVoucherId == DCVI).FirstOrDefault();
                                discountVoucherDbVersion.ItemsVouchers.Remove(deleteVoucherItems);
                            }
                        }
                    }
                    // add product category vouchers
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
                                //List<Item> CategoryProducts = GetItemsByCategoryId(obj.ProductCategoryId ?? 0);

                                //foreach (var itm in CategoryProducts)
                                //{
                                //    ItemsVoucher ObjItemVoucher = new ItemsVoucher();
                                //    ObjItemVoucher.ItemId = itm.ItemId;
                                //    ObjItemVoucher.VoucherId = discountVoucherDbVersion.DiscountVoucherId;
                                //    db.ItemsVouchers.Add(ObjItemVoucher);
                                //}

                            }

                        }

                    }
                    // add items voucher
                    if (discountVoucher.ItemsVouchers != null && discountVoucher.ItemsVouchers.Count() > 0)
                    {
                        foreach (var obj in discountVoucher.ItemsVouchers)
                        {
                            if (obj.ItemVoucherId == 0)
                            {
                                ItemsVoucher objVoucher = new ItemsVoucher();
                                objVoucher.ItemId = obj.ItemId;
                                objVoucher.VoucherId = discountVoucherDbVersion.DiscountVoucherId;
                                // objVoucher.DiscountVoucher = null;
                                db.ItemsVouchers.Add(objVoucher);


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

        public DiscountVoucher CreateDiscountVoucher(DiscountVoucher discountVoucher)
        {
            try
            {
                discountVoucher.VoucherCode = Guid.NewGuid().ToString();
                discountVoucher.OrganisationId = OrganisationId;
                discountVoucher.IsEnabled = true;

                // Check for Code Duplication
                if (discountVoucher.HasCoupon == true)
                {
                    bool isDuplicateCode = IsDuplicateCouponCode(discountVoucher.CouponCode, discountVoucher.CompanyId, discountVoucher.DiscountVoucherId);
                    if (isDuplicateCode)
                    {
                        throw new MPCException("Coupon Code already exist.", OrganisationId);
                    }

                }




                db.DiscountVouchers.Add(discountVoucher);

                db.SaveChanges();


                //if (discountVoucher.ProductCategoryVouchers != null && discountVoucher.ProductCategoryVouchers.Count() > 0)
                //{
                //    foreach (var obj in discountVoucher.ProductCategoryVouchers)
                //    {


                //            List<Item> CategoryProducts = GetItemsByCategoryId(obj.ProductCategoryId ?? 0);

                //            foreach (var itm in CategoryProducts)
                //            {
                //                ItemsVoucher ObjItemVoucher = new ItemsVoucher();
                //                ObjItemVoucher.ItemId = itm.ItemId;
                //                ObjItemVoucher.VoucherId = discountVoucher.DiscountVoucherId;
                //                db.ItemsVouchers.Add(ObjItemVoucher);
                //            }



                //    }
                //    db.SaveChanges();
                //}

                //if (discountVoucher.ProductCategoryVouchers != null && discountVoucher.ProductCategoryVouchers.Count() > 0)
                //{
                //    foreach (var obj in discountVoucher.ProductCategoryVouchers)
                //    {


                //        List<Item> CategoryProducts = GetItemsByCategoryId(obj.ProductCategoryId ?? 0);

                //        foreach (var itm in CategoryProducts)
                //        {
                //            ItemsVoucher ObjItemVoucher = new ItemsVoucher();
                //            ObjItemVoucher.ItemId = itm.ItemId;
                //            ObjItemVoucher.VoucherId = discountVoucher.DiscountVoucherId;
                //            db.ItemsVouchers.Add(ObjItemVoucher);
                //        }



                //    }
                //    db.SaveChanges();
                //}
                return discountVoucher;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        /// <summary>
        /// Check if coupon code provided already exists
        /// </summary>
        public bool IsDuplicateCouponCode(string CouponCode, long? companyId, long DiscountVoucherId)
        {
            try
            {
                return db.DiscountVouchers.Any(c => c.CouponCode == CouponCode && c.CompanyId == companyId && c.DiscountVoucherId != DiscountVoucherId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long IsDiscountVoucherApplied(long StoreId, long OrganisationId)
        {
            try
            {
                List<DiscountVoucher> freeShippingV = db.DiscountVouchers.Where(d => d.CompanyId == StoreId && (d.HasCoupon == null || d.HasCoupon == false) && d.IsEnabled == true && d.DiscountType == (int)DiscountTypes.FreeShippingonEntireorder).ToList();
                if (freeShippingV != null && freeShippingV.Count > 0)
                {
                    return freeShippingV.FirstOrDefault().DiscountVoucherId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool isCouponVoucher(long DiscountVoucherId)
        {
            try
            {
                if (DiscountVoucherId > 0)
                {
                    DiscountVoucher voucher = db.DiscountVouchers.Where(d => d.DiscountVoucherId == DiscountVoucherId).FirstOrDefault();
                    if (voucher != null && (voucher.HasCoupon == false || voucher.HasCoupon == null))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DiscountVoucher> getDiscountVouchersByCompanyId(long companyId)
        {
            try
            {

                return db.DiscountVouchers.Include("ProductCategoryVouchers").Include("ItemsVouchers").Where(c => c.CompanyId == companyId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}

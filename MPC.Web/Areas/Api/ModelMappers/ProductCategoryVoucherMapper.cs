using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ProductCategoryVoucherMapper
    {

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ProductCategoryVoucher CreateFrom(this MPC.Models.DomainModels.ProductCategoryVoucher source)
        {
            if (source == null)
            {
                return null;
            }

            return new ProductCategoryVoucher
            {

                CategoryVoucherId = source.CategoryVoucherId,
                ProductCategoryId = source.ProductCategoryId,
                VoucherId = source.VoucherId,
                CategoryName = source.CategoryName,
                IsSelected = true
                 
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static MPC.Models.DomainModels.ProductCategoryVoucher CreateFrom(this ProductCategoryVoucher source)
        {
            if (source == null)
            {
                return null;
            }

            return new MPC.Models.DomainModels.ProductCategoryVoucher
            {
                CategoryVoucherId = source.CategoryVoucherId,
                ProductCategoryId = source.ProductCategoryId,
                VoucherId = source.VoucherId,
                CategoryName = source.CategoryName,
              
            };
        }

    }
}
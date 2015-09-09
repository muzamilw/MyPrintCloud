using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ItemsVoucherMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemsVoucher CreateFrom(this MPC.Models.DomainModels.ItemsVoucher source)
        {
            if (source == null)
            {
                return null;
            }

            return new ItemsVoucher
            {

                ItemVoucherId = source.ItemVoucherId,
                ItemId = source.ItemId,
                VoucherId = source.VoucherId,
                ProductName = source.ProductName,
                IsSelected = true
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static MPC.Models.DomainModels.ItemsVoucher CreateFrom(this ItemsVoucher source)
        {
            if (source == null)
            {
                return null;
            }

            return new MPC.Models.DomainModels.ItemsVoucher
            {
                ItemVoucherId = source.ItemVoucherId,
                ItemId = source.ItemId,
                VoucherId = source.VoucherId,
                ProductName = source.ProductName,
            };
        }

    }
}
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Item Stock Update History Api Mapper
    /// </summary>
    public static class ItemStockUpdateHistoryMapper
    {
        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ItemStockUpdateHistory CreateFrom(this DomainModels.ItemStockUpdateHistory source)
        {
            return new ItemStockUpdateHistory
            {
                StockHistoryId = source.StockHistoryId,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                LastModifiedQty = source.LastModifiedQty,
                ModifyEvent = source.ModifyEvent,
                LastModifiedByName = source.SystemUser != null ? source.SystemUser.FullName : string.Empty,
                Action = ActionName(source.ModifyEvent),
            };
        }

        /// <summary>
        /// Create From API Model
        /// </summary>
        public static DomainModels.ItemStockUpdateHistory CreateFrom(this ItemStockUpdateHistory source)
        {
            return new DomainModels.ItemStockUpdateHistory
            {
                StockHistoryId = source.StockHistoryId,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                LastModifiedQty = source.LastModifiedQty,
                ModifyEvent = source.ModifyEvent,
            };
        }
        #endregion

        #region private
        //According to enum 
        private static string ActionName(int? modifyEvent)
        {
            switch (modifyEvent)
            {
                case 1:
                    return "Added";
                case 2:
                    return "Ordered";
                case 3:
                    return "Threshold Order";
                case 4:
                    return "Back Order";
                case 5:
                    return "Removed";
            }
            return "";
        }
        #endregion
    }
}
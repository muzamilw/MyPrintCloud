using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class StockItemForDropDownMapper
    {
        #region Public
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static StockItemForDropDown CreateFromDropDown(this MPC.Models.DomainModels.StockItem source)
        {
            return new StockItemForDropDown
            {
               StockItemId = source.StockItemId,
               AlternateName = source.AlternateName,
               ItemCode = source.ItemCode,
               ItemName = source.ItemName
            };
        }
        #endregion
    }
}
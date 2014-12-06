using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    ///  Item Mapper actions
    /// </summary>
    public sealed class ItemMapperActions
    {
        #region Public

        /// <summary>
        /// Action to create a Item Vdp Price
        /// </summary>
        public Func<ItemVdpPrice> CreateItemVdpPrice { get; set; }

        /// <summary>
        /// Action to delete a Item Vdp Price
        /// </summary>
        public Action<ItemVdpPrice> DeleteItemVdpPrice { get; set; }

        #endregion
    }
}

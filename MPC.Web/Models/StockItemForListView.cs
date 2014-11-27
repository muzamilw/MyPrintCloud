namespace MPC.MIS.Models
{
    /// <summary>
    /// Stock Item For Lit View
    /// </summary>
    public class StockItemForListView
    {
        /// <summary>
        /// Stock Item ID
        /// </summary>
        public int StockItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Item Weight
        /// </summary>
        public int? ItemWeight { get; set; }

        /// <summary>
        /// Item Colour
        /// </summary>
        public string ItemColour { get; set; }
    }
}
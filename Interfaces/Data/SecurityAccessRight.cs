namespace MPC.Interfaces.Data
{
    /// <summary>
    /// Security Access Right
    /// Has Right Id - Right Id should not be autoincremented but it should take section id if parent section
    /// and for child actions it should be incremented or we need to change the rightId with RightName
    /// </summary>
    public enum SecurityAccessRight
    {
        /// <summary>
        /// Can View Security
        /// </summary>
        CanViewSecurity = 1,

        /// <summary>
        /// Can View Organisation
        /// </summary>
        CanViewOrganisation = 2,

        /// <summary>
        /// Can Change Marup
        /// </summary>
        CanChangeMarkup = 3,

        /// <summary>
        /// Can View PaperSheet
        /// </summary>
        CanViewPaperSheet = 4,

        /// <summary>
        /// Can View Inventory
        /// </summary>
        CanViewInventory = 5,

        /// <summary>
        /// Can View Inventory Category
        /// </summary>
        CanViewInventoryCategory = 6,

        /// <summary>
        /// Can View Product
        /// </summary>
        CanViewProduct = 7,

        /// <summary>
        /// Can View Order
        /// </summary>
        CanViewOrder = 8
    }
}

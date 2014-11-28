namespace MPC.Interfaces.Data
{
    /// <summary>
    /// Security Access Right
    /// Has Right Id
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
        CanVeiwPaperSheet = 4,

        /// <summary>
        /// Can View Inventory
        /// </summary>
        CanVeiwInventory = 5,

        /// <summary>
        /// Can View Inventory Category
        /// </summary>
        CanVeiwInventoryCategory = 6
    }
}

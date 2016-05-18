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
        CanViewOrder = 8,

        /// <summary>
        /// Can View Dashboard
        /// </summary>
        CanViewDashboard = 9,

        /// <summary>
        /// Can View CRM
        /// </summary>
        CanViewCrm = 10,

        /// <summary>
        /// Can View Prospect
        /// </summary>
        CanViewProspect = 11,

        /// <summary>
        /// Can View Supplier
        /// </summary>
        CanViewSupplier = 12,

        /// <summary>
        /// Can View Calendar
        /// </summary>
        CanViewCalendar = 13,

        /// <summary>
        /// Can View Contacts
        /// </summary>
        CanViewContact = 14,

        /// <summary>
        /// Can View Store
        /// </summary>
        CanViewStore = 15,
        CanViewEstimating = 16,
        CanViewProductionBoard = 17,
        CanViewInvoicing = 18,
        CanViewShipping = 19,
        CanViewPurchaseOrders = 20,
        CanViewSettings = 21

    }
}

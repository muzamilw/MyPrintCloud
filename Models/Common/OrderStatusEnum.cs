
namespace MPC.Models.Common
{
    public enum OrderStatusEnum
    {

        ShoppingCart = 3,


        PendingOrder = 4,


        ConfirmedOrder = 5,


        InProduction = 6,


        Completed_NotShipped = 7,


        CompletedAndShipped_Invoiced = 8,


        CancelledOrder = 9,


        ArchivedOrder = 23,


        PendingCorporateApprovel = 34, //corporate case

        RejectOrder = 35,

        CompletedOrders=36
    }
}

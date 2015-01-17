using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Models.Common
{
    public enum StoreMode : int
    {
        Retail = 1,
        Corp = 3,
        NotSet = 99

    }
    public enum CompanyTypes
    {
        TemporaryCustomer = 53,
        SalesCustomer = 57
    }

    public enum HashAlgos
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }

    public enum Events : int
    {
        Registration = 1,
        OnlineOrder = 2,
        UnOrderCart = 3,
        DirectOrder = 4,
        CorporateOrderForApproval = 5,
        OnlinePaymentConfirmed = 6,
        SendEstimate = 7,
        ForgotPassword = 8,
        SaveDesignes = 9,
        SendFavorites = 10,
        PendingLogoUpload = 11,
        RequestAQuote = 12,
        NewOrderToSalesManager = 13,
        NewRegistrationToSalesManager = 14,
        NewQuoteToSalesManager = 15,
        ProofArtWorkByEmail = 16,
        RejectOrder = 17,
        PinkFreeRegistrationConfirmation = 18,
        PinkReservationConfirmation = 19,
        PinkFreeRegistrationConfirmationSales = 20,
        PinkReservationConfirmationSales = 21,
        OrderArtworkNotificationToBroker = 22,
        SubscriptionConfirmation = 23,
        SendInquiry = 24,
        CorpUserRegistration = 25,
        CorporateRegistrationForApproval = 26,
        CorpUserSuccessfulRegistration = 27,
        DomainChangeNotificationBroker = 28,
        ThresholdLevelReached_Notification_To_Manager = 29,
        Order_Approval_By_Manager = 30,
        BackOrder_Notifiaction_To_Manager = 31,
        ShippedOrder_Notifiaction_To_Customer = 32,
        PO_Notification_To_SalesManager = 33,
        PO_Notification_To_Supplier = 34
    }
    public enum ScheduledStatus
    {
        Draft = 0,
        Scheduled = 1,
        InProgress = 2,
        Paused = 3,
        Compeleted = 4,
        Disabled = 5
    }


    public enum Campaigns : int
    {
        MarketingCampaign = 3
    }

    public enum Roles : int
    {
        Adminstrator = 1,
        Manager = 2,
        User = 3,
        Sales = 39
    }

    public enum CustomerTypes
    {
        Prospects = 0,
        Customers = 1,
        Suppliers = 2,
        Corporate = 3,
        Broker = 4
    }

    public enum ItemTypes : int
    {
        Delivery = 2
    }
    public enum ProductType
    {
        FinishedGoodWithImageRotator = 1,
        TemplateProductWithImage = 2,
        TemplateProductWithBanner = 3,
        FinishedGoodWithBanner = 4,
        InventoryItem = 5
    }

    public enum ItemStatuses
    {
        ShoppingCart = 3,
        NotProgressedToJob = 17
    }

    public enum OrderStatus
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

        //[Model.StringValue("Rejected")]
        //Rejected = 25 //corporate case

    };

    [Serializable]
    public enum UploadFileTypes : int
    {
        Artwork,
        Document,
        Draft,
        None
    };



    public enum CostCentresForWeb : int
    {
        WebOrderCostCentre = 206
    }

    public enum TypeReturnMode : int
    {
        All = 1,
        System = 2,
        UserDefined = 3
    }

    public enum ResourceReturnType : int
    {
        CostPerHour = 1
    }

    public enum StockPriceType : int
    {
        PerUnit = 1,
          PerPack = 2
    }

    public enum CostCentreExecutionMode : int
    {
        PromptMode = 1,
        ExecuteMode = 2
    }

}
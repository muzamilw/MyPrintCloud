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
        InventoryItem = 5,
        RealEstate = 6
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
     public enum VariableProperty : int 
     {
        Side1Inks = 1,
        Side2Inks = 2,
        PrintSheetQty_ProRata = 3,
        PressSpeed_ProRata = 4,
        ColourHeads = 5,
        ImpressionQty_ProRata = 6,
        PressHourlyCharge = 7,
        MinInkDuctqty = 8,
        MakeReadycharge = 9,
        PrintChargeExMakeReady_ProRata = 10,
        PaperGsm = 11,
        SetupSpoilage = 12,
        RunningSpoilage = 13,
        PaperPackPrice = 14,
        AdditionalPlateUsed = 15,
        AdditionalFilmUsed = 16,
        ItemGutterHorizontal = 17,
        ItemGutterVertical = 18,
        PTVRows = 19,
        PTVColoumns = 20,
        PrintViewLayoutLandScape = 21,
        PrintViewLayoutPortrait = 22,
        FilmQty = 23,
        PlateQty = 24,
        GuilotineMakeReadycharge = 25,
        GuilotineChargePerCut = 26,
        GuillotineFirstCut = 27,
        GuillotineSecondCut = 28,
        PrintToView = 29,
        FinishedItemQtyIncSpoilage_ProRata = 30,
        TotalSections = 31,
        PaperWeight_ProRata = 32,
        PrintSheetQtyIncSpoilage_ProRata = 33,
        FinishedItemQty_ProRata = 34,
        NoOfSides = 35,
        PressSizeRatio = 36,
        SectionPaperWeightExSelfQty_ProRata = 37,
        WashupQty = 38,
        MakeReadyQty = 39
     }
     public enum PrintViewOrientation : int
     {
         Landscape = 1,
         Portrait = 0
     }
     public enum SecondryPagesInfo : int
     {
         AboutUs = 2,
         ContactUs = 3,
         SpecialOffer = 35,
         HowToOrder = 36,
         PrivacyPolicy = 5,
         TermsAndConditions = 11
     }
}
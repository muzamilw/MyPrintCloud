using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Models.Common
{
    public enum FieldVariableScopeType : int
    {
        Store = 1,
        Contact = 2,
        Address = 3,
        Territory = 4,
        RealEstate = 5,
        RealEstateImages = 6,
        SystemStore = 7,
        SystemContact = 8,
        SystemAddress = 9,
        SystemTerritory = 10,
    }

    public enum StoreMode : int
    {
        Retail = 4,
        Corp = 3,
        NotSet = 99

    }
    public enum SmartFormDetailFieldType : int
    {
        GroupCaption = 1,
        LineSeperator = 2,
        VariableField = 3,
    }
    public enum CreditCardTypeType
    {
        Visa = 1,
        MasterCard = 2,
        DinersClub = 3,
        Amex = 4
    }
    public enum CompanyTypes
    {
        TemporaryCustomer = 53,
        SalesCustomer = 57
    }
    public enum CostCenterTypes
    {
        SystemCostCentres = 1,
        Delivery = 11,
        WebOrder = 29
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
        PrintProduct = 1,
        MarketingBrief = 2,
        NonPrintProduct = 3

    }


    public enum ProductDisplayOption
    {
        ThumbAndBanner = 1,
        ThumbWithMultipleBanners = 2
        

    }


    public enum PaymentMethods
    {
        PayPal = 1,
        Cash = 99,
        authorizeNET = 2,
        ANZ = 3,
        StGeorge = 5,
        NAB = 6
    }
    public enum ItemStatuses
    {
        ShoppingCart = 3,
        NotProgressedToJob = 17
    }
    public enum PaymentRequestStatus
    {
        Pending = 1,
        Successfull = 2
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

    public enum ContactCompanyUserRoles
    {
        Administrator = 1,
        Manager = 2,
        User = 3
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
    public enum GripSide :int
    {
        LongSide = 1,
        ShortSide = 2
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

    public enum QuestionType : int
    {
        InputQuestion = 1,
        BooleanQuestion = 2,
        MultipleChoiceQuestion = 3,

    }
    public enum ClientStatus : int
    {
        inProgress = 38,
        completed = 37
    }
    public enum StockLogEvents
    {
        Ordered = 2,
        ReachedThresholdLevel = 3,
        BackOrder = 4
    }
    public enum DeliveryCarriers
    {
        Fedex = 1,
        UPS = 2,
        Other = 3
    }

    public enum CostCentrCalculationMethods
    {
        Fixed = 1,
        PerHour = 2,
        QuantityBase = 3,
        FormulaBase = 4
    }
    public enum SubscriberStatus
    {
        Pending = 1,
        Confirmed = 2
    }
    public enum ProductWidget
    {
        FeaturedProducts = 1,
        PopularProducts = 2,
        SpecialProducts = 3
    }

    public enum ClickChargeReturnType
    {
        Cost = 1,
        Price = 2
    }
    public enum BreadCrumbMode : int
    {
        CategoryBrowsing = 1,
        MyAccount = 2
    }
    public enum TemplateMode : int
    {
        UnrestrictedDesignerMode = 1,
        RestrictedDesignerMode = 2,
        SmartFormMode = 3,
        DoNotLoadDesigner = 4
    }

    /// <summary>
    /// Length Unit Enum
    /// </summary>
    public enum LengthUnit
    {
        Mm = 1,
        Cm = 2,
        Inch = 3,
        Meters = 4
    }
    public enum SystemCostCenterTypes
    {
        Ink = 1,
        Paper = 2,
        Film = 3,
        Plate = 4,
        Makeready = 5,
        Press = 6,
        Washup = 7,
        Guillotine = 8,
        UserDefinedCostcentres = 9,
        Stock = 10,
        Outwork = 11,
        ReelMakeready = 12,
        FinishedGood = 13
    }

    public enum MachineCategories
    {
        Guillotin = 4,
        Presses = 1,
        DigitalPresses = 2,
        copier = 3
    }
    public enum PressReRunModes
    {
        NotReRun = 1,
        CalculateValuesToShow = 2,
        ReRunPress = 3
    }
    public enum MethodTypes
    {
        ClickCharge = 1,
        SpeedWeight = 3,
        PerHour = 4,
        ClickChargeZone = 5,
        Guilotine = 6,
        MeterPerHour = 8
    }
    public enum WeightUnits
    {
        lbs = 1,
        GSM = 2,
        KG = 3
    }
    public enum WebSpoilageTypes
    {
        inSheets = 1,
        inMetters = 2
    }
    public enum PrintingTypeEnum
    {
        SheetFed = 1,
        WebFed = 2,
        Flexo = 3,
        Continuous = 4
    }

    public enum OrderSectionFlag : int
    {
        UrgentOrder = 54
    }

    public enum ReportType : int
    {
        Internal = 0,
        JobCard = 1,
        Order = 2,
        Estimate = 3,
        Invoice = 4,
        DeliveryNotes = 5,
        PurchaseOrders = 6,
        Customer = 7,
        CriteriaFields = 8

    }

    public enum ReportCategoryEnum : int
    {
        CRM = 4,
        Stores = 1,
        Suppliers = 2,
        PurchaseOrders = 5,
        Delivery = 6,
        Order = 12,
        Estimate = 3,
        Invoice = 13,
        GRN = 15,
        Inventory = 7

    }


    /// <summary>
    /// Impression Coverage (Used in Order - Item Section)
    /// </summary>
    public enum ImpressionCoverageEnum
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
}
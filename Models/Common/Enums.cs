using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Models.Common
{
    public enum StoreMode : int
    {
        Retail = 1,
        Corp = 2,
        Broker = 3,
        NotSet = 99

    }
    public enum ContactCompanyTypes
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

}
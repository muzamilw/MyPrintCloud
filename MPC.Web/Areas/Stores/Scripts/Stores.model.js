

define("stores/stores.model", ["ko", "underscore", "underscore-ko"], function (ko) {
    var
    // #region ____________ S T O R E   L I S T    V I E W____________________

        // ReSharper disable once InconsistentNaming
        StoreListView = function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedIsCustomer, specifiedStoreImageFileBinary, specifiedDefaultDomain) {
            var
                self,
                companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
                name = ko.observable(specifiedName),
                status = ko.observable(specifiedStatus),
                image = ko.observable(specifiedImage),
                url = ko.observable(specifiedUrl),
                isCustomer = ko.observable(specifiedIsCustomer),
                storeImageFileBinary = ko.observable(specifiedStoreImageFileBinary),
                type = ko.observable(),
                defaultDomain = ko.observable(specifiedDefaultDomain),
                // Errors
                errors = ko.validation.group({

                }),
                // Is Valid 
                isValid = ko.computed(function () {
                    return errors().length === 0 ? true : false;
                }),
                // ReSharper disable InconsistentNaming
                dirtyFlag = new ko.dirtyFlag({
                    // ReSharper restore InconsistentNaming
                    companyId: companyId,
                    name: name,
                    status: status,
                    image: image,
                    url: url,
                    type: type,
                    isCustomer: isCustomer
                }),
                // Has Changes
                hasChanges = ko.computed(function () {
                    return dirtyFlag.isDirty();
                }),
                //Convert To Server
                convertToServerData = function (source) {
                    var result = {};
                    result.CompanyId = source.companyId();
                    result.Name = source.name();
                    result.Status = source.status();
                    result.Image = source.image();
                    result.URL = source.url();
                    result.IsCustomer = source.isCustomer();
                    return result;
                },
                // Reset
                reset = function () {
                    dirtyFlag.reset();
                };
            self = {
                companyId: companyId,
                name: name,
                status: status,
                image: image,
                url: url,
                type: type,
                isCustomer: isCustomer,
                domain: defaultDomain,
                storeImageFileBinary: storeImageFileBinary,
                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset
            };
            return self;
        };
    StoreListView.CreateFromClientModel = function (source) {
        var result = new StoreListView(
            source.companyId,
            source.name,
            source.status,
            source.image,
            source.url,
            source.isCustomer,
            source.defaultDomain
        );
        return result;
    };
    StoreListView.Create = function (source) {
        var store = new StoreListView(
            source.CompanyId,
            source.Name,
            source.Status,
            source.Image,
            source.URL,
            source.IsCustomer,
            source.ImageBytes,
            source.DefaultDomain
        );

        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        if (source.IsCustomer == 4) {//changed from 1
            store.type("Retail");
        }
            //else if (source.IsCustomer == 2) {
            //    store.type("Prospect");
            //}
        else if (source.IsCustomer == 3) {
            store.type("Corporate");
        }

        return store;
    };
    // #endregion _________ S T O R E   L I S T    V I E W____________________

    // #region _____________________ S T O R E ______________________________

    //WebMasterTag WebAnalyticCode
    // ReSharper disable once InconsistentNaming
    var Store = function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedAvatRegNumber,
        specifiedAvatRegReference, specifiedPhoneNo, specifiedIsCustomer, specifiedNotes, specifiedWebMasterTag, specifiedWebAnalyticCode, specifiedWebAccessCode, specifiedTwitterUrl,
        specifiedFacebookUrl, specifiedLinkedinUrl, specifiedFacebookAppId, specifiedFacebookAppKey, specifiedTwitterAppId, specifiedTwitterAppKey,
        specifiedSalesAndOrderManagerId1, specifiedSalesAndOrderManagerId2, specifiedProductionManagerId1, specifiedProductionManagerId2,
        specifiedStockNotificationManagerId1, specifiedStockNotificationManagerId2, specifiedisDisplayBanners, specifiedisStoreModePrivate, specifiedisTextWatermark,
        specifiedWatermarkText, specifiedisBrokerPaymentRequired, specifiedisBrokerCanAcceptPaymentOnline, specifiedcanUserPlaceOrderWithoutApproval,
        specifiedisIncludeVAT, specifiedincludeEmailBrokerArtworkOrderReport, specifiedincludeEmailBrokerArtworkOrderXML, specifiedincludeEmailBrokerArtworkOrderJobCard
        , specifiedIsDeliveryTaxAble, specifiedPickupAddressId,
        specifiedmakeEmailBrokerArtworkOrderProductionReady, specifiedStoreImageFileBinary, specifiedStoreBackgroudImageSource, specifiedIsShowGoogleMap,
        specifiedDefaultSpriteImageSource, specifiedUserDefinedSpriteImageSource, specifiedUserDefinedSpriteFileName, specifiedCustomCSS, specifiedStoreBackgroundImage, specifiedStoreImagePath
    , specifiedIsDidplayInFooter, specifiedCurrentThemeId, specifiedPriceFlagId) {
        var self,
            storeId = ko.observable(undefined),
            companyId = ko.observable(specifiedCompanyId), //.extend({ required: true }),
            name = ko.observable(specifiedName).extend({ required: true }),
            status = ko.observable(specifiedStatus),
            isDidplayInFooter = ko.observable(specifiedIsDidplayInFooter),
            image = ko.observable(specifiedImage),
            url = ko.observable(specifiedUrl),
            accountOpenDate = ko.observable(specifiedAccountOpenDate ? moment(specifiedAccountOpenDate).toDate() : undefined),
            accountManagerId = ko.observable(specifiedAccountManagerId),
            avatRegNumber = ko.observable(specifiedAvatRegNumber),
            avatRegReference = ko.observable(specifiedAvatRegReference),
            phoneNo = ko.observable(specifiedPhoneNo),
            isCustomer = ko.observable(specifiedIsCustomer),
            notes = ko.observable(specifiedNotes),
            webMasterTag = ko.observable(specifiedWebMasterTag),
            webAnalyticCode = ko.observable(specifiedWebAnalyticCode),
            type = ko.observable(),
            storeImagePath = ko.observable(specifiedStoreImagePath),
            currentThemeId = ko.observable(),
            currentThemeName = ko.observable(),
            //webAccessCode = ko.observable(specifiedWebAccessCode).extend({
            //    required: {
            //        onlyIf: function () {
            //            return type() == 3;
            //        }
            //    }
            //}),
            webAccessCode = ko.observable(specifiedWebAccessCode).extend({ required: true }),
            twitterUrl = ko.observable(specifiedTwitterUrl),
            facebookUrl = ko.observable(specifiedFacebookUrl),
            linkedinUrl = ko.observable(specifiedLinkedinUrl),
            facebookAppId = ko.observable(specifiedFacebookAppId),
            facebookAppKey = ko.observable(specifiedFacebookAppKey),
            twitterAppId = ko.observable(specifiedTwitterAppId),
            twitterAppKey = ko.observable(specifiedTwitterAppKey),
            salesAndOrderManagerId1 = ko.observable(specifiedSalesAndOrderManagerId1),
            salesAndOrderManagerId2 = ko.observable(specifiedSalesAndOrderManagerId2),
            productionManagerId1 = ko.observable(specifiedProductionManagerId1),
            productionManagerId2 = ko.observable(specifiedProductionManagerId2),
            stockNotificationManagerId1 = ko.observable(specifiedStockNotificationManagerId1),
            stockNotificationManagerId2 = ko.observable(specifiedStockNotificationManagerId2),
            isStoreModePrivate = ko.observable(specifiedisStoreModePrivate),
            isTextWatermark = ko.observable(specifiedisTextWatermark),
            watermarkText = ko.observable(specifiedWatermarkText),
            isBrokerPaymentRequired = ko.observable(specifiedisBrokerPaymentRequired),
            isBrokerCanAcceptPaymentOnline = ko.observable(specifiedisBrokerCanAcceptPaymentOnline),
            canUserPlaceOrderWithoutApproval = ko.observable(specifiedcanUserPlaceOrderWithoutApproval),
            isIncludeVAT = ko.observable(specifiedisIncludeVAT),
            isCalculateTaxByService = ko.observable(undefined),
            includeEmailBrokerArtworkOrderReport = ko.observable(specifiedincludeEmailBrokerArtworkOrderReport),
            includeEmailBrokerArtworkOrderXML = ko.observable(specifiedincludeEmailBrokerArtworkOrderXML),
            includeEmailBrokerArtworkOrderJobCard = ko.observable(specifiedincludeEmailBrokerArtworkOrderJobCard),
            makeEmailBrokerArtworkOrderProductionReady = ko.observable(specifiedmakeEmailBrokerArtworkOrderProductionReady),
            isAllowRegistrationFromWeb = ko.observable(undefined),
            isDisplayDiscountVoucherCode = ko.observable(undefined),
            canUserEditProfile = ko.observable(undefined),
            isWhiteLabel = ko.observable(undefined),
            showPrices = ko.observable(undefined),
            // isDeliveryTaxAble = ko.observable(specifiedIsDeliveryTaxAble),
            // is Delivery TaxAble
            isDeliveryTaxAble = ko.observable(!specifiedIsDeliveryTaxAble ? 2 : 1),
            // is Delivery TaxAble ui
            isDeliveryTaxAbleUi = ko.computed({
                read: function () {
                    return '' + isDeliveryTaxAble();
                },
                write: function (value) {
                    var deliveryTaxAble = parseInt(value);
                    if (deliveryTaxAble === isDeliveryTaxAble()) {
                        return;
                    }

                    isDeliveryTaxAble(deliveryTaxAble);
                }
            }),
            pickupAddressId = ko.observable(specifiedPickupAddressId),
            //store Image Logo
            storeImageFileBinary = ko.observable(),
            storeImageName = ko.observable(),

            storeWorkflowImageBinary = ko.observable(undefined),
            storeWorkflowImage = ko.observable(),
            mapImageUrl = ko.observable(),
            mapImageUrlBinary = ko.observable(),
            //company type
            companyType = ko.observable(),
            //type = ko.observable(),
            isDisplayBanners = ko.observable(specifiedisDisplayBanners),
            raveReviews = ko.observableArray([]),
            companyTerritories = ko.observableArray([]),
            addresses = ko.observableArray([]),
            users = ko.observableArray([]),
            //secondary Pages List
            secondaryPages = ko.observableArray([]),
            //system Pages List
            systemPages = ko.observableArray([]),
            // ReSharper disable InconsistentNaming
            companyCMYKColors = ko.observableArray([]),
            //Color Palette
            colorPalette = ko.observable(new ColorPalette()),
            //Company Banner Set List
            companyBannerSets = ko.observableArray([]),
            //Payment Gateways
            paymentGateway = ko.observableArray([]),
            //Payment Methods
            paymentMethod = ko.observableArray([]),
            // ReSharper restore InconsistentNaming
            //Product Categories
            productCategories = ko.observableArray([]),
            //Company Domains
            companyDomains = ko.observableArray([]),
            //Products
            products = ko.observableArray([]),
            //Media Libraries
            mediaLibraries = ko.observableArray([]),
            //Company Cost Center
            companyCostCenters = ko.observableArray([]),
            //Scope Variables
            scopeVariables = ko.observableArray([]),
            //store Backgroud Image Image Source
            storeBackgroudImageImageSource = ko.observable(),
            //store Backgroud Image Path
            storeBackgroudImagePath = ko.observable(specifiedStoreBackgroundImage),
            storeBackgroudImagePathWithCacheRemoveTechnique = ko.computed(function () {
                if (storeBackgroudImagePath() !== null && storeBackgroudImagePath() !== undefined) {
                    return storeBackgroudImagePath() + "?" + Date();
                }

            }),
        //store Backgroud Image File Name
        storeBackgroudImageFileName = ko.observable(),
        defaultSpriteImageSource = ko.observable(specifiedDefaultSpriteImageSource),
        defaultSpriteImageFileName = ko.observable(),
        userDefinedSpriteImageSource = ko.observable(specifiedUserDefinedSpriteImageSource),
        userDefinedSpriteImageFileName = ko.observable(specifiedUserDefinedSpriteFileName),
        storeLayoutChange = ko.observable(),
        //Is Show Google Map
        isShowGoogleMap = ko.observable(specifiedIsShowGoogleMap != undefined ? specifiedIsShowGoogleMap.toString() : "1"),
        customCSS = ko.observable(specifiedCustomCSS),
        //Company Domain Copy
        defaultCompanyDomainCopy = ko.observable(),
        taxLabel = ko.observable(undefined),
        taxRate = ko.observable(undefined).extend({ number: true }),
        activeBannerSetId = ko.observable().extend({ required: true }),
        priceFlagId = ko.observable(specifiedPriceFlagId),
        // Errors
        errors = ko.validation.group({
            companyId: companyId,
            name: name,
            webAccessCode: webAccessCode,
            url: url,
            activeBannerSetId: activeBannerSetId,
            taxRate: taxRate
        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),


        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            //#region Dirty Flag 
            // ReSharper restore InconsistentNaming
            companyId: companyId,
            name: name,
            storeWorkflowImage: storeWorkflowImage,
            storeWorkflowImageBinary: storeWorkflowImageBinary,
            mapImageUrl: mapImageUrl,
            mapImageUrlBinary: mapImageUrlBinary,
            activeBannerSetId: activeBannerSetId,
            status: status,
            image: image,
            url: url,
            accountOpenDate: accountOpenDate,
            accountManagerId: accountManagerId,
            avatRegNumber: avatRegNumber,
            avatRegReference: avatRegReference,
            phoneNo: phoneNo,
            isCalculateTaxByService: isCalculateTaxByService,
            isCustomer: isCustomer,
            notes: notes,
            webAccessCode: webAccessCode,
            twitterUrl: twitterUrl,
            mediaLibraries: mediaLibraries,
            facebookUrl: facebookUrl,
            linkedinUrl: linkedinUrl,
            facebookAppId: facebookAppId,
            facebookAppKey: facebookAppKey,
            twitterAppId: twitterAppId,
            twitterAppKey: twitterAppKey,
            companyType: companyType,
            type: type,
            raveReviews: raveReviews,
            secondaryPages: secondaryPages,
            systemPages: systemPages,
            companyCMYKColors: companyCMYKColors,
            webMasterTag: webMasterTag,
            webAnalyticCode: webAnalyticCode,
            salesAndOrderManagerId1: salesAndOrderManagerId1,
            salesAndOrderManagerId2: salesAndOrderManagerId2,
            productionManagerId1: productionManagerId1,
            productionManagerId2: productionManagerId2,
            stockNotificationManagerId1: stockNotificationManagerId1,
            stockNotificationManagerId2: stockNotificationManagerId2,
            isStoreModePrivate: isStoreModePrivate,
            isTextWatermark: isTextWatermark,
            watermarkText: watermarkText,
            isBrokerPaymentRequired: isBrokerPaymentRequired,
            isBrokerCanAcceptPaymentOnline: isBrokerCanAcceptPaymentOnline,
            canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
            isIncludeVAT: isIncludeVAT,
            includeEmailBrokerArtworkOrderReport: includeEmailBrokerArtworkOrderReport,
            includeEmailBrokerArtworkOrderXML: includeEmailBrokerArtworkOrderXML,
            includeEmailBrokerArtworkOrderJobCard: includeEmailBrokerArtworkOrderJobCard,
            makeEmailBrokerArtworkOrderProductionReady: makeEmailBrokerArtworkOrderProductionReady,
            storeImageFileBinary: storeImageFileBinary,
            storeImageName: storeImageName,
            isDisplayBanners: isDisplayBanners,
            storeBackgroudImageImageSource: storeBackgroudImageImageSource,
            storeBackgroudImageFileName: storeBackgroudImageFileName,
            isShowGoogleMap: isShowGoogleMap,
            customCSS: customCSS,
            companyDomains: companyDomains,
            isDeliveryTaxAble: isDeliveryTaxAble,
            pickupAddressId: pickupAddressId,
            taxLabel: taxLabel,
            taxRate: taxRate,
            scopeVariables: scopeVariables,
            paymentGateway: paymentGateway,
            isDisplayDiscountVoucherCode: isDisplayDiscountVoucherCode,
            showPrices: showPrices,
            isWhiteLabel: isWhiteLabel,
            isAllowRegistrationFromWeb: isAllowRegistrationFromWeb,
            canUserEditProfile: canUserEditProfile,
            priceFlagId: priceFlagId
            //#endregion
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.isDisplaySecondaryPages = source.isDidplayInFooter();
            result.CompanyId = source.companyId();
            result.Name = source.name();
            result.Status = source.status();
            //result.ImageBytes = source.image();
            result.URL = source.url();
            result.AccountOpenDate = source.accountOpenDate() ? moment(source.accountOpenDate()).format(ist.utcFormat) + 'Z' : undefined;
            result.AccountManagerId = source.accountManagerId();
            result.AvatRegNumber = source.avatRegNumber();
            result.PvatRegReference = source.avatRegReference();
            result.PhoneNo = source.phoneNo();
            result.ActiveBannerSetId = source.activeBannerSetId();
            //result.IsCustomer = source.isCustomer();
            result.IsCustomer = source.type();
            result.Notes = source.notes();
            result.WebMasterTag = source.webMasterTag();
            result.WebAnalyticCode = source.webAnalyticCode();
            result.WebAccessCode = source.webAccessCode();
            result.TwitterURL = source.twitterUrl();
            result.FacebookURL = source.facebookUrl();
            result.LinkedinURL = source.linkedinUrl();
            result.facebookAppId = source.facebookAppId();
            result.facebookAppKey = source.facebookAppKey();
            result.twitterAppId = source.twitterAppId();
            result.twitterAppKey = source.twitterAppKey();
            result.StoreImagePath = source.storeImagePath();
            result.SalesAndOrderManagerId1 = source.salesAndOrderManagerId1();
            result.SalesAndOrderManagerId2 = source.salesAndOrderManagerId2();
            result.ProductionManagerId1 = source.productionManagerId1();
            result.ProductionManagerId2 = source.productionManagerId2();
            result.StockNotificationManagerId1 = source.stockNotificationManagerId1();
            result.StockNotificationManagerId2 = source.stockNotificationManagerId2();
            result.isStoreModePrivate = source.isStoreModePrivate();
            result.isTextWatermark = source.isTextWatermark();
            result.isShowGoogleMap = source.isShowGoogleMap();
            result.WatermarkText = source.watermarkText();
            result.isPaymentRequired = source.isBrokerPaymentRequired();
            result.isCanAcceptPaymentOnline = source.isBrokerCanAcceptPaymentOnline();
            result.canUserPlaceOrderWithoutApproval = source.canUserPlaceOrderWithoutApproval();
            result.isIncludeVAT = source.isIncludeVAT();
            result.StoreBackgroundImage = source.storeBackgroudImagePath();
            result.includeEmailArtworkOrderReport = source.includeEmailBrokerArtworkOrderReport();
            result.includeEmailArtworkOrderXML = source.includeEmailBrokerArtworkOrderXML();
            result.includeEmailArtworkOrderJobCard = source.includeEmailBrokerArtworkOrderJobCard();
            result.makeEmailArtworkOrderProductionReady = source.makeEmailBrokerArtworkOrderProductionReady();
            result.isDisplayBanners = source.isDisplayBanners();
            result.IsDeliveryTaxAble = source.isDeliveryTaxAble() === 2 ? false : true;
            result.PickupAddressId = source.pickupAddressId();
            result.CompanyType = source.companyType() != undefined ? CompanyType().convertToServerData(source.companyType()) : null;
            result.CustomCSS = source.customCSS();
            result.StoreWorkflowImage = source.storeWorkflowImage();
            result.StoreWorkflowImageBytes = source.storeWorkflowImageBinary();
            result.MapImageUrl = source.mapImageUrlBinary();
            result.isCalculateTaxByService = source.isCalculateTaxByService();
            result.TaxLabel = source.taxLabel();
            result.TaxRate = source.taxRate();
            result.isAllowRegistrationFromWeb = source.isAllowRegistrationFromWeb();
            result.IsDisplayDiscountVoucherCode = source.isDisplayDiscountVoucherCode();
            result.CanUserEditProfile = source.canUserEditProfile();
            result.isWhiteLabel = source.isWhiteLabel();
            result.ShowPrices = source.showPrices();
            result.PriceFlagId = source.priceFlagId();
            result.RaveReviews = [];
            result.PaymentGateways = [];
            result.CompanyContacts = [];
            _.each(source.raveReviews(), function (item) {
                result.RaveReviews.push(item.convertToServerData());
            });
            result.CompanyTerritories = [];
            _.each(source.companyTerritories(), function (item) {
                result.CompanyTerritories.push(item.convertToServerData());
            });
            result.TemplateColorStyles = [];
            _.each(source.companyCMYKColors(), function (item) {
                result.TemplateColorStyles.push(item.convertToServerData());
            });
            result.CompanyDomains = [];
            _.each(source.companyDomains(), function (item) {
                result.CompanyDomains.push(item.convertToServerData());
            });
            result.CompanyCostCenters = [];
            _.each(source.companyCostCenters(), function (item) {
                result.CompanyCostCenters.push(item.convertToServerData());
            });
            //_.each(source.users(), function (item) {
            //    result.CompanyContacts.push(item.convertToServerData());
            //});
            //#region Arrays
            result.NewAddedCompanyTerritories = [];
            result.EdittedCompanyTerritories = [];
            result.DeletedCompanyTerritories = [];
            result.NewAddedCompanyContacts = [];
            result.EdittedCompanyContacts = [];
            result.DeletedCompanyContacts = [];
            result.NewAddedAddresses = [];
            result.CompanyBannerSets = [];
            result.EdittedAddresses = [];
            result.DeletedAddresses = [];
            result.NewAddedCmsPages = [];
            result.EditCmsPages = [];
            result.DeletedCmsPages = [];
            result.PageCategories = [];
            result.CmsPageWithWidgetList = [];
            result.EdittedProductCategories = [];
            result.DeletedProductCategories = [];
            result.NewProductCategories = [];
            result.NewAddedProducts = [];
            result.EdittedProducts = [];
            result.Deletedproducts = [];
            result.Campaigns = [];
            result.CompanyCostCentres = [];

            _.each(source.paymentGateway(), function (item) {
                result.PaymentGateways.push(item.convertToServerData());
            });

            result.ColorPalletes = [];
            result.StoreBackgroundFile = source.storeBackgroudImageImageSource() !== undefined ? source.storeBackgroudImageImageSource() : null;
            result.StoreBackgroudImageFileName = source.storeBackgroudImageFileName();
            //#endregion
            result.ImageName = source.storeImageName() === undefined ? null : source.storeImageName();
            result.ImageBytes = source.storeImageFileBinary() === undefined ? null : source.storeImageFileBinary();
            result.DefaultSpriteSource = source.defaultSpriteImageSource() === undefined ? null : source.defaultSpriteImageSource();
            result.UserDefinedSpriteSource = source.userDefinedSpriteImageSource() === undefined ? null : source.userDefinedSpriteImageSource();
            result.UserDefinedSpriteFileName = source.userDefinedSpriteImageFileName() === undefined ? null : source.userDefinedSpriteImageFileName();
            result.CurrentThemeId = source.currentThemeId() === undefined ? null : source.currentThemeId();
            result.CmsOffers = [];
            result.MediaLibraries = [];
            result.FieldVariables = [];
            result.SmartForms = [];
            result.ScopeVariables = [];
            result.NewAddedCampaigns = [];
            result.EdittedCampaigns = [];
            result.DeletedCampaigns = [];
            return result;
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            //#region SELF
            isDidplayInFooter: isDidplayInFooter,
            storeBackgroudImagePathWithCacheRemoveTechnique: storeBackgroudImagePathWithCacheRemoveTechnique,
            companyId: companyId,
            name: name,
            storeId: storeId,
            status: status,
            image: image,
            url: url,
            accountOpenDate: accountOpenDate,
            accountManagerId: accountManagerId,
            avatRegNumber: avatRegNumber,
            currentThemeId: currentThemeId,
            avatRegReference: avatRegReference,
            isCalculateTaxByService: isCalculateTaxByService,
            phoneNo: phoneNo,
            isCustomer: isCustomer,
            activeBannerSetId: activeBannerSetId,
            notes: notes,
            webMasterTag: webMasterTag,
            webAnalyticCode: webAnalyticCode,
            webAccessCode: webAccessCode,
            twitterUrl: twitterUrl,
            facebookUrl: facebookUrl,
            linkedinUrl: linkedinUrl,
            facebookAppId: facebookAppId,
            facebookAppKey: facebookAppKey,
            twitterAppId: twitterAppId,
            salesAndOrderManagerId1: salesAndOrderManagerId1,
            salesAndOrderManagerId2: salesAndOrderManagerId2,
            productionManagerId1: productionManagerId1,
            productionManagerId2: productionManagerId2,
            stockNotificationManagerId1: stockNotificationManagerId1,
            stockNotificationManagerId2: stockNotificationManagerId2,
            twitterAppKey: twitterAppKey,
            companyType: companyType,
            storeImagePath: storeImagePath,
            isStoreModePrivate: isStoreModePrivate,
            isTextWatermark: isTextWatermark,
            watermarkText: watermarkText,
            isBrokerPaymentRequired: isBrokerPaymentRequired,
            isBrokerCanAcceptPaymentOnline: isBrokerCanAcceptPaymentOnline,
            canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
            isIncludeVAT: isIncludeVAT,
            includeEmailBrokerArtworkOrderReport: includeEmailBrokerArtworkOrderReport,
            includeEmailBrokerArtworkOrderXML: includeEmailBrokerArtworkOrderXML,
            includeEmailBrokerArtworkOrderJobCard: includeEmailBrokerArtworkOrderJobCard,
            makeEmailBrokerArtworkOrderProductionReady: makeEmailBrokerArtworkOrderProductionReady,
            isDisplayBanners: isDisplayBanners,
            storeImageFileBinary: storeImageFileBinary,
            storeImageName: storeImageName,
            storeWorkflowImage: storeWorkflowImage,
            currentThemeName: currentThemeName,
            type: type,
            raveReviews: raveReviews,
            companyTerritories: companyTerritories,
            addresses: addresses,
            users: users,
            companyCMYKColors: companyCMYKColors,
            colorPalette: colorPalette,
            companyBannerSets: companyBannerSets,
            secondaryPages: secondaryPages,
            systemPages: systemPages,
            paymentGateway: paymentGateway,
            paymentMethod: paymentMethod,
            productCategories: productCategories,
            products: products,
            isDeliveryTaxAble: isDeliveryTaxAble,
            storeBackgroudImageImageSource: storeBackgroudImageImageSource,
            storeBackgroudImageFileName: storeBackgroudImageFileName,
            storeBackgroudImagePath: storeBackgroudImagePath,
            isShowGoogleMap: isShowGoogleMap,
            defaultSpriteImageSource: defaultSpriteImageSource,
            defaultSpriteImageFileName: defaultSpriteImageFileName,
            userDefinedSpriteImageSource: userDefinedSpriteImageSource,
            userDefinedSpriteImageFileName: userDefinedSpriteImageFileName,
            isDeliveryTaxAbleUi: isDeliveryTaxAbleUi,
            pickupAddressId: pickupAddressId,
            customCSS: customCSS,
            companyDomains: companyDomains,
            mediaLibraries: mediaLibraries,
            companyCostCenters: companyCostCenters,
            storeLayoutChange: storeLayoutChange,
            defaultCompanyDomainCopy: defaultCompanyDomainCopy,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset,
            storeWorkflowImageBinary: storeWorkflowImageBinary,
            mapImageUrl: mapImageUrl,
            mapImageUrlBinary: mapImageUrlBinary,
            taxLabel: taxLabel,
            taxRate: taxRate,
            scopeVariables: scopeVariables,
            isAllowRegistrationFromWeb: isAllowRegistrationFromWeb,
            isDisplayDiscountVoucherCode: isDisplayDiscountVoucherCode,
            canUserEditProfile: canUserEditProfile,
            isWhiteLabel: isWhiteLabel,
            showPrices: showPrices,
            priceFlagId: priceFlagId

            //#endregion
        };
        return self;
    };
    Store.CreateFromClientModel = function (source) {
        var result = new Store(
            source.companyId,
            source.name,
            source.status,
            source.image,
            source.url,
            source.accountOpenDate,
            source.accountManagerId,
            source.avatRegNumber,
            source.avatRegReference,
            source.phoneNo,
            source.isCustomer,
            source.notes,
            source.webMasterTag,
            source.webAnalyticCode,
            source.webAccessCode,
            source.twitterUrl,
            source.facebookUrl,
            source.linkedinUrl,
            source.facebookAppId,
            source.facebookAppKey,
            source.twitterAppId,
            source.twitterAppKey,
            source.salesAndOrderManagerId1,
            source.salesAndOrderManagerId2,
            source.productionManagerId1,
            source.productionManagerId2,
            source.stockNotificationManagerId1,
            source.stockNotificationManagerId2,
            source.isStoreModePrivate,
            source.isTextWatermark,
            source.watermarkText,
            source.isBrokerPaymentRequired,
            source.isBrokerCanAcceptPaymentOnline,
            source.canUserPlaceOrderWithoutApproval,
            source.isIncludeVAT,
            source.includeEmailBrokerArtworkOrderReport,
            source.includeEmailBrokerArtworkOrderXML,
            source.includeEmailBrokerArtworkOrderJobCard,
            source.isDeliveryTaxAble,
            source.pickupAddressId,
            source.makeEmailBrokerArtworkOrderProductionReady
        );
        //result.companyType(CompanyType.CreateFromClientModel(source.companyType));
        _.each(source.raveReviews, function (item) {
            result.raveReviews.push(RaveReview.CreateFromClientModel(item));
        });
        _.each(source.companyTerritories, function (item) {
            result.companyTerritories.push(CompanyTerritory.CreateFromClientModel(item));
        });
        _.each(source.addresses, function (item) {
            result.addresses.push(Address.CreateFromClientModel(item));
        });
        _.each(source.companyCMYKColors, function (item) {
            result.companyCMYKColors.push(CompanyCMYKColor.CreateFromClientModel(item));
        });
        _.each(source.users, function (item) {
            result.users.push(CompanyContact.CreateFromClientModel(item));
        });
        _.each(source.paymentMethod, function (item) {
            result.paymentMethod.push(PaymentMethod.CreateFromClientModel(item));
        });
        _.each(source.paymentGateway, function (item) {
            result.paymentGateway.push(PaymentGateway.CreateFromClientModel(item));
        });
        _.each(source.productCategories, function (item) {
            result.productCategories.push(ProductCategoryListView.CreateFromClientModel(item));
        });
        _.each(source.companyCostCenters, function (item) {
            result.companyCostCenters.push(CostCenter.CreateFromClientModel(item));
        });
        result.defaultCompanyDomainCopy = source.defaultCompanyDomainCopy();
        return result;
    };
    Store.Create = function (source) {
        //#region Store
        var store = new Store(
            source.CompanyId,
            source.Name,
            source.Status,
            source.Image,
            source.URL,
            source.AccountOpenDate,
            source.AccountManagerId,
            source.AvatRegNumber,
            source.AvatRegReference,
            source.PhoneNo,
            source.IsCustomer,
            source.Notes,
            source.WebMasterTag,
            source.WebAnalyticCode,
            source.WebAccessCode,
            source.TwitterURL,
            source.FacebookURL,
            source.LinkedinURL,
            source.facebookAppId,
            source.facebookAppKey,
            source.twitterAppId,
            source.twitterAppKey,
            source.SalesAndOrderManagerId1,
            source.SalesAndOrderManagerId2,
            source.ProductionManagerId1,
            source.ProductionManagerId2,
            source.StockNotificationManagerId1,
            source.StockNotificationManagerId2,
            source.isDisplayBanners,
            source.isStoreModePrivate,
            source.isTextWatermark,
            source.WatermarkText,
            source.isPaymentRequired,
            source.isCanAcceptPaymentOnline,
            source.canUserPlaceOrderWithoutApproval,
            source.isIncludeVAT,
            source.includeEmailArtworkOrderReport,
            source.includeEmailArtworkOrderXML,
            source.includeEmailArtworkOrderJobCard,
            source.IsDeliveryTaxAble,
            source.PickupAddressId,
            source.makeEmailArtworkOrderProductionReady,
            source.ImageSource,
            source.StoreBackgroudImageSource,
            source.isShowGoogleMap,
            source.DefaultSpriteImageSource,
            source.UserDefinedSpriteImageSource,
            source.UserDefinedSpriteFileName,
            source.CustomCSS,
            source.StoreBackgroundImage,
            source.StoreImagePath

        );
        store.isDidplayInFooter(source.isDisplaySecondaryPages != null ? source.isDisplaySecondaryPages : false);
        store.storeId(source.StoreId);
        // store.activeBannerSetId(source.ActiveBannerSetId);
        store.companyType(CompanyType.Create(source.CompanyType));
        // store.storeWorkflowImage(source.StoreWorkflowImage);
        store.storeWorkflowImageBinary(source.WatermarkText);
        store.mapImageUrl(source.MapImageUrl);
        store.isCalculateTaxByService(source.isCalculateTaxByService == true ? 'true' : 'false');
        store.taxLabel(source.TaxLabel);
        store.taxRate(source.TaxRate);
        store.isAllowRegistrationFromWeb(source.isAllowRegistrationFromWeb);
        store.isDisplayDiscountVoucherCode(source.IsDisplayDiscountVoucherCode);
        store.canUserEditProfile(source.CanUserEditProfile);
        store.isWhiteLabel(source.isWhiteLabel);
        store.showPrices(source.ShowPrices);
        store.priceFlagId(source.PriceFlagId);
        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        //else if (source.IsCustomer == 2) {
        //    store.type("Prospect");
        //}
        //if (source.IsCustomer == 1) {
        //    store.type("1");
        //}
        if (source.IsCustomer == 4) {
            store.type("4");
        }

        else if (source.IsCustomer == 3) {
            store.type("3");
        }
        //isFinishedGoods = ko.observable(specifiedIsFinishedGoods !== undefined && specifiedIsFinishedGoods != null ?(specifiedIsFinishedGoods === 0 ? 0 : specifiedIsFinishedGoods) : undefined),
        if (source.RaveReviews) {
            _.each(source.RaveReviews, function (item) {
                store.raveReviews.push(RaveReview.Create(item));
            });
        }
        if (source.CompanyTerritories) {
            _.each(source.CompanyTerritories, function (item) {
                store.companyTerritories.push(CompanyTerritory.Create(item));
            });
        }
        if (source.Addresses) {
            _.each(source.Addresses, function (item) {
                store.addresses.push(Address.Create(item));
            });
        }
        if (source.TemplateColorStyles) {
            _.each(source.TemplateColorStyles, function (item) {
                store.companyCMYKColors.push(CompanyCMYKColor.Create(item));
            });
        }
        if (source.CompanyDomains) {
            var temp = [];
            _.each(source.CompanyDomains, function (item) {
                temp.push(CompanyDomain.Create(item));
                //If first item is pushing then it is the mandatory one
                if (temp.length == 1) {
                    temp[0].isMandatoryDomain(true);
                    store.defaultCompanyDomainCopy(CompanyDomain.Create(item));
                }
            });
            store.companyDomains(temp.reverse());
        }
        if (source.CompanyContacts) {
            _.each(source.CompanyContacts, function (item) {
                store.users.push(CompanyContact.Create(item));
            });
        }
        if (source.PaymentGateways) {
            _.each(source.PaymentGateways, function (item) {
                store.paymentGateway.push(PaymentGateway.Create(item));
            });
        }
        if (source.PaymentMethods) {
            _.each(source.PaymentMethods, function (item) {
                store.paymentMethod.push(PaymentMethod.Create(item));
            });
        }
        if (source.ProductCategoriesListView) {
            _.each(source.ProductCategoriesListView, function (item) {
                store.productCategories.push(ProductCategory.Create(item));
            });
        }
        if (source.CompanyCostCentres) {
            _.each(source.CompanyCostCentres, function (item) {
                store.companyCostCenters.push(CostCenter.Create(item));
            });
        }

        //#endregion
        return store;
    };
    // #endregion _____________________ S T O R E ______________________________

    // #region ______________  C O M P A N Y    T Y P E   _________________
    // ReSharper disable once InconsistentNaming
    var CompanyType = function (specifiedCompanyTypeId, specifiedIsFixed, specifiedTypeName) {
        var
            self,
            typeId = ko.observable(specifiedCompanyTypeId),
            isFixed = ko.observable(specifiedIsFixed),
            typeName = ko.observable(specifiedTypeName),
            // Errors
            errors = ko.validation.group({
                typeName: typeName
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                typeId: typeId,
                isFixed: isFixed,
                typeName: typeName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.TypeId = source.typeId();
                result.IsFixed = source.isFixed();
                result.TypeName = source.typeName();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            typeId: typeId,
            isFixed: isFixed,
            typeName: typeName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyType.CreateFromClientModel = function (source) {
        return new CompanyType(
            source.typeId,
            source.isFixed,
            source.typeName
        );
    };
    CompanyType.Create = function (source) {
        var companyType = new CompanyType(
            source.TypeId,
            source.IsFixed,
            source.TypeName
        );
        return companyType;
    };
    // #endregion ______________  C O M P A N Y    T Y P E   _________________

    // #region ______________  S Y S T E M     U S E R   _________________
    // ReSharper disable once InconsistentNaming
    var SystemUser = function (specifiedSystemUserId, specifiedUserName, specifiedfullName) {
        var self,
            systemUserId = ko.observable(specifiedSystemUserId),
            userName = ko.observable(specifiedUserName),
            fullName = ko.observable(specifiedfullName),
            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                systemUserId: systemUserId,
                userName: userName,
                fullName: fullName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.SystemUserId = source.systemUserId();
                result.UserName = source.userName();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            systemUserId: systemUserId,
            userName: userName,
            fullName: fullName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    SystemUser.CreateFromClientModel = function (source) {
        return new SystemUser(
            source.systemUserId,
            source.userName
        );
    };
    SystemUser.Create = function (source) {
        var systemUser = new SystemUser(
            source.SystemUserId,
            source.UserName,
            source.FullName
        );
        return systemUser;
    };
    // #endregion______________  S Y S T E M     U S E R   _________________

    // #region ______________  R A V E    R E V I E W   _________________

    // ______________  R A V E    R E V I E W   _________________
    // ReSharper disable once InconsistentNaming
    var RaveReview = function (specifiedReviewId, specifiedReviewBy, specifiedReview, specifiedReviewDate, specifiedisDisplay, specifiedSortOrder, specifiedOrganisationId, specifiedCompanyId) {
        var self,
            reviewId = ko.observable(specifiedReviewId),
            reviewBy = ko.observable(specifiedReviewBy).extend({ required: true }),
            review = ko.observable(specifiedReview).extend({ required: true }),
            reviewDate = ko.observable(specifiedReviewDate),
            isDisplay = ko.observable(specifiedisDisplay),
            sortOrder = ko.observable(specifiedSortOrder),
            organisationId = ko.observable(specifiedOrganisationId),
            companyId = ko.observable(specifiedCompanyId),
            // Errors
            errors = ko.validation.group({
                reviewBy: reviewBy,
                review: review
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                reviewId: reviewId,
                reviewBy: reviewBy,
                review: review,
                reviewDate: reviewDate,
                isDisplay: isDisplay,
                sortOrder: sortOrder,
                organisationId: organisationId,
                companyId: companyId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    ReviewId: reviewId(),
                    ReviewBy: reviewBy(),
                    Review: review(),
                    ReviewDate: reviewDate(),
                    IsDisplay: isDisplay(),
                    SortOrder: sortOrder(),
                    OrganisationId: organisationId(),
                    CompanyId: companyId()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            reviewId: reviewId,
            reviewBy: reviewBy,
            review: review,
            reviewDate: reviewDate,
            isDisplay: isDisplay,
            sortOrder: sortOrder,
            organisationId: organisationId,
            companyId: companyId,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    RaveReview.CreateFromClientModel = function (source) {
        return new RaveReview(
            source.reviewId,
            source.reviewBy,
            source.review,
            source.reviewDate,
            source.isDisplay,
            source.sortOrder,
            source.organisationId,
            source.companyId
        );
    };
    RaveReview.Create = function (source) {
        var raveReview = new RaveReview(
            //       
            source.ReviewId,
            source.ReviewBy,
            source.Review,
            source.ReviewDate,
            source.isDisplay,
            source.SortOrder,
            source.OrganisationId,
            source.CompanyId
        );
        return raveReview;
    };
    // #endregion ______________  R A V E    R E V I E W   _________________

    // #region ______________  C O M P A N Y    C M Y K    C O L O R   _________________

    // ReSharper disable once InconsistentNaming    
    var CompanyCMYKColor = function (specifiedColorId, specifiedCompanyId, specifiedColorName, specifiedColorC, specifiedColorM, specifiedColorY, specifiedColorK,
    specifiedIsSpotColor, specifiedSpotColor, specifiedIsActive) {
        var self,
            colorId = ko.observable(specifiedColorId),
            companyId = ko.observable(specifiedCompanyId),
            colorName = ko.observable(specifiedColorName).extend({ required: true }),
            colorC = ko.observable(specifiedColorC).extend({ required: true, number: true, min: 0, max: 200 }),
            colorM = ko.observable(specifiedColorM).extend({ required: true, number: true, min: 0, max: 200 }),
            colorY = ko.observable(specifiedColorY).extend({ required: true, number: true, min: 0, max: 200 }),
            colorK = ko.observable(specifiedColorK).extend({ required: true, number: true, min: 0, max: 200 }),
            isActive = ko.observable(specifiedIsActive || true),
            isSpotColor = ko.observable(specifiedIsSpotColor || true),
            spotColor = ko.observable(specifiedSpotColor || undefined),
            // Errors
            errors = ko.validation.group({
                colorName: colorName,
                colorC: colorC,
                colorM: colorM,
                colorY: colorY,
                colorK: colorK
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                colorId: colorId,
                companyId: companyId,
                colorName: colorName,
                colorC: colorC,
                colorM: colorM,
                colorY: colorY,
                colorK: colorK,
                isActive: isActive
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    PelleteId: colorId(),
                    CustomerId: companyId(),
                    Name: colorName(),
                    ColorC: colorC(),
                    ColorM: colorM(),
                    ColorY: colorY(),
                    ColorK: colorK(),
                    SpotColor: !spotColor() ? colorName() : spotColor(),
                    IsSpotColor: isSpotColor(),
                    IsColorActive: isActive()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            colorId: colorId,
            companyId: companyId,
            colorName: colorName,
            colorC: colorC,
            colorM: colorM,
            colorY: colorY,
            colorK: colorK,
            isSpotColor: isSpotColor,
            spotColor: spotColor,
            isActive: isActive,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyCMYKColor.CreateFromClientModel = function (source) {
        return new CompanyCMYKColor(
            source.colorId,
            source.companyId,
            source.colorName,
            source.colorC,
            source.colorM,
            source.colorY,
            source.colorK,
            source.isSpotColor,
            source.spotColor,
            source.isActive
        );
    };
    CompanyCMYKColor.Create = function (source) {
        var companyCMYKColor = new CompanyCMYKColor(
            source.PelleteId,
            source.CustomerId,
            source.Name,
            source.ColorC,
            source.ColorM,
            source.ColorY,
            source.ColorK,
            source.IsSpotColor,
            source.SpotColor,
            source.IsColorActive
        );
        return companyCMYKColor;
    };
    // #endregion ______________  C O M P A N Y    C M Y K    C O L O R   _________________

    // #region ______________  C O M P A N Y    T E R R I T O R Y    _________________

    // ReSharper disable once InconsistentNaming
    var CompanyTerritory = function (specifiedTerritoryId, specifiedTerritoryName, specifiedCompanyId, specifiedTerritoryCode, specifiedisDefault) {

        var self,
            territoryId = ko.observable(specifiedTerritoryId),
            territoryName = ko.observable(specifiedTerritoryName).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId),
            territoryCode = ko.observable(specifiedTerritoryCode).extend({ required: true }),
            isDefault = ko.observable(specifiedisDefault),
            scopeVariables = ko.observableArray([]),
             isSelected = ko.observable(),
            // Errors
            errors = ko.validation.group({
                territoryName: territoryName,
                territoryCode: territoryCode
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                territoryId: territoryId,
                territoryName: territoryName,
                companyId: companyId,
                territoryCode: territoryCode,
                isDefault: isDefault
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    TerritoryId: territoryId(),
                    TerritoryName: territoryName(),
                    CompanyId: companyId(),
                    TerritoryCode: territoryCode(),
                    isDefault: isDefault(),
                    ScopeVariables: []
                }
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            territoryId: territoryId,
            territoryName: territoryName,
            companyId: companyId,
            territoryCode: territoryCode,
            isDefault: isDefault,
            isSelected: isSelected,
            scopeVariables: scopeVariables,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyTerritory.CreateFromClientModel = function (source) {
        return new CompanyTerritory(
            source.territoryId,
            source.territoryName,
            source.companyId,
            source.territoryCode,
            source.isDefault
        );
    };
    CompanyTerritory.Create = function (source) {
        var companyTerritory = new CompanyTerritory(
            source.TerritoryId,
            source.TerritoryName,
            source.CompanyId,
            source.TerritoryCode,
            source.isDefault
        );
        return companyTerritory;
    };

    // #endregion ______________  C O M P A N Y    T E R R I T O R Y    _________________

    // #region ______________  Color Palettes   _________________

    // ReSharper disable once InconsistentNaming
    var ColorPalette = function (specifiedPalleteId, specifiedPalleteName, specifiedColor1, specifiedColor2, specifiedColor3, specifiedColor4, specifiedColor5,
        specifiedColor6, specifiedColor7, specifiedSkinId, specifiedIsDefault, specifiedCompanyId) {
        var self,
            id = ko.observable(specifiedPalleteId),
            palleteName = ko.observable(specifiedPalleteName),
            color1 = ko.observable(specifiedColor1),
            color2 = ko.observable(specifiedColor2),
            color3 = ko.observable(specifiedColor3),
            color4 = ko.observable(specifiedColor4),
            color5 = ko.observable(specifiedColor5),
            color6 = ko.observable(specifiedColor6),
            color7 = ko.observable(specifiedColor7),
            skinId = ko.observable(specifiedSkinId),
            isDefault = ko.observable(specifiedIsDefault),
            companyId = ko.observable(specifiedCompanyId),
            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                color1: color1,
                color2: color2,
                color3: color3,
                color4: color4,
                color5: color5,
                color6: color6,
                color7: color7,
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.PalleteId = source.id();
                result.Color1 = source.color1();
                result.Color2 = source.color2();
                result.Color3 = source.color3();
                result.Color4 = source.color4();
                result.Color5 = source.color5();
                result.Color6 = source.color6();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            palleteName: palleteName,
            color1: color1,
            color2: color2,
            color3: color3,
            color4: color4,
            color5: color5,
            color6: color6,
            color7: color7,
            skinId: skinId,
            isDefault: isDefault,
            companyId: companyId,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    ColorPalette.Create = function (source) {
        return new ColorPalette(source.PalleteId, source.PalleteName, source.Color1, source.Color2, source.Color3, source.Color4, source.Color5, source.Color5, "", "", 0);
    }
    // #endregion ______________  Color Palettes   _________________

    // #region ______________  A D D R E S S   _________________

    // ReSharper disable once InconsistentNaming
    var Address = function (specifiedAddressId, specifiedCompanyId, specifiedAddressName, specifiedAddress1, specifiedAddress2, specifiedAddress3, specifiedCity, specifiedState, specifiedCountry, specifiedStateName, specifiedCountryName, specifiedPostCode, specifiedFax,
        specifiedEmail, specifiedURL, specifiedTel1, specifiedTel2, specifiedExtension1, specifiedExtension2, specifiedReference, specifiedFAO, specifiedIsDefaultAddress, specifiedIsDefaultShippingAddress,
        specifiedisArchived, specifiedTerritoryId, specifiedTerritoryName, specifiedGeoLatitude, specifiedGeoLongitude, specifiedisPrivate,
        specifiedisDefaultTerrorityBilling, specifiedisDefaultTerrorityShipping, specifiedOrganisationId, specifiedStateCode) {
        var
            self,
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1).extend({ required: true }),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity).extend({ required: true }),
            state = ko.observable(specifiedState),
            country = ko.observable(specifiedCountry),
            stateName = ko.observable(specifiedStateName),
            stateCode = ko.observable(specifiedStateCode),
            stateNamenCode = ko.computed(function () {
                return stateName() + "( " + stateCode() + " )";
            }),
            countryName = ko.observable(specifiedCountryName),
            postCode = ko.observable(specifiedPostCode),
            fax = ko.observable(specifiedFax),
            email = ko.observable(specifiedEmail).extend({ email: true }),
            uRL = ko.observable(specifiedURL),
            tel1 = ko.observable(specifiedTel1),
            tel2 = ko.observable(specifiedTel2),
            extension1 = ko.observable(specifiedExtension1),
            extension2 = ko.observable(specifiedExtension2),
            reference = ko.observable(specifiedReference),
            fAO = ko.observable(specifiedFAO),
            isDefaultAddress = ko.observable(specifiedIsDefaultAddress),
            isDefaultShippingAddress = ko.observable(specifiedIsDefaultShippingAddress),
            isArchived = ko.observable(specifiedisArchived),
            territoryId = ko.observable(specifiedTerritoryId).extend({ required: true }),
            territoryName = ko.observable(specifiedTerritoryName),
            geoLatitude = ko.observable(specifiedGeoLatitude),//.extend({ number: true }),
            geoLongitude = ko.observable(specifiedGeoLongitude),//.extend({ number: true }),
            isPrivate = ko.observable(specifiedisPrivate),
            isDefaultTerrorityBilling = ko.observable(specifiedisDefaultTerrorityBilling),
            isDefaultTerrorityShipping = ko.observable(specifiedisDefaultTerrorityShipping),
            organisationId = ko.observable(specifiedOrganisationId),
            territory = ko.observable(),
            scopeVariables = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                addressName: addressName,
                territoryId: territoryId,
                address1: address1,
                city: city,
                //geoLatitude: geoLatitude,
                //geoLongitude: geoLongitude,
                email: email
                //fax: fax
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                addressId: addressId,
                companyId: companyId,
                addressName: addressName,
                address1: address1,
                address2: address2,
                address3: address3,
                city: city,
                state: state,
                country: country,
                postCode: postCode,
                fax: fax,
                email: email,
                uRL: uRL,
                tel1: tel1,
                tel2: tel2,
                extension1: extension1,
                extension2: extension2,
                reference: reference,
                fAO: fAO,
                isDefaultAddress: isDefaultAddress,
                isDefaultShippingAddress: isDefaultShippingAddress,
                isArchived: isArchived,
                territoryId: territoryId,
                geoLatitude: geoLatitude,
                geoLongitude: geoLongitude,
                isPrivate: isPrivate,
                isDefaultTerrorityBilling: isDefaultTerrorityBilling,
                isDefaultTerrorityShipping: isDefaultTerrorityShipping,
                organisationId: organisationId,
                scopeVariables: scopeVariables
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                return {
                    AddressId: addressId(),
                    CompanyId: companyId(),
                    AddressName: addressName(),
                    Address1: address1(),
                    Address2: address2(),
                    Address3: address3(),
                    City: city(),
                    StateId: state(),
                    CountryId: country(),
                    PostCode: postCode(),
                    Fax: fax(),
                    URL: uRL(),
                    Tel1: tel1(),
                    Tel2: tel2(),
                    Extension1: extension1(),
                    Extension2: extension2(),
                    Reference: reference(),
                    FAO: fAO(),
                    IsDefaultAddress: isDefaultAddress(),
                    IsDefaultShippingAddress: isDefaultShippingAddress(),
                    isArchived: isArchived(),
                    TerritoryId: territoryId(),
                    GeoLatitude: geoLatitude(),
                    GeoLongitude: geoLongitude(),
                    isPrivate: isPrivate(),
                    isDefaultTerrorityBilling: isDefaultTerrorityBilling(),
                    isDefaultTerrorityShipping: isDefaultTerrorityShipping(),
                    Email: email(),
                    OrganisationId: organisationId(),
                    ScopeVariables: []
                    //Territory: territory().convertToServerData(),
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            addressId: addressId,
            companyId: companyId,
            addressName: addressName,
            address1: address1,
            address2: address2,
            address3: address3,
            city: city,
            state: state,
            country: country,
            stateName: stateName,
            stateCode: stateCode,
            stateNamenCode:stateNamenCode,
            countryName: countryName,
            postCode: postCode,
            fax: fax,
            email: email,
            uRL: uRL,
            tel1: tel1,
            tel2: tel2,
            extension1: extension1,
            extension2: extension2,
            reference: reference,
            fAO: fAO,
            isDefaultAddress: isDefaultAddress,
            isDefaultShippingAddress: isDefaultShippingAddress,
            isArchived: isArchived,
            territoryId: territoryId,
            geoLatitude: geoLatitude,
            geoLongitude: geoLongitude,
            isPrivate: isPrivate,
            isDefaultTerrorityBilling: isDefaultTerrorityBilling,
            isDefaultTerrorityShipping: isDefaultTerrorityShipping,
            organisationId: organisationId,
            territory: territory,
            territoryName: territoryName,
            scopeVariables: scopeVariables,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    Address.CreateFromClientModel = function (source) {
        return new Address(
            source.addressId,
            source.companyId,
            source.addressName,
            source.address1,
            source.address2,
            source.address3,
            source.city,
            source.state,
            source.country,
            source.stateName,
            source.countryName,
            source.postCode,
            source.fax,
            source.email,
            source.uRL,
            source.tel1,
            source.tel2,
            source.extension1,
            source.extension2,
            source.reference,
            source.fAO,
            source.isDefaultAddress,
            source.isDefaultShippingAddress,
            source.isArchived,
            source.territoryId,
            source.territoryName,
            source.geoLatitude,
            source.geoLongitude,
            source.isPrivate,
            source.isDefaultTerrorityBilling,
            source.isDefaultTerrorityShipping,
            source.organisationId
        );
    };
    Address.Create = function (source) {
        var address = new Address(
            source.AddressId,
            source.CompanyId,
            source.AddressName,
            source.Address1,
            source.Address2,
            source.Address3,
            source.City,
            source.StateId,
            source.CountryId,
            source.StateName,
            source.CountryName,
            source.PostCode,
            source.Fax,
            source.Email,
            source.URL,
            source.Tel1,
            source.Tel2,
            source.Extension1,
            source.Extension2,
            source.Reference,
            source.FAO,
            source.IsDefaultAddress,
            source.IsDefaultShippingAddress,
            source.isArchived,
            source.TerritoryId,
            source.TerritoryName,
            source.GeoLatitude,
            source.GeoLongitude,
            source.isPrivate,
            source.isDefaultTerrorityBilling,
            source.isDefaultTerrorityShipping,
            source.OrganisationId,
            source.StateCode
        );
        return address;
    };
    // #endregion ______________  A D D R E S S   _________________

    // #region ______________  Company Banner   _________________

    // ReSharper disable once InconsistentNaming
    var CompanyBanner = function (specifiedCompanyBannerId, specifiedHeading, specifiedDescription, specifiedItemURL, specifiedButtonURL, specifiedCompanySetId,
        specifiedImageSource, specifiedImageURL) {
        var self,
            id = ko.observable(specifiedCompanyBannerId),
            heading = ko.observable(specifiedHeading).extend({ required: true }),
            description = ko.observable(specifiedDescription),
            itemURL = ko.observable(specifiedItemURL),
            buttonURL = ko.observable(specifiedButtonURL),
            companySetId = ko.observable(specifiedCompanySetId).extend({ required: true }),
            filename = ko.observable(""),
            fileBinary = ko.observable(),
            fileType = ko.observable(),
            imageSource = ko.observable(),
            filePath = ko.observable(specifiedImageURL),
            filePathWithCacheRemoveTechnique = ko.computed(function () {
                if (filePath() !== undefined && filePath() !== null) {
                    return filePath() + "?" + Date();
                }
            }),
            //Set Name For List View
            setName = ko.observable(),
            // Errors
            errors = ko.validation.group({
                heading: heading,
                companySetId: companySetId
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                heading: heading,
                description: description,
                itemURL: itemURL,
                buttonURL: buttonURL,
                companySetId: companySetId,
                setName: setName,
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.CompanyBannerId = source.id() === undefined ? 0 : source.id();
                result.Heading = source.heading() === undefined ? null : source.heading();;
                result.Description = source.description() === undefined ? null : source.description();
                result.ItemURL = source.itemURL() === undefined ? null : source.itemURL();
                result.ButtonURL = source.buttonURL() === undefined ? null : source.buttonURL();
                result.CompanySetId = source.companySetId() === undefined ? null : source.companySetId();
                result.ImageURL = source.filePath() === undefined ? null : source.filePath();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            heading: heading,
            description: description,
            itemURL: itemURL,
            buttonURL: buttonURL,
            companySetId: companySetId,
            setName: setName,
            filename: filename,
            fileBinary: fileBinary,
            fileType: fileType,
            imageSource: imageSource,
            filePath: filePath,
            filePathWithCacheRemoveTechnique: filePathWithCacheRemoveTechnique,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Company Banner Create Factory
    CompanyBanner.Create = function (source) {
        return new CompanyBanner(
            source.CompanyBannerId,
            source.Heading,
            source.Description,
            source.ItemURL,
            source.ButtonURL,
            source.CompanySetId,
            source.ImageSource,
            source.ImageURL
        );
    };
    CompanyBanner.CreateFromClientModel = function (source) {
        return new CompanyBanner(
            source.id,
            source.heading,
            source.description,
            source.itemURL,
            source.buttonURL,
            source.companySetId,
            source.setName,
            source.filename,
            source.fileBinary,
            source.fileType,
            source.imageSource
            );
    };
    // #endregion ______________  Company Banner   _________________

    // #region ______________  Company Banner  Set _________________

    // ReSharper disable once InconsistentNaming
    var CompanyBannerSet = function (specifiedCompanySetId, specifiedSetName) {
        var self,
            id = ko.observable(specifiedCompanySetId),
            setName = ko.observable(specifiedSetName).extend({ required: true }),
            //Compnay Banners
            companyBanners = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                setName: setName
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.CompanySetId = source.id() === undefined ? 0 : source.id();
                result.FakeId = source.id() === undefined ? 0 : source.id();
                result.SetName = source.setName() === undefined ? null : source.setName();;
                result.CompanyBanners = [];
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            setName: setName,
            companyBanners: companyBanners,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyBannerSet.Create = function (source) {
        return new CompanyBannerSet(
            source.CompanySetId,
            source.SetName
        );
    };
    //Company Banner Set Create Factory
    CompanyBannerSet.CreateNew = function () {
        return new CompanyBannerSet(0, undefined);
    };
    // #endregion ______________  Company Banner  Set _________________

    // #region ______________ Email _________________

    // ReSharper disable once InconsistentNaming
    var Campaign = function (specifiedCampaignId, specifiedCampaignName, specifiedDescription, specifiedCampaignType, specifiedIsEnabled, specifiedStartDateTime
        , specifiedIncludeCustomers, specifiedIncludeSuppliers, specifiedIncludeProspects, specifiedIncludeNewsLetterSubscribers, specifiedIncludeFlag, specifiedFlagIDs,
        specifiedCustomerTypeIDs, specifiedGroupIDs, specifiedSubjectA, specifiedHTMLMessageA, specifiedFromName, specifiedFromAddress, specifiedReturnPathAddress,
        specifiedReplyToAddress, specifiedEmailLogFileAddress2, specifiedEmailEvent, specifiedEventName, specifiedSendEmailAfterDays, specifiedIncludeType,
        specifiedIncludeCorporateCustomers, specifiedEnableLogFiles, specifiedEmailLogFileAddress3
        ) {
        var self,
            id = ko.observable(specifiedCampaignId),
            campaignName = ko.observable(specifiedCampaignName).extend({ required: true }),
            description = ko.observable(specifiedDescription),
            campaignType = ko.observable(specifiedCampaignType),
            isEnabled = ko.observable(specifiedIsEnabled),
            startDateTime = ko.observable((specifiedStartDateTime !== undefined && specifiedStartDateTime !== null) ? moment(specifiedStartDateTime, ist.utcFormat).toDate() : undefined).extend({
                required: {
                    message: "Start Date is required",
                    onlyIf: function () {
                        return campaignType() === 3;
                    }
                }
            }),
        includeCustomers = ko.observable(specifiedIncludeCustomers),
        includeSuppliers = ko.observable(specifiedIncludeSuppliers),
        includeProspects = ko.observable(specifiedIncludeProspects),
        includeNewsLetterSubscribers = ko.observable(specifiedIncludeNewsLetterSubscribers),
        includeFlag = ko.observable(specifiedIncludeFlag),
        flagIDs = ko.observable(specifiedFlagIDs),
        customerTypeIDs = ko.observable(specifiedCustomerTypeIDs),
        groupIDs = ko.observable(specifiedGroupIDs),
        subjectA = ko.observable(specifiedSubjectA).extend({ required: true }),
        hTMLMessageA = ko.observable(specifiedHTMLMessageA),
        fromName = ko.observable(specifiedFromName),
        fromAddress = ko.observable(specifiedFromAddress),
        returnPathAddress = ko.observable(specifiedReturnPathAddress),
        replyToAddress = ko.observable(specifiedReplyToAddress),
        emailLogFileAddress2 = ko.observable(specifiedEmailLogFileAddress2).extend({ email: true }),
        emailEventId = ko.observable(specifiedEmailEvent),
        eventName = ko.observable(specifiedEventName),
        sendEmailAfterDays = ko.observable(specifiedSendEmailAfterDays).extend({ number: true }),
        campaignImages = ko.observableArray([]),
        includeType = ko.observable(specifiedIncludeType),
        includeCorporateCustomers = ko.observable(specifiedIncludeCorporateCustomers),
        enableLogFiles = ko.observable(specifiedEnableLogFiles),
        emailLogFileAddress3 = ko.observable(specifiedEmailLogFileAddress3).extend({ email: true }),

        // Errors
        errors = ko.validation.group({
            campaignName: campaignName,
            emailLogFileAddress3: emailLogFileAddress3,
            sendEmailAfterDays: sendEmailAfterDays,
            emailLogFileAddress2: emailLogFileAddress2,
            startDateTime: startDateTime,
        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            campaignName: campaignName,
            description: description,
            campaignType: campaignType,
            isEnabled: isEnabled,
            startDateTime: startDateTime,
            includeCustomers: includeCustomers,
            includeSuppliers: includeSuppliers,
            includeProspects: includeProspects,
            includeNewsLetterSubscribers: includeNewsLetterSubscribers,
            includeFlag: includeFlag,
            flagIDs: flagIDs,
            customerTypeIDs: customerTypeIDs,
            groupIDs: groupIDs,
            subjectA: subjectA,
            hTMLMessageA: hTMLMessageA,
            fromName: fromName,
            fromAddress: fromAddress,
            returnPathAddress: returnPathAddress,
            replyToAddress: replyToAddress,
            emailLogFileAddress2: emailLogFileAddress2,
            emailEventId: emailEventId,
            sendEmailAfterDays: sendEmailAfterDays,
            campaignImages: campaignImages,
            includeType: includeType,
            includeCorporateCustomers: includeCorporateCustomers,
            enableLogFiles: enableLogFiles,
            emailLogFileAddress3: emailLogFileAddress3,
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.CampaignId = source.id() === undefined ? 0 : source.id();
            result.CampaignName = source.campaignName() === undefined ? null : source.campaignName();
            result.Description = source.description() === undefined ? null : source.description();
            result.CampaignType = source.campaignType() === undefined ? null : source.campaignType();
            result.IsEnabled = source.isEnabled() === undefined ? false : source.isEnabled();
            result.StartDateTime = (startDateTime() === undefined || startDateTime() === null) ? null : moment(startDateTime()).format(ist.utcFormat);
            // result.StartDateTime = moment(new Date()).format(ist.utcFormat);
            result.IncludeCustomers = (source.includeCustomers() === undefined || source.includeCustomers() === null) ? false : source.includeCustomers();
            result.IncludeSuppliers = source.includeSuppliers() === undefined ? false : source.includeSuppliers();
            result.IncludeProspects = source.includeProspects() === undefined ? false : source.includeProspects();
            result.IncludeNewsLetterSubscribers = source.includeNewsLetterSubscribers() === undefined ? false : source.includeNewsLetterSubscribers();
            result.IncludeFlag = source.includeFlag() === undefined ? null : source.includeFlag();
            result.FlagIDs = source.flagIDs() === undefined ? null : source.flagIDs();
            result.CustomerTypeIDs = source.customerTypeIDs() === undefined ? null : source.customerTypeIDs();
            result.GroupIDs = source.groupIDs() === undefined ? null : source.groupIDs();
            result.SubjectA = source.subjectA() === undefined ? null : source.subjectA();
            result.HTMLMessageA = source.hTMLMessageA() === undefined ? null : source.hTMLMessageA();
            result.FromName = source.fromName() === undefined ? null : source.fromName();
            result.FromAddress = source.fromAddress() === undefined ? null : source.fromAddress();
            result.ReturnPathAddress = source.returnPathAddress() === undefined ? null : source.returnPathAddress();
            result.ReplyToAddress = source.replyToAddress() === undefined ? null : source.replyToAddress();
            result.EmailLogFileAddress2 = source.emailLogFileAddress2() === undefined ? null : source.emailLogFileAddress2();
            result.EmailEvent = source.emailEventId() === undefined ? null : source.emailEventId();
            result.SendEmailAfterDays = source.sendEmailAfterDays() === undefined ? null : source.sendEmailAfterDays();
            result.IncludeType = source.includeType() === undefined ? false : source.includeType();
            result.IncludeCorporateCustomers = source.includeCorporateCustomers() === undefined ? false : source.includeCorporateCustomers();
            result.EnableLogFiles = source.enableLogFiles() === undefined ? false : source.enableLogFiles();
            result.EmailLogFileAddress3 = source.emailLogFileAddress3() === undefined ? null : source.emailLogFileAddress3();
            result.CampaignImages = [];
            return result;
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            id: id,
            campaignName: campaignName,
            description: description,
            campaignType: campaignType,
            isEnabled: isEnabled,
            startDateTime: startDateTime,
            includeCustomers: includeCustomers,
            includeSuppliers: includeSuppliers,
            includeProspects: includeProspects,
            includeNewsLetterSubscribers: includeNewsLetterSubscribers,
            includeFlag: includeFlag,
            flagIDs: flagIDs,
            customerTypeIDs: customerTypeIDs,
            groupIDs: groupIDs,
            subjectA: subjectA,
            hTMLMessageA: hTMLMessageA,
            fromName: fromName,
            fromAddress: fromAddress,
            returnPathAddress: returnPathAddress,
            replyToAddress: replyToAddress,
            emailLogFileAddress2: emailLogFileAddress2,
            emailEventId: emailEventId,
            eventName: eventName,
            sendEmailAfterDays: sendEmailAfterDays,
            campaignImages: campaignImages,
            includeType: includeType,
            includeCorporateCustomers: includeCorporateCustomers,
            enableLogFiles: enableLogFiles,
            emailLogFileAddress3: emailLogFileAddress3,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    Campaign.Create = function (source) {
        return new Campaign(
            source.CampaignId,
            source.CampaignName,
            source.Description,
            source.CampaignType,
            source.IsEnabled,
            source.StartDateTime,
            source.IncludeCustomers,
            source.IncludeSuppliers,
            source.IncludeProspects,
            source.IncludeNewsLetterSubscribers,
            source.IncludeFlag,
            source.FlagIDs,
            source.CustomerTypeIDs,
            source.GroupIDs,
            source.SubjectA,
            source.HTMLMessageA,
            source.FromName,
            source.FromAddress,
            source.ReturnPathAddress,
            source.ReplyToAddress,
            source.EmailLogFileAddress2,
            source.EmailEvent,
            source.EventName,
            source.SendEmailAfterDays,
            source.IncludeType,
            source.IncludeCorporateCustomers,
            source.EnableLogFiles,
            source.EmailLogFileAddress3
      );
    };
    Campaign.CreateFromClientModel = function (source) {
        return new Campaign(
            source.id,
            source.campaignName,
            source.emailEventId,
            source.startDateTime,
            source.isEnabled,
            source.sendEmailAfterDays,
            source.eventName,
            source.campaignType
            );
    };
    // #endregion ______________ Email _________________


    // #region ______________ Campaign Section _________________
    var CampaignSection = function (specifiedSectionId, specifiedSectionName) {
        var self,
            id = ko.observable(specifiedSectionId),
            sectionName = ko.observable(specifiedSectionName),
            //Is Expanded
            isExpanded = ko.observable(false),
            campaignEmailVariables = ko.observableArray([]);

        self = {
            id: id,
            sectionName: sectionName,
            isExpanded: isExpanded,
            campaignEmailVariables: campaignEmailVariables,
        };
        return self;
    };
    CampaignSection.Create = function (source) {
        return new CampaignSection(source.SectionId, source.SectionName);
    };

    var CampaignEmailVariable = function (specifiedVariableId, specifiedVariableName, specifiedVariableTag) {
        var self,
            id = ko.observable(specifiedVariableId),
            variableName = ko.observable(specifiedVariableName),
           variableTag = ko.observable(specifiedVariableTag);

        self = {
            id: id,
            variableName: variableName,
            variableTag: variableTag,
        };
        return self;
    };
    CampaignEmailVariable.Create = function (source) {
        return new CampaignEmailVariable(source.VariableId, source.VariableName, source.VariableTag);
    };

    var CampaignImage = function (specifiedCampaignImageId, specifiedCampaignId, specifiedImagePath, specifiedImageName, specifiedImageSource) {
        var self,
            id = ko.observable(specifiedCampaignImageId),
            campaignId = ko.observable(specifiedCampaignId),
            imagePath = ko.observable(specifiedImagePath),
            imageName = ko.observable(specifiedImageName),
            imageSource = ko.observable(specifiedImageSource);
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.CampaignImageId = source.id() === undefined ? 0 : source.id();
            result.CampaignId = source.campaignId() === undefined ? 0 : source.campaignId();
            result.ImagePath = source.imagePath() === undefined ? 0 : source.imagePath();
            result.ImageName = source.imageName() === undefined ? 0 : source.imageName();
            result.ImageByteSource = source.imageSource() === undefined ? 0 : source.imageSource();
            return result;
        },
        self = {
            id: id,
            campaignId: campaignId,
            imagePath: imagePath,
            imageName: imageName,
            imageSource: imageSource,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    CampaignImage.Create = function (source) {
        return new CampaignImage(source.CampaignImageId, source.CampaignId, source.ImagePath, source.ImageName, source.ImageSource);
    };

    var SectionFlag = function (specifiedSectionFlagId, specifiedFlagName) {
        var self,
            id = ko.observable(specifiedSectionFlagId),
            name = ko.observable(specifiedFlagName),
            isChecked = ko.observable(false);

        self = {
            id: id,
            name: name,
            isChecked: isChecked,
        };
        return self;
    };
    SectionFlag.Create = function (source) {
        return new SectionFlag(source.SectionFlagId, source.FlagName);
    };


    var CampaignCompanyType = function (specifiedTypeId, specifiedTypeName) {
        var self,
            id = ko.observable(specifiedTypeId),
            name = ko.observable(specifiedTypeName),
            isChecked = ko.observable(false);

        self = {
            id: id,
            name: name,
            isChecked: isChecked,
        };
        return self;
    };
    CampaignCompanyType.Create = function (source) {
        return new CampaignCompanyType(source.TypeId, source.TypeName);
    };

    var Group = function (specifiedGroupId, specifiedGroupName) {
        var self,
            id = ko.observable(specifiedGroupId),
            name = ko.observable(specifiedGroupName),
            isChecked = ko.observable(false);

        self = {
            id: id,
            name: name,
            isChecked: isChecked,
        };
        return self;
    };
    Group.Create = function (source) {
        return new Group(source.GroupId, source.GroupName);
    };


    // #endregion ______________ Campaign Section _________________

    // #region ______________  CMS Page   _________________

    // ReSharper disable once InconsistentNaming
    var CMSPage = function (specifiedPageId, specifiedPageTitle, specifiedPageKeywords, specifiedMetaTitle, specifiedMetaDescriptionContent, specifiedMetaCategoryContent,
        specifiedMetaRobotsContent, specifiedMetaAuthorContent, specifiedMetaLanguageContent, specifiedMetaRevisitAfterContent, specifiedCategoryId, specifiedPageHTML,
        specifiedImageSource, specifiedDefaultPageKeyWords, specifiedFileName, specifiedPageBanner, specifiedisEnabled, specifiedCompanyId) {
        var self,
            id = ko.observable(specifiedPageId),
           // pageTitle = ko.observable(specifiedPageTitle).extend({ required: true }),
           isUserDefined = ko.observable(undefined),
            pageTitle = ko.observable(specifiedPageTitle).extend({
                required: {
                    onlyIf: function () {
                        return isUserDefined();
                    }
                }
            }),
        pageKeywords = ko.observable(specifiedPageKeywords),
        metaTitle = ko.observable(specifiedMetaTitle),
        metaDescriptionContent = ko.observable(specifiedMetaDescriptionContent),
        metaCategoryContent = ko.observable(specifiedMetaCategoryContent),
        metaRobotsContent = ko.observable(specifiedMetaRobotsContent),
        metaAuthorContent = ko.observable(specifiedMetaAuthorContent),
        metaLanguageContent = ko.observable(specifiedMetaLanguageContent),
        metaRevisitAfterContent = ko.observable(specifiedMetaRevisitAfterContent),
        categoryId = ko.observable(specifiedCategoryId),
        pageHTML = ko.observable(specifiedPageHTML === undefined ? "Go ahead..." : specifiedPageHTML),
        imageSrc = ko.observable(),
        fileName = ko.observable(specifiedFileName),
        isEnabled = ko.observable(specifiedisEnabled != null ? specifiedisEnabled : true),
        defaultPageKeyWords = ko.observable(specifiedDefaultPageKeyWords),
        pageBanner = ko.observable(specifiedPageBanner),
        companyId = ko.observable(specifiedCompanyId),

        pageBannerWithCacheRemoveTechnique = ko.computed(function () {
            if (pageBanner() !== undefined && pageBanner() !== null) {
                return pageBanner() + "?" + Date();
            }
        }),
        // Errors
        errors = ko.validation.group({
            pageTitle: pageTitle,

        }),
        // Is Valid 
        isValid = ko.computed(function () {
            return errors().length === 0 ? true : false;
        }),

        // ReSharper disable InconsistentNaming
        dirtyFlag = new ko.dirtyFlag({
            pageTitle: pageTitle,
            pageKeywords: pageKeywords,
            metaTitle: metaTitle,
            metaDescriptionContent: metaDescriptionContent,
            metaCategoryContent: metaCategoryContent,
            metaRobotsContent: metaRobotsContent,
            metaAuthorContent: metaAuthorContent,
            metaLanguageContent: metaLanguageContent,
            metaRevisitAfterContent: metaRevisitAfterContent,
            categoryId: categoryId,
            pageHTML: pageHTML,
            imageSrc: imageSrc,
            fileName: fileName,
            defaultPageKeyWords: defaultPageKeyWords,
            pageBanner: pageBanner,
            isEnabled: isEnabled
        }),
        // Has Changes
        hasChanges = ko.computed(function () {
            return dirtyFlag.isDirty();
        }),
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.PageId = source.id() === undefined ? 0 : source.id();
            result.PageTitle = source.pageTitle() === undefined ? null : source.pageTitle();;
            result.PageKeywords = source.pageKeywords() === undefined ? null : source.pageKeywords();
            result.Meta_Title = source.metaTitle() === undefined ? null : source.metaTitle();
            result.Meta_DescriptionContent = source.metaDescriptionContent() === undefined ? null : source.metaDescriptionContent();
            result.Meta_CategoryContent = source.metaCategoryContent() === undefined ? null : source.metaCategoryContent();
            result.Meta_RobotsContent = source.metaRobotsContent() === undefined ? null : source.metaRobotsContent();
            result.Meta_AuthorContent = source.metaAuthorContent() === undefined ? null : source.metaAuthorContent();
            result.Meta_LanguageContent = source.metaLanguageContent() === undefined ? null : source.metaLanguageContent();
            result.Meta_RevisitAfterContent = source.metaRevisitAfterContent() === undefined ? null : source.metaRevisitAfterContent();
            result.CategoryId = source.categoryId() === undefined ? null : source.categoryId();
            result.PageHTML = source.pageHTML() === undefined ? null : source.pageHTML();
            result.FileName = source.fileName() === undefined ? null : source.fileName();
            result.Bytes = source.imageSrc() === undefined ? null : source.imageSrc();
            result.PageBanner = source.pageBanner() === undefined ? null : source.pageBanner();
            result.isEnabled = source.isEnabled();
            result.IsUserDefined = source.isUserDefined();
            result.CompanyId = source.companyId();
            return result;
        },
        // Reset
        reset = function () {
            dirtyFlag.reset();
        };
        self = {
            id: id,
            pageTitle: pageTitle,
            pageKeywords: pageKeywords,
            metaTitle: metaTitle,
            metaDescriptionContent: metaDescriptionContent,
            metaCategoryContent: metaCategoryContent,
            metaRobotsContent: metaRobotsContent,
            metaAuthorContent: metaAuthorContent,
            metaLanguageContent: metaLanguageContent,
            metaRevisitAfterContent: metaRevisitAfterContent,
            categoryId: categoryId,
            pageHTML: pageHTML,
            imageSrc: imageSrc,
            fileName: fileName,
            defaultPageKeyWords: defaultPageKeyWords,
            pageBanner: pageBanner,
            isValid: isValid,
            isUserDefined: isUserDefined,
            pageBannerWithCacheRemoveTechnique: pageBannerWithCacheRemoveTechnique,
            companyId: companyId,
            errors: errors,
            isEnabled: isEnabled,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //CMS Page Create Factory
    CMSPage.Create = function (source) {
        var obj = new CMSPage(
            source.PageId,
            source.PageTitle,
            source.PageKeywords,
            source.Meta_Title,
            source.Meta_DescriptionContent,
            source.Meta_CategoryContent,
            source.Meta_RobotsContent,
            source.Meta_AuthorContent,
            source.Meta_LanguageContent,
            source.Meta_RevisitAfterContent,
            source.CategoryId,
            source.PageHTML,
            source.ImageSource,
            source.DefaultPageKeyWords,
            source.FileName,
            source.PageBanner,
            source.isEnabled
        );
        obj.isUserDefined(source.IsUserDefined);
        return obj;
    };
    // #endregion ______________  CMS Page   _________________

    // #region ______________  P A G E  C A T E G O R Y  _________________

    // ReSharper disable once InconsistentNaming
    var PageCategory = function (specifiedCategoryId, specifiedCategoryName) {
        var self,
            id = ko.observable(specifiedCategoryId),
            name = ko.observable(specifiedCategoryName).extend({ required: true }),
            // Errors
            errors = ko.validation.group({
                name: name,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.CategoryId = source.id() === undefined ? 0 : source.id();
                result.CategoryName = source.name() === undefined ? null : source.name();
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            name: name,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    PageCategory.Create = function (source) {
        return new PageCategory(source.CategoryId, source.CategoryName);
    };
    // #endregion ______________  P A G E  C A T E G O R Y  _________________

    // #region ___________  Secondary Page List View ____________________

    SecondaryPageListView = function (specifiedPageId, specifiedPageTitle, specifiedMetaTitle, specifiedIsEnabled, specifiedIsDisplay, specifiedCategoryName,
    specifiedImageSource) {
        var
            self,
            //Unique ID
            pageId = ko.observable(specifiedPageId),
            pageTitle = ko.observable(specifiedPageTitle),
            metaTitle = ko.observable(specifiedMetaTitle),
            isEnabled = ko.observable(specifiedIsEnabled),
            imageSource = ko.observable(specifiedImageSource),
            isDisplay = ko.observable(specifiedIsDisplay === null ? false : true),
            categoryName = ko.observable(specifiedCategoryName),
            isUserDefined = ko.observable(undefined),
            convertToServerData = function () {
                return {
                    PageId: pageId(),
                    IsUserDefined: isUserDefined()
                };
            };
        self = {
            pageId: pageId,
            isUserDefined: isUserDefined,
            pageTitle: pageTitle,
            metaTitle: metaTitle,
            isEnabled: isEnabled,
            isDisplay: isDisplay,
            categoryName: categoryName,
            imageSource: imageSource,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    SecondaryPageListView.Create = function (source) {
        var obj = new SecondaryPageListView(source.PageId, source.PageTitle, source.Meta_Title, source.IsEnabled, source.IsDisplay,
            source.CategoryName, source.ImageSource);
        obj.isUserDefined(source.IsUserDefined);
        return obj;
    };
    // #endregion ___________  Secondary Page List View ____________________

    // #region ________________C O M P A N Y   C O N T A C T ___________________

    // ReSharper disable once InconsistentNaming
    // ReSharper restore InconsistentNaming
    var CompanyContact = function (specifiedContactId, specifiedAddressId, specifiedCompanyId, specifiedFirstName, specifiedMiddleName, specifiedLastName, specifiedTitle,
        specifiedHomeTel1, specifiedHomeTel2, specifiedHomeExtension1, specifiedHomeExtension2, specifiedMobile, specifiedEmail, specifiedFAX, specifiedJobTitle, specifiedDOB,
        specifiedNotes, specifiedIsDefaultContact, specifiedHomeAddress1, specifiedHomeAddress2, specifiedHomeCity, specifiedHomeState, specifiedHomePostCode, specifiedHomeCountry,
        specifiedSecretQuestion, specifiedSecretAnswer, specifiedPassword, specifiedURL, specifiedIsEmailSubscription, specifiedIsNewsLetterSubscription, specifiedimage,
        specifiedquickFullName, specifiedquickTitle, specifiedquickCompanyName, specifiedquickAddress1, specifiedquickAddress2, specifiedquickAddress3, specifiedquickPhone,
        specifiedquickFax, specifiedquickEmail, specifiedquickWebsite, specifiedquickCompMessage, specifiedQuestionId, specifiedIsApprover, specifiedisWebAccess, specifiedisPlaceOrder,
        specifiedCreditLimit, specifiedisArchived, specifiedContactRoleId, specifiedTerritoryId, specifiedClaimIdentifer, specifiedAuthentifiedBy, specifiedIsPayByPersonalCreditCard,
        specifiedIsPricingshown, specifiedSkypeId, specifiedLinkedinURL, specifiedFacebookURL, specifiedTwitterURL, specifiedauthenticationToken, specifiedtwitterScreenName,
        specifiedShippingAddressId, specifiedisUserLoginFirstTime, specifiedquickMobileNumber, specifiedquickTwitterId, specifiedquickFacebookId, specifiedquickLinkedInId,
        specifiedquickOtherId, specifiedPOBoxAddress, specifiedCorporateUnit, specifiedOfficeTradingName, specifiedContractorName, specifiedBPayCRN, specifiedABN, specifiedACN,
        specifiedAdditionalField1, specifiedAdditionalField2, specifiedAdditionalField3, specifiedAdditionalField4, specifiedAdditionalField5, specifiedcanUserPlaceOrderWithoutApproval,
        specifiedCanUserEditProfile, specifiedcanPlaceDirectOrder, specifiedOrganisationId, specifiedBussinessAddressId, specifiedRoleName) {
        var self,
            contactId = ko.observable(specifiedContactId),
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            firstName = ko.observable(specifiedFirstName || "").extend({ required: true }),
            middleName = ko.observable(specifiedMiddleName || ""),
            lastName = ko.observable(specifiedLastName || ""),
            title = ko.observable(specifiedTitle),
            homeTel1 = ko.observable(specifiedHomeTel1),
            homeTel2 = ko.observable(specifiedHomeTel2),
            homeExtension1 = ko.observable(specifiedHomeExtension1),
            homeExtension2 = ko.observable(specifiedHomeExtension2),
            mobile = ko.observable(specifiedMobile),
            email = ko.observable(specifiedEmail).extend({ required: true, email: true }),
            fAX = ko.observable(specifiedFAX),
            jobTitle = ko.observable(specifiedJobTitle),
            dOB = ko.observable(specifiedDOB),
            notes = ko.observable(specifiedNotes),
            isDefaultContact = ko.observable(specifiedIsDefaultContact),
            homeAddress1 = ko.observable(specifiedHomeAddress1),
            homeAddress2 = ko.observable(specifiedHomeAddress2),
            homeCity = ko.observable(specifiedHomeCity),
            homeState = ko.observable(specifiedHomeState),
            homePostCode = ko.observable(specifiedHomePostCode),
            homeCountry = ko.observable(specifiedHomeCountry),
            secretQuestion = ko.observable(specifiedSecretQuestion),
            secretAnswer = ko.observable(specifiedSecretAnswer),
            password = ko.observable(specifiedPassword).extend({ required: { params: true, message: 'This field is required with minimum 6 characters!' }, minLength: 6 }),
            uRL = ko.observable(specifiedURL),
            isEmailSubscription = ko.observable(specifiedIsEmailSubscription),
            isNewsLetterSubscription = ko.observable(specifiedIsNewsLetterSubscription),
            image = ko.observable(specifiedimage),
            quickFullName = ko.observable(specifiedquickFullName),
            quickTitle = ko.observable(specifiedquickTitle),
            quickCompanyName = ko.observable(specifiedquickCompanyName),
            quickAddress1 = ko.observable(specifiedquickAddress1),
            quickAddress2 = ko.observable(specifiedquickAddress2),
            quickAddress3 = ko.observable(specifiedquickAddress3),
            quickPhone = ko.observable(specifiedquickPhone),
            quickFax = ko.observable(specifiedquickFax),
            quickEmail = ko.observable(specifiedquickEmail),
            quickWebsite = ko.observable(specifiedquickWebsite),
            quickCompMessage = ko.observable(specifiedquickCompMessage),
            questionId = ko.observable(specifiedQuestionId),
            isApprover = ko.observable(specifiedIsApprover),
            isWebAccess = ko.observable(specifiedisWebAccess),
            isPlaceOrder = ko.observable(specifiedisPlaceOrder),
            creditLimit = ko.observable(specifiedCreditLimit).extend({ numberInput: ist.numberFormat }), //.extend({ number: true }),
            isArchived = ko.observable(specifiedisArchived),
            contactRoleId = ko.observable(specifiedContactRoleId),
            territoryId = ko.observable(specifiedTerritoryId),
            claimIdentifer = ko.observable(specifiedClaimIdentifer),
            authentifiedBy = ko.observable(specifiedAuthentifiedBy),
            isPayByPersonalCreditCard = ko.observable(specifiedIsPayByPersonalCreditCard),
            isPricingshown = ko.observable(specifiedIsPricingshown),
            skypeId = ko.observable(specifiedSkypeId),
            linkedinURL = ko.observable(specifiedLinkedinURL),
            facebookURL = ko.observable(specifiedFacebookURL),
            twitterURL = ko.observable(specifiedTwitterURL),
            authenticationToken = ko.observable(specifiedauthenticationToken),
            twitterScreenName = ko.observable(specifiedtwitterScreenName),
            shippingAddressId = ko.observable(specifiedShippingAddressId),
            isUserLoginFirstTime = ko.observable(specifiedisUserLoginFirstTime),
            quickMobileNumber = ko.observable(specifiedquickMobileNumber),
            quickTwitterId = ko.observable(specifiedquickTwitterId),
            quickFacebookId = ko.observable(specifiedquickFacebookId),
            quickLinkedInId = ko.observable(specifiedquickLinkedInId),
            quickOtherId = ko.observable(specifiedquickOtherId),
            pOBoxAddress = ko.observable(specifiedPOBoxAddress),
            corporateUnit = ko.observable(specifiedCorporateUnit),
            officeTradingName = ko.observable(specifiedOfficeTradingName),
            contractorName = ko.observable(specifiedContractorName),
            bPayCRN = ko.observable(specifiedBPayCRN),
            aBN = ko.observable(specifiedABN),
            aCN = ko.observable(specifiedACN),
            additionalField1 = ko.observable(specifiedAdditionalField1),
            additionalField2 = ko.observable(specifiedAdditionalField2),
            additionalField3 = ko.observable(specifiedAdditionalField3),
            additionalField4 = ko.observable(specifiedAdditionalField4),
            additionalField5 = ko.observable(specifiedAdditionalField5),
            canUserPlaceOrderWithoutApproval = ko.observable(specifiedcanUserPlaceOrderWithoutApproval),
            canUserEditProfile = ko.observable(specifiedCanUserEditProfile),
            canPlaceDirectOrder = ko.observable(specifiedcanPlaceDirectOrder),
            organisationId = ko.observable(specifiedOrganisationId),
            bussinessAddressId = ko.observable(specifiedBussinessAddressId).extend({ required: true }),
            roleName = ko.observable(specifiedRoleName),
            fileName = ko.observable(),
            bussinessAddress = ko.observable(),
            shippingAddress = ko.observable(),
            stateName = ko.observable(),

            companyContactVariables = ko.observableArray([]),
            confirmPassword = ko.observable(specifiedPassword).extend({ compareWith: password }),


            // Errors
            errors = ko.validation.group({
                firstName: firstName,
                email: email,
                bussinessAddressId: bussinessAddressId,
                password: password,
                confirmPassword: confirmPassword
                //creditLimit: creditLimit
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                contactId: contactId,
                addressId: addressId,
                companyId: companyId,
                firstName: firstName,
                middleName: middleName,
                lastName: lastName,
                title: title,
                homeTel1: homeTel1,
                homeTel2: homeTel2,
                homeExtension1: homeExtension1,
                homeExtension2: homeExtension2,
                mobile: mobile,
                email: email,
                fAX: fAX,
                jobTitle: jobTitle,
                dOB: dOB,
                notes: notes,
                isDefaultContact: isDefaultContact,
                homeAddress1: homeAddress1,
                homeAddress2: homeAddress2,
                homeCity: homeCity,
                homeState: homeState,
                homePostCode: homePostCode,
                homeCountry: homeCountry,
                secretQuestion: secretQuestion,
                secretAnswer: secretAnswer,
                password: password,
                uRL: uRL,
                isEmailSubscription: isEmailSubscription,
                isNewsLetterSubscription: isNewsLetterSubscription,
                image: image,
                quickFullName: quickFullName,
                quickTitle: quickTitle,
                quickCompanyName: quickCompanyName,
                quickAddress1: quickAddress1,
                quickAddress2: quickAddress2,
                quickAddress3: quickAddress3,
                quickPhone: quickPhone,
                quickFax: quickFax,
                quickEmail: quickEmail,
                quickWebsite: quickWebsite,
                quickCompMessage: quickCompMessage,
                questionId: questionId,
                isApprover: isApprover,
                isWebAccess: isWebAccess,
                isPlaceOrder: isPlaceOrder,
                creditLimit: creditLimit,
                isArchived: isArchived,
                contactRoleId: contactRoleId,
                territoryId: territoryId,
                claimIdentifer: claimIdentifer,
                authentifiedBy: authentifiedBy,
                isPayByPersonalCreditCard: isPayByPersonalCreditCard,
                isPricingshown: isPricingshown,
                skypeId: skypeId,
                linkedinURL: linkedinURL,
                facebookURL: facebookURL,
                twitterURL: twitterURL,
                authenticationToken: authenticationToken,
                twitterScreenName: twitterScreenName,
                shippingAddressId: shippingAddressId,
                isUserLoginFirstTime: isUserLoginFirstTime,
                quickMobileNumber: quickMobileNumber,
                quickTwitterId: quickTwitterId,
                quickFacebookId: quickFacebookId,
                quickLinkedInId: quickLinkedInId,
                quickOtherId: quickOtherId,
                pOBoxAddress: pOBoxAddress,
                corporateUnit: corporateUnit,
                officeTradingName: officeTradingName,
                contractorName: contractorName,
                bPayCRN: bPayCRN,
                aBN: aBN,
                aCN: aCN,
                additionalField1: additionalField1,
                additionalField2: additionalField2,
                additionalField3: additionalField3,
                additionalField4: additionalField4,
                additionalField5: additionalField5,
                canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
                canUserEditProfile: canUserEditProfile,
                canPlaceDirectOrder: canPlaceDirectOrder,
                organisationId: organisationId,
                bussinessAddressId: bussinessAddressId,
                fileName: fileName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    ContactId: contactId(),
                    AddressId: addressId(),
                    CompanyId: companyId(),
                    FirstName: firstName(),
                    MiddleName: middleName(),
                    LastName: lastName(),
                    Title: title(),
                    HomeTel1: homeTel1(),
                    HomeTel2: homeTel2(),
                    HomeExtension1: homeExtension1(),
                    HomeExtension2: homeExtension2(),
                    Mobile: mobile(),
                    Email: email(),
                    FAX: fAX(),
                    JobTitle: jobTitle(),
                    DOB: dOB(),
                    Notes: notes(),
                    IsDefaultContact: isDefaultContact() == true ? 1 : 0,
                    HomeAddress1: homeAddress1(),
                    HomeAddress2: homeAddress2(),
                    HomeCity: homeCity(),
                    HomeState: homeState(),
                    HomePostCode: homePostCode(),
                    HomeCountry: homeCountry(),
                    SecretQuestion: secretQuestion(),
                    SecretAnswer: secretAnswer(),
                    Password: password(),
                    URL: uRL(),
                    IsEmailSubscription: isEmailSubscription(),
                    IsNewsLetterSubscription: isNewsLetterSubscription(),
                    ImageBytes: image(),
                    quickFullName: quickFullName(),
                    quickTitle: quickTitle(),
                    quickCompanyName: quickCompanyName(),
                    quickAddress1: quickAddress1(),
                    quickAddress2: quickAddress2(),
                    quickAddress3: quickAddress3(),
                    quickPhone: quickPhone(),
                    quickFax: quickFax(),
                    quickEmail: quickEmail(),
                    quickWebsite: quickWebsite(),
                    quickCompMessage: quickCompMessage(),
                    QuestionId: questionId(),
                    IsApprover: isApprover(),
                    isWebAccess: isWebAccess(),
                    isPlaceOrder: isPlaceOrder(),
                    CreditLimit: creditLimit(),
                    isArchived: isArchived(),
                    ContactRoleId: contactRoleId(),
                    TerritoryId: territoryId(),
                    ClaimIdentifer: claimIdentifer(),
                    AuthentifiedBy: authentifiedBy(),
                    IsPayByPersonalCreditCard: isPayByPersonalCreditCard(),
                    IsPricingshown: isPricingshown(),
                    SkypeId: skypeId(),
                    LinkedinURL: linkedinURL(),
                    FacebookURL: facebookURL(),
                    TwitterURL: twitterURL(),
                    authenticationToken: authenticationToken(),
                    twitterScreenName: twitterScreenName(),
                    ShippingAddressId: shippingAddressId(),
                    isUserLoginFirstTime: isUserLoginFirstTime(),
                    quickMobileNumber: quickMobileNumber(),
                    quickTwitterId: quickTwitterId(),
                    quickFacebookId: quickFacebookId(),
                    quickLinkedInId: quickLinkedInId(),
                    quickOtherId: quickOtherId(),
                    POBoxAddress: pOBoxAddress(),
                    CorporateUnit: corporateUnit(),
                    OfficeTradingName: officeTradingName(),
                    ContractorName: contractorName(),
                    BPayCRN: bPayCRN(),
                    ABN: aBN(),
                    ACN: aCN(),
                    AdditionalField1: additionalField1(),
                    AdditionalField2: additionalField2(),
                    AdditionalField3: additionalField3(),
                    AdditionalField4: additionalField4(),
                    AdditionalField5: additionalField5(),
                    canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval(),
                    CanUserEditProfile: canUserEditProfile(),
                    canPlaceDirectOrder: !isPayByPersonalCreditCard() ? false : canPlaceDirectOrder(),
                    OrganisationId: organisationId(),
                    BussinessAddressId: bussinessAddressId(),
                    FileName: fileName(),
                    ScopVariables: []
                    //BussinessAddress: bussinessAddress() != undefined ? bussinessAddress().convertToServerData(): null,
                    //ShippingAddress: shippingAddress() != undefined ? shippingAddress().convertToServerData() : null,
                };
            },
             update = function (source) {
                 contactId(source.contactId());
                 contactId(source.contactId());
                 addressId(source.addressId());
                 companyId(source.companyId());
                 firstName(source.firstName());
                 middleName(source.middleName());
                 lastName(source.lastName());
                 title(source.title());
                 homeTel1(source.homeTel1());
                 homeTel2(source.homeTel2());
                 homeExtension1(source.homeExtension1());
                 homeExtension2(source.homeExtension2());
                 mobile(source.mobile());
                 email(source.email());
                 fAX(source.fAX());
                 jobTitle(source.jobTitle());
                 dOB(source.dOB());
                 notes(source.notes());
                 isDefaultContact(source.isDefaultContact());
                 homeAddress1(source.homeAddress1());
                 homeAddress2(source.homeAddress2());
                 homeCity(source.homeCity());
                 homeState(source.homeState());
                 homePostCode(source.homePostCode());
                 homeCountry(source.homeCountry());
                 secretQuestion(source.secretQuestion());
                 secretAnswer(source.secretAnswer());
                 password(source.password());
                 uRL(source.uRL());
                 isEmailSubscription(source.isEmailSubscription());
                 isNewsLetterSubscription(source.isNewsLetterSubscription());
                 image(source.image());
                 quickFullName(source.quickFullName());
                 quickTitle(source.quickTitle());
                 quickCompanyName(source.quickCompanyName());
                 quickAddress1(source.quickAddress1());
                 quickAddress2(source.quickAddress2());
                 quickAddress3(source.quickAddress3());
                 quickPhone(source.quickPhone());
                 quickFax(source.quickFax());
                 quickEmail(source.quickEmail());
                 quickWebsite(source.quickWebsite());
                 quickCompMessage(source.quickCompMessage());
                 questionId(source.questionId());
                 isApprover(source.isApprover());
                 isWebAccess(source.isWebAccess());
                 isPlaceOrder(source.isPlaceOrder());
                 creditLimit(source.creditLimit());
                 isArchived(source.isArchived());
                 contactRoleId(source.contactRoleId());
                 territoryId(source.territoryId());
                 claimIdentifer(source.claimIdentifer());
                 authentifiedBy(source.authentifiedBy());
                 isPayByPersonalCreditCard(source.isPayByPersonalCreditCard());
                 isPricingshown(source.isPricingshown());
                 skypeId(source.skypeId());
                 linkedinURL(source.linkedinURL());
                 facebookURL(source.facebookURL());
                 twitterURL(source.twitterURL());
                 authenticationToken(source.authenticationToken());
                 twitterScreenName(source.twitterScreenName());
                 shippingAddressId(source.shippingAddressId());
                 isUserLoginFirstTime(source.isUserLoginFirstTime());
                 quickMobileNumber(source.quickMobileNumber());
                 quickTwitterId(source.quickTwitterId());
                 quickFacebookId(source.quickFacebookId());
                 quickLinkedInId(source.quickLinkedInId());
                 quickOtherId(source.quickOtherId());
                 pOBoxAddress(source.pOBoxAddress());
                 corporateUnit(source.corporateUnit());
                 officeTradingName(source.officeTradingName());
                 contractorName(source.contractorName());
                 bPayCRN(source.bPayCRN());
                 aBN(source.aBN());
                 aCN(source.aCN());
                 additionalField1(source.additionalField1());
                 additionalField2(source.additionalField2());
                 additionalField3(source.additionalField3());
                 additionalField4(source.additionalField4());
                 additionalField5(source.additionalField5());
                 canUserPlaceOrderWithoutApproval(source.canUserPlaceOrderWithoutApproval());
                 canUserEditProfile(source.canUserEditProfile());
                 canPlaceDirectOrder(source.canPlaceDirectOrder());
                 organisationId(source.organisationId());
                 bussinessAddressId(source.bussinessAddressId());
                 confirmPassword(source.confirmPassword());
                 roleName(source.roleName());
                 fileName(source.fileName());
                 bussinessAddress(source.bussinessAddress());
                 shippingAddress(source.shippingAddress());
                 stateName(source.stateName());
                 companyContactVariables(source.companyContactVariables());
             },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            contactId: contactId,
            addressId: addressId,
            companyId: companyId,
            firstName: firstName,
            middleName: middleName,
            lastName: lastName,
            title: title,
            homeTel1: homeTel1,
            homeTel2: homeTel2,
            homeExtension1: homeExtension1,
            homeExtension2: homeExtension2,
            mobile: mobile,
            email: email,
            fAX: fAX,
            jobTitle: jobTitle,
            dOB: dOB,
            notes: notes,
            isDefaultContact: isDefaultContact,
            homeAddress1: homeAddress1,
            homeAddress2: homeAddress2,
            homeCity: homeCity,
            homeState: homeState,
            homePostCode: homePostCode,
            homeCountry: homeCountry,
            secretQuestion: secretQuestion,
            secretAnswer: secretAnswer,
            password: password,
            uRL: uRL,
            isEmailSubscription: isEmailSubscription,
            isNewsLetterSubscription: isNewsLetterSubscription,
            image: image,
            quickFullName: quickFullName,
            quickTitle: quickTitle,
            quickCompanyName: quickCompanyName,
            quickAddress1: quickAddress1,
            quickAddress2: quickAddress2,
            quickAddress3: quickAddress3,
            quickPhone: quickPhone,
            quickFax: quickFax,
            quickEmail: quickEmail,
            quickWebsite: quickWebsite,
            quickCompMessage: quickCompMessage,
            questionId: questionId,
            isApprover: isApprover,
            isWebAccess: isWebAccess,
            isPlaceOrder: isPlaceOrder,
            creditLimit: creditLimit,
            isArchived: isArchived,
            contactRoleId: contactRoleId,
            territoryId: territoryId,
            claimIdentifer: claimIdentifer,
            authentifiedBy: authentifiedBy,
            isPayByPersonalCreditCard: isPayByPersonalCreditCard,
            isPricingshown: isPricingshown,
            skypeId: skypeId,
            linkedinURL: linkedinURL,
            facebookURL: facebookURL,
            twitterURL: twitterURL,
            authenticationToken: authenticationToken,
            twitterScreenName: twitterScreenName,
            shippingAddressId: shippingAddressId,
            isUserLoginFirstTime: isUserLoginFirstTime,
            quickMobileNumber: quickMobileNumber,
            quickTwitterId: quickTwitterId,
            quickFacebookId: quickFacebookId,
            quickLinkedInId: quickLinkedInId,
            quickOtherId: quickOtherId,
            pOBoxAddress: pOBoxAddress,
            corporateUnit: corporateUnit,
            officeTradingName: officeTradingName,
            contractorName: contractorName,
            bPayCRN: bPayCRN,
            aBN: aBN,
            aCN: aCN,
            additionalField1: additionalField1,
            additionalField2: additionalField2,
            additionalField3: additionalField3,
            additionalField4: additionalField4,
            additionalField5: additionalField5,
            canUserPlaceOrderWithoutApproval: canUserPlaceOrderWithoutApproval,
            canUserEditProfile: canUserEditProfile,
            canPlaceDirectOrder: canPlaceDirectOrder,
            organisationId: organisationId,
            bussinessAddressId: bussinessAddressId,
            confirmPassword: confirmPassword,
            roleName: roleName,
            fileName: fileName,
            bussinessAddress: bussinessAddress,
            shippingAddress: shippingAddress,
            stateName: stateName,
            companyContactVariables: companyContactVariables,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            update: update,
            reset: reset
        };
        return self;
    };
    CompanyContact.CreateFromClientModel = function (source) {
        return new CompanyContact(
            source.contactId,
            source.addressId,
            source.companyId,
            source.firstName,
            source.middleName,
            source.lastName,
            source.title,
            source.homeTel1,
            source.homeTel2,
            source.homeExtension1,
            source.homeExtension2,
            source.mobile,
            source.email,
            source.fAX,
            source.jobTitle,
            source.dOB,
            source.notes,
            source.isDefaultContact,
            source.homeAddress1,
            source.homeAddress2,
            source.homeCity,
            source.homeState,
            source.homePostCode,
            source.homeCountry,
            source.secretQuestion,
            source.secretAnswer,
            source.password,
            source.uRL,
            source.isEmailSubscription,
            source.isNewsLetterSubscription,
            source.image,
            source.quickFullName,
            source.quickTitle,
            source.quickCompanyName,
            source.quickAddress1,
            source.quickAddress2,
            source.quickAddress3,
            source.quickPhone,
            source.quickFax,
            source.quickEmail,
            source.quickWebsite,
            source.quickCompMessage,
            source.questionId,
            source.isApprover,
            source.isWebAccess,
            source.isPlaceOrder,
            source.creditLimit,
            source.isArchived,
            source.contactRoleId,
            source.territoryId,
            source.claimIdentifer,
            source.authentifiedBy,
            source.isPayByPersonalCreditCard,
            source.isPricingshown,
            source.skypeId,
            source.linkedinURL,
            source.facebookURL,
            source.twitterURL,
            source.authenticationToken,
            source.twitterScreenName,
            source.shippingAddressId,
            source.isUserLoginFirstTime,
            source.quickMobileNumber,
            source.quickTwitterId,
            source.quickFacebookId,
            source.quickLinkedInId,
            source.quickOtherId,
            source.pOBoxAddress,
            source.corporateUnit,
            source.officeTradingName,
            source.contractorName,
            source.bPayCRN,
            source.aBN,
            source.aCN,
            source.additionalField1,
            source.additionalField2,
            source.additionalField3,
            source.additionalField4,
            source.additionalField5,
            source.canUserPlaceOrderWithoutApproval,
            source.canUserEditProfile,
            source.canPlaceDirectOrder,
            source.organisationId,
            source.BussinessAddressId,
            source.FileName
        );
    };
    CompanyContact.Create = function (source) {
        var companyContact = new CompanyContact(
            source.ContactId,
            source.AddressId,
            source.CompanyId,
            source.FirstName,
            source.MiddleName,
            source.LastName,
            source.Title,
            source.HomeTel1,
            source.HomeTel2,
            source.HomeExtension1,
            source.HomeExtension2,
            source.Mobile,
            source.Email,
            source.FAX,
            source.JobTitle,
            source.DOB,
            source.Notes,
            source.IsDefaultContact,
            source.HomeAddress1,
            source.HomeAddress2,
            source.HomeCity,
            source.HomeState,
            source.HomePostCode,
            source.HomeCountry,
            source.SecretQuestion,
            source.SecretAnswer,
            source.Password,
            source.URL,
            source.IsEmailSubscription,
            source.IsNewsLetterSubscription,
            source.ImageSource,
            source.quickFullName,
            source.quickTitle,
            source.quickCompanyName,
            source.quickAddress1,
            source.quickAddress2,
            source.quickAddress3,
            source.quickPhone,
            source.quickFax,
            source.quickEmail,
            source.quickWebsite,
            source.quickCompMessage,
            source.QuestionId,
            source.IsApprover,
            source.isWebAccess,
            source.isPlaceOrder,
            source.CreditLimit,
            source.isArchived,
            source.ContactRoleId,
            source.TerritoryId,
            source.ClaimIdentifer,
            source.AuthentifiedBy,
            source.IsPayByPersonalCreditCard,
            source.IsPricingshown,
            source.SkypeId,
            source.LinkedinURL,
            source.FacebookURL,
            source.TwitterURL,
            source.authenticationToken,
            source.twitterScreenName,
            source.ShippingAddressId,
            source.isUserLoginFirstTime,
            source.quickMobileNumber,
            source.quickTwitterId,
            source.quickFacebookId,
            source.quickLinkedInId,
            source.quickOtherId,
            source.POBoxAddress,
            source.CorporateUnit,
            source.OfficeTradingName,
            source.ContractorName,
            source.BPayCRN,
            source.ABN,
            source.ACN,
            source.AdditionalField1,
            source.AdditionalField2,
            source.AdditionalField3,
            source.AdditionalField4,
            source.AdditionalField5,
            source.canUserPlaceOrderWithoutApproval,
            source.CanUserEditProfile,
            source.canPlaceDirectOrder,
            source.OrganisationId,
            //source.BussinessAddressId,
            source.AddressId,
            source.RoleName,
            source.FileName
        );
        return companyContact;
    };

    // #endregion ________________COMPANY CONTACT ___________________

    // #region __________________  R O L E   ______________________

    // ReSharper disable once InconsistentNaming
    var Role = function (specifiedRoleId, specifiedRoleName) {

        var self,
            roleId = ko.observable(specifiedRoleId),
            roleName = ko.observable(specifiedRoleName),            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                roleId: roleId,
                roleName: roleName,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    RoleId: roleId(),
                    RoleName: roleName()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            roleId: roleId,
            roleName: roleName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    Role.CreateFromClientModel = function (source) {
        return new Role(
            source.roleId,
            source.rolesName
            );
    };
    Role.Create = function (source) {
        var role = new Role(
            source.ContactRoleId,
            source.ContactRoleName
            );
        return role;
    };
    // #endregion __________________  R O L E   ______________________

    // #region __________________  R E G I S  T R A T I O N   Q U E S T I O N  ______________________

    // ReSharper disable once InconsistentNaming
    var RegistrationQuestion = function (specifiedQuestionId, specifiedQuestion) {

        var self,
            questionId = ko.observable(specifiedQuestionId),
            question = ko.observable(specifiedQuestion),            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                questionId: questionId,
                question: question,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    QuestionId: questionId(),
                    Question: question()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            questionId: questionId,
            question: question,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    RegistrationQuestion.CreateFromClientModel = function (source) {
        return new RegistrationQuestion(
            source.questionId,
            source.question
            );
    };
    RegistrationQuestion.Create = function (source) {
        var registrationQuestion = new RegistrationQuestion(
            source.QuestionId,
            source.Question
            );
        return registrationQuestion;
    };
    // #endregion __________________  R E G I S  T R A T I O N   Q U E S T I O N  ______________________

    // #region ______________________    P A Y M E N T   G A T E W A Y S _________________________________

    // ReSharper disable once InconsistentNaming
    // ReSharper restore InconsistentNaming
    var PaymentGateway = function (specifiedPaymentGatewayId, specifiedBusinessEmail, specifiedIdentityToken, specifiedIsActive, specifiedCompanyId, specifiedPaymentMethodId, specifiedSecureHash, specifiedPaymentMethodName) {
        var self,
            paymentGatewayId = ko.observable(specifiedPaymentGatewayId),
            businessEmail = ko.observable(specifiedBusinessEmail).extend({ required: true }),//marchant id
            identityToken = ko.observable(specifiedIdentityToken).extend({ required: true }),//access code 
            isActive = ko.observable(specifiedIsActive),
            companyId = ko.observable(specifiedCompanyId),
            paymentMethodId = ko.observable(specifiedPaymentMethodId),
            secureHash = ko.observable(specifiedSecureHash),// secure hash
            paymentMethodName = ko.observable(specifiedPaymentMethodName),
            // Errors
            errors = ko.validation.group({
                paymentMethodId: paymentMethodId,
                businessEmail: businessEmail,
                //identityToken: identityToken
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                paymentGatewayId: paymentGatewayId,
                businessEmail: businessEmail,
                identityToken: identityToken,
                companyId: companyId,
                isActive: isActive,
                paymentMethodId: paymentMethodId,
                secureHash: secureHash,
                paymentMethodName: paymentMethodName
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    PaymentGatewayId: paymentGatewayId(),
                    BusinessEmail: businessEmail(),
                    IdentityToken: identityToken(),
                    IsActive: isActive(),
                    CompanyId: companyId(),
                    SecureHash: secureHash(),
                    PaymentMethodId: paymentMethodId()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            paymentGatewayId: paymentGatewayId,
            businessEmail: businessEmail,
            identityToken: identityToken,
            companyId: companyId,
            isActive: isActive,
            paymentMethodId: paymentMethodId,
            secureHash: secureHash,
            paymentMethodName: paymentMethodName,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    PaymentGateway.CreateFromClientModel = function (source) {
        return new PaymentGateway(
            source.paymentGatewayId,
            source.businessEmail,
            source.identityToken,
            source.isActive,
            source.companyId,
            source.paymentMethodId,
            source.secureHash,
            source.paymentMethodName
        );
    };
    PaymentGateway.Create = function (source) {

        var paymentGateway = new PaymentGateway(
            source.PaymentGatewayId,
            source.BusinessEmail,
            source.IdentityToken,
            source.IsActive,
            source.CompanyId,
            source.PaymentMethodId,
            source.SecureHash,
            source.PaymentMethodName
        );
        return paymentGateway;
    };
    // #endregion ______________________    P A Y M E N T   G A T E W A Y S ______________________________

    // #region __________________  Widget   ______________________

    // ReSharper disable once InconsistentNaming
    var Widget = function (specifiedWidgetId, specifiedWidgetName, specifiedWidgetCode, specifiedWidgetControlName) {

        var self,
            widgetId = ko.observable(specifiedWidgetId),
            widgetName = ko.observable(specifiedWidgetName),
            widgetCode = ko.observable(specifiedWidgetCode),
            widgetControlName = ko.observable(specifiedWidgetControlName);
        //id = ko.computed(function () {
        //    ist.stores.viewModel.selectedWidget(widgetId);

        //}, this);

        self = {
            widgetId: widgetId,
            widgetName: widgetName,
            widgetCode: widgetCode,
            widgetControlName: widgetControlName,
        };
        return self;
    };

    Widget.Create = function (source) {
        return new Widget(
             source.WidgetId,
             source.WidgetName,
             source.WidgetCode,
             source.WidgetControlName
               );

    };
    // #endregion __________________  Widget   ______________________

    // #region __________________  CMS Skin Page Widget   ______________________

    // ReSharper disable once InconsistentNaming
    var CmsSkingPageWidget = function (specifiedPageWidgetId, specifiedPageId, specifiedWidgetId, specifiedSequence, specifiedHtml, specifiedWidgetName, specifiedCompanyId) {

        var self,
            pageWidgetId = ko.observable(specifiedPageWidgetId),
            pageId = ko.observable(specifiedPageId),
            widgetId = ko.observable(specifiedWidgetId),
            sequence = ko.observable(specifiedSequence),
            htmlData = ko.observable(specifiedHtml),
            widgetName = ko.observable(specifiedWidgetName),
            companyId = ko.observable(specifiedCompanyId),
            cmsSkinPageWidgetParam = ko.observable(new CmsSkinPageWidgetParam()),
        //Convert To Server
        convertToServerData = function () {
            return {
                PageWidgetId: pageWidgetId() === undefined ? 0 : pageWidgetId(),
                PageId: pageId(),
                WidgetId: widgetId(),
                Sequence: sequence(),
                CompanyId: companyId(),
                CmsSkinPageWidgetParam: cmsSkinPageWidgetParam().convertToServerData(),
                CmsSkinPageWidgetParams: []
            };
        };
        self = {
            pageWidgetId: pageWidgetId,
            pageId: pageId,
            widgetId: widgetId,
            sequence: sequence,
            htmlData: htmlData,
            widgetName: widgetName,
            companyId: companyId,
            cmsSkinPageWidgetParam: cmsSkinPageWidgetParam,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    CmsSkingPageWidget.Create = function (source) {
        return new CmsSkingPageWidget(
             source.PageWidgetId,
             source.PageId,
             source.WidgetId,
             source.Sequence,
             source.Html,
             source.WidgetName
               );

    };
    CmsPageWithWidgetList = function () {
        var self,
            pageId = ko.observable(),
            widgets = ko.observableArray([]),
            //Convert To Server
            convertToServerData = function () {
                return {
                    PageId: pageId(),
                    CmsSkinPageWidgets: [],

                };
            };
        self = {
            pageId: pageId,
            widgets: widgets,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    // #endregion __________________  CMS Skin Page Widget   ______________________

    // #region __________________  P A Y M E N T   M E T H O D   ______________________

    //// __________________ Cms Skin Page Widget Param  ______________________//
    var CmsSkinPageWidgetParam = function (specifiedPageWidgetParamId, specifiedPageWidgetId, specifiedParamValue) {

        var self,
            pageWidgetParamId = ko.observable(specifiedPageWidgetParamId),
            pageWidgetId = ko.observable(specifiedPageWidgetId),
            paramValue = ko.observable(specifiedParamValue !== undefined ? specifiedParamValue : ""),
            editorId = ko.observable(),
              //Convert To Server
        convertToServerData = function () {
            return {
                PageWidgetParamId: pageWidgetParamId() === undefined ? 0 : pageWidgetParamId(),
                PageWidgetId: pageWidgetId() < 0 ? 0 : pageWidgetId(),
                ParamValue: paramValue(),
            };
        };
        self = {
            pageWidgetParamId: pageWidgetParamId,
            pageWidgetId: pageWidgetId,
            paramValue: paramValue,
            editorId: editorId,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    CmsSkinPageWidgetParam.Create = function (source) {
        return new CmsSkinPageWidgetParam(
             source.PageWidgetParamId,
             source.PageWidgetId,
             source.ParamValue);
    };

    // ReSharper disable once InconsistentNaming
    var PaymentMethod = function (specifiedPaymentMethodId, specifiedMethodName, specifiedIsActive) {

        var self,
            paymentMethodId = ko.observable(specifiedPaymentMethodId),
            methodName = ko.observable(specifiedMethodName),
            isActive = ko.observable(specifiedIsActive),
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),//PaymentMethodId  MethodName  IsActive


            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                paymentMethodId: paymentMethodId,
                methodName: methodName,
                isActive: isActive,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    QuestionId: paymentMethodId(),
                    MethodName: methodName(),
                    IsActive: isActive()
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            paymentMethodId: paymentMethodId,
            methodName: methodName,
            isActive: isActive,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    PaymentMethod.CreateFromClientModel = function (source) {
        return new PaymentMethod(
            source.paymentMethodId,
            source.methodName,
            source.isActive
            );
    };
    PaymentMethod.Create = function (source) {
        var paymentMethod = new PaymentMethod(
            source.PaymentMethodId,
            source.MethodName,
            source.IsActive
            );
        return paymentMethod;
    };
    // #endregion __________________  P A Y M E N T   M E T H O D   ______________________

    // #region ___________________ P R O D U C T     C A T E G O R Y    L I S T   V I E W _____________________
    // ReSharper disable once InconsistentNaming
    var ProductCategoryListView = function (specifiedProductCategoryId, specifiedCategoryName, specifiedContentType, specifiedLockedBy, specifiedParentCategoryId) {
        var // Unique key
            productCategoryId = ko.observable(specifiedProductCategoryId || 0),
            // CategoryName
            categoryName = ko.observable(specifiedCategoryName || undefined),
            // ContentType
            contentType = ko.observable(specifiedContentType || undefined),
            // LockedBy
            lockedBy = ko.observable(specifiedLockedBy || undefined),

            // Product Code
            parentCategoryId = ko.observable(specifiedParentCategoryId || undefined),
            errors = ko.validation.group({

            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                productCategoryId: productCategoryId,
                categoryName: categoryName,
                contentType: contentType,
                lockedBy: lockedBy,
                parentCategoryId: parentCategoryId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            });
        return {
            productCategoryId: productCategoryId,
            categoryName: categoryName,
            contentType: contentType,
            lockedBy: lockedBy,
            parentCategoryId: parentCategoryId,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
        };
    };
    ProductCategoryListView.CreateFromClientModel = function (source) {
        return new ProductCategoryListView(
            source.productCategoryId,
            source.categoryName,
            source.contentType,
            source.lockedBy,
            source.parentCategoryId
            );
    };
    ProductCategoryListView.Create = function (source) {
        var productCategoryListViewItem = new ProductCategoryListView(
            source.ProductCategoryId,
            source.CategoryName,
            source.ContentType,
            source.LockedBy,
            source.ParentCategoryId
            );
        return productCategoryListViewItem;
    };
    // #endregion ___________________ P R O D U C T     C A T E G O R Y    L I S T   V I E W _____________________

    // #region ____________________P R O D U C T   C A T E G O R Y  _________________________________

    // ReSharper disable once InconsistentNaming
    var ProductCategory = function (specifiedProductCategoryId, specifiedCategoryName, specifiedContentType, specifiedDescription1, specifiedDescription2, specifiedLockedBy,
        specifiedCompanyId, specifiedParentCategoryId, specifiedDisplayOrder, specifiedImagePath, specifiedThumbnailPath, specifiedisEnabled, specifiedisMarketPlace, specifiedTemplateDesignerMappedCategoryName,
        specifiedisArchived, specifiedisPublished, specifiedTrimmedWidth, specifiedTrimmedHeight, specifiedisColorImposition, specifiedisOrderImposition, specifiedisLinkToTemplates,
        specifiedSides, specifiedApplySizeRestrictions, specifiedApplyFoldLines, specifiedWidthRestriction, specifiedHeightRestriction, specifiedCategoryTypeId, specifiedRegionId,
        specifiedZoomFactor, specifiedScaleFactor, specifiedisShelfProductCategory, specifiedMetaKeywords, specifiedMetaDescription, specifiedMetaTitle, specifiedOrganisationId,
        specifiedSubCategoryDisplayMode1, specifiedSubCategoryDisplayMode2, specifiedSubCategoryDisplayColumns, specifiedCategoryURLText, specifiedMetaOverride, specifiedShortDescription,
        specifiedSecondaryDescription, specifiedDefaultSortBy, specifiedProductsDisplayColumns, specifiedProductsDisplayRows, specifiedIsDisplayFeaturedproducts, specifiedIsShowAvailablity,
        specifiedIsShowRewardPoints, specifiedIsShowListPrice, specifiedIsShowSalePrice, specifiedIsShowStockStatus, specifiedIsShowProductDescription, specifiedIsShowProductShortDescription,
        specifiedProductCategoryThumbnailFileBinary, specifiedProductCategoryImageFileBinary
    ) {
        var productCategoryId = ko.observable(specifiedProductCategoryId),
            categoryName = ko.observable(specifiedCategoryName).extend({ required: true }),
            contentType = ko.observable(specifiedContentType),
            description1 = ko.observable(specifiedDescription1),
            description2 = ko.observable(specifiedDescription2),
            lockedBy = ko.observable(specifiedLockedBy),
            companyId = ko.observable(specifiedCompanyId),
            parentCategoryId = ko.observable(specifiedParentCategoryId),
            displayOrder = ko.observable(specifiedDisplayOrder),
            imagePath = ko.observable(specifiedImagePath),
            thumbnailPath = ko.observable(specifiedThumbnailPath),
            isEnabled = ko.observable(specifiedisEnabled),
            isMarketPlace = ko.observable(specifiedisMarketPlace),
            templateDesignerMappedCategoryName = ko.observable(specifiedTemplateDesignerMappedCategoryName),
            isArchived = ko.observable(specifiedisArchived),
            isPublished = ko.observable(specifiedisPublished),
            trimmedWidth = ko.observable(specifiedTrimmedWidth),
            trimmedHeight = ko.observable(specifiedTrimmedHeight),
            isColorImposition = ko.observable(specifiedisColorImposition),
            isOrderImposition = ko.observable(specifiedisOrderImposition),
            isLinkToTemplates = ko.observable(specifiedisLinkToTemplates),
            sides = ko.observable(specifiedSides),
            applySizeRestrictions = ko.observable(specifiedApplySizeRestrictions),
            applyFoldLines = ko.observable(specifiedApplyFoldLines),
            widthRestriction = ko.observable(specifiedWidthRestriction),
            heightRestriction = ko.observable(specifiedHeightRestriction),
            categoryTypeId = ko.observable(specifiedCategoryTypeId),
            regionId = ko.observable(specifiedRegionId),
            zoomFactor = ko.observable(specifiedZoomFactor),
            scaleFactor = ko.observable(specifiedScaleFactor),
            isShelfProductCategory = ko.observable(specifiedisShelfProductCategory),
            metaKeywords = ko.observable(specifiedMetaKeywords),
            metaDescription = ko.observable(specifiedMetaDescription),
            metaTitle = ko.observable(specifiedMetaTitle),
            organisationId = ko.observable(specifiedOrganisationId),
            subCategoryDisplayMode1 = ko.observable(specifiedSubCategoryDisplayMode1),
            subCategoryDisplayMode2 = ko.observable(specifiedSubCategoryDisplayMode2),
            subCategoryDisplayColumns = ko.observable(specifiedSubCategoryDisplayColumns),
            categoryURLText = ko.observable(specifiedCategoryURLText),
            metaOverride = ko.observable(specifiedMetaOverride),
            shortDescription = ko.observable(specifiedShortDescription),
            secondaryDescription = ko.observable(specifiedSecondaryDescription),
            defaultSortBy = ko.observable(specifiedDefaultSortBy),
            productsDisplayColumns = ko.observable(specifiedProductsDisplayColumns),
            productsDisplayRows = ko.observable(specifiedProductsDisplayRows),
            isDisplayFeaturedproducts = ko.observable(specifiedIsDisplayFeaturedproducts),
            isShowAvailablity = ko.observable(specifiedIsShowAvailablity),
            isShowRewardPoints = ko.observable(specifiedIsShowRewardPoints),
            isShowListPrice = ko.observable(specifiedIsShowListPrice),
            isShowSalePrice = ko.observable(specifiedIsShowSalePrice),
            isShowStockStatus = ko.observable(specifiedIsShowStockStatus),
            isShowProductDescription = ko.observable(specifiedIsShowProductDescription),
            isShowProductShortDescription = ko.observable(specifiedIsShowProductShortDescription),
            productCategoryThumbnailFileBinary = ko.observable(specifiedProductCategoryThumbnailFileBinary),
            productCategoryThumbnailName = ko.observable(),
            productCategoryImageName = ko.observable(),
            productCategoryImageFileBinary = ko.observable(specifiedProductCategoryImageFileBinary),
            errors = ko.validation.group({
                categoryName: categoryName
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                productCategoryId: productCategoryId,
                categoryName: categoryName,
                contentType: contentType,
                description1: description1,
                description2: description2,
                lockedBy: lockedBy,
                companyId: companyId,
                parentCategoryId: parentCategoryId,
                displayOrder: displayOrder,
                imagePath: imagePath,
                thumbnailPath: thumbnailPath,
                isEnabled: isEnabled,
                isMarketPlace: isMarketPlace,
                templateDesignerMappedCategoryName: templateDesignerMappedCategoryName,
                isArchived: isArchived,
                isPublished: isPublished,
                trimmedWidth: trimmedWidth,
                trimmedHeight: trimmedHeight,
                isColorImposition: isColorImposition,
                isOrderImposition: isOrderImposition,
                isLinkToTemplates: isLinkToTemplates,
                sides: sides,
                applySizeRestrictions: applySizeRestrictions,
                applyFoldLines: applyFoldLines,
                widthRestriction: widthRestriction,
                heightRestriction: heightRestriction,
                categoryTypeId: categoryTypeId,
                regionId: regionId,
                zoomFactor: zoomFactor,
                scaleFactor: scaleFactor,
                isShelfProductCategory: isShelfProductCategory,
                metaKeywords: metaKeywords,
                metaDescription: metaDescription,
                metaTitle: metaTitle,
                organisationId: organisationId,
                subCategoryDisplayMode1: subCategoryDisplayMode1,
                subCategoryDisplayMode2: subCategoryDisplayMode2,
                subCategoryDisplayColumns: subCategoryDisplayColumns,
                categoryURLText: categoryURLText,
                metaOverride: metaOverride,
                shortDescription: shortDescription,
                secondaryDescription: secondaryDescription,
                defaultSortBy: defaultSortBy,
                productsDisplayColumns: productsDisplayColumns,
                productsDisplayRows: productsDisplayRows,
                isDisplayFeaturedproducts: isDisplayFeaturedproducts,
                isShowAvailablity: isShowAvailablity,
                isShowRewardPoints: isShowRewardPoints,
                isShowListPrice: isShowListPrice,
                isShowSalePrice: isShowSalePrice,
                isShowStockStatus: isShowStockStatus,
                isShowProductDescription: isShowProductDescription,
                isShowProductShortDescription: isShowProductShortDescription,
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                var result = {
                    //RoleId: roleId(),
                    //RoleName: roleName()
                    ProductCategoryId: productCategoryId(),
                    CategoryName: categoryName(),
                    ContentType: contentType(),
                    Description1: description1(),
                    Description2: description2(),
                    LockedBy: lockedBy(),
                    CompanyId: companyId(),
                    ParentCategoryId: parentCategoryId(),
                    DisplayOrder: displayOrder(),
                    ImagePath: imagePath(),
                    ThumbnailPath: thumbnailPath(),
                    //isEnabled: isEnabled(),
                    isEnabled: isEnabled(),
                    isMarketPlace: isMarketPlace(),
                    TemplateDesignerMappedCategoryName: templateDesignerMappedCategoryName(),
                    isArchived: isEnabled() == true ? false : true,
                    isPublished: isPublished(),
                    TrimmedWidth: trimmedWidth(),
                    TrimmedHeight: trimmedHeight(),
                    isColorImposition: isColorImposition(),
                    isOrderImposition: isOrderImposition(),
                    isLinkToTemplates: isLinkToTemplates(),
                    Sides: sides(),
                    ApplySizeRestrictions: applySizeRestrictions(),
                    ApplyFoldLines: applyFoldLines(),
                    WidthRestriction: widthRestriction(),
                    HeightRestriction: heightRestriction(),
                    CategoryTypeId: categoryTypeId(),
                    RegionId: regionId(),
                    ZoomFactor: zoomFactor(),
                    ScaleFactor: scaleFactor(),
                    isShelfProductCategory: isShelfProductCategory(),
                    MetaKeywords: metaKeywords(),
                    MetaDescription: metaDescription(),
                    MetaTitle: metaTitle(),
                    OrganisationId: organisationId(),
                    SubCategoryDisplayMode1: subCategoryDisplayMode1(),
                    SubCategoryDisplayMode2: subCategoryDisplayMode2(),
                    SubCategoryDisplayColumns: subCategoryDisplayColumns(),
                    CategoryURLText: categoryURLText(),
                    MetaOverride: metaOverride(),
                    ShortDescription: shortDescription(),
                    SecondaryDescription: secondaryDescription(),
                    DefaultSortBy: defaultSortBy(),
                    ProductsDisplayColumns: productsDisplayColumns(),
                    ProductsDisplayRows: productsDisplayRows(),
                    IsDisplayFeaturedproducts: isDisplayFeaturedproducts(),
                    IsShowAvailablity: isShowAvailablity(),
                    IsShowRewardPoints: isShowRewardPoints(),
                    IsShowListPrice: isShowListPrice(),
                    IsShowSalePrice: isShowSalePrice(),
                    IsShowStockStatus: isShowStockStatus(),
                    IsShowProductDescription: isShowProductDescription(),
                    IsShowProductShortDescription: isShowProductShortDescription(),
                };
                //productCategoryThumbnailName: productCategoryThumbnailName,
                //productCategoryImageName: productCategoryImageName,
                result.ThumbnailName = productCategoryThumbnailName() === undefined ? null : productCategoryThumbnailName();
                result.ImageName = productCategoryImageName() === undefined ? null : productCategoryImageName();
                result.ThumbnailBytes = productCategoryThumbnailFileBinary() === undefined ? null : productCategoryThumbnailFileBinary();
                result.ImageBytes = productCategoryImageFileBinary() === undefined ? null : productCategoryImageFileBinary();

                return result;
            };
        return {
            productCategoryId: productCategoryId,
            categoryName: categoryName,
            contentType: contentType,
            description1: description1,
            description2: description2,
            lockedBy: lockedBy,
            companyId: companyId,
            parentCategoryId: parentCategoryId,
            displayOrder: displayOrder,
            imagePath: imagePath,
            thumbnailPath: thumbnailPath,
            isEnabled: isEnabled,
            isMarketPlace: isMarketPlace,
            templateDesignerMappedCategoryName: templateDesignerMappedCategoryName,
            isArchived: isArchived,
            isPublished: isPublished,
            trimmedWidth: trimmedWidth,
            trimmedHeight: trimmedHeight,
            isColorImposition: isColorImposition,
            isOrderImposition: isOrderImposition,
            isLinkToTemplates: isLinkToTemplates,
            sides: sides,
            applySizeRestrictions: applySizeRestrictions,
            applyFoldLines: applyFoldLines,
            widthRestriction: widthRestriction,
            heightRestriction: heightRestriction,
            categoryTypeId: categoryTypeId,
            regionId: regionId,
            zoomFactor: zoomFactor,
            scaleFactor: scaleFactor,
            isShelfProductCategory: isShelfProductCategory,
            metaKeywords: metaKeywords,
            metaDescription: metaDescription,
            metaTitle: metaTitle,
            organisationId: organisationId,
            subCategoryDisplayMode1: subCategoryDisplayMode1,
            subCategoryDisplayMode2: subCategoryDisplayMode2,
            subCategoryDisplayColumns: subCategoryDisplayColumns,
            categoryURLText: categoryURLText,
            metaOverride: metaOverride,
            shortDescription: shortDescription,
            secondaryDescription: secondaryDescription,
            defaultSortBy: defaultSortBy,
            productsDisplayColumns: productsDisplayColumns,
            productsDisplayRows: productsDisplayRows,
            isDisplayFeaturedproducts: isDisplayFeaturedproducts,
            isShowAvailablity: isShowAvailablity,
            isShowRewardPoints: isShowRewardPoints,
            isShowListPrice: isShowListPrice,
            isShowSalePrice: isShowSalePrice,
            isShowStockStatus: isShowStockStatus,
            isShowProductDescription: isShowProductDescription,
            isShowProductShortDescription: isShowProductShortDescription,
            productCategoryThumbnailFileBinary: productCategoryThumbnailFileBinary,
            productCategoryImageFileBinary: productCategoryImageFileBinary,
            productCategoryThumbnailName: productCategoryThumbnailName,
            productCategoryImageName: productCategoryImageName,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData
        };
    };
    ProductCategory.CreateFromClientModel = function (source) {
        return new ProductCategory(
            source.productCategoryId,
            source.categoryName,
            source.contentType,
            source.description1,
            source.description2,
            source.lockedBy,
            source.companyId,
            source.parentCategoryId,
            source.displayOrder,
            source.imagePath,
            source.thumbnailPath,
            source.isEnabled,
            source.isMarketPlace,
            source.templateDesignerMappedCategoryName,
            source.isArchived,
            source.isPublished,
            source.trimmedWidth,
            source.trimmedHeight,
            source.isColorImposition,
            source.isOrderImposition,
            source.isLinkToTemplates,
            source.sides,
            source.applySizeRestrictions,
            source.applyFoldLines,
            source.widthRestriction,
            source.heightRestriction,
            source.categoryTypeId,
            source.regionId,
            source.zoomFactor,
            source.scaleFactor,
            source.isShelfProductCategory,
            source.metaKeywords,
            source.metaDescription,
            source.metaTitle,
            source.organisationId,
            source.subCategoryDisplayMode1,
            source.subCategoryDisplayMode2,
            source.subCategoryDisplayColumns,
            source.categoryURLText,
            source.metaOverride,
            source.shortDescription,
            source.secondaryDescription,
            source.defaultSortBy,
            source.productsDisplayColumns,
            source.productsDisplayRows,
            source.isDisplayFeaturedproducts,
            source.isShowAvailablity,
            source.isShowRewardPoints,
            source.isShowListPrice,
            source.isShowSalePrice,
            source.isShowStockStatus,
            source.isShowProductDescription,
            source.isShowProductShortDescription
        );
    };
    ProductCategory.Create = function (source) {
        var productCategory = new ProductCategory(
            source.ProductCategoryId,
            source.CategoryName,
            source.ContentType,
            source.Description1,
            source.Description2,
            source.LockedBy,
            source.CompanyId,
            source.ParentCategoryId,
            source.DisplayOrder,
            source.ImagePath,
            source.ThumbnailPath,
            source.isEnabled,
            source.isMarketPlace,
            source.TemplateDesignerMappedCategoryName,
            source.isArchived,
            source.isPublished,
            source.TrimmedWidth,
            source.TrimmedHeight,
            source.isColorImposition,
            source.isOrderImposition,
            source.isLinkToTemplates,
            source.Sides,
            source.ApplySizeRestrictions,
            source.ApplyFoldLines,
            source.WidthRestriction,
            source.HeightRestriction,
            source.CategoryTypeId,
            source.RegionId,
            source.ZoomFactor,
            source.ScaleFactor,
            source.isShelfProductCategory,
            source.MetaKeywords,
            source.MetaDescription,
            source.MetaTitle,
            source.OrganisationId,
            source.SubCategoryDisplayMode1,
            source.SubCategoryDisplayMode2,
            source.SubCategoryDisplayColumns,
            source.CategoryURLText,
            source.MetaOverride,
            source.ShortDescription,
            source.SecondaryDescription,
            source.DefaultSortBy,
            source.ProductsDisplayColumns,
            source.ProductsDisplayRows,
            source.IsDisplayFeaturedproducts,
            source.IsShowAvailablity,
            source.IsShowRewardPoints,
            source.IsShowListPrice,
            source.IsShowSalePrice,
            source.IsShowStockStatus,
            source.IsShowProductDescription,
            source.IsShowProductShortDescription,
            source.ThumbNailSource,
            source.ImageSource
        );
        return productCategory;
    };
    // #endregion ______________________P R O D U C T   C A T E G O R Y  _________________________________

    // #region ______________ Item For Widgets _________________

    // ReSharper disable once InconsistentNaming
    var ItemForWidgets = function (specifiedItemId, specifiedProductName) {
        var self,
            id = ko.observable(specifiedItemId),
            productName = ko.observable(specifiedProductName),
            isInSelectedList = ko.observable(false);

        self = {
            id: id,
            productName: productName,
            isInSelectedList: isInSelectedList,
        };
        return self;
    };
    ItemForWidgets.Create = function (source) {
        return new ItemForWidgets(
            source.ItemId,
            source.ProductName);
    };

    // #endregion ______________ Item For Widgets _________________

    //#region ___________________C O M P A N Y   D O M A I N ______________________________

    // ReSharper disable once InconsistentNaming
    var CompanyDomain = function (specifiedCompanyDomainId, specifiedDomain, specifiedCompanyId) {
        var // Unique key
            companyDomainId = ko.observable(specifiedCompanyDomainId || 0),
            // Domain
            domain = ko.observable(specifiedDomain || undefined).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId || undefined),
            isMandatoryDomain = ko.observable(false),
            errors = ko.validation.group({
                domain: domain
            }),
            // Is Valid
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),
            // True if the product has been changed
            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                domain: domain,
                companyId: companyId
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
             convertToServerData = function () {
                 return {
                     CompanyDomainId: companyDomainId() === undefined ? 0 : companyDomainId(),
                     Domain: domain(),
                     CompanyId: companyId(),
                 };
             };
        return {
            companyDomainId: companyDomainId,
            domain: domain,
            companyId: companyId,
            isMandatoryDomain: isMandatoryDomain,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData
        };
    };
    CompanyDomain.CreateFromClientModel = function (source) {
        return new CompanyDomain(
            source.companyDomainId,
            source.domain,
            source.CompanyId
            );
    };
    CompanyDomain.Create = function (source) {
        var companyDomain = new CompanyDomain(
            source.CompanyDomainId,
            source.Domain,
            source.CompanyId
            );
        return companyDomain;
    };
    //#endregion

    // #region ______________ CMS Offer _________________

    // ReSharper disable once InconsistentNaming
    var CmsOffer = function (specifiedOfferId, specifiedItemId, specifiedOfferType, specifiedItemName, specifiedSortOrder) {
        var self,
            id = ko.observable(specifiedOfferId),
            itemId = ko.observable(specifiedItemId),
            offerType = ko.observable(specifiedOfferType),
            itemName = ko.observable(specifiedItemName),
            sortOrder = ko.observable(specifiedSortOrder),
            companyId = ko.observable(),
        //Convert To Server
        convertToServerData = function () {
            return {
                PageWidgetId: id() === undefined ? 0 : id(),
                ItemId: itemId(),
                OfferType: offerType(),
                ItemName: itemName(),
                SortOrder: sortOrder(),
                CompanyId: companyId(),
            };
        };
        self = {
            id: id,
            itemId: itemId,
            offerType: offerType,
            itemName: itemName,
            sortOrder: sortOrder,
            companyId: companyId,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    CmsOffer.Create = function (source) {
        return new CmsOffer(
            source.OfferId,
            source.ItemId,
            source.OfferType,
            source.ItemName,
            source.SortOrder);
    };

    // #endregion ______________ CMS Offer _________________

    // #region ______________ Media Library _________________

    // ReSharper disable once InconsistentNaming
    var MediaLibrary = function (specifiedMediaId, specifiedFilePath, specifiedFileName, specifiedFileType, specifiedCompanyId, specifiedImageSource,
        specifiedImageSourcePath) {
        var self,
            id = ko.observable(specifiedMediaId),
            fakeId = ko.observable(),
            //Contain File Db Path 
            filePath = ko.observable(specifiedFilePath),
            fileName = ko.observable(specifiedFileName),
            fileType = ko.observable(specifiedFileType),
            companyId = ko.observable(specifiedCompanyId),
            fileSource = ko.observable(),
            //Contain Full image path with cahe remove logic for image
            imageSourcePath = ko.observable(specifiedImageSourcePath),
            isSelected = ko.observable(false),

        //Convert To Server
        convertToServerData = function () {
            return {
                MediaId: id() === undefined ? 0 : id(),
                FilePath: filePath,
                FileName: fileName(),
                FileType: fileType(),
                CompanyId: companyId(),
                FileSource: fileSource(),
                FakeId: fakeId(),
            };
        };
        self = {
            id: id,
            fakeId: fakeId,
            filePath: filePath,
            fileName: fileName,
            fileType: fileType,
            companyId: companyId,
            fileSource: fileSource,
            isSelected: isSelected,
            imageSourcePath: imageSourcePath,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    MediaLibrary.Create = function (source) {
        return new MediaLibrary(
            source.MediaId,
            source.FilePath,
            source.FileName,
            source.FileType,
            source.CompanyId,
        source.ImageSource,
        source.ImageSourcePath);
    };

    // #endregion ______________ MediaLibrary _________________

    // #region ______________ C O S T   C E N T E R_________________
    // ReSharper disable once InconsistentNaming
    var CostCenter = function (specifiedCostCentreId, specifiedName) {

        var self,
            costCentreId = ko.observable(specifiedCostCentreId),
            name = ko.observable(specifiedName),
            isSelected = ko.observable(),
            // Errors
            errors = ko.validation.group({

            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                costCentreId: costCentreId,
                name: name,

            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    CostCentreId: costCentreId(),
                    Name: name(),
                };
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            costCentreId: costCentreId,
            name: name,
            isSelected: isSelected,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CostCenter.CreateFromClientModel = function (source) {
        return new CostCenter(
            source.costCentreId,
            source.name
            );
    };
    CostCenter.Create = function (source) {
        var costCenter = new CostCenter(
            source.CostCentreId,
            source.Name
            );
        return costCenter;
    };
    // #endregion 

    // #region ______________  Field Variable   _________________
    var FieldVariable = function (specifiedVariableId, specifiedVariableName, specifiedVariableType, specifiedScope, specifiedWaterMark, specifiedDefaultValue,
          specifiedInputMask, specifiedCompanyId, specifiedVariableTag, specifiedVariableTitle, specifiedIsSystem) {
        var self,
            id = ko.observable(specifiedVariableId),
            variableName = ko.observable(specifiedVariableName).extend({ required: true }),
            variableType = ko.observable(specifiedVariableType),
            scope = ko.observable(specifiedScope),
            isSystem = ko.observable(specifiedIsSystem),
            waterMark = ko.observable(specifiedWaterMark || "").extend({ required: true }),
            defaultValue = ko.observable(specifiedDefaultValue || ""),
            defaultValueForInput = ko.observable(specifiedDefaultValue || ""),
            inputMask = ko.observable(specifiedInputMask),
            companyId = ko.observable(specifiedCompanyId),
            variableTag = ko.observable(specifiedVariableTag).extend({
                required: true,
                variableTagRule: true
            }),
            scopeName = ko.observable(),
            typeName = ko.observable(),
            variableTitle = ko.observable(specifiedVariableTitle),
            fakeId = ko.observable(),
            variableOptions = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                variableName: variableName,
                waterMark: waterMark,
                variableTag: variableTag
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.VariableId = source.id() === undefined ? 0 : source.id();
                result.VariableName = source.variableName() === undefined ? null : source.variableName();
                result.VariableType = source.variableType() === undefined ? null : source.variableType();
                result.WaterMark = source.waterMark() === undefined ? null : source.waterMark();
                result.Scope = source.scope() === undefined ? null : source.scope();
                result.IsSystem = source.isSystem() === undefined ? null : source.isSystem();
                result.DefaultValue = source.variableType() === 1 ? (source.defaultValue() === undefined ? null : source.defaultValue()) : (defaultValueForInput() === undefined ? null : defaultValueForInput());
                result.CompanyId = source.companyId() === undefined ? null : source.companyId();
                result.InputMask = source.inputMask() === undefined ? null : source.inputMask();
                result.VariableTag = source.variableTag() === undefined ? null : source.variableTag();
                result.VariableTitle = source.variableTitle() === undefined ? null : source.variableTitle();
                result.FakeIdVariableId = source.fakeId() === undefined ? 0 : source.fakeId();
                result.VariableOptions = [];
                return result;
            },
        // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            variableName: variableName,
            variableType: variableType,
            scope: scope,
            isSystem: isSystem,
            waterMark: waterMark,
            defaultValueForInput: defaultValueForInput,
            defaultValue: defaultValue,
            companyId: companyId,
            variableTag: variableTag,
            inputMask: inputMask,
            scopeName: scopeName,
            typeName: typeName,
            variableTitle: variableTitle,
            variableOptions: variableOptions,
            fakeId: fakeId,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Field Variable Create Factory
    FieldVariable.Create = function (source) {
        return new FieldVariable(
            source.VariableId,
             source.VariableName,
             source.VariableType,
             source.Scope,
            source.WaterMark,
             source.DefaultValue,
             source.InputMask,
             source.CompanyId,
             source.VariableTag,
             source.VariableTitle,
            source.IsSystem
            );
    };
    // #endregion ______________  Field Variable   _________________

    // #region ______________  Variable Option  _________________
    var VariableOption = function (specifiedVariableOptionId, specifiedVariableName, specifiedValue, specifiedSortOrder) {
        var self,
            id = ko.observable(specifiedVariableOptionId),
            variableId = ko.observable(specifiedVariableName),
            value = ko.observable(specifiedValue).extend({ required: true }),
            sortOrder = ko.observable(specifiedSortOrder),
            fakeId = ko.observable(),

            // Errors
            errors = ko.validation.group({
                value: value,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.VariableOptionId = source.id() === undefined ? 0 : source.id();
                result.VariableId = source.variableId() === undefined ? 0 : source.variableId();
                result.Value = source.value() === undefined ? null : source.value();
                result.SortOrder = source.sortOrder() === undefined ? 0 : source.sortOrder();
                result.FakeId = source.fakeId() === undefined ? 0 : source.fakeId();
                return result;
            },
        // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            variableId: variableId,
            value: value,
            sortOrder: sortOrder,
            fakeId: fakeId,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Field Option Create Factory
    VariableOption.Create = function (source) {
        return new VariableOption(
            source.VariableOptionId,
             source.VariableId,
             source.Value,
             source.SortOrder
            );
    };
    // #endregion ______________  Variable Option   _________________

    // #region ______________  Scope Variable  _________________
    var ScopeVariable = function (specifiedContactVariableId, specifiedContactId, specifiedVariableId, specifiedValue, specifiedTitle, specifiedType,
        specifiedScope, specifiedWaterMark) {
        var self,
            id = ko.observable(specifiedContactVariableId),
            contactId = ko.observable(specifiedContactId),
            variableId = ko.observable(specifiedVariableId),
            value = ko.observable(specifiedValue || ""),
            fakeId = ko.observable(),
            title = ko.observable(specifiedTitle),
            type = ko.observable(specifiedType),
            scope = ko.observable(specifiedScope),
            optionId = ko.observable(specifiedValue),
            waterMark = ko.observable(specifiedWaterMark),
            setValue = ko.computed(function () {
                if (value() === undefined || value() === null || value() === "") {
                    value(waterMark());
                }
            }),
            variableOptions = ko.observableArray([]),

            // Errors
            errors = ko.validation.group({
                // value: value,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.ScopeVariableId = source.id() === undefined ? 0 : source.id();
                result.Id = source.contactId() === undefined ? 0 : source.contactId();
                result.VariableId = source.variableId() === undefined ? 0 : source.variableId();
                result.Value = source.value() === undefined ? null : source.value();
                result.Scope = source.scope() === undefined ? null : source.scope();
                result.FakeVariableId = source.fakeId() === undefined ? 0 : source.fakeId();
                return result;
            },
        // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            contactId: contactId,
            variableId: variableId,
            value: value,
            title: title,
            scope: scope,
            fakeId: fakeId,
            type: type,
            optionId: optionId,
            waterMark: waterMark,
            variableOptions: variableOptions,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    //Field Option Create Factory
    ScopeVariable.Create = function (source) {
        return new ScopeVariable(
            source.ScopeVariableId,
             source.Id,
             source.VariableId,
             source.Value,
             source.Title,
             source.Type,
             source.Scope,
            source.WaterMark
            );
    };
    // #endregion ______________  Company Contact Variable   _________________

    // #region ______________  Field Variable  For Smart Form _________________
    var FieldVariableForSmartForm = function (specifiedVariableId, specifiedVariableName, specifiedVariableType,
        specifiedVariableTag, specifiedScopeName, specifiedTypeName, specifiedDefaultValue, specifiedVariableTitle, specifiedWaterMark, specifiedScope, specifiedIsSystem) {
        var self,
            id = ko.observable(specifiedVariableId),
            variableName = ko.observable(specifiedVariableName),
            variableType = ko.observable(specifiedVariableType),
            variableTag = ko.observable(specifiedVariableTag),
            scopeName = ko.observable(specifiedScopeName),
            typeName = ko.observable(specifiedTypeName),
            scope = ko.observable(specifiedScope),
            isSystem = ko.observable(specifiedIsSystem),
            waterMark = ko.observable(specifiedWaterMark),
            defaultValue = ko.observable(specifiedDefaultValue === null ? "" : specifiedDefaultValue),
            title = ko.observable(specifiedVariableTitle === null ? "" : specifiedVariableTitle),
            variableOptions = ko.observableArray([]);

        self = {
            id: id,
            variableName: variableName,
            variableType: variableType,
            title: title,
            variableTag: variableTag,
            scopeName: scopeName,
            typeName: typeName,
            defaultValue: defaultValue,
            waterMark: waterMark,
            scope: scope,
            isSystem: isSystem,
            variableOptions: variableOptions
        };
        return self;
    };
    //Field Variable For Smart Form Create Factory
    FieldVariableForSmartForm.Create = function (source) {
        return new FieldVariableForSmartForm(
            source.VariableId,
             source.VariableName,
             source.Type,
             source.VariableTag,
             source.ScopeName,
             source.TypeName,
             source.DefaultValue,
        source.VariableTitle, source.WaterMark, source.Scope, source.IsSystem);
    };
    // #endregion ______________  Field Variable   _________________

    // #region ______________  Smart Form _________________
    var SmartForm = function (specifiedSmartFormId, specifiedName, specifiedCompanyId, specifiedHeading) {
        var self,
            id = ko.observable(specifiedSmartFormId),
            name = ko.observable(specifiedName).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId),
            //Check Whether Drop field Variable,Line Seperator or Group Caption
            dropFrom = ko.observable(),
            heading = ko.observable(specifiedHeading),
            smartFormDetails = ko.observableArray([]),
            // Errors
            errors = ko.validation.group({
                name: name,
            }),
            // Is Valid 
            isValid = ko.computed(function () {
                return errors().length === 0 ? true : false;
            }),

            // ReSharper disable InconsistentNaming
            dirtyFlag = new ko.dirtyFlag({
                name: name,
                heading: heading,
                smartFormDetails: smartFormDetails
            }),
            // True If Has Changes
            hasChanges = ko.computed(function() {
                return dirtyFlag.isDirty();
            }),
            // Reset Dirty State
            reset = function() {
                dirtyFlag.reset();
            },
            //Convert To Server
            convertToServerData = function (source) {
                var result = {};
                result.SmartFormId = source.id() === undefined ? 0 : source.id();
                result.Name = source.name() === undefined ? null : source.name();
                result.CompanyId = source.companyId() === undefined ? 0 : source.companyId();
                result.Heading = source.heading() === undefined ? null : source.heading();
                result.SmartFormDetails = [];
                return result;
            };
        self = {
            id: id,
            name: name,
            companyId: companyId,
            heading: heading,
            smartFormDetails: smartFormDetails,
            dropFrom: dropFrom,
            errors: errors,
            isValid: isValid,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            reset: reset,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    //Smart Form Create Factory
    SmartForm.Create = function (source) {
        return new SmartForm(
            source.SmartFormId,
             source.Name,
             source.CompanyId,
            source.Heading);
    };
    // #endregion ______________  Field Variable   _________________

    // #region ______________  Smart Form Detail _________________
    var SmartFormDetail = function (specifiedSmartFormDetailId, specifiedSmartFormId, specifiedObjectType,
        specifiedSortOrder, specifiedIsRequired, specifiedVariableId, specifiedCaptionValue, specifiedWaterMark) {
        var self,
            id = ko.observable(specifiedSmartFormDetailId),
            smartFormId = ko.observable(specifiedSmartFormId),
            objectType = ko.observable(specifiedObjectType),
            sortOrder = ko.observable(specifiedSortOrder),
            isRequired = ko.observable((specifiedIsRequired !== null && specifiedIsRequired !== undefined && specifiedIsRequired === true) ? "1" : "0"),
            variableId = ko.observable(specifiedVariableId),
            captionValue = ko.observable(specifiedCaptionValue),
            waterMark = ko.observable(specifiedWaterMark),
            html = ko.observable(),
        //Convert To Server
        convertToServerData = function (source) {
            var result = {};
            result.SmartFormDetailId = source.id() === undefined ? 0 : source.id();
            result.SmartFormId = source.smartFormId() === undefined ? 0 : source.smartFormId();
            result.ObjectType = source.objectType() === undefined ? 0 : source.objectType();
            result.SortOrder = source.sortOrder() === undefined ? 0 : source.sortOrder();
            result.VariableId = source.variableId() === undefined ? null : source.variableId();
            result.IsRequired = source.isRequired() === "1" ? true : false;
            result.CaptionValue = source.captionValue() === undefined ? null : source.captionValue();
            return result;
        };
        self = {
            id: id,
            smartFormId: smartFormId,
            objectType: objectType,
            isRequired: isRequired,
            sortOrder: sortOrder,
            variableId: variableId,
            captionValue: captionValue,
            html: html,
            waterMark: waterMark,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    //Smart Form Detail Create Factory
    SmartFormDetail.Create = function (source) {
        return new SmartFormDetail(
            source.SmartFormDetailId,
             source.SmartFormId,
             source.ObjectType,
             source.SortOrder,
             source.IsRequired,
            source.VariableId,
            source.CaptionValue,
            source.WaterMark
            );
    };
    // #endregion ______________  Field Variable   _________________

    //#region ______________ R E T U R N ______________
    return {
        StoreListView: StoreListView,
        Store: Store,
        CompanyType: CompanyType,
        SystemUser: SystemUser,
        RaveReview: RaveReview,
        CompanyCMYKColor: CompanyCMYKColor,
        CompanyTerritory: CompanyTerritory,
        ColorPalette: ColorPalette,
        Address: Address,
        CompanyBanner: CompanyBanner,
        CompanyBannerSet: CompanyBannerSet,
        CompanyContact: CompanyContact,
        Role: Role,
        RegistrationQuestion: RegistrationQuestion,
        SecondaryPageListView: SecondaryPageListView,
        CMSPage: CMSPage,
        PageCategory: PageCategory,
        Campaign: Campaign,
        PaymentGateway: PaymentGateway,
        PaymentMethod: PaymentMethod,
        ProductCategoryListView: ProductCategoryListView,
        ProductCategory: ProductCategory,
        Widget: Widget,
        CmsSkingPageWidget: CmsSkingPageWidget,
        CmsPageWithWidgetList: CmsPageWithWidgetList,
        CmsSkinPageWidgetParam: CmsSkinPageWidgetParam,
        ItemForWidgets: ItemForWidgets,
        CmsOffer: CmsOffer,
        CompanyDomain: CompanyDomain,
        MediaLibrary: MediaLibrary,
        CostCenter: CostCenter,
        CampaignSection: CampaignSection,
        CampaignEmailVariable: CampaignEmailVariable,
        CampaignImage: CampaignImage,
        SectionFlag: SectionFlag,
        CampaignCompanyType: CampaignCompanyType,
        Group: Group,
        FieldVariable: FieldVariable,
        VariableOption: VariableOption,
        ScopeVariable: ScopeVariable,
        FieldVariableForSmartForm: FieldVariableForSmartForm,
        SmartForm: SmartForm,
        SmartFormDetail: SmartFormDetail,
    };
    // #endregion 
});

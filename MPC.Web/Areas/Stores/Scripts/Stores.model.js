﻿

define("stores/stores.model", ["ko", "underscore", "underscore-ko"], function (ko) {
    var
        //WebMasterTag WebAnalyticCode
        // ReSharper disable once InconsistentNaming
        Store = function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedAvatRegNumber,
            specifiedAvatRegReference, specifiedPhoneNo, specifiedIsCustomer, specifiedNotes, specifiedWebMasterTag, specifiedWebAnalyticCode, specifiedWebAccessCode, specifiedTwitterUrl,
            specifiedFacebookUrl, specifiedLinkedinUrl, specifiedFacebookAppId, specifiedFacebookAppKey, specifiedTwitterAppId, specifiedTwitterAppKey,
            specifiedSalesAndOrderManagerId1, specifiedSalesAndOrderManagerId2, specifiedProductionManagerId1, specifiedProductionManagerId2, specifiedStockNotificationManagerId1, specifiedStockNotificationManagerId2
        ) {
            var
                self,
                companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
                name = ko.observable(specifiedName),
                status = ko.observable(specifiedStatus),
                image = ko.observable(specifiedImage),
                url = ko.observable(specifiedUrl),
                accountOpenDate = ko.observable(specifiedAccountOpenDate),
                accountManagerId = ko.observable(specifiedAccountManagerId),
                avatRegNumber = ko.observable(specifiedAvatRegNumber),
                avatRegReference = ko.observable(specifiedAvatRegReference),
                phoneNo = ko.observable(specifiedPhoneNo),
                isCustomer = ko.observable(specifiedIsCustomer),
                notes = ko.observable(specifiedNotes),
                webMasterTag = ko.observable(specifiedWebMasterTag),
                webAnalyticCode = ko.observable(specifiedWebAnalyticCode),
                webAccessCode = ko.observable(specifiedWebAccessCode),
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
                companyType = ko.observable(),
                type = ko.observable(),
                raveReviews = ko.observableArray([]),
                companyTerritories = ko.observableArray([]),

                // ReSharper disable InconsistentNaming
                companyCMYKColors = ko.observableArray([]),
                //Color Palette
                colorPalette = ko.observable(new ColorPalette()),
                //Company Banner
                companyBanner = ko.observable(new CompanyBanner()),
                //Company Banner Set
                companyBannerSet = ko.observable(new CompanyBannerSet()),
                // ReSharper restore InconsistentNaming

                // Errors
                errors = ko.validation.group({
                    companyId: companyId,
                    name: name,
                    url: url,
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
                    accountOpenDate: accountOpenDate,
                    accountManagerId: accountManagerId,
                    avatRegNumber: avatRegNumber,
                    avatRegReference: avatRegReference,
                    phoneNo: phoneNo,
                    isCustomer: isCustomer,
                    notes: notes,
                    webAccessCode: webAccessCode,
                    twitterUrl: twitterUrl,
                    facebookUrl: facebookUrl,
                    linkedinUrl: linkedinUrl,
                    facebookAppId: facebookAppId,
                    facebookAppKey: facebookAppKey,
                    twitterAppId: twitterAppId,
                    twitterAppKey: twitterAppKey,
                    companyType: companyType,
                    type: type,
                    raveReviews: raveReviews,
                    companyTerritories: companyTerritories,
                    companyCMYKColors: companyCMYKColors,
                    webMasterTag: webMasterTag,
                    webAnalyticCode: webAnalyticCode,
                    salesAndOrderManagerId1: salesAndOrderManagerId1,
                    salesAndOrderManagerId2: salesAndOrderManagerId2,
                    productionManagerId1: productionManagerId1,
                    productionManagerId2: productionManagerId2,
                    stockNotificationManagerId1: stockNotificationManagerId1,
                    stockNotificationManagerId2: stockNotificationManagerId2
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
                    result.AccountOpenDate = source.accountOpenDate();
                    result.AccountManagerId = source.accountManagerId();
                    result.AvatRegNumber = source.avatRegNumber();
                    result.PvatRegReference = source.avatRegReference();
                    result.PhoneNo = source.phoneNo();
                    result.IsCustomer = source.isCustomer();
                    result.Notes = source.notes();
                    result.WebMasterTag = source.webMasterTag();
                    result.WebAnalyticCode = source.webAnalyticCode();
                    result.WebAccessCode = source.webAccessCode();
                    result.TwitterUrl = source.twitterUrl();
                    result.FacebookUrl = source.facebookUrl();
                    result.LinkedinUrl = source.linkedinUrl();
                    result.FacebookAppId = source.facebookAppId();
                    result.FacebookAppKey = source.facebookAppKey();
                    result.TwitterAppId = source.twitterAppId();
                    result.TwitterAppKey = source.twitterAppKey();
                    result.SalesAndOrderManagerId1 = source.salesAndOrderManagerId1();
                    result.SalesAndOrderManagerId2 = source.salesAndOrderManagerId2();
                    result.ProductionManagerId1 = source.productionManagerId1();
                    result.ProductionManagerId2 = source.productionManagerId2();
                    result.StockNotificationManagerId1 = source.stockNotificationManagerId1();
                    result.StockNotificationManagerId2 = source.stockNotificationManagerId2();
                    result.CompanyType = CompanyType().convertToServerData(source.companyType());
                    result.RaveReviews = [];
                    _.each(source.raveReviews(), function (item) {
                        result.RaveReviews.push(item.convertToServerData());
                    });
                    result.CompanyTerritories = [];
                    _.each(source.companyTerritories(), function (item) {
                        result.CompanyTerritories.push(item.convertToServerData());
                    });
                    result.CompanyCmykColors = [];
                    _.each(source.companyCMYKColors(), function (item) {
                        result.CompanyCmykColors.push(item.convertToServerData());
                    });
                    result.NewAddedCompanyTerritories = [];
                    result.EdittedCompanyTerritories = [];
                    result.DeletedCompanyTerritories = [];
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
                accountOpenDate: accountOpenDate,
                accountManagerId: accountManagerId,
                avatRegNumber: avatRegNumber,
                avatRegReference: avatRegReference,
                phoneNo: phoneNo,
                isCustomer: isCustomer,
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
                type: type,
                raveReviews: raveReviews,
                companyTerritories: companyTerritories,
                companyCMYKColors: companyCMYKColors,
                colorPalette: colorPalette,
                isValid: isValid,
                errors: errors,
                dirtyFlag: dirtyFlag,
                hasChanges: hasChanges,
                convertToServerData: convertToServerData,
                reset: reset
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
            source.stockNotificationManagerId2
            );
        result.companyType(CompanyType.CreateFromClientModel(source.companyType));
        _.each(source.raveReviews, function (item) {
            result.raveReviews.push(RaveReview.CreateFromClientModel(item));
        });
        _.each(source.companyTerritories, function (item) {
            result.companyTerritories.push(CompanyTerritory.CreateFromClientModel(item));
        });
        _.each(source.companyCMYKColors, function (item) {
            result.companyCMYKColors.push(CompanyCMYKColor.CreateFromClientModel(item));
        });
        return result;
    };
    Store.Create = function (source) {
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
            source.TwitterUrl,
            source.FacebookUrl,
            source.LinkedinUrl,
            source.FacebookAppId,
            source.FacebookAppKey,
            source.TwitterAppId,
            source.TwitterAppKey,
            source.SalesAndOrderManagerId1,
            source.SalesAndOrderManagerId2,
            source.ProductionManagerId1,
            source.ProductionManagerId2,
            source.StockNotificationManagerId1,
            source.StockNotificationManagerId2
            );

        store.companyType(CompanyType.Create(source.CompanyType));
        if (source.IsCustomer == 0) {
            store.type("Supplier");
        }
        else if (source.IsCustomer == 1) {
            store.type("Retail Customer");
        }
        else if (source.IsCustomer == 2) {
            store.type("Prospect");
        }
        else if (source.IsCustomer == 3) {
            store.type("Corporate");//companyTerritories
        }
        _.each(source.RaveReviews, function (item) {
            store.raveReviews.push(RaveReview.Create(item));
        });
        _.each(source.CompanyTerritories, function (item) {
            store.companyTerritories.push(CompanyTerritory.Create(item));
        });
        _.each(source.CompanyCmykColors, function (item) {
            store.companyCMYKColors.push(CompanyCMYKColor.Create(item));
        });
        return store;
    };

    // ______________  C O M P A N Y    T Y P E   _________________//
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

    // ______________  S Y S T E M     U S E R   _________________//
    // ReSharper disable once InconsistentNaming
    var SystemUser = function (specifiedSystemUserId, specifiedUserName) {
        var self,
            systemUserId = ko.observable(specifiedSystemUserId),
            userName = ko.observable(specifiedUserName),
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
                userName: userName
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
            source.UserName
            );
        return systemUser;
    };

    // ______________  R A V E    R E V I E W   _________________//
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
                }
                //var result = {};
                //result.ReviewId = source.reviewId();
                //result.ReviewBy = source.reviewBy();
                //result.Review = source.review();
                //result.IsDisplay = source.isDisplay();
                //result.SortOrder = source.sortOrder();
                //result.OrganisationId = source.organisationId();
                //result.CompanyId = source.companyId();
                //return result;
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

    // ______________  C O M P A N Y    C M Y K    C O L O R   _________________//
    // ReSharper disable once InconsistentNaming    
    var CompanyCMYKColor = function (specifiedColorId, specifiedCompanyId, specifiedColorName, specifiedColorC, specifiedColorM, specifiedColorY, specifiedColorK) {
        var self,
            colorId = ko.observable(specifiedColorId),
            companyId = ko.observable(specifiedCompanyId),
            colorName = ko.observable(specifiedColorName).extend({ required: true }),
            colorC = ko.observable(specifiedColorC).extend({ required: true }),
            colorM = ko.observable(specifiedColorM).extend({ required: true }),
            colorY = ko.observable(specifiedColorY).extend({ required: true }),
            colorK = ko.observable(specifiedColorK).extend({ required: true }),
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
                colorK: colorK
            }),
            // Has Changes
            hasChanges = ko.computed(function () {
                return dirtyFlag.isDirty();
            }),
            //Convert To Server
            convertToServerData = function () {
                return {
                    ColorId: colorId(),
                    CompanyId: companyId(),
                    ColorName: colorName(),
                    ColorC: colorC(),
                    ColorM: colorM(),
                    ColorY: colorY(),
                    ColorK: colorK()
                }
                //var result = {};//ColorId CompanyId ColorName ColorC ColorM ColorY ColorK
                //result.ColorId = source.colorId();
                //result.CompanyId = source.companyId();
                //result.ColorName = source.colorName();
                //result.ColorC = source.colorC();
                //result.ColorM = source.colorM();
                //result.ColorY = source.colorY();
                //result.ColorK = source.colorK();
                //return result;
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
            source.colorK
            );
    };
    CompanyCMYKColor.Create = function (source) {
        var companyCMYKColor = new CompanyCMYKColor(
            source.ColorId,
            source.CompanyId,
            source.ColorName,
            source.ColorC,
            source.ColorM,
            source.ColorY,
            source.ColorK
            );
        return companyCMYKColor;
    };

    //// ______________  C O M P A N Y    T E R R I T O R Y    _________________//
    // ReSharper disable once InconsistentNaming
    var CompanyTerritory = function (specifiedTerritoryId, specifiedTerritoryName, specifiedCompanyId, specifiedTerritoryCode, specifiedisDefault) {

        var self,
            territoryId = ko.observable(specifiedTerritoryId),
            territoryName = ko.observable(specifiedTerritoryName).extend({ required: true }),
            companyId = ko.observable(specifiedCompanyId),
            territoryCode = ko.observable(specifiedTerritoryCode).extend({ required: true }),
            isDefault = ko.observable(specifiedisDefault),
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
                    isDefault: isDefault()
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
    // ______________  Color Palettes   _________________//
    // ReSharper disable once InconsistentNaming
    var ColorPalette = function (specifiedPalleteId, specifiedPalleteName, specifiedColor1, specifiedColor2, specifiedColor3, specifiedColor4, specifiedColor5,
        specifiedColor6, specifiedColor7, specifiedSkinId, specifiedIsDefault, specifiedCompanyId) {
        var self,
          id = ko.observable(specifiedPalleteId),
          palleteName = ko.observable(specifiedPalleteName),
          color1 = ko.observable("#320B0B"),
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
                skinId: skinId,
                isDefault: isDefault,
                companyId: companyId
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

    // ______________  Company Banner   _________________//
    // ReSharper disable once InconsistentNaming
    var CompanyBanner = function (specifiedCompanyBannerId, specifiedHeading, specifiedDescription, specifiedItemURL, specifiedButtonURL, specifiedCompanySetId) {
        var self,
          id = ko.observable(specifiedCompanyBannerId),
          heading = ko.observable(specifiedHeading),
          description = ko.observable(specifiedDescription),
          itemURL = ko.observable(specifiedItemURL),
          buttonURL = ko.observable(specifiedButtonURL),
          companySetId = ko.observable(specifiedCompanySetId),
               // Errors
            errors = ko.validation.group({

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
                result.CompanyBannerId = source.id() === undefined ? 0 : source.id();
                result.Heading = source.heading() === undefined ? null : source.heading();;
                result.Description = source.description() === undefined ? null : source.description();
                result.ItemURL = source.itemURL() === undefined ? null : source.itemURL();
                result.ButtonURL = source.buttonURL() === undefined ? null : source.buttonURL();
                result.CompanySetId = source.companySetId() === undefined ? null : source.companySetId();
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
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CompanyBanner.Create = function (source) {
        return new CompanyBanner(
            source.CompanyBannerId,
            source.Heading,
            source.Description,
            source.ItemURL,
            source.ButtonURL,
            source.CompanySetId
            );
    };

    // ______________  Company Banner  Set _________________//
    // ReSharper disable once InconsistentNaming
    var CompanyBannerSet = function (specifiedCompanySetId, specifiedSetName) {
        var self,
          id = ko.observable(specifiedCompanySetId),
          setName = ko.observable(specifiedSetName),
           // Errors
            errors = ko.validation.group({

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
                result.SetName = source.setName() === undefined ? null : source.setName();;
                return result;
            },
            // Reset
            reset = function () {
                dirtyFlag.reset();
            };
        self = {
            id: id,
            setName: setName,
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
        return new CompanyBanner(
            source.CompanySetId,
            source.SetName
           );
    };
    return {
        Store: Store,
        CompanyType: CompanyType,
        SystemUser: SystemUser,
        RaveReview: RaveReview,
        CompanyCMYKColor: CompanyCMYKColor,
        ColorPalette: ColorPalette,
        CompanyTerritory: CompanyTerritory, //territoryId territoryName  companyId  territoryCode  isDefault
        CompanyBanner: CompanyBanner,
        CompanyBannerSet: CompanyBannerSet
    };

});




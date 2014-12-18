﻿

define("stores/stores.model", ["ko", "underscore", "underscore-ko"], function (ko) {
    var
        //_________ S T O R E   L I S T    V I E W____________________//
        // ReSharper disable once InconsistentNaming
        StoreListView = function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedIsCustomer) {
            var
                self,
                companyId = ko.observable(specifiedCompanyId).extend({ required: true }),
                name = ko.observable(specifiedName),
                status = ko.observable(specifiedStatus),
                image = ko.observable(specifiedImage),
                url = ko.observable(specifiedUrl),
                isCustomer = ko.observable(specifiedIsCustomer),
                type = ko.observable(),
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
            source.isCustomer
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
            source.IsCustomer
        );

        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        if (source.IsCustomer == 1) {
            store.type("Retail Customer");
        }
            //else if (source.IsCustomer == 2) {
            //    store.type("Prospect");
            //}
        else if (source.IsCustomer == 3) {
            store.type("Corporate");
        }

        return store;
    };

    //_____________________ S T O R E ______________________________//
    //WebMasterTag WebAnalyticCode
    // ReSharper disable once InconsistentNaming
    var Store = function (specifiedCompanyId, specifiedName, specifiedStatus, specifiedImage, specifiedUrl, specifiedAccountOpenDate, specifiedAccountManagerId, specifiedAvatRegNumber,
        specifiedAvatRegReference, specifiedPhoneNo, specifiedIsCustomer, specifiedNotes, specifiedWebMasterTag, specifiedWebAnalyticCode, specifiedWebAccessCode, specifiedTwitterUrl,
        specifiedFacebookUrl, specifiedLinkedinUrl, specifiedFacebookAppId, specifiedFacebookAppKey, specifiedTwitterAppId, specifiedTwitterAppKey,
        specifiedSalesAndOrderManagerId1, specifiedSalesAndOrderManagerId2, specifiedProductionManagerId1, specifiedProductionManagerId2,
        specifiedStockNotificationManagerId1, specifiedStockNotificationManagerId2, specifiedisDisplayBanners
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
            //type = ko.observable(),
            type = ko.observable(specifiedIsCustomer !== undefined && specifiedIsCustomer != null ? (specifiedIsCustomer === 1 ? 1 : specifiedIsCustomer) : undefined),
            customerTypeCheck = ko.computed({
                read: function () {
                    //        if (type() !== undefined ) {
                    //            debugger;
                    //            if (type() == '1') {
                    //                debugger;
                    //            }
                    //        }
                    //        //if (isFinishedGoods() === 0) {
                    //        //    return '3';
                    //        }
                    //        //return '' + isFinishedGoods();
                },
                write: function (value) {
                    //        debugger;
                    //        //var finishedGoods = parseInt(value);
                    //        //if (finishedGoods === isFinishedGoods()) {
                    //        //    return;
                    //        //}

                    //        //isFinishedGoods(finishedGoods);
                }
            }),
            isDisplayBanners = ko.observable(specifiedisDisplayBanners),
            raveReviews = ko.observableArray([]),
            companyTerritories = ko.observableArray([]),
            addresses = ko.observableArray([]),
            users = ko.observableArray([]),
            //secondary Pages List
            secondaryPages = ko.observableArray([]),
            // ReSharper disable InconsistentNaming
            companyCMYKColors = ko.observableArray([]),
            //Color Palette
            colorPalette = ko.observable(new ColorPalette()),
            //Company Banner Set List
            companyBannerSets = ko.observableArray([]),
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
                addresses: addresses,
                users: users,
                secondaryPages: secondaryPages,
                companyCMYKColors: companyCMYKColors,
                webMasterTag: webMasterTag,
                webAnalyticCode: webAnalyticCode,
                salesAndOrderManagerId1: salesAndOrderManagerId1,
                salesAndOrderManagerId2: salesAndOrderManagerId2,
                productionManagerId1: productionManagerId1,
                productionManagerId2: productionManagerId2,
                stockNotificationManagerId1: stockNotificationManagerId1,
                stockNotificationManagerId2: stockNotificationManagerId2,
                isDisplayBanners: isDisplayBanners,
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
                result.isDisplayBanners = source.isDisplayBanners();
                result.CompanyType = CompanyType().convertToServerData(source.companyType());
                result.RaveReviews = [];
                result.CompanyContacts = [];
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
                _.each(source.users(), function (item) {
                    result.CompanyContacts.push(item.convertToServerData());
                });
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
            isDisplayBanners: isDisplayBanners,
            type: type,
            raveReviews: raveReviews,
            companyTerritories: companyTerritories,
            addresses: addresses,
            users: users,
            companyCMYKColors: companyCMYKColors,
            colorPalette: colorPalette,
            companyBannerSets: companyBannerSets,
            secondaryPages: secondaryPages,
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            customerTypeCheck: customerTypeCheck,
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
            source.StockNotificationManagerId2,
            source.isDisplayBanners
        );

        store.companyType(CompanyType.Create(source.CompanyType));
        //if (source.IsCustomer == 0) {
        //    store.type("Supplier");
        //}
        // if (source.IsCustomer == 1) {
        //    store.type("1");
        //}
        //else if (source.IsCustomer == 2) {
        //    store.type("Prospect");
        //}
        //else if (source.IsCustomer == 3) {
        //    store.type("3");
        //}
        //isFinishedGoods = ko.observable(specifiedIsFinishedGoods !== undefined && specifiedIsFinishedGoods != null ?(specifiedIsFinishedGoods === 0 ? 0 : specifiedIsFinishedGoods) : undefined),
        _.each(source.RaveReviews, function (item) {
            store.raveReviews.push(RaveReview.Create(item));
        });
        _.each(source.CompanyTerritories, function (item) {
            store.companyTerritories.push(CompanyTerritory.Create(item));
        });
        _.each(source.Addresses, function (item) {
            store.addresses.push(Address.Create(item));
        });
        _.each(source.CompanyCmykColors, function (item) {
            store.companyCMYKColors.push(CompanyCMYKColor.Create(item));
        });
        _.each(source.ContactCompanies, function (item) {
            store.users.push(CompanyContact.Create(item));
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
    // ______________  A D D R E S S   _________________//
    // ReSharper disable once InconsistentNaming
    var Address = function (specifiedAddressId, specifiedCompanyId, specifiedAddressName, specifiedAddress1, specifiedAddress2, specifiedAddress3, specifiedCity, specifiedState, specifiedCountry, specifiedPostCode, specifiedFax,
        specifiedEmail, specifiedURL, specifiedTel1, specifiedTel2, specifiedExtension1, specifiedExtension2, specifiedReference, specifiedFAO, specifiedIsDefaultAddress, specifiedIsDefaultShippingAddress,
        specifiedisArchived, specifiedTerritoryId, specifiedGeoLatitude, specifiedGeoLongitude, specifiedisPrivate,
        specifiedisDefaultTerrorityBilling, specifiedisDefaultTerrorityShipping, specifiedOrganisationId) {
        var
            self,
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            addressName = ko.observable(specifiedAddressName).extend({ required: true }),
            address1 = ko.observable(specifiedAddress1),
            address2 = ko.observable(specifiedAddress2),
            address3 = ko.observable(specifiedAddress3),
            city = ko.observable(specifiedCity),
            state = ko.observable(specifiedState),
            country = ko.observable(specifiedCountry),
            postCode = ko.observable(specifiedPostCode),
            fax = ko.observable(specifiedFax),
            email = ko.observable(specifiedEmail),
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
            territoryId = ko.observable(specifiedTerritoryId),
            geoLatitude = ko.observable(specifiedGeoLatitude),
            geoLongitude = ko.observable(specifiedGeoLongitude),
            isPrivate = ko.observable(specifiedisPrivate),
            isDefaultTerrorityBilling = ko.observable(specifiedisDefaultTerrorityBilling),
            isDefaultTerrorityShipping = ko.observable(specifiedisDefaultTerrorityShipping),
            organisationId = ko.observable(specifiedOrganisationId),
            // Errors
            errors = ko.validation.group({
                addressName: addressName
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
                organisationId: organisationId
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
                        State: state(),
                        Country: country(),
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
                        OrganisationId: organisationId()
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
            source.State,
            source.Country,
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
            source.GeoLatitude,
            source.GeoLongitude,
            source.isPrivate,
            source.isDefaultTerrorityBilling,
            source.isDefaultTerrorityShipping,
            source.OrganisationId
        );
        return address;
    };
    // ______________  Company Banner   _________________//
    // ReSharper disable once InconsistentNaming
    var CompanyBanner = function (specifiedCompanyBannerId, specifiedHeading, specifiedDescription, specifiedItemURL, specifiedButtonURL, specifiedCompanySetId, specifiedImageSource) {
        var self,
            id = ko.observable(specifiedCompanyBannerId),
            heading = ko.observable(specifiedHeading).extend({ required: true }),
            description = ko.observable(specifiedDescription),
            itemURL = ko.observable(specifiedItemURL),
            buttonURL = ko.observable(specifiedButtonURL),
            companySetId = ko.observable(specifiedCompanySetId),
            filename = ko.observable(""),
            fileBinary = ko.observable(specifiedImageSource),
            fileType = ko.observable(),
            imageSource = ko.observable(specifiedImageSource),
            //Set Name For List View
            setName = ko.observable(),
            // Errors
            errors = ko.validation.group({
                //companySetId: companySetId,
                heading: heading
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
                result.FileName = source.filename() === undefined ? null : source.filename();
                result.Bytes = source.fileBinary() === undefined ? null : source.fileBinary();
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
            source.CompanySetId,
            source.ImageSource
        );
    };

    // ______________  Company Banner  Set _________________//
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
    CompanyBannerSet.CreateNew = function () {
        return new CompanyBannerSet(0, undefined);
    };

    // ______________  CMS Page   _________________//
    // ReSharper disable once InconsistentNaming
    var CMSPage = function (specifiedPageId, specifiedPageTitle, specifiedPageKeywords, specifiedMetaTitle, specifiedMetaDescriptionContent, specifiedMetaCategoryContent,
        specifiedMetaRobotsContent, specifiedMetaAuthorContent, specifiedMetaLanguageContent, specifiedMetaRevisitAfterContent, specifiedCategoryId, specifiedPageHTML,
        specifiedImageSource, specifiedDefaultPageKeyWords) {
        var self,
            id = ko.observable(specifiedPageId),
            pageTitle = ko.observable(specifiedPageTitle).extend({ required: true }),
            pageKeywords = ko.observable(specifiedPageKeywords),
            metaTitle = ko.observable(specifiedMetaTitle),
            metaDescriptionContent = ko.observable(specifiedMetaDescriptionContent),
            metaCategoryContent = ko.observable(specifiedMetaCategoryContent),
            metaRobotsContent = ko.observable(specifiedMetaRobotsContent),
            metaAuthorContent = ko.observable(specifiedMetaAuthorContent),
            metaLanguageContent = ko.observable(specifiedMetaLanguageContent),
            metaRevisitAfterContent = ko.observable(specifiedMetaRevisitAfterContent),
            categoryId = ko.observable(specifiedCategoryId),
            pageHTML = ko.observable(specifiedPageHTML),
            imageSrc = ko.observable(specifiedImageSource),
            fileName = ko.observable(),
            defaultPageKeyWords = ko.observable(specifiedDefaultPageKeyWords),
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
                result.Image = source.imageSrc() === undefined ? null : source.imageSrc();
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
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
            reset: reset
        };
        return self;
    };
    CMSPage.Create = function (source) {
        return new CMSPage(
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
            source.DefaultPageKeyWords
        );
    };

    // ______________  Page Category   _________________//
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

    //___________  Secondary Page List View ________//
    SecondaryPageListView = function (specifiedPageId, specifiedPageTitle, specifiedMetaTitle, specifiedIsEnabled, specifiedIsDisplay, specifiedCategoryName) {
        var
            self,
            //Unique ID
            pageId = ko.observable(specifiedPageId),
            pageTitle = ko.observable(specifiedPageTitle),
            metaTitle = ko.observable(specifiedMetaTitle),
            isEnabled = ko.observable(specifiedIsEnabled),
            isDisplay = ko.observable(specifiedIsDisplay === null ? false : true),
            categoryName = ko.observable(specifiedCategoryName),

            convertToServerData = function () {
                return {
                    PageId: pageId(),
                }
            };
        self = {
            pageId: pageId,
            pageTitle: pageTitle,
            metaTitle: metaTitle,
            isEnabled: isEnabled,
            isDisplay: isDisplay,
            categoryName: categoryName,
            convertToServerData: convertToServerData,
        };
        return self;
    };
    SecondaryPageListView.Create = function (source) {
        return new SecondaryPageListView(source.PageId, source.PageTitle, source.Meta_Title, source.IsEnabled, source.IsDisplay, source.CategoryName);
    };

    //________________COMPANY CONTACT ___________________//

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
        specifiedCanUserEditProfile, specifiedcanPlaceDirectOrder, specifiedOrganisationId, specifiedBussinessAddressId) {
        var self,
                       contactId = ko.observable(specifiedContactId),
            addressId = ko.observable(specifiedAddressId),
            companyId = ko.observable(specifiedCompanyId),
            firstName = ko.observable(specifiedFirstName),
            middleName = ko.observable(specifiedMiddleName),
            lastName = ko.observable(specifiedLastName),
            title = ko.observable(specifiedTitle),
            homeTel1 = ko.observable(specifiedHomeTel1),
            homeTel2 = ko.observable(specifiedHomeTel2),
            homeExtension1 = ko.observable(specifiedHomeExtension1),
            homeExtension2 = ko.observable(specifiedHomeExtension2),
            mobile = ko.observable(specifiedMobile),
            email = ko.observable(specifiedEmail),
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
            password = ko.observable(specifiedPassword),
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
            creditLimit = ko.observable(specifiedCreditLimit),
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
                       bussinessAddressId = ko.observable(specifiedBussinessAddressId),

            // Errors
            errors = ko.validation.group({

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
                HomeCountry: homeCountry,
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
                bussinessAddressId: bussinessAddressId
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
                    IsDefaultContact: isDefaultContact(),
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
                    image: image(),
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
                    canPlaceDirectOrder: canPlaceDirectOrder(),
                    OrganisationId: organisationId(),
                    BussinessAddressId: bussinessAddressId()
                };
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
            HomeCountry: homeCountry,
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
            isValid: isValid,
            errors: errors,
            dirtyFlag: dirtyFlag,
            hasChanges: hasChanges,
            convertToServerData: convertToServerData,
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
            source.BussinessAddressId
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
            source.BussinessAddressId
        );
        return companyContact;
    };

    //// __________________  R O L E   _____-_________________//
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
        return new roleName(
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


    //// __________________  R E G I S  T R A T I O N   Q U E S T I O N  ______________________//
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
    };

});




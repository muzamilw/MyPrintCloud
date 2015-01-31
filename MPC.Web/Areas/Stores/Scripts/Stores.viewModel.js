﻿/*
    Module with the view model for the Store.
*/
define("stores/stores.viewModel",
    ["jquery", "amplify", "ko", "stores/stores.dataservice", "stores/stores.model", "common/confirmation.viewModel", "common/pagination", "stores/store.Product.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, storeProductsViewModel) {
        var ist = window.ist || {};
        ist.stores = {
            viewModel: (function () {
                var //View
                    view,
                //#region ________ O B S E R V A B L E S ________________________
                filteredCompanySetId = ko.observable(),
                //selected Current Page Id In Layout Page Tab
                selectedCurrentPageId = ko.observable(),
                selectedCurrentPageCopy = ko.observable(),
                //Active Widget (use for dynamic controll)
                selectedWidget = ko.observable(),
                //Active Company Domain
                selectedCompanyDomainItem = ko.observable(),
                //New Added fake Id counter
                newAddedWidgetIdCounter = ko.observable(0),
                //Store Image
                storeImage = ko.observable(),
                //On Added new widget id calculate
                // ReSharper disable once UnusedLocals
                newAddedWidgetIdCount = ko.observable(-1),
                //Is Loading stores
                isLoadingStores = ko.observable(false),
                //Is Editorial View Visible
                isEditorVisible = ko.observable(false),
                //Sort On
                sortOn = ko.observable(1),
                //Sort In Ascending
                sortIsAsc = ko.observable(true),
                //Pager
                pager = ko.observable(),
                //Search Filter
                searchFilter = ko.observable(),
                //selectedStore
                selectedStore = ko.observable(new model.Store),
                //Selected Address
                selectedCompanyContact = ko.observable(),
                //Make Edittable
                makeEditable = ko.observable(false),
                selectedWidget = ko.observable(),
                //Items with isPublished true for widgets in Themes And Widget Tab
                itemsForWidgets = ko.observableArray([]),
                //selected tems List For Widgets
                selectedItemsForOfferList = ko.observableArray([]),
                //selected tem For Widgets For Move To add
                selectedItemForAdd = ko.observable(),
                //selected tem For Remove
                selectedItemForRemove = ko.observable(),
                //Active offer Type
                selectedOfferType = ko.observable(),
                //Product Priority Radio Option
                productPriorityRadioOption = ko.observable("1"),

                //#endregion

                //#region ________ O B S E R V A B L E S   A R R A Y S___________
                //stores List
                stores = ko.observableArray([]),
                //system Users
                systemUsers = ko.observableArray([]),
                //Tab User And Addressed, Addresses Section Company Territories Filter
                addressCompanyTerritoriesFilter = ko.observableArray([]),
                contactCompanyTerritoriesFilter = ko.observableArray([]),
                //Addresses to be used in store users shipping and billing address
                allCompanyAddressesList = ko.observableArray([]),
                //Company Banners
                companyBanners = ko.observableArray([]),
                //Cms Pages For Store Layout DropDown
                cmsPagesForStoreLayout = ko.observableArray([]),
                //Roles
                roles = ko.observableArray([]),
                //RegistrationQuestions
                registrationQuestions = ko.observableArray([]),
                //Filetered Company Bannens List
                filteredCompanyBanners = ko.observableArray([]),
                //Company Banner Set List
                companyBannerSetList = ko.observableArray([]),
                //Page Categories
                pageCategories = ko.observableArray([]),
                //Payment Methods
                paymentMethods = ko.observableArray([]),
                //Email Events
                emailEvents = ko.observableArray([]),
                //Emails
                emails = ko.observableArray([]),
                //Widgets List
                widgets = ko.observableArray([]),
                //Page Skin Widgets
                pageSkinWidgets = ko.observableArray([]),
                //All widgets list for pages (on page change added to it all widget list )
                allPagesWidgets = ko.observableArray([]),
                //parent Categories Used in Products Add/Edit
                parentCategories = ko.observableArray([]),

                selectedWidgetsList = ko.observableArray([]),
                costCentersList = ko.observableArray([]),

                //#endregion

                //#region _________E D I T O R I AL   V I E W    M O D E L_______

                // Editor View Model
                editorViewModel = new ist.ViewModel(model.StoreListView),
                //Selected store
                selectedStoreListView = editorViewModel.itemForEditing,
                    //#endregion

                //#region _________C O M P A N Y   D O M A I N___________________

                //Template To Use
                templateToUse = function (store) {
                    return (store === selectedStore() ? 'itemStoreTemplate' : 'itemStoreTemplate');
                },
                //app = sammy(function () {
                //    //this.get("#/byId/:raMainId", function () {
                //    this.get(":url", function () {
                //        //load(this.params["raMainId"]);
                //        toastr.success(this.params["url"]);
                //    });
                //}),

                // Select Company Domain
                selectCompanyDomain = function (companyDomain) {
                    if (selectedCompanyDomainItem() !== companyDomain) {
                        selectedCompanyDomainItem(companyDomain);
                    }
                },
                // Template Chooser
                templateToUseCompanyDomain = function (companyDomain) {

                    if (selectedStore().companyDomains().length > 0 && selectedStore().companyDomains()[selectedStore().companyDomains().length - 1] == companyDomain) {
                        return 'itemCompanyDomainTemplate';
                    }
                    return (companyDomain === selectedCompanyDomainItem() ? 'editCompanyDomainTemplate' : 'itemCompanyDomainTemplate');
                },
                //Delete Company Domain
                onDeleteCompanyDomainItem = function (companyDomain) {
                    if (selectedStore().companyDomains().length > 0 && selectedStore().companyDomains()[selectedStore().companyDomains().length - 1] == companyDomain) {
                        return;
                    }
                    selectedStore().companyDomains.remove(companyDomain);
                },
                //Create New Company Domain
                createCompanyDomainItem = function () {

                    //if (selectedStore().companyDomains().length > 0) {
                    var companyDomain = new model.CompanyDomain();
                    selectedCompanyDomainItem(companyDomain);
                    selectedStore().companyDomains.splice(0, 0, companyDomain);
                    //}

                    //if (costItem !== undefined && costItem !== null && !costItem.isValid()) {
                    //    costItem.errors.showAllMessages();
                    //    selectedCostItem(costItem);
                    //    flag = false;
                    //}

                },
                //Function to maintain check that first company domain is correct as Web Access Code
                maintainCompanyDomain = ko.computed(function () {
                    if (selectedStore() && selectedStore().webAccessCode() != undefined) {
                        if (selectedStore().companyDomains().length == 0) {
                            selectedStore().companyDomains.splice(0, 0, new model.CompanyDomain());
                            selectedStore().companyDomains()[0].domain(window.location.host + '/' + selectedStore().webAccessCode() + '/login');
                        }
                        else if (selectedStore().companyDomains().length > 0) {
                            _.each(selectedStore().companyDomains(), function (companyDomain) {
                                if (companyDomain.isMandatoryDomain()) {
                                    companyDomain.domain(window.location.host + '/' + selectedStore().webAccessCode() + '/login');
                                }
                            });
                        }
                    }
                }),
            //#endregion

                //#region _________S T O R E ____________________________________

                //getItemsForWidgets
                getItemsForWidgets = function (callBack) {
                    dataservice.getItemsForWidgets({
                        success: function (data) {
                            if (data != null) {
                                itemsForWidgets.removeAll();
                                _.each(data, function (item) {
                                    var itemForWidget = model.ItemForWidgets.Create(item);
                                    itemsForWidgets.push(itemForWidget);
                                });
                            }
                        },
                        error: function (response) {
                            //toastr.error("Failed to Delete . Error: " + response);
                        }
                    });
                },
                //Create New Store
                createNewStore = function () {
                    var store = new model.Store();
                    editorViewModel.selectItem(store);
                    //selectedStore(store);
                    isStoreEditorVisible(true);
                    // getItemsForWidgets();
                },
                //On Edit Click Of Store
                onEditItem = function (item) {

                    editorViewModel.selectItem(item);
                    openEditDialog();
                    $('.nav-tabs').children().removeClass('active');
                    $('#generalInfoTab').addClass('active');
                },
                //On Edit Click Of Store
                onCreateNewStore = function () {
                    filteredCompanyBanners.removeAll();
                    companyBannerSetList.removeAll();
                    selectedStore(new model.Store);
                    isEditorVisible(true);
                    view.initializeForm();
                    getBaseDataFornewCompany();
                    $('.nav-tabs').children().removeClass('active');
                    $('#generalInfoTab').addClass('active');

                    selectedItemsForOfferList.removeAll();
                    selectedItemForAdd(undefined);
                    selectedItemForRemove(undefined);
                    if (itemsForWidgets().length === 0) {
                        getItemsForWidgets();
                    }
                },
                //To Show/Hide Edit Section
                isStoreEditorVisible = ko.observable(false),
                //Delete Stock Category
                deleteStore = function (store) {
                    dataservice.deleteStore({
                        CompanyId: store.companyId(),
                    }, {
                        success: function (data) {
                            if (data != null) {
                                stores.remove(store);
                                toastr.success(" Deleted Successfully !");
                            }
                        },
                        error: function (response) {
                            toastr.error("Failed to Delete . Error: " + response);
                        }
                    });
                },
                //GET Stores For Stores List View
                getStores = function () {
                    isLoadingStores(true);
                    dataservice.getStores({
                        SearchString: searchFilter(),
                        PageSize: pager().pageSize(),
                        PageNo: pager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            stores.removeAll();
                            if (data != null) {
                                pager().totalCount(data.RowCount);
                                _.each(data.Companies, function (item) {
                                    var module = model.StoreListView.Create(item);
                                    stores.push(module);
                                });
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Error: Failed To load Stores " + response);
                        }
                    });
                },
                //Store Image Files Loaded Callback
                    storeImageFilesLoadedCallback = function (file, data) {
                        selectedStore().storeImageFileBinary(data);
                        selectedStore().storeImageName(file.name);
                        //selectedProductCategoryForEditting().fileType(data.imageType);
                    },
                //store Backgroud Image Upload Callback
                 storeBackgroudImageUploadCallback = function (file, data) {
                     selectedStore().storeBackgroudImageImageSource(data);
                     selectedStore().storeBackgroudImageFileName(file.name);
                 },

                //Restore sprite Image
                 restoreSpriteImage = function () {
                     selectedStore().userDefinedSpriteImageSource(selectedStore().defaultSpriteImageSource());
                     selectedStore().userDefinedSpriteImageFileName("default.jpg");
                 },
                spriteImageLoadedCallback = function (file, data) {
                    selectedStore().userDefinedSpriteImageSource(data);
                    selectedStore().userDefinedSpriteImageFileName(file.name);
                },
                //#endregion _____________________  S T O R E ____________________

                // #region _________R A V E   R E V I E W_________________________

                //Selected Rave Review
                selectedRaveReview = ko.observable(),
                // Template Chooser For Rave Review
                templateToUseRaveReviews = function (raveReview) {
                    return (raveReview === selectedRaveReview() ? 'editRaveReviewTemplate' : 'itemRaveReviewTemplate');
                },
                //Create Stock Sub Category
                onCreateNewRaveReview = function () {
                    var raveReview = new model.RaveReview();
                    selectedRaveReview(raveReview);
                    view.showRaveReviewDialog();

                    //var raveReview = selectedRaveReview().raveReviews()[0];
                    ////Create Rave Reviews for the very First Time
                    //if (raveReview == undefined) {
                    //    selectedRaveReview().raveReviews.splice(0, 0, new model.RaveReview());
                    //    selectedRaveReview(selectedStore().raveReviews()[0]);
                    //}
                    //    //If There are already rave reviews in list
                    //else {
                    //    if (!raveReview.isValid()) {
                    //        raveReview.errors.showAllMessages();
                    //    }
                    //    else {
                    //        selectedRaveReview().raveReviews.splice(0, 0, new model.RaveReview());
                    //        selectedRaveReview(selectedStore().raveReviews()[0]);
                    //    }
                    //}
                },
                // Delete a Rave review
                onDeleteRaveReview = function (raveReview) {
                    selectedStore().raveReviews.remove(raveReview);
                    return;
                },
                onEditRaveReview = function (raveReview) {
                    selectedRaveReview(raveReview);
                    view.showRaveReviewDialog();
                },
                onCloseRaveReview = function () {
                    view.hideRaveReviewDialog();
                },
                //Do Before Save Rave Review
                doBeforeSaveRaveReview = function () {
                    var flag = true;
                    if (!selectedRaveReview().isValid()) {
                        selectedRaveReview().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveRaveReview = function () {
                    if (doBeforeSaveRaveReview()) {
                        selectedStore().raveReviews.splice(0, 0, selectedRaveReview());
                        view.hideRaveReviewDialog();
                    }
                },
                // #endregion ______________ RAVE REVIEW  _______________

                // #region _________C O M P A N Y   T E R R I T O R Y ____________

                //Selected CompanyTerritory
                selectedCompanyTerritory = ko.observable(),
                //Deleted Company Territory 
                deletedCompanyTerritories = ko.observableArray([]),
                edittedCompanyTerritories = ko.observableArray([]),
                newCompanyTerritories = ko.observableArray([]),
                //Company Territory Pager
                companyTerritoryPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //CompanyTerritory Search Filter
                searchCompanyTerritoryFilter = ko.observable(),
                //Search Company Territory
                searchCompanyTerritory = function () {
                    dataservice.searchCompanyTerritory({
                        SearchFilter: searchCompanyTerritoryFilter(),
                        CompanyId: selectedStore().companyId(),
                        PageSize: companyTerritoryPager().pageSize(),
                        PageNo: companyTerritoryPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            companyTerritoryPager().totalCount(data.RowCount);
                            selectedStore().companyTerritories.removeAll();
                            _.each(data.CompanyTerritories, function (companyTerritoryItem) {
                                var companyTerritory = new model.CompanyTerritory.Create(companyTerritoryItem);
                                selectedStore().companyTerritories.push(companyTerritory);
                            });
                            _.each(edittedCompanyTerritories(), function (item) {
                                _.each(selectedStore().companyTerritories(), function (territoryItem) {
                                    if (item.territoryId() == territoryItem.territoryId()) {
                                        selectedStore().companyTerritories.remove(territoryItem);
                                    }
                                });
                            });
                            _.each(deletedCompanyTerritories(), function (item) {
                                _.each(selectedStore().companyTerritories(), function (territoryItem) {
                                    if (item.territoryId() == territoryItem.territoryId()) {
                                        selectedStore().companyTerritories.remove(territoryItem);
                                    }
                                });
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Company territories" + response);
                        }
                    });
                },
                //isSavingNewCompanyTerritory
                isSavingNewCompanyTerritory = ko.observable(false),
                // Template Chooser For Rave Review
                templateToUseCompanyTerritories = function (companyTerritory) {
                    return (companyTerritory === selectedCompanyTerritory() ? 'editCompanyTerritoryTemplate' : 'itemCompanyTerritoryTemplate');
                },
                //Create Company Territory
                onCreateNewCompanyTerritory = function () {
                    var companyTerritory = new model.CompanyTerritory();
                    selectedCompanyTerritory(companyTerritory);
                    isSavingNewCompanyTerritory(true);
                    view.showCompanyTerritoryDialog();
                },
                // Delete Company Territory
                onDeleteCompanyTerritory = function (companyTerritory) {
                    if (companyTerritory.companyId() !== undefined) {
                        _.each(edittedCompanyTerritories(), function (item) {
                            if (item.territoryId() == companyTerritory.territoryId()) {
                                edittedCompanyTerritories.remove(companyTerritory);
                            }
                        });
                        deletedCompanyTerritories.push(companyTerritory);
                    }
                    selectedStore().companyTerritories.remove(companyTerritory);
                    return;
                },
                onEditCompanyTerritory = function (companyTerritory) {
                    selectedCompanyTerritory(companyTerritory);
                    isSavingNewCompanyTerritory(false);
                    view.showCompanyTerritoryDialog();
                },
                onCloseCompanyTerritory = function () {
                    view.hideCompanyTerritoryDialog();
                    isSavingNewCompanyTerritory(false);
                },
                //Do Before Save Company Territory
                doBeforeSaveCompanyTerritory = function () {
                    var flag = true;
                    if (!selectedCompanyTerritory().isValid()) {
                        selectedCompanyTerritory().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveCompanyTerritory = function () {
                    if (doBeforeSaveCompanyTerritory()) {
                        if (selectedCompanyTerritory().territoryId() === undefined && isSavingNewCompanyTerritory() === true) {
                            selectedStore().companyTerritories.splice(0, 0, selectedCompanyTerritory());
                            newCompanyTerritories.push(selectedCompanyTerritory());
                        } else {
                            //pushing item in editted Company Territories List
                            if (selectedCompanyTerritory().companyId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedCompanyTerritories(), function (item) {
                                    return (selectedCompanyTerritory().territoryName() === item.territoryName() && selectedCompanyTerritory().territoryCode() === item.territoryCode());
                                });

                                if (!match) {
                                    edittedCompanyTerritories.push(selectedCompanyTerritory());
                                }

                            }
                        }
                        view.hideCompanyTerritoryDialog();
                    }
                },
                // #endregion __________________ C O M P A N Y   T E R R I T O R Y __________________

                // #region _________C O M P A N Y    C M Y K   C O L O R  ________

                //Selected Company CMYK Color
                // ReSharper disable InconsistentNaming
                selectedCompanyCMYKColor = ko.observable(),
                isSavingNew = ko.observable(false),
                // Template Chooser For Company CMYK Color
                templateToUseCompanyCMYKColors = function (companyCMYKColor) {
                    return (companyCMYKColor === selectedCompanyCMYKColor() ? 'editCompanyCMYKColorTemplate' : 'itemCompanyCMYKColorTemplate');
                },
                //Create Stock Sub Category
                onCreateNewCompanyCMYKColor = function () {
                    var companyCMYKColor = new model.CompanyCMYKColor();
                    selectedCompanyCMYKColor(companyCMYKColor);
                    view.showCompanyCMYKColorDialog();
                    isSavingNew(true);
                    //var companyCMYKColor = selectedStore().companyCMYKColors()[0];
                    ////Create Company CMYK Color for the very First Time
                    //if (companyCMYKColor == undefined) {
                    //    selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                    //    selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                    //}
                    //    //If There are already company CMYK Color in list
                    //else {
                    //    if (!companyCMYKColor.isValid()) {
                    //        companyCMYKColor.errors.showAllMessages();
                    //    }
                    //    else {
                    //        selectedStore().companyCMYKColors.splice(0, 0, new model.CompanyCMYKColor());
                    //        selectedCompanyCMYKColor(selectedStore().companyCMYKColors()[0]);
                    //    }
                    //}
                },
                // Delete a company CMYK Color
                onDeleteCompanyCMYKColors = function (companyCMYKColor) {
                    selectedStore().companyCMYKColors.remove(companyCMYKColor);
                    return;
                },
                onEditCompanyCMYKColor = function (companyCMYKColor) {
                    selectedCompanyCMYKColor(companyCMYKColor);
                    view.showCompanyCMYKColorDialog();
                },
                onCloseCompanyCMYKColor = function () {
                    view.hideCompanyCMYKColorDialog();
                    isSavingNew(false);
                },
                //Do Before Save Rave Review
                doBeforeSaveCompanyCMYKColor = function () {
                    var flag = true;
                    if (!selectedCompanyCMYKColor().isValid()) {
                        selectedCompanyCMYKColor().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveCompanyCMYKColor = function () {
                    if (doBeforeSaveCompanyCMYKColor() && isSavingNew() == true) {
                        selectedStore().companyCMYKColors.splice(0, 0, selectedCompanyCMYKColor());
                        view.hideCompanyCMYKColorDialog();
                        isSavingNew(false);
                    }

                },
                // #endregion ____________ C O M P A N Y    C M Y K   C O L O R  ___________________ 

                //#region _________COMPANY BANNER AND COMPANY BANNER SET ________
                bannerEditorViewModel = new ist.ViewModel(model.CompanyBanner),
                selectedCompanyBanner = bannerEditorViewModel.itemForEditing,
                selectedCompanyBannerSet = ko.observable(),
                addBannerCount = ko.observable(-1),
                addBannerSetCount = ko.observable(-1),
                //Craete Banner
                onCreateBanner = function () {
                    selectedCompanyBanner(model.CompanyBanner());
                    selectedCompanyBanner().description("");
                    view.showEditBannerDialog();
                },
                //Create Banner Set
                onAddSetBanner = function () {
                    selectedCompanyBannerSet(model.CompanyBannerSet.CreateNew());
                    view.showSetBannerDialog();
                },
                //Save Company Banner
                onSaveCompanyBanner = function (companyBanner) {
                    if (doBeforeSaveCompanyBanner()) {
                        _.each(companyBannerSetList(), function (item) {
                            if (item.id() === companyBanner.companySetId()) {
                                companyBanner.setName(item.setName());
                            }
                        });
                        if (companyBanner.id() === undefined) {
                            companyBanner.id(addBannerCount() - 1);
                            if (companyBanner.companySetId() === filteredCompanySetId() || filteredCompanySetId() === undefined) {
                                filteredCompanyBanners.splice(0, 0, companyBanner);
                                companyBanners.splice(0, 0, companyBanner);
                            } else {
                                companyBanners.splice(0, 0, companyBanner);
                            }
                        } else {
                            _.each(companyBanners(), function (item) {
                                if (item.id() === companyBanner.id()) {
                                    item.heading(companyBanner.heading());
                                    item.description(companyBanner.description());
                                    item.itemURL(companyBanner.itemURL());
                                    item.buttonURL(companyBanner.buttonURL());
                                    item.companySetId(companyBanner.companySetId());
                                    item.fileBinary(companyBanner.fileBinary());
                                    item.imageSource(companyBanner.fileBinary());
                                    item.filename(companyBanner.filename());

                                }
                            });
                        }
                        view.hideEditBannerDialog();
                    }
                },
                //Save Banner Set
                onSaveBannerSet = function (bannerSet) {
                    if (doBeforeSaveCompanyBannerSet()) {
                        bannerSet.id(addBannerSetCount() - 1);
                        companyBannerSetList.push(bannerSet);
                        view.hideSetBannerDialog();
                    }
                },
                // Do Before Logic
                doBeforeSaveCompanyBannerSet = function () {
                    var flag = true;
                    if (!selectedCompanyBannerSet().isValid()) {
                        selectedCompanyBannerSet().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                // Do Before Logic
                doBeforeSaveCompanyBanner = function () {
                    var flag = true;
                    if (!selectedCompanyBanner().isValid()) {
                        selectedCompanyBanner().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Edit Company Banner
                onEditCompanyBanner = function (banner) {
                    bannerEditorViewModel.selectItem(banner);
                    selectedCompanyBanner().reset();
                    view.showEditBannerDialog();
                },
                //Cancel Company Banner
                onCloseCompanyBanner = function () {
                    if (selectedCompanyBanner() != undefined) {

                        view.hideEditBannerDialog();
                        bannerEditorViewModel.revertItem();
                    }
                },
                //Filter Banners based on banner set id
                // ReSharper disable once UnusedLocals
                filterBannerSet = ko.computed(function () {
                    if (filteredCompanySetId() !== undefined) {
                        filteredCompanyBanners.removeAll();
                        _.each(companyBanners(), function (item) {
                            if (item.companySetId() === filteredCompanySetId()) {
                                filteredCompanyBanners.push(item);
                            }
                        });
                    } else {
                        filteredCompanyBanners.removeAll();
                        ko.utils.arrayPushAll(filteredCompanyBanners(), companyBanners());
                        filteredCompanyBanners.valueHasMutated();
                    }


                }, this),
                //Dalete company Banner
                onDeleteCompanyBanner = function (banner) {
                    if (!banner.id()) {
                        companyBanners.remove(banner);
                        return;
                    }
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        _.each(companyBanners(), function (item) {
                            if (item.id() === banner.id()) {
                                companyBanners.remove(item);
                            }
                        });
                        filteredCompanyBanners.remove(banner);
                    });
                    confirmation.show();
                },
                //#endregion 

                //#region _________EMAIL ______________________________________
                selectedEmail = ko.observable(),
                selectedSection = ko.observable(),
                campaignSectionFlags = ko.observableArray([]),
                campaignCompanyTypes = ko.observableArray([]),
                campaignGroups = ko.observableArray([]),
                emailCampaignSections = ko.observableArray([]),
                //Create One Time Marketing Email
                onCreateOneTimeMarketingEmail = function () {
                    var campaign = model.Campaign();
                    campaign.campaignType(3);
                    campaign.reset();
                    selectedEmail(campaign);
                    selectedEmail().reset();
                    view.showEmailCamapaignDialog();
                    if (campaignSectionFlags().length === 0) {
                        getCampaignBaseData();
                    }

                    resetEmailBaseDataArrays();

                    makeCkeditorDropable();
                },

                resetEmailBaseDataArrays = function () {
                    _.each(campaignSectionFlags(), function (item) {
                        item.isChecked(false);
                    });
                    _.each(campaignCompanyTypes(), function (item) {
                        item.isChecked(false);
                    });

                    _.each(campaignGroups(), function (item) {
                        item.isChecked(false);
                    });
                }
                //Create Interval Marketing Email
                onCreateIntervalMarketingEmail = function () {
                    var campaign = model.Campaign();
                    campaign.campaignType(2);
                    selectedEmail(campaign);
                    selectedEmail().reset();
                    view.showEmailCamapaignDialog();
                    if (campaignSectionFlags().length === 0) {
                        getCampaignBaseData();
                    }
                    makeCkeditorDropable();
                },
                //Save Campaign
                onSaveEmail = function (email) {
                    if (dobeforeSaveEmail()) {
                        if (email.campaignType() === 3) {
                            var flags = null;
                            _.each(campaignSectionFlags(), function (item) {
                                if (item.isChecked()) {
                                    if (flags === null) {
                                        flags = item.id();
                                    } else {
                                        flags = flags + "," + item.id();
                                    }
                                }
                            });
                            email.flagIDs(flags);
                            var cTypes = null;
                            _.each(campaignCompanyTypes(), function (item) {
                                if (item.isChecked()) {
                                    if (cTypes === null) {
                                        cTypes = item.id();
                                    } else {
                                        cTypes = cTypes + "," + item.id();
                                    }
                                }
                            });
                            email.customerTypeIDs(flags);
                            var groups = "";
                            _.each(campaignGroups(), function (item) {
                                if (item.isChecked()) {
                                    if (groups === null) {
                                        groups = item.id();
                                    } else {
                                        groups = groups + "," + item.id();
                                    }
                                }
                            });
                            email.groupIDs(groups);
                        }

                        if (email.emailEventId() !== undefined) {
                            _.each(emailEvents(), function (item) {
                                if (item.EmailEventId === email.emailEventId()) {
                                    email.eventName(item.EventName);
                                }
                            });
                        }
                        if (email.id() === undefined) {
                            emails.splice(0, 0, email);
                        } else {

                        }
                        view.hideEmailCamapaignDialog();
                    }
                },
                //Do Before Save Email
                dobeforeSaveEmail = function () {
                    var flag = true;
                    if (!selectedEmail().isValid()) {
                        selectedEmail().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Edit Email
                onEditEmail = function (campaign) {
                    selectedEmail(campaign);
                    selectedEmail().reset();
                    view.showEmailCamapaignDialog();
                    if (campaignSectionFlags().length === 0) {
                        getCampaignBaseData();
                    }
                    debugger;
                    if (selectedEmail().campaignType() === 3) {
                        resetEmailBaseDataArrays();
                        if (campaign.flagIDs() !== null && campaign.flagIDs() !== undefined) {
                            var flagIds = campaign.flagIDs().split(',');
                            for (var i = 0; i < flagIds.length; i++) {
                                _.each(campaignSectionFlags(), function (item) {
                                    if (parseInt(flagIds[i]) === item.id()) {
                                        item.isChecked(true);
                                    }

                                });
                            }
                        }
                        if (campaign.customerTypeIDs() !== null && campaign.customerTypeIDs() !== undefined) {
                            var customerTypeIDs = campaign.customerTypeIDs().split(',');
                            for (var j = 0; j < customerTypeIDs.length; j++) {
                                _.each(campaignCompanyTypes(), function (item) {
                                    if (parseInt(customerTypeIDs[j]) === item.id()) {
                                        item.isChecked(true);
                                    }

                                });
                            }
                        }
                        if (campaign.groupIDs() !== null && campaign.groupIDs() !== undefined) {
                            var groupIDs = campaign.groupIDs().split(',');
                            for (var k = 0; k < groupIDs.length; k++) {
                                _.each(campaignCompanyTypes(), function (item) {
                                    if (parseInt(groupIDs[k]) === item.id()) {
                                        item.isChecked(true);
                                    }
                                });
                            }
                        }
                    }
                    makeCkeditorDropable();
                },
                // Delete Email
                onDeleteEmail = function (email) {
                    emails.remove(email);
                },

                //Get Campaign Base
                getCampaignBaseData = function (callBack) {
                    dataservice.getCampaignBaseData({
                        success: function (data) {
                            //Section Flags
                            campaignSectionFlags.removeAll();
                            _.each(data.SectionFlags, function (item) {
                                var section = model.SectionFlag.Create(item);
                                campaignSectionFlags.push(section);
                            });

                            //Company Types
                            campaignCompanyTypes.removeAll();
                            _.each(data.CompanyTypes, function (item) {
                                var companyType = model.CampaignCompanyType.Create(item);
                                campaignCompanyTypes.push(companyType);
                            });
                            //Groups
                            campaignGroups.removeAll();
                            _.each(data.Groups, function (item) {
                                var group = model.Group.Create(item);
                                campaignGroups.push(group);
                            });

                            // Campaign Sections
                            emailCampaignSections.removeAll();
                            _.each(data.CampaignSections, function (item) {
                                var section = model.CampaignSection.Create(item);
                                _.each(item.CampaignEmailVariables, function (emailVariable) {
                                    section.campaignEmailVariables.push(model.CampaignEmailVariable.Create(emailVariable));
                                });
                                emailCampaignSections.push(section);
                            });


                            if (callBack && typeof callBack === 'function') {
                                callBack();
                            }
                        },
                        error: function () {
                            toastr.error("Failed to load base data.");
                        }
                    });
                },
                //Make Ckeditor Dropable
                makeCkeditorDropable = function () {
                    setTimeout(
                        function () {
                            $(CKEDITOR.instances.content.container.find('iframe').$[0]).droppable({
                                tolerance: 'pointer',
                                hoverClass: 'dragHover',
                                activeClass: 'dragActive',
                                drop: function (evt, ui) {
                                    droppedEmailSection(ui.helper.data('ko.draggable.data'), null, evt);
                                }
                            });
                        }, 10000);


                },
                //Active Section Item
                 selectSection = function (section) {
                     //old menu collapse
                     if (selectedSection() !== undefined) {
                         selectedSection().isExpanded(false);
                     }
                     //new selected section expand
                     section.isExpanded(true);
                     selectedSection(section);
                 },
                 campaignEmailImagesLoadedCallback = function (file, data) {
                     var campaignImage = model.CampaignImage();
                     campaignImage.imageName(file.name);
                     campaignImage.imageSource(data);
                     selectedEmail().campaignImages.push(campaignImage);
                 },
                // Returns the item being dragged
                draggedSection = function (source) {

                    return {
                        row: source.$parent,
                        section: source.$data
                    };
                },
                draggedImage = function (source) {
                    return {
                        row: source.$parent,
                        image: source.$data
                    };
                },
                draggedEmailVaribale = function (source) {
                    return {
                        row: source.$parent,
                        emailVariable: source.$data
                    };
                },

                // Widget being dropped
                // ReSharper disable UnusedParameter
                droppedEmailSection = function (source, target, event) {
                    var hTMLMessageA = CKEDITOR.instances.content.getData();
                    if (selectedEmail() !== undefined && source !== undefined && source !== null && source.section !== undefined && source.section !== null) {
                        //variableName //sectionName
                        selectedEmail().hTMLMessageA(hTMLMessageA + source.section.sectionName());
                    }
                    else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.emailVariable !== undefined && source.emailVariable !== null) {
                        selectedEmail().hTMLMessageA(hTMLMessageA + source.emailVariable.variableName());
                    }
                    else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.image !== undefined && source.image !== null) {
                        // var img = "<img  src=" + source.image.imageSource() + "/>";
                        var img = "<img width=\"100px\"  height=\"100px\" src=\"" + source.image.imageSource() + "\"/>";
                        selectedEmail().hTMLMessageA(hTMLMessageA + img);
                        //selectedEmail().hTMLMessageA(); //imageSource
                    }

                },
                //#endregion

                // #region _________A D D R E S S E S __________________________

                //Selected Address
                selectedAddress = ko.observable(),
                //SelectedAddressTerritoryFilter
                addressTerritoryFilter = ko.observable(),
                //List for Address Territory
                addressTerritoryList = ko.observableArray([]),
                //Deleted Address
                deletedAddresses = ko.observableArray([]),
                edittedAddresses = ko.observableArray([]),
                newAddresses = ko.observableArray([]),
                shippingAddresses = ko.observableArray([]),
                bussinessAddresses = ko.observableArray([]),
                selectedBussinessAddress = ko.observable(),
                selectedShippingAddress = ko.observable(),
                selectedBussinessAddressId = ko.observable(),
                selectedShippingAddressId = ko.observable(),
                //Populate addresses lists
                populateAddressesList = ko.computed(function () {
                    if (selectedCompanyContact() != undefined && selectedCompanyContact().territoryId() != undefined) {
                        shippingAddresses.removeAll();
                        bussinessAddresses.removeAll();
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                shippingAddresses.push(item);
                            }
                            if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedCompanyContact().territoryId()) {
                                bussinessAddresses.push(item);
                            }
                        });
                    }
                }),
                selectBussinessAddress = ko.computed(function () {
                    if (selectedBussinessAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedBussinessAddressId()) {
                                selectedBussinessAddress(item);
                                selectedCompanyContact().bussinessAddressId(item.addressId());
                            }
                        });
                    }
                    if (selectedBussinessAddressId() == undefined) {
                        selectedBussinessAddress(undefined);
                        if (selectedCompanyContact() != undefined) {
                            selectedCompanyContact().bussinessAddressId(undefined);
                        }
                    }
                }),
                selectShippingAddress = ko.computed(function () {
                    if (selectedShippingAddressId() != undefined) {
                        _.each(allCompanyAddressesList(), function (item) {
                            if (item.addressId() == selectedShippingAddressId()) {
                                selectedShippingAddress(item);
                                selectedCompanyContact().shippingAddressId(item.addressId());
                            }
                        });
                    }
                    if (selectedShippingAddressId() == undefined) {
                        selectedShippingAddress(undefined);
                        if (selectedCompanyContact() != undefined) {
                            selectedCompanyContact().shippingAddressId(undefined);
                        }
                    }
                }),
                //Address Pager
                addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Contact Company Pager
                contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Secondary Page Pager
                secondaryPagePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Address Search Filter
                searchAddressFilter = ko.observable(),
                //Search Address
                searchAddress = function () {
                    dataservice.searchAddress({
                        SearchFilter: searchAddressFilter(),
                        CompanyId: selectedStore().companyId(),
                        TerritoryId: addressTerritoryFilter(),
                        PageSize: addressPager().pageSize(),
                        PageNo: addressPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            selectedStore().addresses.removeAll();
                            addressPager().totalCount(data.RowCount);
                            _.each(data.Addresses, function (addressItem) {
                                var address = new model.Address.Create(addressItem);
                                selectedStore().addresses.push(address);
                            });
                            _.each(edittedAddresses(), function (item) {
                                _.each(selectedStore().addresses(), function (addressItem) {
                                    if (item.addressId() == addressItem.addressId()) {
                                        selectedStore().addresses.remove(addressItem);
                                    }
                                });
                            });
                            _.each(deletedAddresses(), function (item) {
                                _.each(selectedStore().addresses(), function (addressItem) {
                                    if (item.addressId() == addressItem.addressId()) {
                                        selectedStore().addresses.remove(addressItem);
                                    }
                                });
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Addresses" + response);
                        }
                    });
                },
                addressTerritoryFilterSelected = ko.computed(function () {
                    if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined && selectedStore().companyId() !== undefined) {
                        searchAddress();
                    }
                }),
                //isSavingNewAddress
                isSavingNewAddress = ko.observable(false),
                // Template Chooser For Address
                templateToUseAddresses = function (address) {
                    return (address === selectedAddress() ? 'editAddressTemplate' : 'itemAddressTemplate');
                },
                //Create Address
                onCreateNewAddress = function () {
                    var address = new model.Address();
                    selectedAddress(address);
                    isSavingNewAddress(true);
                    view.showAddressDialog();
                },
                // Delete Address
                onDeleteAddress = function (address) {
                    if (address.addressId() !== undefined) {
                        _.each(edittedAddresses(), function (item) {
                            if (item.addressId() == address.addressId()) {
                                edittedAddresses.remove(address);
                            }
                        });
                        deletedAddresses.push(address);
                    }
                    selectedStore().addresses.remove(address);
                    return;
                },
                onEditAddress = function (address) {
                    selectedAddress(address);
                    isSavingNewAddress(false);
                    view.showAddressDialog();
                },
                onCloseAddress = function () {
                    view.hideAddressDialog();
                    isSavingNewAddress(false);
                },
                //Do Before Save Address
                doBeforeSaveAddress = function () {
                    var flag = true;
                    if (!selectedAddress().isValid()) {
                        selectedAddress().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveAddress = function () {
                    if (doBeforeSaveAddress()) {
                        if (selectedAddress().addressId() === undefined && isSavingNewAddress() === true) {
                            selectedStore().addresses.splice(0, 0, selectedAddress());
                            newAddresses.push(selectedAddress());
                        } else {
                            //pushing item in editted Addresses List
                            if (selectedAddress().addressId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedAddresses(), function (item) {
                                    return (selectedAddress().addressId() === item.addressId());
                                });

                                if (!match) {
                                    edittedAddresses.push(selectedAddress());
                                }
                            }
                        }
                        view.hideAddressDialog();
                    }
                },
                // #endregion

                //#region _________Secondry Page ________________________________
                selectedSecondaryPage = ko.observable(),
                selectedPageCategory = ko.observable(),
                newAddedSecondaryPage = ko.observableArray([]),
                editedSecondaryPage = ko.observableArray([]),
                deletedSecondaryPage = ko.observableArray([]),
                nextSecondaryPageIdCounter = ko.observable(0),
                //Add New Secondary PAge
                onAddSecondaryPage = function () {
                    selectedSecondaryPage(model.CMSPage());
                    selectedSecondaryPage().metaTitle("");
                    view.showSecondoryPageDialog();
                },
                //Add Secondry Page Category
                onAddSecondryPageCategory = function () {
                    selectedPageCategory(model.PageCategory());
                    view.showSecondaryPageCategoryDialog();
                },
                colseSecondaryPageCategoryDialog = function () {
                    view.hideSecondaryPageCategoryDialog();
                }
                //Get Secondory Pages
                getSecondoryPages = function () {
                    dataservice.getSecondaryPages({
                        CompanyId: selectedStore().companyId(),
                        PageSize: secondaryPagePager().pageSize(),
                        PageNo: secondaryPagePager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            selectedStore().secondaryPages.removeAll();
                            _.each(data.CmsPages, function (cmsPage) {
                                selectedStore().secondaryPages.push(model.SecondaryPageListView.Create(cmsPage));
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Secondary Pages" + response);
                        }
                    });
                },
                //Edit Secondary Page
                onEditSecondaryPage = function (secondaryPage) {
                    //If Newly added item edited i-e It is not save in db yet
                    if (secondaryPage.pageId() < 0) {
                        _.each(newAddedSecondaryPage(), function (item) {
                            if (item.id() === secondaryPage.pageId())
                                selectedSecondaryPage(item);
                            view.showSecondoryPageDialog();
                        });
                    } else {
                        dataservice.getSecondryPageById({
                            id: secondaryPage.pageId(),
                        }, {
                            success: function (data) {
                                if (data != null) {
                                    selectedSecondaryPage(model.CMSPage.Create(data));
                                    view.showSecondoryPageDialog();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Secondary Page Detail . Error: " + response);
                            }
                        });
                    }


                },
                //Delete Secondary Page
                onDeleteSecondaryPage = function (secondaryPage) {
                    if (!secondaryPage.pageId()) {
                        //companyBanners.remove(secondaryPage);
                        return;
                    }
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        _.each(newAddedSecondaryPage(), function (item) {
                            if (item.id() == secondaryPage.pageId()) {
                                newAddedSecondaryPage.remove(item);
                            }
                        });
                        //Delete From Current Secondary Page
                        _.each(selectedStore().secondaryPages(), function (item) {
                            if (item.pageId() == secondaryPage.pageId()) {
                                selectedStore().secondaryPages.remove(item);
                            }
                        });
                        //delete Form edit Secondary Pages
                        _.each(editedSecondaryPage(), function (item) {
                            if (item.id() == secondaryPage.pageId()) {
                                editedSecondaryPage.remove(item);
                            }
                        });
                    });
                    confirmation.show();
                },
                //Add Default PAge Keywords
                addDefaultPageKeyWords = function () {
                    selectedSecondaryPage().pageKeywords(selectedSecondaryPage().defaultPageKeyWords());
                },
                //Save Secondary Page
                onSaveSecondaryPage = function (sPage) {
                    if (doBeforeSaveSecondaryPage()) {
                        var pageHtml = CKEDITOR.instances.content.getData();
                        sPage.pageHTML(pageHtml);
                        //Newly Added, Edit 
                        if (sPage.id() < 0) {
                            _.each(newAddedSecondaryPage(), function (item) {
                                if (item.id() == sPage.id()) {
                                    editedSecondaryPage.remove(item);
                                }
                            });
                            _.each(selectedStore().secondaryPages(), function (item) {
                                if (item.pageId() == sPage.id()) {
                                    secondaryPageCopierForListView(item, sPage);
                                }
                            });
                            editedSecondaryPage.push(sPage);
                        }
                            //Old Secondary Page Edited that is saved in db already
                        else if (sPage.id() > 0) {
                            _.each(editedSecondaryPage(), function (item) {
                                if (item.id() == sPage.id()) {
                                    editedSecondaryPage.remove(item);
                                }
                            });
                            _.each(selectedStore().secondaryPages(), function (item) {
                                if (item.pageId() == sPage.id()) {
                                    secondaryPageCopierForListView(item, sPage);
                                }
                            });
                            editedSecondaryPage.push(sPage);
                        }
                            //New Secondary PAge Added
                        else if (sPage.id() === undefined) {
                            var nextId = nextSecondaryPageIdCounter() - 1;
                            sPage.id(nextId);
                            newAddedSecondaryPage.push(sPage);
                            var newPage = model.SecondaryPageListView();
                            newPage.pageId(sPage.id());
                            secondaryPageCopierForListView(newPage, sPage);
                            selectedStore().secondaryPages.splice(0, 0, newPage);
                            nextSecondaryPageIdCounter(nextId);
                        }
                        //Hide Dialog
                        view.hideSecondoryPageDialog();
                    }
                },
                //Secondary Page Copier
                // ReSharper disable once UnusedLocals
                secondaryPageCopier = function (target, source) {
                    target.pageTitle(source.pageTitle());
                    target.pageKeywords(source.pageKeywords);
                    target.metaTitle(source.metaTitle());
                    target.metaDescriptionContent(source.metaDescriptionContent());
                    target.metaCategoryContent(source.metaCategoryContent());
                    target.metaRobotsContent(source.metaRobotsContent());
                    target.metaAuthorContent(source.metaAuthorContent());
                    target.metaLanguageContent(source.metaLanguageContent());
                    target.metaRevisitAfterContent(source.metaRevisitAfterContent());
                    target.categoryId(source.categoryId());
                    target.pageHTML(source.pageHTML());
                    target.imageSrc(source.imageSrc());
                    target.fileName(source.fileName());
                    target.defaultPageKeyWords(source.defaultPageKeyWords());
                    //return target;
                },
                //Secondary Page Copier FOr List View
                secondaryPageCopierForListView = function (target, source) {
                    target.pageTitle(source.pageTitle());
                    target.metaTitle(source.metaTitle());
                    target.isEnabled(false);
                    target.isDisplay(false);
                    _.each(pageCategories(), function (item) {
                        if (item.id() == source.categoryId()) {
                            target.categoryName(item.name());
                        }
                    });
                },
                //Save Page Category
                // ReSharper disable once UnusedParameter
                onSavePageCategory = function (categoryPage) {
                    var flag = true;
                    _.each(pageCategories(), function (item) {
                        if (flag) {
                            if (!doBeforeSavePageCategory(item)) {
                                flag = false;
                            }
                        }
                    });
                    if (flag) {
                        //Update The Page Category Name in Secondary Page List
                        _.each(pageCategories(), function (item) {
                            _.each(selectedStore().secondaryPages(), function (sPage) {
                                if (item.pageId() === sPage.categoryId()) {
                                    sPage.categoryName(item.name);
                                }
                            });
                        });
                        //Hide Dialog
                        view.hideSecondaryPageCategoryDialog();
                    }
                },
                //Before Save Logic
                doBeforeSaveSecondaryPage = function () {
                    var flag = true;
                    if (!selectedSecondaryPage().isValid()) {
                        selectedSecondaryPage().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //Before Save Logic
                doBeforeSavePageCategory = function (categoryPage) {
                    var flag = true;
                    if (!categoryPage.isValid()) {
                        categoryPage.errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //#endregion

                // #region _________C O M P A N  Y   C O N T A C T _________________

                //companyContactFilter
                companyContactFilter = ko.observable(),
                contactCompanyTerritoryFilter = ko.observable(),
                //Deleted Company Contact 
                deletedCompanyContacts = ko.observableArray([]),
                edittedCompanyContacts = ko.observableArray([]),
                newCompanyContacts = ko.observableArray([]),
                //Company Contact  Pager
                companyContactPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
                //Company Contact Search Filter
                searchCompanyContactFilter = ko.observable(),
                //Search Company Contact        
                searchCompanyContact = function () {
                    dataservice.searchCompanyContact({
                        SearchFilter: searchCompanyContactFilter(),
                        CompanyId: selectedStore().companyId(),
                        TerritoryId: contactCompanyTerritoryFilter(),
                        PageSize: companyContactPager().pageSize(),
                        PageNo: companyContactPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            selectedStore().users.removeAll();
                            contactCompanyPager().totalCount(data.RowCount);
                            _.each(data.CompanyContacts, function (companyContactItem) {
                                var companyContact = new model.CompanyContact.Create(companyContactItem);
                                selectedStore().users.push(companyContact);
                            });
                            _.each(edittedCompanyContacts(), function (item) {
                                _.each(selectedStore().users(), function (companyContactItem) {
                                    if (item.contactId() == companyContactItem.contactId()) {
                                        selectedStore().users.remove(companyContactItem);
                                    }
                                });
                            });
                            _.each(deletedCompanyContacts(), function (item) {
                                _.each(selectedStore().users(), function (companyContactItem) {
                                    if (item.contactId() == companyContactItem.contactId()) {
                                        selectedStore().users.remove(companyContactItem);
                                    }
                                });
                            });
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Users" + response);
                        }
                    });
                },
                companyContactFilterSelected = ko.computed(function () {
                    if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined) {
                        searchCompanyContact();
                    }
                }),
                //isSavingNewCompanyContact
                isSavingNewCompanyContact = ko.observable(false),
                // Template Chooser For CompanyContact
                templateToUseCompanyContacts = function (companyContact) {
                    return (companyContact === selectedCompanyContact() ? 'editCompanyContactTemplate' : 'itemCompanyContactTemplate');
                },
                //Create CompanyContact
                onCreateNewCompanyContact = function () {
                    var user = new model.CompanyContact();
                    selectedCompanyContact(user);
                    isSavingNewCompanyContact(true);
                    view.showCompanyContactDialog();
                },
                // Delete CompanyContact
                onDeleteCompanyContact = function (companyContact) {
                    if (companyContact.contactId() !== undefined) {
                        _.each(edittedCompanyContacts(), function (item) {
                            if (item.contactId() == companyContact.contactId()) {
                                edittedCompanyContacts.remove(companyContact);
                            }
                        });
                        deletedCompanyContacts.push(companyContact);
                    }
                    selectedStore().users.remove(companyContact);
                    return;
                },
                onEditCompanyContact = function (companyContact) {
                    selectedCompanyContact(companyContact);
                    isSavingNewCompanyContact(false);
                    view.showCompanyContactDialog();
                },
                onCloseCompanyContact = function () {
                    view.hideCompanyContactDialog();
                    isSavingNewCompanyContact(false);
                },
                //Do Before Save CompanyContact
                doBeforeSaveCompanyContact = function () {
                    var flag = true;
                    if (!selectedCompanyContact().isValid()) {
                        selectedCompanyContact().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSaveCompanyContact = function () {
                    if (doBeforeSaveCompanyContact()) {
                        if (selectedCompanyContact().contactId() === undefined && isSavingNewCompanyContact() === true) {
                            selectedStore().users.splice(0, 0, selectedCompanyContact());
                            newCompanyContacts.push(selectedCompanyContact());
                        } else {
                            //pushing item in editted CompanyContacts List
                            if (selectedCompanyContact().contactId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedCompanyContacts(), function (item) {
                                    return (selectedCompanyContact().contactId() === item.contactId());
                                });

                                if (!match) {
                                    edittedCompanyContacts.push(selectedCompanyContact());
                                }

                            }
                        }
                        view.hideCompanyContactDialog();
                    }
                },
                UserProfileImageFileLoadedCallback = function (file, data) {
                    selectedCompanyContact().image(data);
                    selectedCompanyContact().fileName(file.name);
                },
                // #endregion

                // #region _________P A Y M E N T    G A T E W A Y _________________

                isAccessCodeSectionVisible = ko.observable(false),
                paymentMethodName = ko.observable(),
                //Selected Payment Gateway
                selectedPaymentGateway = ko.observable(),
                // Template Chooser For Payment Gateway
                templateToUsePaymentGateways = function (paymentGateway) {
                    return (paymentGateway === selectedPaymentGateway() ? 'editPaymentGatewayTemplate' : 'itemPaymentGatewayTemplate');
                },
                //Create Payment Gateway
                onCreateNewPaymentGateway = function () {
                    var paymentGateway = new model.PaymentGateway();
                    selectedPaymentGateway(paymentGateway);
                    view.showPaymentGatewayDialog();
                },
                // Delete a Payment Gateway
                onDeletePaymentGateway = function (paymentGateway) {
                    selectedStore().paymentGateway.remove(paymentGateway);
                    return;
                },
                onEditPaymentGateway = function (paymentGateway) {
                    selectedPaymentGateway(paymentGateway);
                    view.showPaymentGatewayDialog();
                },
                onClosePaymentGateway = function () {
                    view.hidePaymentGatewayDialog();
                },
                //Do Before Save Payment Gateway
                doBeforeSavePaymentGateway = function () {
                    var flag = true;
                    if (!selectedPaymentGateway().isValid()) {
                        selectedPaymentGateway().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                onSavePaymentGateway = function () {
                    if (doBeforeSavePaymentGateway()) {
                        _.each(paymentMethods(), function (paymentMethod) {
                            if (paymentMethod.paymentMethodId() == selectedPaymentGateway().paymentMethodId()) {
                                selectedPaymentGateway().paymentMethodName(paymentMethod.methodName());
                            }
                        });
                        selectedStore().paymentGateway.splice(0, 0, selectedPaymentGateway());
                        view.hidePaymentGatewayDialog();
                    }
                },
                checkPaymentMethodSelection = ko.computed(function () {
                    if (isEditorVisible() && selectedPaymentGateway() != null && selectedPaymentGateway() != undefined && selectedPaymentGateway().paymentMethodId() != "") {
                        var id = selectedPaymentGateway().paymentMethodId();
                        _.each(paymentMethods(), function (paymentMethod) {
                            if (paymentMethod.paymentMethodId() == id) {

                                if (paymentMethod.methodName() == "PayPal" || paymentMethod.methodName() == "Cash" || paymentMethod.methodName() == "") {
                                    isAccessCodeSectionVisible(false);
                                } else {
                                    paymentMethodName(paymentMethod.methodName());
                                    isAccessCodeSectionVisible(true);
                                }
                            }
                        });
                    }
                }),
                // #endregion

                // #region _________P R O D U C T    C A T E G O R Y _______________

                //Product Category Counter To represent id's of new saving product categories
                productCategoryCounter = -1,
                //Counter to add 1 in Product Category 
                addProductCategoryCounter = function () {
                    productCategoryCounter = productCategoryCounter - 1;
                },
                //Counter to reset Product Category Counter
                resetProductCategoryCounter = function () {
                    productCategoryCounter = productCategoryCounter + 1;
                },
                //Selected Product Category
                selectedProductCategory = ko.observable(),
                //Selected Product Category For Editting
                selectedProductCategoryForEditting = ko.observable(),
                //Deleted Product Categories List
                deletedProductCategories = ko.observableArray([]),
                //Editted Product Categories List
                edittedProductCategories = ko.observableArray([]),
                //New Added Product Categories List
                newProductCategories = ko.observableArray([]),
                //Select Product Category
                selectProductCategory = function (category) {
                    if (selectedProductCategory() != category) {
                        selectedProductCategory(category);
                    }
                },
                //Get Category Child List Items
                getCategoryChildListItems = function (dataRecieved, event) {
                    var id = $(event.target).closest('li')[0].id;
                    if ($(event.target).closest('li').children('ol').length > 0) {
                        if ($(event.target).closest('li').children('ol').is(':hidden')) {
                            $(event.target).closest('li').children('ol').show();
                        } else {
                            $(event.target).closest('li').children('ol').hide();
                        }
                        return;
                    }
                    dataservice.getProductCategoryChilds({
                        id: id,
                    }, {
                        success: function (data) {
                            if (data.ProductCategories != null) {
                                _.each(data.ProductCategories, function (productCategory) {
                                    $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + productCategory.ProductCategoryId + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + productCategory.CategoryName + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>');
                                    ko.applyBindings(view.viewModel, $("#" + productCategory.ProductCategoryId)[0]);
                                    var category = {
                                        productCategoryId: productCategory.ProductCategoryId,
                                        categoryName: productCategory.CategoryName,
                                        parentCategoryId: id
                                    };
                                    parentCategories.push(category);
                                });
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Error: Failed To load Categories " + response);
                        }
                    });
                },
                //Open Product Category Detail
                // ReSharper disable UnusedParameter
                openProductCategoryDetail = function (dataRecieved, event) {
                    // ReSharper restore UnusedParameter
                    //var id = $(event.target).closest('li')[0].id;
                    var productCategory = new model.ProductCategory();
                    selectedProductCategory(productCategory);
                    view.showProductCategoryDialog();
                },
                //check: is Saving New Product Category
                isSavingNewProductCategory = ko.observable(false),
                //Function Call When create new Product Category 
                onCreateNewProductCategory = function () {
                    var productCategory = new model.ProductCategory();
                    //Set Product category value for by default
                    productCategory.isShelfProductCategory(true);
                    productCategory.isEnabled(true);
                    productCategory.isPublished(true);
                    //Setting Product Category Editting
                    selectedProductCategoryForEditting(productCategory);
                    //Setting drop down list of parent
                    //putting all list of categories
                    populatedParentCategoriesList.removeAll();
                    _.each(parentCategories(), function (category) {
                        populatedParentCategoriesList.splice(0, 0, category);
                    });
                    isSavingNewProductCategory(true);
                    view.showStoreProductCategoryDialog();
                },
                //Delete Product Category
                onDeleteProductCategory = function (productCategory) {
                    if (productCategory.productCategoryId() !== undefined) {
                        _.each(edittedProductCategories(), function (item) {
                            if (item.productCategoryId() == productCategory.productCategoryId()) {
                                edittedProductCategories.remove(productCategory);
                            }
                        });
                        deletedProductCategories.push(productCategory);
                    }
                    //selectedStore().companyTerritories.remove(companyTerritory);
                    return;
                },
                //Check Payment Category Is Newly Added
                checkPaymentCategoryIsNewlyAdded = function (productCategory) {
                    if (productCategory.productCategoryId() < 0) {
                        return true;
                    }
                    return false;
                },
                //On Edit Child Product Category    
                onEditChildProductCategory = function (dataRecieved, event) {
                    var id = $(event.target).closest('li')[0].id;
                    var result = _.find(newProductCategories(), function (productCategory) {
                        return productCategory.productCategoryId() == parseInt(id);
                    });
                    if (id > 0 || (result != undefined && !checkPaymentCategoryIsNewlyAdded(result))) {

                        dataservice.getProductCategoryById({
                            ProductCategoryId: id,
                            IsProductCategoryEditting: 'true'
                        }, {
                            success: function (data) {

                                if (data != null) {
                                    selectedProductCategoryForEditting(model.ProductCategory.Create(data));
                                    updateParentCategoryList(selectedProductCategoryForEditting().productCategoryId());
                                    isSavingNewProductCategory(false);
                                    view.showStoreProductCategoryDialog();
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To load Category " + response);
                            }
                        });
                    } else {
                        selectProductCategory(result);
                        editNewAddedProductCategory();
                    }
                },
                //On Edit Product Category(Parent)
                onEditProductCategory = function (productCategory) {
                    if (selectedProductCategory() != productCategory) {
                        selectProductCategory(productCategory);
                    }
                    if (!checkPaymentCategoryIsNewlyAdded(selectedProductCategory())) {
                        //Get Product Category By Id
                        dataservice.getProductCategoryById({
                            ProductCategoryId: selectedProductCategory().productCategoryId(),
                            IsProductCategoryEditting: 'true'
                        }, {
                            success: function (data) {

                                if (data != null) {
                                    selectedProductCategoryForEditting(model.ProductCategory.Create(data));
                                    updateParentCategoryList(selectedProductCategoryForEditting().productCategoryId());
                                    isSavingNewProductCategory(false);
                                    view.showStoreProductCategoryDialog();
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To load Category " + response);
                            }
                        });
                        //selectedProductCategory(productCategory);
                    } else {
                        editNewAddedProductCategory();
                    }

                },
                //Edit New Added Product Category
                editNewAddedProductCategory = function () {
                    var result = _.find(newProductCategories(), function (productCategory) {
                        return productCategory.productCategoryId() == selectedProductCategory().productCategoryId();
                    });
                    if (result != undefined) {
                        selectedProductCategoryForEditting(result);
                        view.showStoreProductCategoryDialog();
                    }
                },
                //On Close Product Category
                onCloseProductCategory = function () {
                    view.hideStoreProductCategoryDialog();
                    //resetProductCategoryCounter();
                    isSavingNewProductCategory(false);
                },
                //Do Before Save Product Category
                doBeforeSaveProductCategory = function () {
                    var flag = true;
                    if (!selectedProductCategoryForEditting().isValid()) {
                        selectedProductCategoryForEditting().errors.showAllMessages();
                        flag = false;
                    }
                    return flag;
                },
                //on Save Store Product Category
                onSaveStoreProductCategory = function () {
                    if (doBeforeSaveProductCategory()) {
                        //dataService.saveStoreProductCategory()
                        if (selectedProductCategoryForEditting().productCategoryId() === undefined) {
                            //selectedProductCategoryForEditting().productCategoryId(data.ProductCategoryId);
                            //Check Is New and Parent 
                            if (isSavingNewProductCategory() === true && selectedProductCategoryForEditting().parentCategoryId() == undefined) {
                                $("#nestable2").append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" ><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                            }
                            //Check Is New and Child
                            if (isSavingNewProductCategory() === true) {

                            }
                        }
                        //Check Is Updating and Parent is Changing
                        if (COND) {

                        }
                    }
                },
                //On Save Product Category
                onSaveProductCategory = function () {

                    if (doBeforeSaveProductCategory()) {
                        selectedProductCategoryForEditting().companyId(selectedStore().companyId());
                        dataservice.saveProductCategory(
                            selectedProductCategoryForEditting().convertToServerData()
                            //productCategory: model.ProductCategory().convertToServerData(selectedProductCategoryForEditting())
                        , {
                            success: function (data) {

                                if (data.ParentCategoryId == null) {
                                    //if saving product is editting
                                    if (selectedProductCategoryForEditting().productCategoryId() > 0) {
                                        $("#" + selectedProductCategoryForEditting().productCategoryId()).remove();
                                        //update item
                                        _.each(selectedStore().productCategories(), function (item) {
                                            if (item.productCategoryId() == selectedProductCategoryForEditting().productCategoryId()) {
                                                item.categoryName(selectedProductCategoryForEditting().categoryName());
                                                toastr.success("Category Updated Successfully");
                                            }
                                        });
                                    }
                                        //Creating new Product category
                                    else {
                                        selectedStore().productCategories.splice(0, 0, model.ProductCategory.Create(data));
                                        toastr.success("Category Added Successfully");
                                    }
                                    $("#nestable2").append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + data.ProductCategoryId + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    ko.applyBindings(view.viewModel, $("#" + data.ProductCategoryId)[0]);
                                    isLoadingStores(false);
                                    view.hideStoreProductCategoryDialog();
                                }
                                else {


                                    newProductCategories.push(model.ProductCategory.Create(data));
                                    selectedProductCategoryForEditting(model.ProductCategory.Create(data));

                                    if ($("#" + selectedProductCategoryForEditting().productCategoryId()).length > 0) {

                                        $("#" + selectedProductCategoryForEditting().productCategoryId()).remove();
                                    }
                                    //$("#" + selectedProductCategoryForEditting().parentCategoryId()).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems" ><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    $("#" + selectedProductCategoryForEditting().parentCategoryId()).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    //if (!flagAlreadyExist) {
                                    ko.applyBindings(view.viewModel, $("#" + selectedProductCategoryForEditting().productCategoryId())[0]);
                                    //}
                                    toastr.success("Category Updated Successfully");
                                }
                                var category = {
                                    productCategoryId: data.ProductCategoryId,
                                    categoryName: data.CategoryName,
                                    parentCategoryId: data.ParentCategoryId
                                };
                                parentCategories.push(category);

                                isLoadingStores(false);
                                view.hideStoreProductCategoryDialog();
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To Save Category " + response);
                            }
                        });
                    }
                },
                //On Save Product Category
                onSaveProductCategoryOld = function () {
                    //Saving New Record
                    if (doBeforeSaveProductCategory()) {
                        if (selectedProductCategoryForEditting().productCategoryId() === undefined && isSavingNewProductCategory() === true && selectedProductCategoryForEditting().parentCategoryId() == undefined) {
                            selectedProductCategoryForEditting().productCategoryId(productCategoryCounter);
                            newProductCategories.push(selectedProductCategoryForEditting());
                            $("#nestable2").append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" ><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                            ko.applyBindings(view.viewModel, $("#" + selectedProductCategoryForEditting().productCategoryId())[0]);
                            addProductCategoryCounter();
                        }
                        if (selectedProductCategoryForEditting().productCategoryId() === undefined && isSavingNewProductCategory() === true) {

                            selectedProductCategoryForEditting().productCategoryId(productCategoryCounter);
                            newProductCategories.push(selectedProductCategoryForEditting());
                            $("#" + selectedProductCategoryForEditting().parentCategoryId()).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" ><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                            ko.applyBindings(view.viewModel, $("#" + selectedProductCategoryForEditting().productCategoryId())[0]);
                            addProductCategoryCounter();

                        } else {
                            //pushing item in editted Product Categories List
                            if (selectedProductCategoryForEditting().productCategoryId() != undefined && selectedProductCategoryForEditting().productCategoryId() > 0) {
                                var match = ko.utils.arrayFirst(edittedProductCategories(), function (item) {
                                    return (selectedProductCategoryForEditting().productCategoryId() === item.productCategoryId());
                                });
                                _.each(selectedStore().productCategories(), function (item) {
                                    if (item.productCategoryId() == selectedProductCategoryForEditting().productCategoryId()) {
                                        item.categoryName(selectedProductCategoryForEditting().categoryName());
                                    }
                                });
                                //if not found in editted product categories list then push new entry in it
                                if (!match) {
                                    edittedProductCategories.push(selectedProductCategoryForEditting());
                                }
                                    //else match if match found, update item in editted list
                                else {
                                    _.each(edittedProductCategories(), function (item) {
                                        if (item.productCategoryId() == selectedProductCategoryForEditting().productCategoryId()) {
                                            edittedProductCategories.remove(item);
                                            edittedProductCategories.push(selectedProductCategoryForEditting());
                                        }
                                    });
                                }
                            }
                            else if (selectedProductCategoryForEditting().productCategoryId() != undefined && selectedProductCategoryForEditting().productCategoryId() < 0) {
                                _.each(newProductCategories(), function (item) {
                                    if (item.productCategoryId() == selectedProductCategoryForEditting().productCategoryId()) {
                                        newProductCategories.remove(item);
                                        newProductCategories.push(selectedProductCategoryForEditting());
                                        $("#" + item.productCategoryId()).find('span').text(selectedProductCategoryForEditting().categoryName());
                                    }
                                });
                            }
                        }
                        view.hideStoreProductCategoryDialog();
                    }
                },
                //Product Category Thumbnail Files Loaded Callback
                ProductCategoryThumbnailFilesLoadedCallback = function (file, data) {
                    selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(data);
                    selectedProductCategoryForEditting().productCategoryThumbnailName(file.name);
                    //selectedProductCategoryForEditting().fileType(data.imageType);
                },
                //Product Category Image Files Loaded Callback
                ProductCategoryImageFilesLoadedCallback = function (file, data) {
                    selectedProductCategoryForEditting().productCategoryImageFileBinary(data);
                    selectedProductCategoryForEditting().productCategoryImageName(file.name);
                    //selectedProductCategoryForEditting().fileType(data.imageType);
                },

                //Populate Parent Categories List

                populatedParentCategoriesList = ko.observableArray([]),
                categoriesToRemoveForParentCategoriesDropdown = ko.observableArray([]),

                populateParentCategoriesListItems = function (categoryId) {
                    _.each(parentCategories(), function (productCategory) {
                        if (productCategory.parentCategoryId == categoryId) {
                            categoriesToRemoveForParentCategoriesDropdown.splice(0, 0, productCategory);
                            populateParentCategoriesListItems(parseInt(productCategory.productCategoryId));
                        }
                    });
                },
                updateParentCategoryList = function (categoryId) {

                    populatedParentCategoriesList.removeAll();
                    categoriesToRemoveForParentCategoriesDropdown.removeAll();
                    populateParentCategoriesListItems(categoryId);

                    //putting all list of categories
                    _.each(parentCategories(), function (productCategory) {
                        if (selectedProductCategoryForEditting().productCategoryId() !== productCategory.productCategoryId) {
                            populatedParentCategoriesList.splice(0, 0, productCategory);
                        }
                    });
                    //removing child categories
                    _.each(categoriesToRemoveForParentCategoriesDropdown(), function (productCategory) {
                        populatedParentCategoriesList.remove(productCategory);
                    });

                    populatedParentCategoriesList.reverse();
                },

                //Populate Parent Categories
                populateParentCategories = ko.computed(function () {

                    if (selectedStore() != null && selectedStore() != undefined) {
                        if (selectedStore().productCategories() != undefined && selectedStore().productCategories().length > 0) {
                            parentCategories.removeAll();
                            _.each(selectedStore().productCategories(), function (item) {
                                var category = {
                                    productCategoryId: item.productCategoryId(),
                                    categoryName: item.categoryName(),
                                    parentCategoryId: undefined
                                };
                                parentCategories.push(category);
                            });
                        }
                    }
                }),

                // #endregion

                //#region _________U T I L I T Y   F U N C T I O N S__________________

                //Do Before Save
                doBeforeSave = function () {
                    var flag = true;
                    if (!selectedStore().isValid()) {
                        selectedStore().errors.showAllMessages();
                        flag = false;
                    }
                    //1- New saving company should have 1 address and 1 user
                    //2- if company is editting then company should have a 1 address and 1 user in database after saving
                    //1
                    if (!(newAddresses().length - deletedAddresses().length) > 1 || (selectedStore().addresses().length == 0 && newAddresses().length == 0 && deletedAddresses().length == 0)) {
                        toastr.error("There Should be Atleast One Address to save this Store");
                        flag = false;
                    }
                    if (!(newCompanyContacts().length - deletedCompanyContacts().length) > 1 || (selectedStore().users().length == 0 && newCompanyContacts().length == 0 && deletedCompanyContacts().length == 0)) {
                        toastr.error("There Should be Atleast One User to save this Store");
                        flag = false;
                    }

                    if (productPriorityRadioOption() === "1" && selectedItemsForOfferList().length === 0) {
                        toastr.error("There Should be Atleast One Product to Prioritize.");
                        flag = false;
                    }
                    return flag;
                },
                currentPageWidgets = function () {
                    if (selectedCurrentPageId() !== undefined) {
                        var flag = true;
                        _.each(allPagesWidgets(), function (item) {
                            if (selectedCurrentPageId() === item.pageId()) {
                                item.widgets.removeAll();
                                ko.utils.arrayPushAll(item.widgets, pageSkinWidgets());
                                item.widgets.valueHasMutated();
                                flag = false;
                            }
                        });
                        if (flag) {
                            //Add widget list of selected page into All Pages Widgets List
                            var pageWidgetList = model.CmsPageWithWidgetList();
                            pageWidgetList.pageId(selectedCurrentPageId());
                            ko.utils.arrayPushAll(pageWidgetList.widgets, pageSkinWidgets());
                            pageWidgetList.widgets.valueHasMutated();
                            allPagesWidgets.push(pageWidgetList);
                        }
                    }
                    //set Sequence Number
                    if (allPagesWidgets().length > 0) {
                        _.each(allPagesWidgets(), function (item) {
                            _.each(item.widgets(), function (widget, index) {
                                widget.sequence(index + 1);
                                widget.companyId(selectedStore().companyId());
                            });
                        });
                    }
                },
                //Save Store
                saveStore = function () {
                    if (doBeforeSave()) {
                        var storeToSave = model.Store().convertToServerData(selectedStore());
                        storeToSave.ColorPalletes.push(selectedStore().colorPalette().convertToServerData(selectedStore().colorPalette()));
                        //#region Company Territories
                        _.each(newCompanyTerritories(), function (territory) {
                            storeToSave.NewAddedCompanyTerritories.push(territory.convertToServerData());
                        });
                        _.each(edittedCompanyTerritories(), function (territory) {
                            storeToSave.EdittedCompanyTerritories.push(territory.convertToServerData());
                        });
                        _.each(deletedCompanyTerritories(), function (territory) {
                            storeToSave.DeletedCompanyTerritories.push(territory.convertToServerData());
                        });
                        //#endregion
                        //Secondary Pages
                        _.each(newAddedSecondaryPage(), function (sPage) {
                            storeToSave.NewAddedCmsPages.push(sPage.convertToServerData(sPage));
                        });
                        _.each(editedSecondaryPage(), function (sPage) {
                            storeToSave.EditCmsPages.push(sPage.convertToServerData(sPage));
                        });
                        _.each(deletedSecondaryPage(), function (sPage) {
                            storeToSave.DeletedCmsPages.push(sPage.convertToServerData(sPage));
                        });
                        //Page category
                        _.each(pageCategories(), function (pageCategory) {
                            storeToSave.PageCategories.push(pageCategory.convertToServerData(pageCategory));
                        });
                        //#region Emails (Campaigns)
                        _.each(emails(), function (email) {
                            var emailServer = email.convertToServerData(email);
                            _.each(email.campaignImages(), function (campaignImage) {
                                emailServer.CampaignImages.push(campaignImage.convertToServerData(campaignImage));
                            });
                            storeToSave.Campaigns.push(emailServer);
                        });
                        //#endregion
                        _.each(companyBannerSetList(), function (bannerSet) {
                            var bannerSetServer = bannerSet.convertToServerData(bannerSet);
                            var banners = [];
                            _.each(companyBanners(), function (banner) {
                                if (banner.companySetId() === bannerSetServer.CompanySetId) {
                                    banners.push(banner.convertToServerData(banner));
                                }
                            });
                            ko.utils.arrayPushAll(bannerSetServer.CompanyBanners, banners);
                            storeToSave.CompanyBannerSets.push(bannerSetServer);
                        });
                        currentPageWidgets();
                        //#region Page widgets
                        _.each(allPagesWidgets(), function (pageItem) {
                            var page = pageItem.convertToServerData();
                            var widgetList = [];
                            _.each(pageItem.widgets(), function (widget) {
                                var serverWidget = widget.convertToServerData();
                                if (serverWidget.WidgetId === 14) {
                                    serverWidget.CmsSkinPageWidgetParams.push(serverWidget.CmsSkinPageWidgetParam);
                                }
                                widgetList.push(serverWidget);
                                //widgetList.push(widget.convertToServerData());

                            });
                            ko.utils.arrayPushAll(page.CmsSkinPageWidgets, widgetList);
                            storeToSave.CmsPageWithWidgetList.push(page);
                        });
                        //#endregion
                        //#region Addresses
                        _.each(newAddresses(), function (address) {
                            storeToSave.NewAddedAddresses.push(address.convertToServerData());
                        });
                        _.each(edittedAddresses(), function (address) {
                            storeToSave.EdittedAddresses.push(address.convertToServerData());
                        });
                        _.each(deletedAddresses(), function (address) {
                            storeToSave.DeletedAddresses.push(address.convertToServerData());
                        });
                        //#endregion
                        //#region Product Categories
                        _.each(newProductCategories(), function (productCategory) {
                            if (productCategory.productCategoryId() < 0) {
                                productCategory.productCategoryId(undefined);
                            }
                            storeToSave.NewProductCategories.push(productCategory.convertToServerData());
                        });
                        _.each(edittedProductCategories(), function (productCategory) {
                            if (productCategory.productCategoryId() < 0) {
                                productCategory.productCategoryId(undefined);
                            }
                            storeToSave.EdittedProductCategories.push(productCategory.convertToServerData());
                        });
                        _.each(deletedProductCategories(), function (productCategory) {
                            if (productCategory.productCategoryId() < 0) {
                                productCategory.productCategoryId(undefined);
                            }
                            storeToSave.DeletedProductCategories.push(productCategory.convertToServerData());
                        });
                        //#endregion
                        // #region Company Contacts
                        _.each(newCompanyContacts(), function (companyContact) {
                            storeToSave.NewAddedCompanyContacts.push(companyContact.convertToServerData());
                        });
                        _.each(edittedCompanyContacts(), function (companyContact) {
                            storeToSave.EdittedCompanyContacts.push(companyContact.convertToServerData());
                        });
                        _.each(deletedCompanyContacts(), function (companyContact) {
                            storeToSave.DeletedCompanyContacts.push(companyContact.convertToServerData());
                        });
                        //#endregion
                        //#region Products
                        _.each(ist.storeProduct.viewModel.newAddedProducts(), function (product) {
                            if (product.id() < 0) {
                                product.id(undefined);
                            }
                            storeToSave.NewAddedProducts.push(product.convertToServerData());
                        });
                        _.each(ist.storeProduct.viewModel.edittedProducts(), function (product) {
                            storeToSave.EdittedProducts.push(product.convertToServerData());
                        });
                        _.each(ist.storeProduct.viewModel.deletedproducts(), function (product) {
                            storeToSave.Deletedproducts.push(product.convertToServerData());
                        });

                        //#endregion

                        //#region Widget Section in Themes& Widget
                        if (productPriorityRadioOption() === "1") {
                            _.each(selectedItemsForOfferList(), function (item, index) {
                                item.sortOrder(index + 1);
                                item.companyId(storeToSave.CompanyId);
                                storeToSave.CmsOffers.push(item.convertToServerData());
                            });
                        }
                        //#endregion

                        _.each(selectedStore().mediaLibraries(), function (item) {
                            storeToSave.MediaLibraries.push(item.convertToServerData());
                        });

                        //#region Cost Center
                        //storeToSave().companyCostCenters.removeAll();
                        _.each(costCentersList(), function (costCenter) {
                            if (costCenter.isSelected()) {
                                storeToSave.CompanyCostCentres.push(costCenter.convertToServerData());
                            }
                        });
                        //updateCostCentersOnStoreSaving();
                        //#endregion



                        dataservice.saveStore(
                            storeToSave, {
                                success: function (data) {
                                    //new store adding
                                    if (selectedStore().companyId() == undefined || selectedStore().companyId() == 0) {
                                        selectedStore().companyId(data.CompanyId);
                                        stores.splice(0, 0, selectedStore());
                                    }
                                    if (selectedStoreListView() && selectedStoreListView().companyId() == selectedStore().companyId()) {
                                        _.each(stores(), function (store) {
                                            if (store.companyId() == selectedStore().companyId()) {
                                                store.name(selectedStore().name());
                                                store.url(selectedStore().url());
                                                store.status(selectedStore().status());
                                                if (selectedStore().type() == "1") {
                                                    store.type("Retail Customer");
                                                }
                                                else if (selectedStore().type() == "3") {
                                                    store.type("Corporate");
                                                }
                                            }
                                        });
                                    }
                                    //selectedStore().storeId(data.StoreId);
                                    isStoreEditorVisible(false);
                                    isEditorVisible(false);
                                    toastr.success("Successfully save.");
                                    resetObservableArrays();
                                },
                                error: function (response) {
                                    toastr.error("Failed to Update . Error: " + response);
                                    isStoreEditorVisible(false);
                                }
                            });
                    }
                },
                //Open Store Dialog
                openEditDialog = function () {
                    isEditorVisible(true);
                    getStoreForEditting();
                    view.initializeForm();
                    getBaseData();
                },
                //Get Store For editting
                getStoreForEditting = function () {
                    if (itemsForWidgets().length === 0) {
                        getItemsForWidgets();
                    }
                    dataservice.getStoreById({
                        //dataservice.getStores({
                        companyId: selectedStoreListView().companyId()
                    }, {
                        success: function (data) {
                            selectedStore(model.Store());
                            if (data != null) {
                                selectedStore(model.Store.Create(data.Company));
                                //_.each(data.AddressResponse.Addresses, function (item) {
                                //    selectedStore().addresses.push(model.Address.Create(item));
                                //});
                                //_.each(data.CompanyTerritoryResponse.CompanyTerritories, function (item) {
                                //    selectedStore().companyTerritories.push(model.CompanyTerritory.Create(item));
                                //});
                                //_.each(data.CompanyContactResponse.CompanyContacts, function (item) {
                                //    selectedStore().users.push(model.CompanyContact.Create(item));
                                //});

                                _.each(data.Company.ColorPalletes, function (item) {
                                    selectedStore().colorPalette(model.ColorPalette.Create(item));
                                });
                                cmsPagesForStoreLayout.removeAll();
                                if (data.Company.CmsPagesDropDownList !== null) {
                                    ko.utils.arrayPushAll(cmsPagesForStoreLayout(), data.Company.CmsPagesDropDownList);
                                    cmsPagesForStoreLayout.valueHasMutated();
                                }
                                emails.removeAll();
                                _.each(data.Company.Campaigns, function (item) {
                                    var campaign = model.Campaign.Create(item);
                                    _.each(item.CampaignImages, function (campaignImage) {
                                        campaign.campaignImages.push(model.CampaignImage.Create(campaignImage));
                                    });
                                    emails.push(campaign);
                                });

                                addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                                companyTerritoryPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().companyTerritories, searchCompanyTerritory));
                                contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));


                                //Seconday Page List And Pager
                                secondaryPagePager(new pagination.Pagination({ PageSize: 5 }, selectedStore().secondaryPages, getSecondoryPages));
                                secondaryPagePager().totalCount(data.SecondaryPageResponse.RowCount);
                                _.each(data.SecondaryPageResponse.CmsPages, function (item) {
                                    selectedStore().secondaryPages.push(model.SecondaryPageListView.Create(item));
                                });
                                storeImage(data.ImageSource);
                                companyBannerSetList.removeAll();
                                companyBanners.removeAll();
                                filteredCompanyBanners.removeAll();
                                _.each(data.Company.CompanyBannerSets, function (item) {
                                    companyBannerSetList.push(model.CompanyBannerSet.Create(item));
                                    //Extract Company Banners from company banner set item
                                    _.each(item.CompanyBanners, function (bannerSet) {
                                        var banner = model.CompanyBanner.Create(bannerSet);
                                        banner.setName(item.SetName);
                                        companyBanners.push(banner);
                                    });
                                });
                                ko.utils.arrayPushAll(filteredCompanyBanners(), companyBanners());
                                filteredCompanyBanners.valueHasMutated();

                                //Cms Offers
                                selectedItemsForOfferList.removeAll();
                                _.each(data.Company.CmsOffers, function (item) {
                                    selectedItemsForOfferList.push(model.CmsOffer.Create(item));
                                });
                                if (selectedItemsForOfferList().length > 0) {
                                    productPriorityRadioOption("1");
                                } else {
                                    productPriorityRadioOption("2");
                                }

                                //Media Library
                                _.each(data.Company.MediaLibraries, function (item) {
                                    selectedStore().mediaLibraries.push(model.MediaLibrary.Create(item));
                                });
                            }
                            allPagesWidgets.removeAll();
                            pageSkinWidgets.removeAll();
                            selectedCurrentPageId(undefined);
                            selectedCurrentPageCopy(undefined);
                            newUploadedMediaFile(model.MediaLibrary());
                            //Update Cost Centers Selection 
                            updateSelectedStoreCostCenters();

                            selectedStore().reset();
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores . Error: " + response);
                        }
                    });
                },
                //Close Store Dialog
                closeEditDialog = function () {

                    if (selectedStore().hasChanges()) {
                        confirmation.messageText("Do you want to save changes?");
                        confirmation.afterProceed(saveStore);
                        confirmation.afterCancel(function () {
                            if (selectedStore() != undefined) {
                                if (selectedStore().companyId() > 0) {
                                    isEditorVisible(false);
                                } else {
                                    isEditorVisible(false);
                                    stores.remove(selectedStore());
                                }
                                editorViewModel.revertItem();
                                allPagesWidgets.removeAll();
                                pageSkinWidgets.removeAll();
                                selectedCurrentPageId(undefined);
                                resetObservableArrays();
                            }
                        });
                        confirmation.show();
                        return;
                    }
                    isEditorVisible(false);
                },
                resetFilterSection = function () {
                    searchFilter(undefined);
                    getStores();
                },
                //Get Base Data
                getBaseData = function () {
                    dataservice.getBaseData({
                        companyId: selectedStoreListView().companyId()
                    }, {
                        success: function (data) {
                            if (data != null) {
                                systemUsers.removeAll();
                                addressCompanyTerritoriesFilter.removeAll();
                                contactCompanyTerritoriesFilter.removeAll();
                                addressTerritoryList.removeAll();
                                roles.removeAll();
                                registrationQuestions.removeAll();
                                allCompanyAddressesList.removeAll();
                                costCentersList.removeAll();
                                pageCategories.removeAll();
                                _.each(data.SystemUsers, function (item) {
                                    var systemUser = new model.SystemUser.Create(item);
                                    systemUsers.push(systemUser);
                                });
                                _.each(data.CompanyTerritories, function (item) {
                                    var territory = new model.CompanyTerritory.Create(item);
                                    addressCompanyTerritoriesFilter.push(territory);
                                    contactCompanyTerritoriesFilter.push(territory);
                                    addressTerritoryList.push(territory);
                                });
                                _.each(data.CompanyContactRoles, function (item) {
                                    var role = new model.Role.Create(item);
                                    roles.push(role);
                                });
                                _.each(data.RegistrationQuestions, function (item) {
                                    var registrationQuestion = new model.RegistrationQuestion.Create(item);
                                    registrationQuestions.push(registrationQuestion);
                                });
                                _.each(data.Addresses, function (item) {
                                    var address = new model.Address.Create(item);
                                    allCompanyAddressesList.push(address);
                                });
                                _.each(data.PageCategories, function (item) {
                                    pageCategories.push(model.PageCategory.Create(item));
                                });
                                _.each(data.PaymentMethods, function (item) {
                                    paymentMethods.push(model.PaymentMethod.Create(item));
                                });
                                _.each(data.CostCenterDropDownList, function (item) {
                                    costCentersList.push(model.CostCenter.Create(item));
                                });
                                //Email Event List
                                emailEvents.removeAll();
                                if (data.EmailEvents !== null) {
                                    ko.utils.arrayPushAll(emailEvents(), data.EmailEvents);
                                    emailEvents.valueHasMutated();
                                }

                                _.each(data.Widgets, function (item) {
                                    widgets.push(model.Widget.Create(item));
                                });
                            }
                            selectedStore().reset();
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores . Error: " + response);
                        }
                    });
                },
                //Get Base Data For New Company
                    getBaseDataFornewCompany = function () {
                        dataservice.getBaseData({

                        }, {
                            success: function (data) {
                                if (data != null) {
                                    systemUsers.removeAll();
                                    addressCompanyTerritoriesFilter.removeAll();
                                    contactCompanyTerritoriesFilter.removeAll();
                                    addressTerritoryList.removeAll();
                                    addressTerritoryList.removeAll();
                                    roles.removeAll();
                                    registrationQuestions.removeAll();
                                    allCompanyAddressesList.removeAll();
                                    pageCategories.removeAll();
                                    _.each(data.SystemUsers, function (item) {
                                        var systemUser = new model.SystemUser.Create(item);
                                        systemUsers.push(systemUser);
                                    });

                                    _.each(data.CompanyContactRoles, function (item) {
                                        var role = new model.Role.Create(item);
                                        roles.push(role);
                                    });
                                    _.each(data.RegistrationQuestions, function (item) {
                                        var registrationQuestion = new model.RegistrationQuestion.Create(item);
                                        registrationQuestions.push(registrationQuestion);
                                    });

                                    _.each(data.PageCategories, function (item) {
                                        pageCategories.push(model.PageCategory.Create(item));
                                    });
                                    _.each(data.PaymentMethods, function (item) {
                                        paymentMethods.push(model.PaymentMethod.Create(item));
                                    });
                                    //Email Event List
                                    emailEvents.removeAll();
                                    if (data.EmailEvents !== null) {
                                        ko.utils.arrayPushAll(emailEvents(), data.EmailEvents);
                                        emailEvents.valueHasMutated();
                                    }

                                    _.each(data.Widgets, function (item) {
                                        widgets.push(model.Widget.Create(item));
                                    });
                                    //Sefault Sprite Image
                                    selectedStore().userDefinedSpriteImageSource(data.DefaultSpriteImageSource);
                                    selectedStore().userDefinedSpriteImageFileName("default.jpg");
                                    selectedStore().defaultSpriteImageSource(data.DefaultSpriteImageSource);
                                    selectedStore().customCSS(data.DefaultCompanyCss);
                                }
                                isLoadingStores(false);
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Failed to Load Stores . Error: " + response);
                            }
                        });
                    },

                MultipleImageFilesLoadedCallback = function (file, data) {
                    selectedCompanyBanner().fileBinary(data);
                    selectedCompanyBanner().imageSource(data);
                    selectedCompanyBanner().filename(file.name);
                    selectedCompanyBanner().fileType(data.imageType);
                },
                SecondaryImageFileLoadedCallback = function (file, data) {
                    selectedSecondaryPage().imageSrc(data);
                    selectedSecondaryPage().fileName(file.name);
                },
                resetObservableArrays = function () {
                    selectedStore().addresses.removeAll();
                    //allCompanyAddressesList().removeAll();
                    deletedAddresses.removeAll();
                    edittedAddresses.removeAll();
                    newAddresses.removeAll();
                    deletedCompanyTerritories.removeAll();
                    edittedCompanyTerritories.removeAll();
                    newCompanyTerritories.removeAll();
                    deletedCompanyContacts.removeAll();
                    edittedCompanyContacts.removeAll();
                    newCompanyContacts.removeAll();
                    parentCategories.removeAll();

                    newAddedSecondaryPage.removeAll();
                    editedSecondaryPage.removeAll();
                    deletedSecondaryPage.removeAll();
                    allPagesWidgets.removeAll();
                    pageSkinWidgets.removeAll();
                    deletedProductCategories.removeAll();
                    edittedProductCategories.removeAll();
                    newProductCategories.removeAll();
                    selectedCurrentPageId(undefined);
                    selectedCurrentPageCopy(undefined);
                    isProductTabVisited(false);
                    ist.storeProduct.viewModel.resetObservables();
                    selectedItemsForOfferList.removeAll();
                    selectedItemForRemove(undefined);
                    selectedItemForAdd(undefined);
                    productPriorityRadioOption("1");
                },
                //#endregion

                //#region _________P R O D U C T S ______________________
                isProductTabVisited = ko.observable(false),
                getProducts = function () {
                    if (!isProductTabVisited()) {
                        isProductTabVisited(true);
                        ist.storeProduct.viewModel.initialize(selectedStore().companyId());
                    }
                },
                //#endregion 

                // #region_________LAYOUT WIDGET _________________


                selectWidget = function (widget) {
                    this.selectedWidget(widget);
                },
                // ReSharper disable once UnusedLocals
                getPageLayoutWidget = ko.computed(function () {
                    //On page change save widgets against page id. i-e selected Current Page Copy,before change page from dropdown
                    if (selectedCurrentPageCopy() !== undefined && selectedCurrentPageCopy() !== selectedCurrentPageId()) {
                        //Remove Widget 
                        _.each(allPagesWidgets(), function (item) {
                            if (selectedCurrentPageCopy() === item.pageId()) {
                                allPagesWidgets.remove(item);
                            }
                        });
                        var flag = true;
                        _.each(allPagesWidgets(), function (item) {
                            if (selectedCurrentPageCopy() === item.pageId()) {
                                ko.utils.arrayPushAll(item.widgets, pageSkinWidgets());
                                item.widgets.valueHasMutated();
                                flag = false;
                            }
                        });
                        if (flag) {
                            //Add widget list of selected page into All Pages Widgets List
                            var pageWidgetList = model.CmsPageWithWidgetList();
                            pageWidgetList.pageId(selectedCurrentPageCopy());
                            ko.utils.arrayPushAll(pageWidgetList.widgets, pageSkinWidgets());
                            pageWidgetList.widgets.valueHasMutated();
                            allPagesWidgets.push(pageWidgetList);
                        }
                    }

                    //Get current page widgets
                    if (selectedCurrentPageId() !== undefined && selectedCurrentPageCopy() !== selectedCurrentPageId()) {
                        pageSkinWidgets.removeAll();

                        var getSeverOrClientListFlag = true;
                        _.each(allPagesWidgets(), function (item) {
                            if (selectedCurrentPageId() === item.pageId()) {
                                selectedCurrentPageCopy(selectedCurrentPageId());
                                ko.utils.arrayPushAll(pageSkinWidgets, item.widgets());
                                pageSkinWidgets.valueHasMutated();
                                getSeverOrClientListFlag = false;
                            }
                        });
                        if (getSeverOrClientListFlag) {
                            selectedCurrentPageCopy(selectedCurrentPageId());
                            getPageLayoutWidgets();
                        }

                    }
                    if (selectedCurrentPageId() === undefined) {
                        pageSkinWidgets.removeAll();
                        selectedCurrentPageCopy(selectedCurrentPageId());
                    }

                }, this),
                //Get Page Layout Widgets
                getPageLayoutWidgets = function () {
                    dataservice.getCmsPageLayoutWidget({
                        pageId: selectedCurrentPageId(),
                        companyId: selectedStore().companyId()
                    }, {
                        success: function (data) {
                            pageSkinWidgets.removeAll();
                            if (data != null) {
                                _.each(data, function (item) {
                                    var widget = new model.CmsSkingPageWidget.Create(item);
                                    if (widget.widgetId() === 14) {
                                        _.each(item.CmsSkinPageWidgetParams, function (params) {
                                            widget.cmsSkinPageWidgetParam(model.CmsSkinPageWidgetParam.Create(params));
                                            widget.htmlData(widget.cmsSkinPageWidgetParam().paramValue());
                                        });
                                    }
                                    pageSkinWidgets.push(widget);
                                });
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Page Widgets . Error: " + response);
                        }
                    });
                },
                // Widget being dropped
                // ReSharper disable UnusedParameter
                dropped = function (source, target, event) {
                    // ReSharper restore UnusedParameter
                    if (selectedCurrentPageId() !== undefined && source !== undefined && source !== null && source.widget !== undefined && source.widget !== null && source.widget.widgetControlName !== undefined && source.widget.widgetControlName() !== "") {
                        if (source.widget.widgetId() === 14) {
                            var newWidget = new model.CmsSkingPageWidget();
                            newWidget.pageWidgetId(newAddedWidgetIdCounter() - 1);
                            newWidget.widgetName(source.widget.widgetName());
                            newWidget.pageId(selectedCurrentPageId());
                            newWidget.widgetId(source.widget.widgetId());
                            pageSkinWidgets.push(newWidget);
                            newAddedWidgetIdCounter(newAddedWidgetIdCounter() - 1);
                        } else {
                            getWidgetDetail(source.widget);
                        }
                    }
                    if (selectedCurrentPageId() === undefined) {
                        toastr.error("Before add widget please select page !");
                    }
                },
                //Get Widget detail on drag drop
                getWidgetDetail = function (widget) {
                    dataservice.getWidgetDetail({
                        widgetControlName: widget.widgetControlName(),
                    }, {
                        success: function (data) {
                            if (data !== "" && data !== null) {
                                var newWidget = new model.CmsSkingPageWidget();
                                newWidget.htmlData(data);
                                newWidget.widgetName(widget.widgetName());
                                newWidget.pageId(selectedCurrentPageId());
                                newWidget.widgetId(widget.widgetId());
                                pageSkinWidgets.push(newWidget);
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Page Widgets . Error: " + response);
                        }
                    });
                },
                // Returns the item being dragged
                dragged = function (source) {
                    return {
                        row: source.$parent,
                        widget: source.$data
                    };
                },
                //Add Widget To Page Layout
                addWidgetToPageLayout = function (widget) {
                    if (selectedCurrentPageId() !== undefined && widget !== undefined && widget !== null && widget.widgetControlName !== undefined && widget.widgetControlName() !== "") {
                        if (widget.widgetId() === 14) {
                            var newWidget = new model.CmsSkingPageWidget();
                            //newWidget.htmlData(data);
                            newWidget.pageWidgetId(newAddedWidgetIdCounter() - 1);
                            newWidget.widgetName(widget.widgetName());
                            newWidget.pageId(selectedCurrentPageId());
                            newWidget.widgetId(widget.widgetId());
                            pageSkinWidgets.splice(0, 0, newWidget);
                            newAddedWidgetIdCounter(newAddedWidgetIdCounter() - 1);
                        } else {
                            getWidgetDetailOnAdd(widget);
                        }
                    }
                    if (selectedCurrentPageId() === undefined) {
                        toastr.error("Before add widget please select page !");
                    }
                },
                //Click on plus sign , add widget to page
                getWidgetDetailOnAdd = function (widget) {
                    dataservice.getWidgetDetail({
                        widgetControlName: widget.widgetControlName(),
                    }, {
                        success: function (data) {
                            if (data !== "" && data !== null) {
                                var newWidget = new model.CmsSkingPageWidget();
                                newWidget.htmlData(data);
                                newWidget.widgetName(widget.widgetName());
                                newWidget.pageId(selectedCurrentPageId());
                                newWidget.widgetId(widget.widgetId());
                                pageSkinWidgets.splice(0, 0, newWidget);
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Page Widgets . Error: " + response);
                        }
                    });
                },
                //Delete Page Layout Widget
                deletePageLayoutWidget = function (widget) {
                    if (widget !== undefined && widget !== null) {
                        pageSkinWidgets.remove(widget);
                    }
                },
                //show Ck Editor Dialog
                showCkEditorDialog = function (widget) {
                    widget.cmsSkinPageWidgetParam().pageWidgetId(widget.pageWidgetId());
                    //widget.cmsSkinPageWidgetParam().editorId("editor" + newAddedWidgetIdCounter());
                    selectedWidget(widget.cmsSkinPageWidgetParam());
                    view.showCkEditorDialogDialog();
                },
                //Save Widget Params That are set in CkEditor
                onSaveWidgetParamFromCkEditor = function (widgetParams) {
                    var param = CKEDITOR.instances.content.getData();
                    _.each(pageSkinWidgets(), function (item) {
                        if (widgetParams.pageWidgetId() === item.pageWidgetId()) {
                            item.htmlData(param);
                            item.cmsSkinPageWidgetParam().paramValue(param);
                        }
                    });
                    selectedWidget(undefined);
                    view.hideCkEditorDialogDialog();
                },
                //#endregion

                //#region _________WIDGETS IN Themes & Widgets Tab _________________
                //Open Dialog from Featured Product Row
                openItemsForWidgetsDialogFromFeatured = function () {
                    selectedOfferType(1);
                    resetItems();
                    view.showItemsForWidgetsDialog();
                },
                //Open Dialog from Popular Product Row
                 openItemsForWidgetsDialogFromPopular = function () {
                     selectedOfferType(2);
                     resetItems();
                     view.showItemsForWidgetsDialog();
                 },
                //Open Dialog from Special Product Row
                  openItemsForWidgetsDialogFromSpecial = function () {
                      selectedOfferType(3);
                      resetItems();
                      view.showItemsForWidgetsDialog();
                  },
                //Add Item or Move to Right 
                  addItemToWidgetList = function () {
                      if (selectedItemForAdd() !== undefined) {
                          var item = model.CmsOffer();
                          item.itemId(selectedItemForAdd().id());
                          item.offerType(selectedOfferType());
                          item.itemName(selectedItemForAdd().productName());
                          selectedItemsForOfferList.push(item);
                          selectedItemForAdd().isInSelectedList(true);
                          //remove items of other offer type from list, if another offer type items add(At a time only one offer type item selected)
                          _.each(selectedItemsForOfferList(), function (offerItem) {
                              if (selectedOfferType() !== offerItem.offerType()) {
                                  selectedItemsForOfferList.remove(offerItem);
                              }
                          });
                          selectedItemForAdd(undefined);
                      }
                  },
                //Remove Item or Move to Left 
                  removeItemToWidgetList = function () {
                      if (selectedItemForRemove() !== undefined) {
                          _.each(itemsForWidgets(), function (item) {
                              if (selectedItemForRemove().itemId() === item.id()) {
                                  item.isInSelectedList(false);
                              }
                          });
                          selectedItemsForOfferList.remove(selectedItemForRemove());
                          selectedItemForRemove(undefined);
                      }
                  },
                //Select Item
                selectAddItem = function (item) {
                    selectedItemForAdd(item);
                },
                //reset Items
                resetItems = function () {
                    _.each(itemsForWidgets(), function (item) {
                        item.isInSelectedList(false);
                    });

                    _.each(selectedItemsForOfferList(), function (offerItem) {
                        if (selectedOfferType() === offerItem.offerType())
                            _.each(itemsForWidgets(), function (item) {
                                if (offerItem.itemId() == item.id())
                                    item.isInSelectedList(true);
                            });
                    });
                },
                //select remove Item
                selectRemoveItem = function (item) {
                    selectedItemForRemove(item);
                },
                //#endregion

                //#region ________ MEDIA LIBRARY___________

                //Active Media File
                selectedMediaFile = ko.observable(),
                //Media Library Open From
                mediaLibraryOpenFrom = ko.observable(),
                mediaLibraryIdCount = ko.observable(0),
                //New Uploaded Media File
                newUploadedMediaFile = ko.observable(model.MediaLibrary()),
                //Media Library File Loaded Call back
                mediaLibraryFileLoadedCallback = function (file, data) {
                    //Flag check, whether file is already exist in media libray
                    var flag = true;
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (item.fileSource() === data && item.fileName() === file.name) {
                            flag = false;
                        }
                    });

                    if (flag) {
                        var mediaId = mediaLibraryIdCount() - 1;
                        var mediaLibrary = model.MediaLibrary();
                        mediaLibrary.id(mediaId);
                        mediaLibrary.fakeId(mediaId);
                        mediaLibrary.fileSource(data);
                        mediaLibrary.fileName(file.name);
                        mediaLibrary.fileType(file.type);
                        mediaLibrary.companyId(selectedStore().companyId());
                        newUploadedMediaFile(mediaLibrary);
                        selectedStore().mediaLibraries.push(newUploadedMediaFile());
                        //Last set Id
                        mediaLibraryIdCount(mediaId);
                    }
                },

                //Open Media Library From Store Background Image
                showMediaLibraryDialogFromStoreBackground = function () {
                    resetMediaGallery();
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (selectedStore().storeBackgroudImageImageSource() !== undefined && item.fileSource() === selectedStore().storeBackgroudImageImageSource()) {
                            item.isSelected(true);
                            selectedStore().storeBackgroudImageImageSource(item.fileSource());
                        }
                    });
                    mediaLibraryOpenFrom("StoreBackground");
                    showMediaLibrary();
                },
                //Open Media Library From Company Banner
                openMediaLibraryDialogFromCompanyBanner = function () {
                    resetMediaGallery();
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (selectedCompanyBanner().filePath() !== undefined && (item.id() === selectedCompanyBanner().filePath() || item.filePath() === selectedCompanyBanner().filePath())) {
                            item.isSelected(true);
                            selectedCompanyBanner().fileBinary(item.fileSource());
                        }
                    });
                    mediaLibraryOpenFrom("CompanyBanner");
                    showMediaLibrary();
                },
                //Open Media Library From Secondary Page
                openMediaLibraryDialogFromSecondaryPage = function () {
                    resetMediaGallery();
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (selectedSecondaryPage().pageBanner() !== undefined && (item.id() === selectedSecondaryPage().pageBanner() || item.filePath() === selectedSecondaryPage().pageBanner())) {
                            item.isSelected(true);
                            selectedSecondaryPage().imageSrc(item.fileSource());
                        }
                    });
                    mediaLibraryOpenFrom("SecondaryPage");
                    showMediaLibrary();
                },
                //Open Media Library From Product Category Thumbnail
                openMediaLibraryDialogFromProductCategoryThumbnail = function () {
                    resetMediaGallery();
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (selectedProductCategoryForEditting().thumbnailPath() !== undefined && (item.id() === selectedProductCategoryForEditting().thumbnailPath() || item.filePath() === selectedProductCategoryForEditting().thumbnailPath())) {
                            item.isSelected(true);
                            selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(item.fileSource());
                        }
                    });
                    mediaLibraryOpenFrom("ProductCategoryThumbnail");
                    showMediaLibrary();
                },
                //Open Media Library From Product Category Banner
                openMediaLibraryDialogFromProductCategoryBanner = function () {
                    resetMediaGallery();
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        if (selectedProductCategoryForEditting().imagePath() !== undefined && (item.id() === selectedProductCategoryForEditting().imagePath() || item.filePath() === selectedProductCategoryForEditting().imagePath())) {
                            item.isSelected(true);
                            selectedProductCategoryForEditting().productCategoryImageFileBinary(item.fileSource());
                        }
                    });
                    mediaLibraryOpenFrom("ProductCategoryBanner");
                    showMediaLibrary();
                },
                //Hie Media Library
                hideMediaLibraryDialog = function () {
                    view.hideMediaGalleryDialog();
                },

                //Show Media Library 
                showMediaLibrary = function () {
                    view.showMediaGalleryDialog();
                },
                //select Media File
                selectMediaFile = function (media) {
                    resetMediaGallery();
                    media.isSelected(true);
                    selectedMediaFile(media);
                },
                //Reset Media Gallery
                resetMediaGallery = function () {
                    _.each(selectedStore().mediaLibraries(), function (item) {
                        item.isSelected(false);
                    });
                },
                //Save Media File And Close Library Dialog
                onSaveMedia = function () {
                    //Open From Store backgound
                    if (mediaLibraryOpenFrom() === "StoreBackground") {
                        selectedStore().storeBackgroudImageImageSource(selectedMediaFile().fileSource());
                    }
                        //If Open From Company Banner
                    else if (mediaLibraryOpenFrom() === "CompanyBanner") {
                        if (selectedMediaFile().id() > 0) {
                            selectedCompanyBanner().filePath(selectedMediaFile().filePath());
                        } else {
                            selectedCompanyBanner().filePath(selectedMediaFile().id());
                        }
                        selectedCompanyBanner().fileBinary(selectedMediaFile().fileSource());
                        selectedCompanyBanner().imageSource(selectedMediaFile().fileSource());
                    }
                        //If Open From Secondary Page
                    else if (mediaLibraryOpenFrom() === "SecondaryPage") {
                        if (selectedMediaFile().id() > 0) {
                            selectedSecondaryPage().pageBanner(selectedMediaFile().filePath());
                        } else {
                            selectedSecondaryPage().pageBanner(selectedMediaFile().id());
                        }
                        selectedSecondaryPage().imageSrc(selectedMediaFile().fileSource());
                    }
                        //If Open From Product Category Banner
                    else if (mediaLibraryOpenFrom() === "ProductCategoryBanner") {
                        if (selectedMediaFile().id() > 0) {
                            selectedProductCategoryForEditting().imagePath(selectedMediaFile().filePath());
                        } else {
                            selectedProductCategoryForEditting().imagePath(selectedMediaFile().id());
                        }
                        selectedProductCategoryForEditting().productCategoryImageFileBinary(selectedMediaFile().fileSource());
                    }
                        //If Open From Product Category Thumbnail
                    else if (mediaLibraryOpenFrom() === "ProductCategoryThumbnail") {
                        if (selectedMediaFile().id() > 0) {
                            selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().filePath());
                        } else {
                            selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().id());
                        }
                        selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(selectedMediaFile().fileSource());
                    }

                    //Hide gallery
                    hideMediaLibraryDialog();
                },
                //#endregion

                //#region ________D E L I V E R Y    A D D    O N________________
                selectedPickupAddress = ko.observable(),
                pickupAddress = ko.observable(),
                updatePickupAddressFields = ko.computed(function () {
                    if (selectedStore() != undefined) {
                        if (selectedStore().pickupAddressId() != undefined) {
                            _.each(allCompanyAddressesList(), function (address) {
                                if (address.addressId() == selectedStore().pickupAddressId()) {
                                    selectedPickupAddress(address);
                                }
                            });
                        } else {
                            selectedPickupAddress(new model.Address);
                        }
                    }
                }),
                pickUpLocationValue = ko.observable(),

                updatePickupAddress = ko.computed(function () {
                    if (selectedPickupAddress().stateName() != undefined && selectedPickupAddress().countryName() != undefined && selectedPickupAddress().postCode() != undefined) {
                        pickupAddress(selectedPickupAddress());
                        pickUpLocationValue(pickupAddress().addressName() + '/' + pickupAddress().postCode());
                        //selectedStore().pickupAddressId(selectedStore().pickupAddressId());
                    } else {
                        pickupAddress(new model.Address);
                        //selectedStore().pickupAddressId(selectedStore().pickupAddressId());
                    }
                }),
                //updateSelectedStoreCostCenters
                updateSelectedStoreCostCenters = function () {
                    _.each(selectedStore().companyCostCenters(), function (costCenter) {
                        var selectedCostCenter;
                        selectedCostCenter = _.find(costCentersList(), function (costCenterItem) {
                            return costCenterItem.costCentreId() === costCenter.costCentreId();
                        });
                        selectedCostCenter.isSelected(true);
                    });
                },
                onClosePickupDialog = function () {
                    if (selectedPickupAddress().state() != undefined && selectedPickupAddress().country() != undefined && selectedPickupAddress().postCode() != undefined) {
                        pickupAddress(selectedPickupAddress());
                        selectedStore().pickupAddressId(selectedStore().pickupAddressId());
                    } else {
                        pickupAddress(new model.Address);
                        selectedStore().pickupAddressId(undefined);
                    }
                },
                onSavePickupDialog = function () {

                },
                //Update selected Store selected cost centers
                updateCostCentersOnStoreSaving = function () {
                    selectedStore().companyCostCenters.removeAll();
                    _.each(costCentersList(), function (costCenter) {
                        if (costCenter.isSelected()) {
                            selectedStore().companyCostCenters.push(costCenter.convertToServerData());
                        }
                    });
                },
                //#endregion

                //Initialize
                // ReSharper disable once AssignToImplicitGlobalInFunctionScope
                initialize = function (specifiedView) {
                    view = specifiedView;
                    ko.applyBindings(view.viewModel, view.bindingRoot);
                    //ko.applyBindings(view.viewModel, document.getElementById('singleArea'));
                    pager(new pagination.Pagination({ PageSize: 5 }, stores, getStores));
                    getStores();
                    view.initializeForm();
                };
                //#region _________R E T U R N_____________________

                return {
                    //storeProduct: storeProduct,
                    MultipleImageFilesLoadedCallback: MultipleImageFilesLoadedCallback,
                    SecondaryImageFileLoadedCallback: SecondaryImageFileLoadedCallback,
                    filteredCompanySetId: filteredCompanySetId,
                    stores: stores,
                    storeImage: storeImage,
                    systemUsers: systemUsers,
                    isLoadingStores: isLoadingStores,
                    isEditorVisible: isEditorVisible,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    pager: pager,
                    searchFilter: searchFilter,
                    editorViewModel: editorViewModel,
                    selectedStore: selectedStore,
                    templateToUse: templateToUse,
                    makeEditable: makeEditable,
                    createNewStore: createNewStore,
                    storeImageFilesLoadedCallback: storeImageFilesLoadedCallback,
                    onEditItem: onEditItem,
                    isStoreEditorVisible: isStoreEditorVisible,
                    deleteStore: deleteStore,
                    getStores: getStores,
                    doBeforeSave: doBeforeSave,
                    saveStore: saveStore,
                    //saveNewStore: saveNewStore,
                    //saveEdittedStore: saveEdittedStore,
                    openEditDialog: openEditDialog,
                    getStoreForEditting: getStoreForEditting,
                    closeEditDialog: closeEditDialog,
                    resetFilterSection: resetFilterSection,
                    //#region Products
                    getProducts: getProducts,
                    isProductTabVisited: isProductTabVisited,
                    //#endregion
                    //#region Rave Reviews
                    templateToUseRaveReviews: templateToUseRaveReviews,
                    selectedRaveReview: selectedRaveReview,
                    onCreateNewRaveReview: onCreateNewRaveReview,
                    onDeleteRaveReview: onDeleteRaveReview,
                    onEditRaveReview: onEditRaveReview,
                    onCloseRaveReview: onCloseRaveReview,
                    doBeforeSaveRaveReview: doBeforeSaveRaveReview,
                    onSaveRaveReview: onSaveRaveReview,
                    //#endregion Rave Reviews
                    //#region Company CMYK Color
                    templateToUseCompanyCMYKColors: templateToUseCompanyCMYKColors,
                    selectedCompanyCMYKColor: selectedCompanyCMYKColor,
                    onCreateNewCompanyCMYKColor: onCreateNewCompanyCMYKColor,
                    onDeleteCompanyCMYKColors: onDeleteCompanyCMYKColors,
                    onEditCompanyCMYKColor: onEditCompanyCMYKColor,
                    onCloseCompanyCMYKColor: onCloseCompanyCMYKColor,
                    doBeforeSaveCompanyCMYKColor: doBeforeSaveCompanyCMYKColor,
                    onSaveCompanyCMYKColor: onSaveCompanyCMYKColor,
                    //#endregion Company CMYK Color
                    //#region Company territory
                    selectedCompanyTerritory: selectedCompanyTerritory,
                    templateToUseCompanyTerritories: templateToUseCompanyTerritories,
                    searchCompanyTerritoryFilter: searchCompanyTerritoryFilter,
                    onCreateNewCompanyTerritory: onCreateNewCompanyTerritory,
                    onDeleteCompanyTerritory: onDeleteCompanyTerritory,
                    onEditCompanyTerritory: onEditCompanyTerritory,
                    onCloseCompanyTerritory: onCloseCompanyTerritory,
                    doBeforeSaveCompanyTerritory: doBeforeSaveCompanyTerritory,
                    onSaveCompanyTerritory: onSaveCompanyTerritory,
                    companyTerritoryPager: companyTerritoryPager,
                    searchCompanyTerritory: searchCompanyTerritory,
                    deletedCompanyTerritories: deletedCompanyTerritories,
                    edittedCompanyTerritories: edittedCompanyTerritories,
                    newCompanyTerritories: newCompanyTerritories,
                    isSavingNewCompanyTerritory: isSavingNewCompanyTerritory,
                    //#endregion Company territory
                    //#region Addresses
                    selectedAddress: selectedAddress,
                    deletedAddresses: deletedAddresses,
                    edittedAddresses: edittedAddresses,
                    newAddresses: newAddresses,
                    addressPager: addressPager,
                    searchAddressFilter: searchAddressFilter,
                    searchAddress: searchAddress,
                    isSavingNewAddress: isSavingNewAddress,
                    templateToUseAddresses: templateToUseAddresses,
                    onCreateNewAddress: onCreateNewAddress,
                    onDeleteAddress: onDeleteAddress,
                    onEditAddress: onEditAddress,
                    onCloseAddress: onCloseAddress,
                    doBeforeSaveAddress: doBeforeSaveAddress,
                    onSaveAddress: onSaveAddress,
                    addressCompanyTerritoriesFilter: addressCompanyTerritoriesFilter,
                    addressTerritoryFilter: addressTerritoryFilter,
                    addressTerritoryFilterSelected: addressTerritoryFilterSelected,
                    shippingAddresses: shippingAddresses,
                    bussinessAddresses: bussinessAddresses,
                    populateAddressesList: populateAddressesList,
                    selectedBussinessAddress: selectedBussinessAddress,
                    selectedShippingAddress: selectedShippingAddress,
                    selectedBussinessAddressId: selectedBussinessAddressId,
                    selectedShippingAddressId: selectedShippingAddressId,
                    selectBussinessAddress: selectBussinessAddress,
                    selectShippingAddress: selectShippingAddress,
                    //#endregion Addresses
                    // #region Company Banner
                    selectedCompanyBanner: selectedCompanyBanner,
                    companyBanners: companyBanners,
                    selectedCompanyBannerSet: selectedCompanyBannerSet,
                    filteredCompanyBanners: filteredCompanyBanners,
                    companyBannerSetList: companyBannerSetList,
                    onCreateBanner: onCreateBanner,
                    onAddSetBanner: onAddSetBanner,
                    onSaveBannerSet: onSaveBannerSet,
                    onSaveCompanyBanner: onSaveCompanyBanner,
                    onEditCompanyBanner: onEditCompanyBanner,
                    onDeleteCompanyBanner: onDeleteCompanyBanner,
                    // #endregion 
                    //#region Company Contacts
                    selectedCompanyContact: selectedCompanyContact,
                    companyContactFilter: companyContactFilter,
                    deletedCompanyContacts: deletedCompanyContacts,
                    edittedCompanyContacts: edittedCompanyContacts,
                    newCompanyContacts: newCompanyContacts,
                    companyContactPager: companyContactPager,
                    searchCompanyContactFilter: searchCompanyContactFilter,
                    searchCompanyContact: searchCompanyContact,
                    companyContactFilterSelected: companyContactFilterSelected,
                    isSavingNewCompanyContact: isSavingNewCompanyContact,
                    templateToUseCompanyContacts: templateToUseCompanyContacts,
                    onCreateNewCompanyContact: onCreateNewCompanyContact,
                    onDeleteCompanyContact: onDeleteCompanyContact,
                    onEditCompanyContact: onEditCompanyContact,
                    onCloseCompanyContact: onCloseCompanyContact,
                    doBeforeSaveCompanyContact: doBeforeSaveCompanyContact,
                    onSaveCompanyContact: onSaveCompanyContact,
                    //#endregion Company Contacts
                    //#region Company Territories
                    contactCompanyTerritoriesFilter: contactCompanyTerritoriesFilter,
                    contactCompanyTerritoryFilter: contactCompanyTerritoryFilter,
                    addressTerritoryList: addressTerritoryList,
                    //#endregion Company Territories
                    //#region Email
                    emailEvents: emailEvents,
                    emails: emails,
                    onCreateIntervalMarketingEmail: onCreateIntervalMarketingEmail,
                    onCreateOneTimeMarketingEmail: onCreateOneTimeMarketingEmail,
                    selectedEmail: selectedEmail,
                    onEditEmail: onEditEmail,
                    onSaveEmail: onSaveEmail,
                    onDeleteEmail: onDeleteEmail,
                    //#endregion Email
                    //#region Payment Gateway
                    templateToUsePaymentGateways: templateToUsePaymentGateways,
                    onCreateNewPaymentGateway: onCreateNewPaymentGateway,
                    onDeletePaymentGateway: onDeletePaymentGateway,
                    onEditPaymentGateway: onEditPaymentGateway,
                    onClosePaymentGateway: onClosePaymentGateway,
                    doBeforeSavePaymentGateway: doBeforeSavePaymentGateway,
                    onSavePaymentGateway: onSavePaymentGateway,
                    selectedPaymentGateway: selectedPaymentGateway,
                    //#endregion Payment Gateway
                    //#region Product Category
                    selectedProductCategory: selectedProductCategory,
                    selectProductCategory: selectProductCategory,
                    deletedProductCategories: deletedProductCategories,
                    edittedProductCategories: edittedProductCategories,
                    newProductCategories: newProductCategories,
                    isSavingNewProductCategory: isSavingNewProductCategory,
                    onCreateNewProductCategory: onCreateNewProductCategory,
                    onDeleteProductCategory: onDeleteProductCategory,
                    onEditProductCategory: onEditProductCategory,
                    onCloseProductCategory: onCloseProductCategory,
                    doBeforeSaveProductCategory: doBeforeSaveProductCategory,
                    onSaveProductCategory: onSaveProductCategory,
                    parentCategories: parentCategories,
                    populateParentCategories: populateParentCategories,
                    selectedProductCategoryForEditting: selectedProductCategoryForEditting,
                    onEditChildProductCategory: onEditChildProductCategory,
                    ProductCategoryThumbnailFilesLoadedCallback: ProductCategoryThumbnailFilesLoadedCallback,
                    ProductCategoryImageFilesLoadedCallback: ProductCategoryImageFilesLoadedCallback,
                    productCategoryCounter: productCategoryCounter,
                    addProductCategoryCounter: addProductCategoryCounter,
                    resetProductCategoryCounter: resetProductCategoryCounter,
                    getCategoryChildListItems: getCategoryChildListItems,
                    openProductCategoryDetail: openProductCategoryDetail,
                    //#endregion Product Category
                    //editorViewModelListView: editorViewModelListView,
                    selectedStoreListView: selectedStoreListView,
                    showCkEditorDialog: showCkEditorDialog,
                    selectedWidget: selectedWidget,
                    onSaveWidgetParamFromCkEditor: onSaveWidgetParamFromCkEditor,
                    contactCompanyPager: contactCompanyPager,
                    onAddSecondaryPage: onAddSecondaryPage,
                    onAddSecondryPageCategory: onAddSecondryPageCategory,
                    resetObservableArrays: resetObservableArrays,
                    registrationQuestions: registrationQuestions,
                    roles: roles,
                    secondaryPagePager: secondaryPagePager,
                    selectedSecondaryPage: selectedSecondaryPage,
                    onEditSecondaryPage: onEditSecondaryPage,
                    onDeleteSecondaryPage: onDeleteSecondaryPage,
                    pageCategories: pageCategories,
                    addDefaultPageKeyWords: addDefaultPageKeyWords,
                    onSaveSecondaryPage: onSaveSecondaryPage,
                    onSavePageCategory: onSavePageCategory,
                    selectedPageCategory: selectedPageCategory,
                    allCompanyAddressesList: allCompanyAddressesList,
                    UserProfileImageFileLoadedCallback: UserProfileImageFileLoadedCallback,
                    onCloseCompanyBanner: onCloseCompanyBanner,
                    widgets: widgets,
                    paymentMethods: paymentMethods,
                    checkPaymentMethodSelection: checkPaymentMethodSelection,
                    isAccessCodeSectionVisible: isAccessCodeSectionVisible,
                    paymentMethodName: paymentMethodName,
                    selectedWidgetsList: selectedWidgetsList,
                    selectWidget: selectWidget,
                    selectedCurrentPageId: selectedCurrentPageId,
                    cmsPagesForStoreLayout: cmsPagesForStoreLayout,
                    pageSkinWidgets: pageSkinWidgets,
                    dropped: dropped,
                    dragged: dragged,
                    addWidgetToPageLayout: addWidgetToPageLayout,
                    deletePageLayoutWidget: deletePageLayoutWidget,
                    allPagesWidgets: allPagesWidgets,
                    storeProductsViewModel: storeProductsViewModel,
                    onCreateNewStore: onCreateNewStore,
                    initialize: initialize,
                    storeBackgroudImageUploadCallback: storeBackgroudImageUploadCallback,
                    openItemsForWidgetsDialogFromFeatured: openItemsForWidgetsDialogFromFeatured,
                    openItemsForWidgetsDialogFromPopular: openItemsForWidgetsDialogFromPopular,
                    openItemsForWidgetsDialogFromSpecial: openItemsForWidgetsDialogFromSpecial,
                    itemsForWidgets: itemsForWidgets,
                    addItemToWidgetList: addItemToWidgetList,
                    removeItemToWidgetList: removeItemToWidgetList,
                    selectedOfferType: selectedOfferType,
                    selectedItemForAdd: selectedItemForAdd,
                    selectedItemForRemove: selectedItemForRemove,
                    selectAddItem: selectAddItem,
                    selectedItemsForOfferList: selectedItemsForOfferList,
                    selectRemoveItem: selectRemoveItem,
                    productPriorityRadioOption: productPriorityRadioOption,
                    restoreSpriteImage: restoreSpriteImage,
                    spriteImageLoadedCallback: spriteImageLoadedCallback,
                    templateToUseCompanyDomain: templateToUseCompanyDomain,
                    selectedCompanyDomainItem: selectedCompanyDomainItem,
                    selectCompanyDomain: selectCompanyDomain,
                    onDeleteCompanyDomainItem: onDeleteCompanyDomainItem,
                    createCompanyDomainItem: createCompanyDomainItem,
                    newUploadedMediaFile: newUploadedMediaFile,
                    mediaLibraryFileLoadedCallback: mediaLibraryFileLoadedCallback,
                    maintainCompanyDomain: maintainCompanyDomain,
                    populatedParentCategoriesList: populatedParentCategoriesList,
                    showMediaLibraryDialogFromStoreBackground: showMediaLibraryDialogFromStoreBackground,
                    openMediaLibraryDialogFromCompanyBanner: openMediaLibraryDialogFromCompanyBanner,
                    openMediaLibraryDialogFromSecondaryPage: openMediaLibraryDialogFromSecondaryPage,
                    openMediaLibraryDialogFromProductCategoryThumbnail: openMediaLibraryDialogFromProductCategoryThumbnail,
                    openMediaLibraryDialogFromProductCategoryBanner: openMediaLibraryDialogFromProductCategoryBanner,
                    hideMediaLibraryDialog: hideMediaLibraryDialog,
                    selectMediaFile: selectMediaFile,
                    selectedMediaFile: selectedMediaFile,
                    onSaveMedia: onSaveMedia,
                    colseSecondaryPageCategoryDialog: colseSecondaryPageCategoryDialog,
                    selectedPickupAddress: selectedPickupAddress,
                    costCentersList: costCentersList,
                    pickupAddress: pickupAddress,
                    onClosePickupDialog: onClosePickupDialog,
                    onSavePickupDialog: onSavePickupDialog,
                    campaignSectionFlags: campaignSectionFlags,
                    campaignCompanyTypes: campaignCompanyTypes,
                    campaignGroups: campaignGroups,
                    emailCampaignSections: emailCampaignSections,
                    pickUpLocationValue: pickUpLocationValue,
                    selectedSection: selectedSection,
                    selectSection: selectSection,
                    campaignEmailImagesLoadedCallback: campaignEmailImagesLoadedCallback,
                    draggedSection: draggedSection,
                    draggedImage: draggedImage,
                    draggedEmailVaribale: draggedEmailVaribale,
                    droppedEmailSection: droppedEmailSection,
                };
                //#endregion
            })()
        };
        return ist.stores.viewModel;
    });

/*
    Module with the view model for the Store.
*/
define("stores/stores.viewModel",
    ["jquery", "amplify", "ko", "stores/stores.dataservice", "stores/stores.model", "common/confirmation.viewModel", "common/pagination",
        "common/sharedNavigation.viewModel", "product/product.viewModel", "p71", "common/reportManager.viewModel"],
    function ($, amplify, ko, dataservice, model, confirmation, pagination, sharedNavigationVM, productViewModel, p71, reportManager) {
        var ist = window.ist || {};
        ist.stores = {
            viewModel: (function () {
                var //View
                    view,
                    //#region ________ O B S E R V A B L E S ________________________
                    filteredCompanySetId = ko.observable(),
                    //Public Store Name
                    publicStoreName = ko.observable(),
                    // Private Store Name
                    privateStoreName = ko.observable(),
                    //Organization ID
                    organizationId = ko.observable(),
                    //selected Current Page Id In Layout Page Tab
                    selectedCurrentPageId = ko.observable(),
                    selectedCurrentPageCopy = ko.observable(),
                    ckEditorOpenFrom = ko.observable("Campaign"),
                    storeStatus = ko.observable(),
                    productStatus = ko.observable(''),
                    //Active Widget (use for dynamic controll)
                    selectedWidget = ko.observable(),
                    // Error List
                    errorList = ko.observableArray([]),
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

                    // widget section header title
                    productsFilterHeading = ko.observable(),

                    // widget section sub header title for ALL
                    productsFilterSubHeadingAll = ko.observable(),

                    // widget section sub header title for Selected
                    productsFilterSubHeadingSelected = ko.observable(),

                    // Count of Users
                    userCount = ko.observable(0),
                    // Count of Orders
                    orderCount = ko.observable(0),
                    //Sort On
                    sortOn = ko.observable(1),
                    // currency Symbol
                    currencySymbol = ko.observable(),
                    //Sort In Ascending
                    sortIsAsc = ko.observable(true),
                    //Pager
                    pager = ko.observable(),
                    //Search Filter
                    searchFilter = ko.observable(),
                    //selectedStore
                    selectedStore = ko.observable(new model.Store),
                    selectedCategoryName = ko.observable("Products"),
                    //Selected Company Contact
                    companyContactEditorViewModel = new ist.ViewModel(model.CompanyContact),
                    selectedCompanyContact = companyContactEditorViewModel.itemForEditing,
                    //selectedCompanyContact = ko.observable(),
                    //Make Edittable
                    makeEditable = ko.observable(false),
                    isBaseDataLoded = ko.observable(false),
                    isThemeNameSet = ko.observable(false),
                    //selected tem For Widgets For Move To add
                    selectedItemForAdd = ko.observable(),
                    //selected tem For Remove
                    selectedItemForRemove = ko.observable(),
                    //selected hex value for cmyk
                    selectedHexValue = ko.observable(),

                    //Active offer Type
                    selectedOfferType = ko.observable(),
                    //Product Priority Radio Option
                    productPriorityRadioOption = ko.observable("1"),
                    productError = ko.observable(),
                    //Setting up computed method calling 
                    isUserAndAddressesTabOpened = ko.observable(false),
                    isStoreVariableTabOpened = ko.observable(false),
                    //Check Is Base Data Loaded
                    isBaseDataLoaded = ko.observable(false),
                    //#endregion

                    //#region ________ O B S E R V A B L E S   A R R A Y S___________
                    //#region ________ Category Product Default Sort by___________
                    defaultSortBy = [
                        { 'id': '0', 'name': 'Select' }, { 'id': '1', 'name': 'Price Low to High' }, { 'id': '2', 'name': 'Price Hight to Low' }, { 'id': '3', 'name': 'Most Popular' }, { 'id': '4', 'name': 'Title' },
                        { 'id': '5', 'name': 'Manufacturer' }, { 'id': '6', 'name': 'Availabilty' }, { 'id': '7', 'name': 'Newest' }, { 'id': '8', 'name': 'Oldest' }
                    ],
                    //#endregion
                    //stores List
                    stores = ko.observableArray([]),
                    //system Users
                    systemUsers = ko.observableArray([]),
                    systemVariablesForSmartForms = ko.observableArray([]),
                    //Themes
                    themes = ko.observableArray([]),
                    newCompanyTerritories = ko.observableArray([]),
                    //Tab User And Addressed, Addresses Section Company Territories Filter
                    addressCompanyTerritoriesFilter = ko.observableArray([]),
                    contactCompanyTerritoriesFilter = ko.observableArray([]),
                    //Addresses to be used in store users shipping and billing address
                    allCompanyAddressesList = ko.observableArray([]),
                    //Company Banners
                    companyBanners = ko.observableArray([]),
                    //Cms Pages For Store Layout DropDown
                    cmsPagesForStoreLayout = ko.observableArray([]),
                    //Cms Pages for basedata
                    cmsPagesBaseData = ko.observableArray([]),
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
                    //Countries
                    countries = ko.observableArray([]),
                    //States
                    states = ko.observableArray([]),
                    //Items with isPublished true for widgets in Themes And Widget Tab
                    itemsForWidgets = ko.observableArray([]),
                    //selected tems List For Widgets
                    selectedItemsForOfferList = ko.observableArray([]),
                    //Filtered States
                    filteredStates = ko.observableArray([]),
                    priceFlags = ko.observableArray([]),

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
                            toastr.error("Default Company Domain cannot be deleted", "", ist.toastrOptions);
                            return;
                        }
                        // Ask for confirmation
                        confirmation.afterProceed(function () {
                            selectedStore().companyDomains.remove(companyDomain);
                        });
                        confirmation.show();

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
                                selectedStore().companyDomains()[0].domain(window.location.host + '/store/' + selectedStore().webAccessCode());
                                selectedStore().companyDomains()[0].isMandatoryDomain(true);
                                selectedStore().defaultCompanyDomainCopy(window.location.host + '/store/' + selectedStore().webAccessCode());
                            } else if (selectedStore().companyDomains().length > 0) {
                                if (!checkCompanyDomainDuplicates(selectedStore().webAccessCode())) {
                                    _.each(selectedStore().companyDomains(), function (companyDomain) {
                                        if (companyDomain.isMandatoryDomain()) {
                                            companyDomain.domain(window.location.host + '/store/' + selectedStore().webAccessCode());
                                            selectedStore().defaultCompanyDomainCopy().domain(window.location.host + '/store/' + selectedStore().webAccessCode());
                                        }
                                    });
                                } else if (selectedStore().webAccessCode() !== undefined && selectedStore().defaultCompanyDomainCopy() != undefined &&
                                    selectedStore().webAccessCode() !== selectedStore().defaultCompanyDomainCopy().domain().split('/')[2]) {
                                    selectedStore().webAccessCode(selectedStore().defaultCompanyDomainCopy().domain().split('/')[2]);
                                    toastr.error('Company Domain cannot be duplicated', "", ist.toastrOptions);
                                }
                                if (selectedStore().companyDomains() != undefined && selectedCompanyDomainItem() != undefined &&
                                    selectedCompanyDomainItem() != selectedStore().companyDomains()[selectedStore().companyDomains().length - 1]) {
                                    _.each(selectedStore().companyDomains(), function (companyDomainItem) {
                                        if (selectedCompanyDomainItem().domain() === companyDomainItem.domain() && selectedCompanyDomainItem() != companyDomainItem) {
                                            selectedCompanyDomainItem().domain(undefined);
                                            toastr.error('Company Domain cannot be duplicated', "", ist.toastrOptions);
                                        }
                                    });
                                }
                            }
                        }
                    }),
                    checkCompanyDomainDuplicates = function (domain) {
                        var matchFound = false;
                        _.each(selectedStore().companyDomains(), function (domainItem) {
                            var tempDomainName = window.location.host + '/store/' + domain;
                            if (domainItem.domain() == tempDomainName) {
                                matchFound = true;
                                return matchFound;
                            }
                        });
                        return matchFound;
                    },
                    //#endregion
                    getStoreHeading = function () {
                        var value1 = selectedStore().name() != '' && selectedStore().name() != undefined ? selectedStore().name() : '';
                        var value2 = selectedStore().webAccessCode() != '' && selectedStore().webAccessCode() != undefined ? ' - ' + selectedStore().webAccessCode() : '';
                        return value1 + value2;
                    },
                    getProductHeading = function () {
                        var val1 = productViewModel.selectedProduct().productName() != '' && productViewModel.selectedProduct().productName() != undefined ? productViewModel.selectedProduct().productName() : '';
                        var val2 = productViewModel.selectedProduct().productCode() != '' && productViewModel.selectedProduct().productCode() != undefined ? ' - ' + productViewModel.selectedProduct().productCode() : '';
                        return val1 + val2;
                    },
                    storeHeading = ko.computed(function () {
                        if (!productViewModel.isProductDetailsVisible()) {
                            productStatus('');
                            if (selectedStore() != null && selectedStore().companyId() > 0) {
                                storeStatus("Modify Store Details");
                            } else {
                                storeStatus("Store Details");
                            }
                            return getStoreHeading();
                        } else {
                            var storename = selectedStore().name() != '' && selectedStore().name() != undefined ? selectedStore().name() : '';
                            storeStatus(storename);
                            if (productViewModel.isProductDetailsVisible()) {
                                productStatus("Modify Product Details");
                                return getProductHeading();
                            } else {
                                productStatus("Product Details");
                            }

                            return getStoreHeading();
                        }
                    }),
                    openDomainInTab = function (data, event) {
                        window.open(window.location.protocol + "//" + data.domain());
                        event.stopImmediatePropagation();
                    },
                    //#region _________S T O R E ____________________________________

                    onCreatePublicStore = function () {
                        createStore(organizationId(), publicStoreName());
                    },
                      openReport = function (isFromEditor) {
                          reportManager.show(ist.reportCategoryEnums.Stores, isFromEditor == true ? true : false, 0);
                      },
                    onCreatePrivateStore = function () {
                        createStore(organizationId(), privateStoreName());
                    },
                    createStore = function (organisationId, storeName) {
                        dataservice.createStore({
                            parameter1: organisationId,
                            parameter2: storeName
                        }, {
                            success: function (data) {
                                if (data) {
                                    toastr.success("Store created successfully.");
                                    getStores();
                                } else {
                                    toastr.error("Failed to create store.", "", ist.toastrOptions);
                                }

                            },
                            error: function (response) {
                                toastr.error("Failed to create store.", "", ist.toastrOptions);
                            }
                        });
                    },
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
                                view.initializeLabelPopovers();
                            },
                            error: function (response) {
                                //toastr.error("Failed to Delete . Error: " + response);
                                view.initializeLabelPopovers();
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
                    // Sets Data fro new Store 
                    setDataForNewStore = function (newStore) {
                        // Store Workflow parameters 
                        newStore.includeEmailBrokerArtworkOrderReport(true);
                        newStore.includeEmailBrokerArtworkOrderXML(true);
                        newStore.includeEmailBrokerArtworkOrderJobCard(true);
                        newStore.makeEmailBrokerArtworkOrderProductionReady(true);
                        newStore.isStoreModePrivate('false');
                        newStore.isTextWatermark('true');
                        newStore.isBrokerPaymentRequired('true');
                        newStore.isCalculateTaxByService('true');
                        newStore.isIncludeVAT('false');
                    },

            setThemeName = ko.computed(function () {
                if (isBaseDataLoded() && !isThemeNameSet() && selectedTheme() !== undefined) {
                    var theme = _.find(themes(), function (item) {
                        return item.SkinId == selectedTheme();
                    });
                    if (theme) {
                        selectedStore().currentThemeName(theme.Name);
                    }
                    isThemeNameSet(true);
                }
            }),
            selectedThemeName = ko.computed(function () {
                var theme = _.find(themes(), function (item) {
                    return item.SkinId == selectedTheme();
                });
                if (theme) {
                    return theme.Name;
                }

                return "";
            }),
            //On Edit Click Of Store
            onEditItem = function (item) {
                resetObservableArrays();
                editorViewModel.selectItem(item);
                openEditDialog();
                //$('.nav-tabs').children().removeClass('active');
                //$('#generalInfoTab').addClass('active');
                $('.nav-tabs li:first-child a').tab('show');
                $('.nav-tabs li:eq(0) a').tab('show');
                sharedNavigationVM.initialize(selectedStore, function (saveCallback) { saveStore(saveCallback); });
            },
            //On Edit Click Of Store
            onCreateNewStore = function () {
                resetObservableArrays();
                var store = new model.Store();
                setDataForNewStore(store);
                editorViewModel.selectItem(store);
                selectedStore(store);
                //Set By Default Store Type
                selectedStore().type('3');
                isEditorVisible(true);
                view.initializeForm();
                //getBaseDataFornewCompany();
                //$('.nav-tabs').children().removeClass('active');
                //$('#generalInfoTab').addClass('active');
                $('.nav-tabs li:first-child a').tab('show');
                $('.nav-tabs li:eq(0) a').tab('show');
                selectedItemsForOfferList.removeAll();
                selectedItemForAdd(undefined);
                selectedItemForRemove(undefined);

                if (itemsForWidgets().length === 0) {
                    getItemsForWidgets();
                }
                _.each(systemVariablesForSmartForms(), function (item) {
                    fieldVariablesForSmartForm.push(item);
                });
                sharedNavigationVM.initialize(selectedStore, function (saveCallback) { saveStore(saveCallback); });
                view.initializeLabelPopovers();
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
                        toastr.error("Failed to Delete . Error: " + response, "", ist.toastrOptions);
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
                            _.each(data.Companies, function (item) {
                                var module = model.StoreListView.Create(item);
                                stores.push(module);
                            });
                            pager().totalCount(data.RowCount);
                        }
                        isLoadingStores(false);
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Error: Failed To load Stores " + response, "", ist.toastrOptions);
                    }
                });
            },
            getStoresByFilter = function () {
                pager().reset();
                getStores();
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

            //Update: If store is creating and user select this store as Retail
            //  Then Create one new default territory and select this territory in all new creating address and user
            createNewTerritoryForRetailStore = ko.computed(function () {
                //selectedStore is new
                //new CompanyTerritories have no record
                if (selectedStore() != undefined && newCompanyTerritories != undefined && selectedStore().type() != undefined
                    && selectedStore().companyId() == undefined && newCompanyTerritories().length == 0 && selectedStore().type() == 4) {
                    var companyTerritory = new model.CompanyTerritory();
                    companyTerritory.territoryId(companyTerritoryCounter);
                    addCompanyTerritoryCount();
                    companyTerritory.territoryName('Default Retail Territory Name');
                    companyTerritory.territoryCode('Default Retail Territory Code');
                    companyTerritory.isDefault(true);

                    selectedStore().companyTerritories.splice(0, 0, companyTerritory);
                    newCompanyTerritories.push(companyTerritory);
                }
            }),

            selectedTheme = ko.observable(),
            //Apply Theme
            onApplyTheme = function () {
                if (selectedTheme() !== undefined) {
                    var theme = _.find(themes(), function (item) {
                        return item.SkinId == selectedTheme();
                    });
                    if (theme) {
                        confirmation.messageText("Do you want to apply theme? " +
                            "Your current changes for banner, secondary pages, css, sprite will be overridden.");
                        confirmation.afterProceed(function () {
                            selectedStore().currentThemeName(theme.Name);
                            getgetThemeDetailByFullZipPath(selectedTheme(), theme.FullZipPath);
                        });
                        confirmation.afterCancel();
                        confirmation.show();
                    }
                }
            },
            // Select Theme
            selectTheme = function (data) {
                if (data && data.SkinId !== selectedTheme()) {
                    selectedTheme(data.SkinId);
                    view.closeThemeList();
                }
            },
            //Get Theme Detail By Full Zip Path
            getgetThemeDetailByFullZipPath = function (themeId, path) {
                dataservice.getThemeDetail({
                    themeId: themeId,
                    fullZipPath: path,
                    companyId: selectedStore().companyId(),
                }, {
                    success: function (data) {
                        selectedStore().currentThemeId(selectedTheme());
                        toastr.success("Theme Applied Successfully.");
                    },
                    error: function (response) {
                        toastr.error("Failed to apply Theme.", "", ist.toastrOptions);
                    }
                });
            },
            vatHandler = function () {
                var vat = selectedStore().isIncludeVAT();
                if (vat == 'true') {
                    selectedStore().isCalculateTaxByService('false');
                }
                return true;
            },
            calculateTaxByServiceHandler = function () {
                var tax = selectedStore().isCalculateTaxByService();
                if (tax == 'true') {
                    selectedStore().isIncludeVAT('false');
                }
                return true;
            },
            //Delete Media Gallary Item
            onDeleteMedia = function (media) {
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    if (media.fakeId() < 0) {
                        var flag = true;
                        if (selectedStore().storeBackgroudImageImageSource() === media.fileSource()) {
                            toastr.error("File used in Store background Image.", "", ist.toastrOptions);
                            flag = false;
                        }
                        var item = _.find(companyBanners(), function (banner) {
                            return banner.filePath() === media.id();
                        });
                        if (item) {
                            toastr.error("File used in banner.", "", ist.toastrOptions);
                            flag = false;
                        }
                        var secPage = _.find(newAddedSecondaryPage(), function (page) {
                            return page.imageSrc() === media.fileSource();
                        });
                        if (secPage) {
                            toastr.error("File used in Secondary Page.", "", ist.toastrOptions);
                            flag = false;
                        }
                        if (flag) {
                            selectedStore().mediaLibraries.remove(media);
                        }

                    } else {
                        deleteMediaFile(media);
                    }
                });
                confirmation.show();
            },
            deleteMediaFile = function (media) {
                dataservice.deleteMediaLibraryItemById(media.convertToServerData(), {
                    success: function (data) {
                        selectedStore().mediaLibraries.remove(media);
                        toastr.success("Successfully deleted.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage, "", ist.toastrOptions);

                        } else {

                            toastr.error("Failed to delete.", "", ist.toastrOptions);
                        }
                    }
                });
            },
            //#endregion _____________________  S T O R E ____________________

            // #region _________R A V E   R E V I E W_________________________
            newCompanyTerritoryId = -1,
            addNewCompanyTerritoryId = function () {
                newCompanyTerritoryId = newCompanyTerritoryId - 1;
            },
            //Selected Rave Review
            //selectedRaveReview = ko.observable(),
            raveReviewEditorViewModel = new ist.ViewModel(model.RaveReview),
            selectedRaveReview = raveReviewEditorViewModel.itemForEditing,
            // Template Chooser For Rave Review
            templateToUseRaveReviews = function (raveReview) {
                return (raveReview === selectedRaveReview() ? 'editRaveReviewTemplate' : 'itemRaveReviewTemplate');
            },
            //Create Stock Sub Category
            onCreateNewRaveReview = function () {
                var raveReview = new model.RaveReview();
                selectedRaveReview(raveReview);
                selectedRaveReview().isDisplay(false);
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
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    _.each(selectedStore().raveReviews(), function (item) {
                        if (item.reviewId() === raveReview.reviewId()) {
                            selectedStore().raveReviews.remove(raveReview);
                            view.hideRaveReviewDialog();
                        }
                    });
                });
                confirmation.show();

                return;
            },
            onEditRaveReview = function (raveReview) {
                //selectedRaveReview(raveReview);
                raveReviewEditorViewModel.selectItem(raveReview);
                selectedRaveReview().reset();
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
                    if (selectedRaveReview().reviewId() == undefined) {
                        selectedRaveReview().reviewId(newCompanyTerritoryId);
                        addNewCompanyTerritoryId();
                        selectedStore().raveReviews.splice(0, 0, selectedRaveReview());
                    }
                    //else if (selectedRaveReview().reviewId() > 0) {
                    var count = 0;
                    _.each(selectedStore().raveReviews(), function (raveReview) {
                        if (raveReview.reviewId() == selectedRaveReview().reviewId()) {
                            selectedStore().raveReviews.remove(raveReview);
                            selectedStore().raveReviews.splice(count, 0, selectedRaveReview());
                        }
                        count = count + 1;
                    });
                    //}

                    view.hideRaveReviewDialog();
                }

            },
            secondayPageIsDisplayInFooterHandler = function () {
                selectedStore().isDidplayInFooter(!selectedStore().isDidplayInFooter());
                return true;
            },
            // #endregion 

            // #region _________C O M P A N Y   T E R R I T O R Y ____________

            //Selected CompanyTerritory
            companyTerritoryEditorViewModel = new ist.ViewModel(model.CompanyTerritory),
            selectedCompanyTerritory = companyTerritoryEditorViewModel.itemForEditing,

            //Deleted Company Territory 
            deletedCompanyTerritories = ko.observableArray([]),
            edittedCompanyTerritories = ko.observableArray([]),

            //Company Territory Pager
            companyTerritoryPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //CompanyTerritory Search Filter
            searchCompanyTerritoryFilter = ko.observable(),
            //Search Company Territory
            searchCompanyTerritory = function () {

                if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
                    dataservice.searchCompanyTerritory({
                        SearchFilter: searchCompanyTerritoryFilter(),
                        CompanyId: selectedStore().companyId(),
                        PageSize: companyTerritoryPager().pageSize(),
                        PageNo: companyTerritoryPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {

                            var isStoreDirty = selectedStore().hasChanges();
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
                            companyTerritoryPager().totalCount(data.RowCount);
                            //check on client side, push all if new added work
                            if (searchCompanyTerritoryFilter() == "" || searchCompanyTerritoryFilter() == undefined) {
                                _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                    selectedStore().companyTerritories.push(companyTerritoryItem);
                                });
                            }
                            //check on client side, if filter is not null
                            if (searchCompanyTerritoryFilter() != "" && searchCompanyTerritoryFilter() != undefined) {
                                _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                                    if (companyTerritoryItem.territoryName().indexOf(searchCompanyTerritoryFilter()) != -1 || companyTerritoryItem.territoryCode().indexOf(searchCompanyTerritoryFilter()) != -1) {
                                        selectedStore().companyTerritories.push(companyTerritoryItem);
                                    }
                                });
                            }
                            if (!isStoreDirty) {
                                selectedStore().reset();
                            }

                        },
                        error: function (response) {
                            toastr.error("Failed To Load Company territories" + response, "", ist.toastrOptions);
                        }
                    });
                }
                else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                    selectedStore().companyTerritories.removeAll();
                    //check on client side, push all if new added work
                    if (searchCompanyTerritoryFilter() == "" || searchCompanyTerritoryFilter() == undefined) {
                        _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                            selectedStore().companyTerritories.push(companyTerritoryItem);
                        });
                    }
                    //check on client side, if filter is not null
                    if (searchCompanyTerritoryFilter() != "" && searchCompanyTerritoryFilter() != undefined) {
                        _.each(newCompanyTerritories(), function (companyTerritoryItem) {
                            if (companyTerritoryItem.territoryName().indexOf(searchCompanyTerritoryFilter()) != -1 || companyTerritoryItem.territoryCode().indexOf(searchCompanyTerritoryFilter()) != -1) {
                                selectedStore().companyTerritories.push(companyTerritoryItem);
                            }
                        });
                    }
                }
            },
            companyTerritoryFilterSelected = ko.computed(function () {
                if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined && selectedStore().companyId() !== undefined) {
                    searchCompanyTerritory();
                }
            }),
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
                if (selectedStore().companyId() == undefined && newCompanyTerritories().length == 0) {
                    selectedCompanyTerritory().isDefault(true);
                }

                _.each(fieldVariablesOfTerritoryType(), function (item) {
                    selectedCompanyTerritory().scopeVariables.push(scopeVariableMapper(item));
                });


                isSavingNewCompanyTerritory(true);
                view.showCompanyTerritoryDialog();
                if (selectedStore().companyId() !== undefined && selectedCompanyTerritory().territoryId() === undefined) {
                    var scope = 4;
                    getCompanyContactVariable(scope);
                }
                // For Change Detection
                var territoryIsDefault = selectedCompanyTerritory().isDefault();
                selectedCompanyTerritory().isDefault(!territoryIsDefault);
                selectedCompanyTerritory().isDefault(territoryIsDefault || false);
            },
            // Delete Company Territory
            onDeleteCompanyTerritory = function (companyTerritory) {
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    //#region Db Saved Record Id > 0
                    if (companyTerritory.companyId() > 0 && companyTerritory.territoryId() > 0) {
                        //Check if company Territory is default and there exist any other territory to set its isDefualt flag to true
                        //Or Company Territory is not default ones
                        //if (!companyTerritory.isDefault() || companyTerritory.isDefault() && selectedStore().companyTerritories().length > 1) {
                        if (!companyTerritory.isDefault()) {
                            dataservice.deleteCompanyTerritory({
                                //companyTerritory: companyTerritory.convertToServerData()
                                CompanyTerritoryId: companyTerritory.territoryId()
                            }, {
                                success: function (data) {
                                    if (data) {
                                        //companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() - 1);
                                        var storeGotChanges = selectedStore().hasChanges();
                                        selectedStore().companyTerritories.remove(companyTerritory);
                                        if (!storeGotChanges) {
                                            selectedStore().reset();
                                        }

                                        toastr.success("Deleted Successfully");
                                        isLoadingStores(false);
                                        //Updating Drop downs
                                        _.each(addressCompanyTerritoriesFilter(), function (item) {
                                            if (item.territoryId() == companyTerritory.territoryId()) {
                                                addressCompanyTerritoriesFilter.remove(item);
                                            }
                                        });
                                        _.each(contactCompanyTerritoriesFilter(), function (item) {
                                            if (item.territoryId() == companyTerritory.territoryId()) {
                                                contactCompanyTerritoriesFilter.remove(item);
                                            }
                                        });
                                    } else {
                                        toastr.error("Territory can not be deleted. It might exist in Address or Contact", "", ist.toastrOptions);
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Delete Company Territory " + response, "", ist.toastrOptions);
                                }
                            });

                        } else {
                            toastr.error("Make New Default territory first", "", ist.toastrOptions);
                        }
                    }
                        //#endregion
                        //#region Else Company territry is newly created
                    else {
                        //companyTerritoryPager().totalCount(companyTerritoryPager().totalCount() - 1);
                        if (companyTerritory.isDefault() && selectedStore().companyTerritories().length == 1) {
                            toastr.error("Make New Default territory first", "", ist.toastrOptions);

                        } else {
                            // if (selectedStore() != undefined && (selectedStore().newAddedAddresses !== undefined && selectedStore().newAddedCompanyContacts !== undefined && selectedStore().newAddedAddresses().length > 0 || selectedStore().newAddedCompanyContacts().length > 0)) {
                            var flag = true;
                            if (newAddresses != undefined) {
                                _.each(newAddresses(), function (address) {
                                    if (address.territoryId() == companyTerritory.territoryId()) {
                                        toastr.error("Error: Territory can not deleted as it exist in new created address", "", ist.toastrOptions);
                                        flag = false;
                                    }
                                });
                            }
                            if (newCompanyContacts != undefined) {
                                _.each(newCompanyContacts(), function (contact) {
                                    if (contact.territoryId() == companyTerritory.territoryId()) {
                                        toastr.error("Error: Territory can not deleted as it exist in new created contact", "", ist.toastrOptions);
                                        flag = false;
                                    }
                                });
                            }
                            if (flag) {
                                _.each(newCompanyTerritories(), function (item) {
                                    if (item.territoryId() == companyTerritory.territoryId()) {
                                        newCompanyTerritories.remove(companyTerritory);
                                    }
                                });

                                addressCompanyTerritoriesFilter.remove(companyTerritory);
                                contactCompanyTerritoriesFilter.remove(companyTerritory);
                                selectedStore().companyTerritories.remove(companyTerritory);
                                if (selectedStore().companyTerritories()[0] != undefined) {
                                    selectedStore().companyTerritories()[0].isDefault(true);
                                    if (selectedStore().companyTerritories()[0].territoryId() > 0) {
                                        edittedCompanyTerritories.push(selectedStore().companyTerritories()[0]);
                                    }
                                }

                            } else { //flag == false
                                toastr.error("Territory Exist in Address Or Contact. Please delete them first", "", ist.toastrOptions);
                            }
                            // }
                        }

                    }
                    //#endregion
                    view.hideCompanyTerritoryDialog();
                });
                confirmation.show();

                return;
            },
            onEditCompanyTerritory = function (companyTerritory) {
                //selectedCompanyTerritory(companyTerritory);
                companyTerritoryEditorViewModel.selectItem(companyTerritory);
                isSavingNewCompanyTerritory(false);
                if (selectedCompanyTerritory().territoryId() !== undefined && selectedStore().companyId() !== undefined) {
                    var scope = 4;
                    getCompanyContactVariableForEditContact(selectedCompanyTerritory().territoryId(), scope);

                }
                selectedCompanyTerritory().reset();
                view.showCompanyTerritoryDialog();
            },
            onCloseCompanyTerritory = function () {
                selectedCompanyTerritory(undefined);
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
            companyTerritoryCounter = -1,
            addCompanyTerritoryCount = function () {
                companyTerritoryCounter = (companyTerritoryCounter - 1);
            },
            onSaveCompanyTerritory = function () {
                if (doBeforeSaveCompanyTerritory()) {
                    //#region If Store is Editting, CompanyId > 0
                    var territory = selectedCompanyTerritory().convertToServerData();
                    _.each(selectedCompanyTerritory().scopeVariables(), function (item) {
                        territory.ScopeVariables.push(item.convertToServerData(item));
                    });


                    if (selectedStore().companyId() > 0) {
                        var storeGotChanges = selectedStore().hasChanges();
                        selectedCompanyTerritory().companyId(selectedStore().companyId());
                        territory.CompanyId = selectedStore().companyId();
                        dataservice.saveCompanyTerritory(
                            territory,
                            {
                                success: function (data) {
                                    if (data) {
                                        var savedTerritory = model.CompanyTerritory.Create(data);
                                        if (selectedCompanyTerritory().territoryId() <= 0 || selectedCompanyTerritory().territoryId() == undefined) {
                                            selectedStore().companyTerritories.splice(0, 0, savedTerritory);
                                            //Add territory in address drop down to use in saving address
                                            addressCompanyTerritoriesFilter.push(savedTerritory);
                                            contactCompanyTerritoriesFilter.push(savedTerritory);
                                        }

                                        if (savedTerritory.isDefault()) {
                                            _.each(selectedStore().companyTerritories(), function (item) {
                                                if (item.territoryId() != data.TerritoryId) {
                                                    if (item.isDefault() == true) {
                                                        item.isDefault(false);
                                                    }
                                                }

                                            });
                                        }

                                        if (selectedCompanyTerritory().territoryId() > 0) {
                                            var count = 0;
                                            _.each(selectedStore().companyTerritories(), function (item) {
                                                if (item.territoryId() == data.TerritoryId) {
                                                    var totalCount = companyTerritoryPager().totalCount();
                                                    selectedStore().companyTerritories.remove(item);
                                                    selectedStore().companyTerritories.splice(count, 0, savedTerritory);
                                                    companyTerritoryPager().totalCount(totalCount);
                                                }
                                                count = count + 1;
                                            });
                                        }
                                        if (!storeGotChanges) {
                                            selectedStore().reset();
                                        }
                                        toastr.success("Saved Successfully");
                                        view.hideCompanyTerritoryDialog();
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Save Company Territory " + response, "", ist.toastrOptions);
                                }
                            });

                    }
                        //#endregion
                    else {
                        //Is Saving New Territory
                        if (selectedCompanyTerritory().territoryId() < 0) {
                            if (selectedCompanyTerritory().isDefault()) {
                                _.each(newCompanyTerritories(), function (territory) {
                                    if (territory.isDefault() && territory.territoryId() != selectedCompanyTerritory().territoryId()) {
                                        territory.isDefault(false);
                                    }
                                });
                                _.each(selectedStore().companyTerritories(), function (territory) {
                                    if (territory.isDefault() && territory.territoryId() != selectedCompanyTerritory().territoryId()) {
                                        territory.isDefault(false);
                                    }
                                });
                            }
                        }
                        if (selectedCompanyTerritory().territoryId() === undefined && isSavingNewCompanyTerritory() === true) {

                            selectedCompanyTerritory().territoryId(companyTerritoryCounter);
                            addCompanyTerritoryCount();
                            if (selectedCompanyTerritory().isDefault()) {
                                _.each(newCompanyTerritories(), function (territory) {
                                    if (territory.isDefault()) {
                                        territory.isDefault(false);
                                    }
                                });
                                _.each(selectedStore().companyTerritories(), function (territory) {
                                    if (territory.isDefault()) {
                                        territory.isDefault(false);
                                    }
                                });
                            }
                            selectedStore().companyTerritories.splice(0, 0, selectedCompanyTerritory());
                            newCompanyTerritories.splice(0, 0, selectedCompanyTerritory());
                            //Add territory in address drop down to use in saving address
                            addressCompanyTerritoriesFilter.push(selectedCompanyTerritory());
                            contactCompanyTerritoriesFilter.push(selectedCompanyTerritory());

                        } else {
                            //pushing item in editted Company Territories List
                            if (selectedCompanyTerritory().companyId() != undefined) {
                                var match = ko.utils.arrayFirst(edittedCompanyTerritories(), function (item) {
                                    return (selectedCompanyTerritory().territoryId() === item.territoryId());
                                });

                                if (match === undefined || match === null) {
                                    edittedCompanyTerritories.push(selectedCompanyTerritory());
                                }
                            }
                        }

                        view.hideCompanyTerritoryDialog();
                    }

                }
            },
            //search CompanyTerritory By Filter
            searchCompanyTerritoryByFilter = function () {
                companyTerritoryPager().reset();
                searchCompanyTerritory();
            },
            // #endregion __________________ C O M P A N Y   T E R R I T O R Y __________________

            // #region _________C O M P A N Y    C M Y K   C O L O R  ________
            newCompanyCmykId = -1,
            // Editor View Model
            companyCmykColoreditorViewModel = new ist.ViewModel(model.CompanyCMYKColor),

            addNewCompanyCmykId = function () {
                newCompanyCmykId = newCompanyCmykId - 1;
            },
            //Selected Company CMYK Color
            // ReSharper disable InconsistentNaming
            selectedCompanyCMYKColor = companyCmykColoreditorViewModel.itemForEditing,
            isSavingNew = ko.observable(false),
            // Template Chooser For Company CMYK Color
            templateToUseCompanyCMYKColors = function (companyCMYKColor) {
                return (companyCMYKColor === selectedCompanyCMYKColor() ? 'editCompanyCMYKColorTemplate' : 'itemCompanyCMYKColorTemplate');
            },
            //Create Stock Sub Category
            onCreateNewCompanyCMYKColor = function () {
                var companyCMYKColor = new model.CompanyCMYKColor();
                selectedCompanyCMYKColor(companyCMYKColor);
                selectedCompanyCMYKColor().isActive(true);
                view.showCompanyCMYKColorDialog();
                isSavingNew(true);
            },
            // Delete a company CMYK Color
            onDeleteCompanyCMYKColors = function (companyCMYKColor) {
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    selectedStore().companyCMYKColors.remove(companyCMYKColor);
                    view.hideCompanyCMYKColorDialog();
                });
                confirmation.show();

                return;
            },
            //On Edit
            onEditCompanyCMYKColor = function (companyCMYKColor) {
                //selectedCompanyCMYKColor(companyCMYKColor);
                companyCmykColoreditorViewModel.selectItem(companyCMYKColor);
                view.showCompanyCMYKColorDialog();
            },
            //On Close
            onCloseCompanyCMYKColor = function () {
                //revert method is of no use
                //companyCmykColoreditorViewModel.revertItem();
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
            //On Save
            onSaveCompanyCMYKColor = function () {
                if (doBeforeSaveCompanyCMYKColor() && isSavingNew() === true) {
                    selectedCompanyCMYKColor().colorId(newCompanyCmykId);
                    addNewCompanyCmykId();
                    selectedStore().companyCMYKColors.splice(0, 0, selectedCompanyCMYKColor());
                    //companyCmykColoreditorViewModel.selectItem(selectedCompanyCMYKColor());
                    view.hideCompanyCMYKColorDialog();
                    isSavingNew(false);
                }
                if (doBeforeSaveCompanyCMYKColor() && isSavingNew() !== true) {
                    //companyCmykColoreditorViewModel.selectItem(selectedCompanyCMYKColor());
                    var count = 0;
                    _.each(selectedStore().companyCMYKColors(), function (color) {
                        if (color.colorId() == selectedCompanyCMYKColor().colorId()) {
                            selectedStore().companyCMYKColors.remove(color);
                            selectedStore().companyCMYKColors.splice(count, 0, selectedCompanyCMYKColor());
                        }
                        count = count + 1;
                    });
                    view.hideCompanyCMYKColorDialog();
                    isSavingNew(false);
                }

            },

            calculateCyanValue = ko.computed({
                read: function () {
                    if (!selectedCompanyCMYKColor()) {
                        return 0;
                    }

                    var colorC = selectedCompanyCMYKColor().colorC();
                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(colorC, colorM, ColorY, ColorK);

                    selectedHexValue(hex);
                    return selectedCompanyCMYKColor().colorC();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === selectedCompanyCMYKColor().colorC()) {
                        return;
                    }

                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(value, colorM, ColorY, ColorK);
                    selectedHexValue(hex);

                    selectedCompanyCMYKColor().colorC(value);
                    return value;

                }
            }),

            calculateMagentaValue = ko.computed({
                read: function () {
                    if (!selectedCompanyCMYKColor()) {
                        return 0;
                    }

                    var colorC = selectedCompanyCMYKColor().colorC();
                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(colorC, colorM, ColorY, ColorK);
                    selectedHexValue(hex);
                    return selectedCompanyCMYKColor().colorM();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === selectedCompanyCMYKColor().colorM()) {
                        return;
                    }
                    var colorC = selectedCompanyCMYKColor().colorC();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(colorC, value, ColorY, ColorK);
                    selectedHexValue(hex);

                    selectedCompanyCMYKColor().colorM(value);

                    return value;
                }
            }),


            calculateYellowValue = ko.computed({
                read: function () {
                    if (!selectedCompanyCMYKColor()) {
                        return 0;
                    }

                    var colorC = selectedCompanyCMYKColor().colorC();
                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(colorC, colorM, ColorY, ColorK);
                    selectedHexValue(hex);
                    return selectedCompanyCMYKColor().colorY();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === selectedCompanyCMYKColor().colorY()) {
                        return;
                    }

                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorC = selectedCompanyCMYKColor().colorC();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(ColorC, colorM, value, ColorK);
                    selectedHexValue(hex);

                    selectedCompanyCMYKColor().colorY(value);
                    return value;
                }
            }),

            calculateBlackValue = ko.computed({
                read: function () {
                    if (!selectedCompanyCMYKColor()) {
                        return 0;
                    }

                    var colorC = selectedCompanyCMYKColor().colorC();
                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorK = selectedCompanyCMYKColor().colorK();

                    var hex = getColorHex(colorC, colorM, ColorY, ColorK);
                    selectedHexValue(hex);


                    return selectedCompanyCMYKColor().colorK();
                },
                write: function (value) {
                    if ((value === null || value === undefined) || value === selectedCompanyCMYKColor().colorK()) {
                        return;
                    }
                    var colorM = selectedCompanyCMYKColor().colorM();
                    var ColorY = selectedCompanyCMYKColor().colorY();
                    var ColorC = selectedCompanyCMYKColor().colorC();

                    var hex = getColorHex(ColorC, colorM, ColorY, value);
                    selectedHexValue(hex);

                    selectedCompanyCMYKColor().colorK(value);
                    return value;

                }
            }),

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

                    var companyBannerSet = _.find(companyBannerSetList(), function (banner) {
                        return banner.id() === companyBanner.companySetId();
                    });
                    if (companyBannerSet !== undefined && companyBannerSet !== null) {
                        companyBanner.setName(companyBannerSet.setName());
                    }
                    if (companyBanner.id() === undefined) {
                        companyBanner.id(addBannerCount() - 1);
                        addBannerCount(addBannerCount() - 1);
                        if (companyBanner.companySetId() === selectedStore().activeBannerSetId() || selectedStore().activeBannerSetId() === undefined) {
                            filteredCompanyBanners.splice(0, 0, companyBanner);
                            companyBanners.splice(0, 0, companyBanner);
                        } else {
                            companyBanners.splice(0, 0, companyBanner);
                        }
                    } else {
                        var item = _.find(companyBanners(), function (banner) {
                            return banner.id() === companyBanner.id();
                        });
                        //Banner set Change In Edit banner
                        if (selectedStore().activeBannerSetId() !== undefined && companyBanner.companySetId() !== selectedStore().activeBannerSetId()) {
                            filteredCompanyBanners.remove(companyBanner);
                        }
                        if (item) {
                            item.heading(companyBanner.heading());
                            item.description(companyBanner.description());
                            item.itemURL(companyBanner.itemURL());
                            item.buttonURL(companyBanner.buttonURL());
                            item.companySetId(companyBanner.companySetId());
                            item.fileBinary(companyBanner.fileBinary());
                            item.imageSource(companyBanner.fileBinary());
                            item.filename(companyBanner.filename());
                        }

                    }
                    //selectedStore().storeLayoutChange("change");
                    view.hideEditBannerDialog();
                }
            },
            //Save Banner Set
            onSaveBannerSet = function (bannerSet) {
                if (doBeforeSaveCompanyBannerSet()) {
                    bannerSet.id(addBannerSetCount() - 1);
                    addBannerSetCount(addBannerSetCount() - 1);
                    companyBannerSetList.push(bannerSet);
                    //selectedStore().storeLayoutChange("change");
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
            onChangeBannerSet = function () {
                filteredCompanyBanners.removeAll();
                if (selectedStore().activeBannerSetId() !== undefined) {
                    _.each(companyBanners(), function (item) {
                        if (item.companySetId() === selectedStore().activeBannerSetId()) {
                            filteredCompanyBanners.push(item);
                        }
                    });
                } else {
                    ko.utils.arrayPushAll(filteredCompanyBanners(), companyBanners());
                    filteredCompanyBanners.valueHasMutated();
                }
            },
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
                            //selectedStore().storeLayoutChange("change");
                        }
                    });
                    //selectedStore().storeLayoutChange("change");
                    if (banner.id() > 0) {
                        deleteBanner(banner);
                    }
                    filteredCompanyBanners.remove(banner);
                    view.hideEditBannerDialog();
                });
                confirmation.show();
            },
            //Delete Banner
            deleteBanner = function (banner) {

                dataservice.deleteCompanyBanner(banner.convertToServerData(banner), {
                    success: function () {
                        toastr.success("Successfully removed.");
                    },
                    error: function () {
                        toastr.error("Failed to remove.", "", ist.toastrOptions);
                    }
                });
            },
            //#endregion 

            //#region _________EMAIL ______________________________________
            selectedEmail = ko.observable(),
            selectedEmailListViewItem = ko.observable(),
            emailIdCounter = ko.observable(0),
            selectedSection = ko.observable(),
            newAddedCampaigns = ko.observableArray([]),
            editedCampaigns = ko.observableArray([]),
            deletedCampaigns = ko.observableArray([]),
            campaignSectionFlags = ko.observableArray([]),
            campaignCompanyTypes = ko.observableArray([]),
            campaignGroups = ko.observableArray([]),
            emailCampaignSections = ko.observableArray([]),
            //Create One Time Marketing Email
            onCreateOneTimeMarketingEmail = function () {
                ckEditorOpenFrom("Campaign");
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
            },
            //Create Interval Marketing Email
            onCreateIntervalMarketingEmail = function () {
                ckEditorOpenFrom("Campaign");
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
                    var emailMessage = CKEDITOR.instances.content.getData();
                    email.hTMLMessageA(emailMessage);
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
                    //New Added
                    if (email.id() === undefined) {
                        email.id(emailIdCounter() - 1);
                        emails.splice(0, 0, email);
                        newAddedCampaigns.push(email);
                        emailIdCounter(emailIdCounter() - 1);
                    } else {
                        //Not save item, edit case
                        if (email.id() < 0) {
                            var campaign = _.find(newAddedCampaigns(), function (item) {
                                return item.id() === email.id();
                            });
                            if (campaign) {
                                newAddedCampaigns.remove(campaign);
                                newAddedCampaigns.push(email);
                            }
                        }
                        //Edit Email
                        if (email.id() > 0) {
                            var campaignItem = _.find(editedCampaigns(), function (item) {
                                return item.id() === email.id();
                            });
                            if (campaignItem) {
                                editedCampaigns.remove(campaignItem);
                            }
                            editedCampaigns.push(email);
                        }
                        updateCampaignListViewItem(email);
                    }
                    email.reset();
                    view.hideEmailCamapaignDialog();
                }
            },
            //After save, update the selected List View Item
            updateCampaignListViewItem = function (email) {
                selectedEmailListViewItem().eventName(email.eventName());
                selectedEmailListViewItem().startDateTime(email.startDateTime());
                selectedEmailListViewItem().isEnabled(email.isEnabled());
                selectedEmailListViewItem().campaignType(email.campaignType());
                selectedEmailListViewItem().sendEmailAfterDays(email.sendEmailAfterDays());
                selectedEmailListViewItem().campaignName(email.campaignName());
                selectedEmailListViewItem().reset();
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
                selectedEmailListViewItem(campaign);
                ckEditorOpenFrom("Campaign");
                if (campaign.id() < 0) {
                    setCampaignDetail(campaign);
                } else {
                    var campaignItem = _.find(editedCampaigns(), function (item) {
                        return item.id() === campaign.id();
                    });
                    if (campaignItem) {
                        setCampaignDetail(campaignItem);
                    } else {
                        getCampaignDetail(campaign);
                    }
                }


            },

            setCampaignDetail = function (campaign) {
                selectedEmail(campaign);
                selectedEmail().reset();
                view.showEmailCamapaignDialog();
                if (campaignSectionFlags().length === 0) {
                    getCampaignBaseData();
                }
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
                            _.each(campaignGroups(), function (item) {
                                if (parseInt(groupIDs[k]) === item.id()) {
                                    item.isChecked(true);
                                }
                            });
                        }
                    }
                }
                makeCkeditorDropable();
            },
            getCampaignDetail = function (campaign) {
                dataservice.getCampaignDetailById({
                    campaignId: campaign.id(),
                }, {
                    success: function (data) {
                        if (data != null) {
                            var campaignDetail = model.Campaign.Create(data);
                            _.each(data.CampaignImages, function (campaignImage) {
                                campaignDetail.campaignImages.push(model.CampaignImage.Create(campaignImage));
                            });
                            setCampaignDetail(campaignDetail);
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load Detail . Error: ");
                    }
                });
            },
            // Delete Email
            onDeleteEmail = function (email) {
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    emails.remove(selectedEmailListViewItem());
                    view.hideEmailCamapaignDialog();
                    if (selectedEmailListViewItem().id() > 0) {
                        deletedCampaigns.push(selectedEmailListViewItem());
                    }
                    removeCampaignFromAddEditList(selectedEmailListViewItem());
                });
                confirmation.show();

            },
            removeCampaignFromAddEditList = function (email) {
                var campaign = _.find(newAddedCampaigns(), function (item) {
                    return item.id() === email.id();
                });
                if (campaign) {
                    newAddedCampaigns.remove(campaign);
                }
                var campaignItem = _.find(editedCampaigns(), function (item) {
                    return item.id() === email.id();
                });
                if (campaignItem) {
                    editedCampaigns.remove(campaignItem);
                }
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
                        toastr.error("Failed to load base data.", "", ist.toastrOptions);
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
                    }, 4000);
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
                } else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.emailVariable !== undefined && source.emailVariable !== null) {
                    selectedEmail().hTMLMessageA(hTMLMessageA + source.emailVariable.variableTag());
                } else if (selectedEmail() !== undefined && source !== undefined && source !== null && source.image !== undefined && source.image !== null) {
                    // var img = "<img  src=" + source.image.imageSource() + "/>";
                    var img = "<img width=\"100px\"  height=\"100px\" src=\"" + source.image.imageSource() + "\"/>";
                    selectedEmail().hTMLMessageA(hTMLMessageA + img);
                    //selectedEmail().hTMLMessageA(); //imageSource
                }
            },
            //#endregion

            // #region _________A D D R E S S E S __________________________

            //Selected AddresssearchCompanyTerritory
            //Selected Address
            addressEditorViewModel = new ist.ViewModel(model.Address),
            selectedAddress = addressEditorViewModel.itemForEditing,

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
                        if (item.territoryId() == selectedCompanyContact().territoryId()) {
                            shippingAddresses.push(item);
                            bussinessAddresses.push(item);
                        }
                    });
                }
            }),
            selectBussinessAddress = ko.computed(function () {
                if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() != undefined) {
                    var contactHasChanges = selectedCompanyContact().hasChanges();
                    _.each(allCompanyAddressesList(), function (item) {
                        if (item.addressId() == selectedCompanyContact().bussinessAddressId()) {
                            selectedBussinessAddress(item);
                            if (item.city() == null) {
                                selectedBussinessAddress().city(undefined);
                            }
                            if (item.state() == null) {
                                selectedBussinessAddress().state(undefined);
                            }
                            if (selectedCompanyContact() != undefined) {
                                selectedCompanyContact().bussinessAddressId(item.addressId());
                                selectedCompanyContact().addressId(item.addressId());
                                selectedBussinessAddress().stateName(item.stateName());
                                selectedBussinessAddress().stateCode(item.stateCode());
                                if (!contactHasChanges) {
                                    selectedCompanyContact().reset();
                                }
                            }
                        }
                    });
                }
                if (selectedCompanyContact() != undefined && selectedCompanyContact().bussinessAddressId() == undefined) {
                    selectedBussinessAddress(undefined);
                }
            }),
            selectShippingAddress = ko.computed(function () {
                if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() != undefined) {
                    var contactHasChanges = selectedCompanyContact().hasChanges();
                    _.each(allCompanyAddressesList(), function (item) {
                        if (item.addressId() == selectedCompanyContact().shippingAddressId()) {
                            selectedShippingAddress(item);
                            if (item.city() == null) {
                                selectedShippingAddress().city(undefined);
                            }
                            if (item.state() == null) {
                                selectedShippingAddress().state(undefined);
                            }
                            if (selectedCompanyContact() != undefined) {
                                selectedCompanyContact().shippingAddressId(item.addressId());
                                selectedShippingAddress().stateName(item.stateName());
                                selectedShippingAddress().stateCode(item.stateCode());
                                if (!contactHasChanges) {
                                    selectedCompanyContact().reset();
                                }
                            }
                        }
                    });
                }
                if (selectedCompanyContact() != undefined && selectedCompanyContact().shippingAddressId() == undefined) {
                    selectedShippingAddress(undefined);
                }
            }),
            //Get State Name By State Id
            //Method to be called on user and addresses tab selection
            userAndAddressesTabSelected = function () {
                // Resetting filter for Company Contact
                if ((searchCompanyContactFilter() !== "" && searchCompanyContactFilter() !== undefined)) {
                    searchCompanyContactFilter('');
                }

                // Resetting filter for Territory
                if (searchCompanyTerritoryFilter() !== undefined && searchCompanyTerritoryFilter() !== '') {
                    searchCompanyTerritoryFilter('');
                }
                // Resetting filter for Address
                if ((searchAddressFilter() !== "" && searchAddressFilter() !== undefined)) {
                    searchAddressFilter('');
                }

                isUserAndAddressesTabOpened(true);
            },
            //Address Pager
            addressPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Contact Company Pager
            contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Secondary Page Pager
            secondaryPagePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Secondary Page Pager
            systemPagePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Variable Page
            fieldVariablePager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Smart Form Pager
            smartFormPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Address Search Filter
            searchAddressFilter = ko.observable(),
            //wrapper Function For Search Address
            searchAddressByFilter = function () {
                addressPager().reset();
                searchAddress();
            },
            //Search Address
            searchAddress = function () {
                if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
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
                            var isStoreDirty = selectedStore().hasChanges();
                            selectedStore().addresses.removeAll();
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
                            //check on client side, push all if new added work
                            if (searchAddressFilter() == "" || searchAddressFilter() == undefined) {
                                if (addressTerritoryFilter() != undefined) {
                                    _.each(newAddresses(), function (addressItem) {
                                        if (addressItem.territoryId() == addressTerritoryFilter()) {
                                            selectedStore().addresses.push(addressItem);
                                        }
                                    });
                                } else {
                                    _.each(newAddresses(), function (addressItem) {
                                        selectedStore().addresses.push(addressItem);
                                    });
                                }

                            }

                            //check on client side, if filter is not null
                            if (searchAddressFilter() != "" && searchAddressFilter() != undefined) {
                                if (addressTerritoryFilter() == undefined) {
                                    _.each(newAddresses(), function (addressItem) {
                                        if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                            selectedStore().addresses.push(addressItem);
                                        }
                                    });
                                } else {
                                    _.each(newAddresses(), function (addressItem) {
                                        if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                            if (addressItem.territoryId() == addressTerritoryFilter()) {
                                                selectedStore().addresses.push(addressItem);
                                            }

                                        }
                                    });
                                }
                            }
                            if (!isStoreDirty) {
                                selectedStore().reset();
                            }
                            addressPager().totalCount(data.RowCount);
                        },
                        error: function (response) {
                            toastr.error("Failed To Load Addresses" + response, "", ist.toastrOptions);
                        }
                    });
                }
                else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                    selectedStore().addresses.removeAll();
                    //check on client side, push all if new added work
                    if (searchAddressFilter() == "" || searchAddressFilter() == undefined) {
                        if (addressTerritoryFilter() != undefined) {
                            _.each(newAddresses(), function (addressItem) {
                                if (addressItem.territoryId() == addressTerritoryFilter()) {
                                    selectedStore().addresses.push(addressItem);
                                }
                            });
                        } else {
                            _.each(newAddresses(), function (addressItem) {
                                selectedStore().addresses.push(addressItem);
                            });
                        }

                    }

                    //check on client side, if filter is not null
                    if (searchAddressFilter() != "" && searchAddressFilter() != undefined) {
                        if (addressTerritoryFilter() == undefined) {
                            _.each(newAddresses(), function (addressItem) {
                                if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                    selectedStore().addresses.push(addressItem);
                                }
                            });
                        } else {
                            _.each(newAddresses(), function (addressItem) {
                                if (addressItem.addressName().indexOf(searchAddressFilter()) != -1) {
                                    if (addressItem.territoryId() == addressTerritoryFilter()) {
                                        selectedStore().addresses.push(addressItem);
                                    }

                                }
                            });
                        }
                    }
                }
            },
            addressTerritoryFilterSelected = ko.computed(function () {
                if (isEditorVisible() && selectedStore() != null && selectedStore() != undefined) {
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
                //Update If Store is creating new and it is Retail then 
                //Make the first address isBilling and shipping as Default and sets its territory
                if (selectedStore().type() == 4 && selectedStore().companyId() == undefined) {
                    if (newAddresses != undefined && newAddresses().length == 0) {
                        if (newCompanyTerritories().length > 0) {
                            selectedAddress().territoryId(newCompanyTerritories()[0].territoryId());
                        }
                        selectedAddress().isDefaultTerrorityBilling(true);
                        selectedAddress().isDefaultTerrorityShipping(true);
                        selectedAddress().isDefaultAddress(true);
                        selectedAddress().isDefaultShippingAddress(true);
                    }
                    if (newAddresses().length > 0) {
                        if (newCompanyTerritories().length > 0) {
                            selectedAddress().territoryId(newCompanyTerritories()[0].territoryId());
                        }
                    }
                }
                //Updating Case
                if (selectedStore().type() == 4 && selectedStore().companyId() != undefined) {
                    selectedAddress().territoryId(selectedStore().companyTerritories()[0].territoryId());
                }
                if (selectedStore().type() == 3 && selectedStore().companyId() == undefined) {
                    if (newAddresses != undefined && newAddresses().length == 0) {
                        if (newCompanyTerritories().length > 0) {
                            _.each(newCompanyTerritories(), function (territory) {
                                if (territory.isDefault()) {
                                    selectedAddress().territoryId(territory.territoryId());
                                }
                            });
                        }
                        selectedAddress().isDefaultTerrorityBilling(true);
                        selectedAddress().isDefaultTerrorityShipping(true);
                        selectedAddress().isDefaultAddress(true);
                        selectedAddress().isDefaultShippingAddress(true);
                    }
                    if (newAddresses().length > 0) {
                        if (newCompanyTerritories().length > 0) {
                            _.each(newCompanyTerritories(), function (territory) {
                                if (territory.isDefault()) {
                                    selectedAddress().territoryId(territory.territoryId());
                                }
                            });
                        }
                    }
                }
                view.showAddressDialog();

                _.each(fieldVariablesOfAddressType(), function (item) {
                    var scopeVariable = model.ScopeVariable();
                    scopeVariable.id(item.id());
                    scopeVariable.contactId(item.contactId());
                    scopeVariable.variableId(item.variableId());
                    scopeVariable.value(item.value());
                    scopeVariable.fakeId(item.fakeId());
                    scopeVariable.title(item.title());
                    scopeVariable.type(item.type());
                    scopeVariable.scope(item.scope());
                    scopeVariable.optionId(item.optionId());
                    ko.utils.arrayPushAll(scopeVariable.variableOptions, item.variableOptions());
                    scopeVariable.variableOptions.valueHasMutated();
                    selectedAddress().scopeVariables.push(scopeVariable);
                });
                if (selectedStore().companyId() !== undefined && selectedAddress().addressId() === undefined) {
                    var scope = 3;
                    getCompanyContactVariable(scope);
                }
            },
            // Delete Address
            onDeleteAddress = function (address) {
                if (address.isDefaultTerrorityBilling() || address.isDefaultTerrorityShipping() || address.isDefaultAddress()) {
                    toastr.error("Address can not be deleted as it is either Default Billing/ Default Shipping or is default address", "", ist.toastrOptions);
                    return;
                } else {
                    // Ask for confirmation
                    confirmation.afterProceed(function () {
                        //#region Db Saved Record Id > 0
                        if (address.addressId() > 0) {
                            if (address.companyId() > 0 && address.addressId() > 0) {

                                if (!address.isDefaultTerrorityBilling() || !address.isDefaultTerrorityShipping() || !address.isDefaultAddress()) {
                                    dataservice.deleteCompanyAddress({
                                        AddressId: address.addressId()
                                    }, {
                                        success: function (data) {
                                            if (data) {
                                                var storeGotChanges = selectedStore().hasChanges();
                                                selectedStore().addresses.remove(address);
                                                if (!storeGotChanges) {
                                                    selectedStore().reset();
                                                }

                                                toastr.success("Deleted Successfully");
                                                isLoadingStores(false);
                                                //Updating Drop downs
                                                //_.each(addressCompanyTerritoriesFilter(), function (item) {
                                                //    if (item.territoryId() == companyTerritory.territoryId()) {
                                                //        addressCompanyTerritoriesFilter.remove(item);
                                                //    }
                                                //});
                                                bussinessAddresses.remove(address);
                                                shippingAddresses.remove(address);
                                                allCompanyAddressesList.remove(address);
                                            } else {
                                                toastr.error("Address can not be deleted. It might exist in Contact", "", ist.toastrOptions);
                                            }
                                        },
                                        error: function (response) {
                                            isLoadingStores(false);
                                            toastr.error("Error: Failed To Delete Address " + response, "", ist.toastrOptions);
                                        }
                                    });
                                } else {
                                    toastr.error("Address can not be deleted as it contains default values", "", ist.toastrOptions);
                                }
                            }
                        }
                        //#endregion
                        //#region Else Company territry is newly created

                        //check local
                        if (address.addressId() < 0) {
                            var flag = true;

                            _.each(newCompanyContacts(), function (item) {
                                if (item.bussinessAddressId() == address.addressId() || item.shippingAddressId() == address.addressId()) {
                                    flag = false;
                                }
                            });
                            if (flag) {
                                selectedStore().addresses.remove(address);
                            } else {
                                toastr.error("Address can not be deleted as it exist in User", "", ist.toastrOptions);
                            }

                        }
                        //#endregion
                        view.hideAddressDialog();
                    });
                    confirmation.show();

                    return;
                }
            },
            onEditAddress = function (address) {
                //selectedAddress(address);
                //selectedCompanyCMYKColor(companyCMYKColor);
                addressEditorViewModel.selectItem(address);

                isSavingNewAddress(false);
                view.showAddressDialog();
                if (selectedAddress().addressId() !== undefined && selectedStore().companyId() !== undefined) {
                    var scope = 3;
                    getCompanyContactVariableForEditContact(selectedAddress().addressId(), scope);

                }
            },
            onCloseAddress = function () {
                selectedAddress(undefined);
                selectedBussinessAddress(undefined);
                selectedBussinessAddressId(undefined);
                selectedShippingAddress(undefined);
                selectedShippingAddressId(undefined);
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
            newSavingAddressIdCount = -1,
            addNewSavingAddressIdCount = function () {
                newSavingAddressIdCount = newSavingAddressIdCount - 1;
            },
            onSaveAddress = function () {
                if (doBeforeSaveAddress()) {
                    //#region Company Is Editting Case
                    if (selectedStore().companyId() > 0) {
                        var storeGotChanges = selectedStore().hasChanges();
                        selectedAddress().companyId(selectedStore().companyId());

                        var address = selectedAddress().convertToServerData();
                        _.each(selectedAddress().scopeVariables(), function (item) {
                            address.ScopeVariables.push(item.convertToServerData(item));
                        });
                        dataservice.saveAddress(
                            address,
                            {
                                success: function (data) {
                                    if (data) {
                                        var savedAddress = model.Address.Create(data);

                                        _.each(selectedStore().addresses(), function (item) {
                                            if (savedAddress.isDefaultTerrorityBilling()) {
                                                if (item.isDefaultTerrorityBilling() == true) {
                                                    item.isDefaultTerrorityBilling(false);
                                                }
                                                if (savedAddress.isDefaultTerrorityShipping()) {
                                                    if (item.isDefaultTerrorityShipping() == true) {
                                                        item.isDefaultTerrorityShipping(false);
                                                    }
                                                }
                                                if (savedAddress.isDefaultAddress()) {
                                                    if (item.isDefaultAddress() == true) {
                                                        item.isDefaultAddress(false);
                                                    }
                                                }
                                                if (savedAddress.isDefaultShippingAddress()) {
                                                    if (item.isDefaultShippingAddress() == true) {
                                                        item.isDefaultShippingAddress(false);
                                                    }
                                                }
                                            }
                                        });
                                        if (selectedAddress().addressId() <= 0 || selectedAddress().addressId() == undefined) {
                                            selectedStore().addresses.splice(0, 0, savedAddress);
                                        }
                                        _.each(selectedStore().addresses(), function (item) {
                                            if (savedAddress.isDefaultTerrorityBilling()) {
                                                if (item.isDefaultTerrorityBilling() == true && item.territoryId() == savedAddress.territoryId()) {
                                                    item.isDefaultTerrorityBilling(false);
                                                }
                                            }
                                            if (savedAddress.isDefaultTerrorityShipping()) {
                                                if (item.isDefaultTerrorityShipping() == true && item.territoryId() == savedAddress.territoryId()) {
                                                    item.isDefaultTerrorityShipping(false);
                                                }
                                            }
                                            if (savedAddress.isDefaultAddress()) {
                                                if (item.isDefaultAddress() == true && item.territoryId() == savedAddress.territoryId()) {
                                                    item.isDefaultAddress(false);
                                                }
                                            }
                                            if (savedAddress.isDefaultShippingAddress()) {
                                                if (item.isDefaultShippingAddress() == true && item.territoryId() == savedAddress.territoryId()) {
                                                    item.isDefaultShippingAddress(false);
                                                }
                                            }
                                        });
                                        //Adding saved address in address lists on client side
                                        _.each(allCompanyAddressesList(), function (address) {
                                            if (address.addressId() == savedAddress.addressId()) {
                                                bussinessAddresses.remove(address);
                                                shippingAddresses.remove(address);
                                                allCompanyAddressesList.remove(address);
                                            }
                                        });
                                        bussinessAddresses.push(savedAddress);
                                        shippingAddresses.push(savedAddress);
                                        allCompanyAddressesList.push(savedAddress);
                                        if (!storeGotChanges) {
                                            selectedStore().reset();
                                        }
                                        searchAddress();
                                        toastr.success("Saved Successfully");
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Save Address " + response, "", ist.toastrOptions);
                                }
                            });
                    }
                        //#endregion
                        //#region New Company Case 

                    else {
                        if (selectedAddress().addressId() < 0) {
                            _.each(selectedStore().addresses(), function (item) {
                                if (selectedAddress().isDefaultTerrorityBilling() && selectedAddress().addressId() != item.addressId()) {
                                    if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultTerrorityBilling(false);
                                    }
                                }
                                if (selectedAddress().isDefaultTerrorityShipping() && selectedAddress().addressId() != item.addressId()) {
                                    if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultTerrorityShipping(false);
                                    }
                                }
                                if (selectedAddress().isDefaultAddress() && selectedAddress().addressId() != item.addressId()) {
                                    if (item.isDefaultAddress() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultAddress(false);
                                    }
                                }
                            });
                        }
                        if (selectedAddress().addressId() === undefined && isSavingNewAddress() === true) {
                            selectedAddress().addressId(newSavingAddressIdCount);
                            addNewSavingAddressIdCount();
                            _.each(selectedStore().addresses(), function (item) {
                                if (selectedAddress().isDefaultTerrorityBilling()) {
                                    if (item.isDefaultTerrorityBilling() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultTerrorityBilling(false);
                                    }
                                }
                                if (selectedAddress().isDefaultTerrorityShipping()) {
                                    if (item.isDefaultTerrorityShipping() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultTerrorityShipping(false);
                                    }
                                }
                                if (selectedAddress().isDefaultAddress()) {
                                    if (item.isDefaultAddress() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultAddress(false);
                                    }
                                }
                                if (selectedAddress().isDefaultShippingAddress()) {
                                    if (item.isDefaultShippingAddress() == true && item.territoryId() == selectedAddress().territoryId()) {
                                        item.isDefaultShippingAddress(false);
                                    }
                                }
                            });
                            //saving state and country name on client side 
                            _.each(states(), function (state) {
                                if (selectedAddress().state() == state.StateId) {
                                    selectedAddress().stateName(state.StateName);
                                }
                            });
                            _.each(countries(), function (country) {
                                if (selectedAddress().country() == country.CountryId) {
                                    selectedAddress().countryName(country.CountryName);
                                }
                            });
                            selectedStore().addresses.splice(0, 0, selectedAddress());
                            newAddresses.splice(0, 0, selectedAddress());

                            bussinessAddresses.push(selectedAddress());
                            shippingAddresses.push(selectedAddress());
                            allCompanyAddressesList.push(selectedAddress());

                        } else {
                            //pushing item in editted Addresses List
                            if (selectedAddress().addressId() > 0) {
                                var match = ko.utils.arrayFirst(edittedAddresses(), function (item) {
                                    return (selectedAddress().addressId() === item.addressId());
                                });
                                if (match === undefined || match == null) {
                                    edittedAddresses.push(selectedAddress());
                                }
                            }
                        }
                    }
                    //#endregion

                    view.hideAddressDialog();
                }
            },
            // ReSharper disable UnusedLocals
            getTerritoryByTerritoryId = function (territoryId) {
                // ReSharper restore UnusedLocals

                var result = _.find(addressCompanyTerritoriesFilter(), function (territory) {
                    return territory.territoryId() == parseInt(territoryId);
                });
                return result;
            },
            // #endregion

            //#region _________Secondry Page ________________________________
            selectedSecondaryPage = ko.observable(),
            selectedSecondaryPageForListView = ko.observable(),
            selectedPageCategory = ko.observable(),
            newAddedSecondaryPage = ko.observableArray([]),
            editedSecondaryPage = ko.observableArray([]),
            deletedSecondaryPage = ko.observableArray([]),
            nextSecondaryPageIdCounter = ko.observable(0),
            //Add New Secondary PAge
            onAddSecondaryPage = function () {
                ckEditorOpenFrom("SecondaryPage");
                var cmsPage = model.CMSPage();
                cmsPage.isUserDefined(true);
                cmsPage.companyId(selectedStore().companyId());
                selectedSecondaryPage(cmsPage);
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
            },
            //Get Secondory Pages
            getSecondoryPages = function () {
                dataservice.getSecondaryPages({
                    CompanyId: selectedStore().companyId(),
                    IsUserDefined: true,
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
                        secondaryPagePager().totalCount(data.RowCount);
                    },
                    error: function (response) {
                        toastr.error("Failed To Load Secondary Pages" + response, "", ist.toastrOptions);
                    }
                });
            },
            //Get System Pages
            getSystemPages = function () {
                dataservice.getSecondaryPages({
                    CompanyId: selectedStore().companyId(),
                    IsUserDefined: false,
                    PageSize: systemPagePager().pageSize(),
                    PageNo: systemPagePager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc()
                }, {
                    success: function (data) {
                        selectedStore().systemPages.removeAll();
                        _.each(data.CmsPages, function (cmsPage) {
                            selectedStore().systemPages.push(model.SecondaryPageListView.Create(cmsPage));
                        });
                    },
                    error: function (response) {
                        toastr.error("Failed To Load System Pages" + response, "", ist.toastrOptions);
                    }
                });
            },
            //Edit Secondary Page
            onEditSecondaryPage = function (secondaryPage) {
                selectedSecondaryPageForListView(secondaryPage);
                ckEditorOpenFrom("SecondaryPage");
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
                            toastr.error("Failed to load Secondary Page Detail . Error: " + response, "", ist.toastrOptions);
                        }
                    });
                }


            },
            //Delete Secondary Page
            onDeleteSecondaryPage = function (secondaryPage) {
                if (secondaryPage.isUserDefined() != true) {
                    toastr.error("System Page can not be deleted!", "", ist.toastrOptions);
                    return;
                }

                // Ask for confirmation
                confirmation.afterProceed(function () {
                    deleteSecondaryPage(secondaryPage);
                });
                confirmation.show();
            },
            deleteSecondaryPage = function (secondaryPage) {
                view.hideSecondoryPageDialog();
                dataservice.deleteSecondaryPage(secondaryPage.convertToServerData(secondaryPage), {
                    success: function () {
                        selectedStore().secondaryPages.remove(secondaryPage);
                        view.hideSecondoryPageDialog();
                        toastr.success("Successfully removed.");
                    },
                    error: function () {
                        toastr.error("Failed to remove.");
                    }
                });
            },
            //Add Default PAge Keywords
            addDefaultPageKeyWords = function () {
                loadDefaultPageKeywords();

            },
            //get CMS Tags For Load default for CMS Page
            loadDefaultPageKeywords = function () {
                dataservice.getCmsTags({
                    success: function (data) {
                        if (data != null) {
                            selectedSecondaryPage().pageKeywords(data);
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load defaults.", "", ist.toastrOptions);
                    }
                });
            },
            //Save Secondary Page
            onSaveSecondaryPage = function (sPage) {
                if (doBeforeSaveSecondaryPage()) {
                    var pageHtml = CKEDITOR.instances.content.getData();
                    sPage.pageHTML(pageHtml);
                    sPage.companyId(selectedStore().companyId());
                    saveSecondaryPage(sPage.convertToServerData(sPage));


                    ////Newly Added, Edit 
                    //if (sPage.id() < 0) {
                    //    _.each(newAddedSecondaryPage(), function (item) {
                    //        if (item.id() == sPage.id()) {
                    //            editedSecondaryPage.remove(item);
                    //        }
                    //    });
                    //    _.each(selectedStore().secondaryPages(), function (item) {
                    //        if (item.pageId() == sPage.id()) {
                    //            secondaryPageCopierForListView(item, sPage);
                    //        }
                    //    });
                    //    editedSecondaryPage.push(sPage);
                    //}
                    //    //Old Secondary Page Edited that is saved in db already
                    //else if (sPage.id() > 0) {
                    //    _.each(editedSecondaryPage(), function (item) {
                    //        if (item.id() == sPage.id()) {
                    //            editedSecondaryPage.remove(item);
                    //        }
                    //    });
                    //    _.each(selectedStore().secondaryPages(), function (item) {
                    //        if (item.pageId() == sPage.id()) {
                    //            secondaryPageCopierForListView(item, sPage);
                    //        }
                    //    });
                    //    editedSecondaryPage.push(sPage);
                    //}
                    //    //New Secondary PAge Added
                    //else if (sPage.id() === undefined) {
                    //    var nextId = nextSecondaryPageIdCounter() - 1;
                    //    sPage.id(nextId);
                    //    newAddedSecondaryPage.push(sPage);
                    //    var newPage = model.SecondaryPageListView();
                    //    newPage.pageId(sPage.id());
                    //    secondaryPageCopierForListView(newPage, sPage);
                    //    selectedStore().secondaryPages.splice(0, 0, newPage);
                    //    nextSecondaryPageIdCounter(nextId);
                    //}
                    ////Hide Dialog


                }
            },
            saveSecondaryPage = function (secondaryPage) {
                dataservice.saveSecondaryPage(secondaryPage, {
                    success: function (data) {
                        var newPage = model.SecondaryPageListView();
                        if (selectedSecondaryPage().id() === undefined) {
                            selectedSecondaryPage().id(data);
                            newPage.pageId(data);
                            secondaryPageCopierForListView(newPage, selectedSecondaryPage());
                            selectedStore().secondaryPages.splice(0, 0, newPage);

                        } else {
                            secondaryPageCopierForListView(selectedSecondaryPageForListView(), selectedSecondaryPage());
                        }
                        view.hideSecondoryPageDialog();
                        toastr.success("Successfully saved.");
                    },
                    error: function (exceptionMessage, exceptionType) {
                        if (exceptionType === ist.exceptionType.MPCGeneralException) {
                            toastr.error(exceptionMessage, "", ist.toastrOptions);
                        } else {
                            toastr.error("Failed to saved.", "", ist.toastrOptions);
                        }
                    }
                });
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
                target.isEnabled(source.isEnabled());
                target.isUserDefined(source.isUserDefined());
                target.isDisplay(false);
                target.imageSource(source.imageSrc());
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
            contactCompanyTerritoryFilter = ko.observable(undefined),
            //Deleted Company Contact 
            deletedCompanyContacts = ko.observableArray([]),
            edittedCompanyContacts = ko.observableArray([]),
            newCompanyContacts = ko.observableArray([]),
            //Company Contact  Pager
            //contactCompanyPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, ko.observableArray([]), null)),
            //Company Contact Search Filter
            searchCompanyContactFilter = ko.observable(),
            //Search Company Contact        
            searchCompanyContact = function () {
                if (isUserAndAddressesTabOpened() && selectedStore().companyId() != undefined && isEditorVisible()) {
                    dataservice.searchCompanyContact({
                        SearchFilter: searchCompanyContactFilter(),
                        CompanyId: selectedStore().companyId(),
                        TerritoryId: contactCompanyTerritoryFilter(),
                        PageSize: contactCompanyPager().pageSize(),
                        PageNo: contactCompanyPager().currentPage(),
                        SortBy: sortOn(),
                        IsAsc: sortIsAsc()
                    }, {
                        success: function (data) {
                            var isStoreDirty = selectedStore().hasChanges();
                            selectedStore().users.removeAll();
                            _.each(data.CompanyContacts, function (companyContactItem) {
                                var companyContact = new model.CompanyContact.Create(companyContactItem);
                                selectedStore().users.push(companyContact);
                            });

                            contactCompanyPager().totalCount(data.RowCount);
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

                            //check on client side, push all if new added work
                            if (searchCompanyContactFilter() == "" || searchCompanyContactFilter() == undefined) {
                                if (contactCompanyTerritoryFilter() != undefined) {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                            selectedStore().users.push(companyContactItem);
                                        }
                                    });
                                } else {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        selectedStore().users.push(companyContactItem);
                                    });
                                }
                            }
                            //check on client side, if filter is not null
                            if (searchCompanyContactFilter() != "" && searchCompanyContactFilter() != undefined) {
                                if (contactCompanyTerritoryFilter() == undefined) {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                            selectedStore().users.push(companyContactItem);
                                        }
                                    });
                                } else {
                                    _.each(newCompanyContacts(), function (companyContactItem) {
                                        if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                            if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                                selectedStore().users.push(companyContactItem);
                                            }
                                        }
                                    });
                                }

                            }
                            if (!isStoreDirty) {
                                selectedStore().reset();
                            }

                        },
                        error: function (response) {
                            toastr.error("Failed To Load Users" + response, "", ist.toastrOptions);
                        }
                    });
                }
                else if (isUserAndAddressesTabOpened() && selectedStore().companyId() == undefined && isEditorVisible()) {
                    selectedStore().users.removeAll();
                    //check on client side, push all if new added work
                    if (searchCompanyContactFilter() == "" || searchCompanyContactFilter() == undefined) {
                        if (contactCompanyTerritoryFilter() != undefined) {
                            _.each(newCompanyContacts(), function (companyContactItem) {
                                if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                    selectedStore().users.push(companyContactItem);
                                }
                            });
                        } else {
                            _.each(newCompanyContacts(), function (companyContactItem) {
                                selectedStore().users.push(companyContactItem);
                            });
                        }
                    }
                    //check on client side, if filter is not null
                    if (searchCompanyContactFilter() != "" && searchCompanyContactFilter() != undefined) {
                        if (contactCompanyTerritoryFilter() == undefined) {
                            _.each(newCompanyContacts(), function (companyContactItem) {
                                if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                    selectedStore().users.push(companyContactItem);
                                }
                            });
                        } else {
                            _.each(newCompanyContacts(), function (companyContactItem) {
                                if (companyContactItem.email().indexOf(searchCompanyContactFilter()) != -1 || companyContactItem.firstName().indexOf(searchCompanyContactFilter()) != -1) {
                                    if (companyContactItem.territoryId() == contactCompanyTerritoryFilter()) {
                                        selectedStore().users.push(companyContactItem);
                                    }
                                }
                            });
                        }

                    }
                }

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
                selectedShippingAddressId(undefined);
                selectedCompanyContactEmail(undefined);
                isSavingNewCompanyContact(true);
                selectedCompanyContact(user);
                selectedCompanyContact().isWebAccess(true);
                selectedCompanyContact().isPlaceOrder(true);
                selectedCompanyContact().contactId(newSavingCompanyContactIdCount);
                addCompanyContactId();
                if (selectedStore().type() == 4) {
                    if (newAddresses != undefined && newAddresses().length == 0) {
                        if (newCompanyTerritories.length > 0) {
                            selectedCompanyContact().territoryId(newCompanyTerritories()[0].territoryId());
                        }
                    }
                    if (selectedStore().companyId() > 0) {
                        if (selectedStore().companyTerritories().length > 0) {
                            selectedCompanyContact().territoryId(selectedStore().companyTerritories()[0].territoryId());
                        }
                    }
                    //Updating role of user as "user", if is retail store
                    _.each(roles(), function (role) {
                        if (role.roleName().toLowerCase() == "user") {
                            selectedCompanyContact().contactRoleId(role.roleId());
                        }
                    });
                }
                if (isSavingNewCompanyContact != undefined && isSavingNewCompanyContact() && selectedStore().companyId() == undefined) {
                    _.each(newCompanyTerritories(), function (territory) {
                        if (territory.isDefault()) {
                            selectedCompanyContact().territoryId(territory.territoryId());
                        }
                    });
                    _.each(newAddresses(), function (address) {
                        //Billing Address Id Selection
                        if (address.isDefaultTerrorityBilling() && address.territoryId() === selectedCompanyContact().territoryId()) {
                            selectedBussinessAddressId(address.addressId());
                            selectedCompanyContact().bussinessAddressId(address.addressId());
                        }
                        //Shipping Address Id Selection
                        if (address.isDefaultTerrorityShipping() && address.territoryId() === selectedCompanyContact().territoryId()) {
                            selectedShippingAddressId(address.addressId());
                            selectedCompanyContact().shippingAddressId(address.addressId());
                        }
                    });
                    //select isDefaultContact for the very first contact by defaault
                    if (newCompanyContacts().length == 0) {
                        selectedCompanyContact().isDefaultContact(true);
                    }
                    _.each(fieldVariablesOfContactType(), function (item) {
                        selectedCompanyContact().companyContactVariables.push(scopeVariableMapper(item));
                    });

                }
                //for the first time of contact creation make default shipping address and default billing address, as the selected shipping and billing respectively.
                if (selectedStore().companyId() !== undefined && (selectedCompanyContact().contactId() === undefined || selectedCompanyContact().contactId() < 0)) {
                    var scope = 2;
                    getCompanyContactVariable(scope);
                }
                view.showCompanyContactDialog();
            },

            scopeVariableMapper = function (item) {
                var scopeVariable = model.ScopeVariable();
                scopeVariable.id(item.id());
                scopeVariable.contactId(item.contactId());
                scopeVariable.variableId(item.variableId());
                scopeVariable.value(item.value());
                scopeVariable.fakeId(item.fakeId());
                scopeVariable.title(item.title());
                scopeVariable.type(item.type());
                scopeVariable.waterMark(item.waterMark());
                scopeVariable.scope(item.scope());
                scopeVariable.optionId(item.optionId());
                ko.utils.arrayPushAll(scopeVariable.variableOptions, item.variableOptions());
                scopeVariable.variableOptions.valueHasMutated();
                return scopeVariable;
            },
            // Delete CompanyContact
            onDeleteCompanyContact = function (companyContact) {
                if (companyContact.isDefaultContact()) {
                    toastr.error("Default Contact Cannot be deleted", "", ist.toastrOptions);
                    return;
                }
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    //#region Db Saved Record Id > 0
                    if (companyContact.contactId() > 0) {
                        if (companyContact.companyId() > 0 && companyContact.contactId() > 0) {
                            dataservice.deleteCompanyContact({
                                CompanyContactId: companyContact.contactId()
                            }, {
                                success: function (data) {
                                    if (data) {
                                        var storeGotChanges = selectedStore().hasChanges();
                                        selectedStore().users.remove(companyContact);
                                        if (!storeGotChanges) {
                                            selectedStore().reset();
                                        }
                                        toastr.success("Deleted Successfully");
                                        isLoadingStores(false);
                                    } else {
                                        toastr.error("Contact can not be deleted", "", ist.toastrOptions);
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Delete Company Contact " + response, "", ist.toastrOptions);
                                }
                            });
                        }
                    }
                        //#endregion
                    else {
                        if (companyContact.contactId() < 0 || companyContact.contactId() == undefined) {

                            _.each(newCompanyContacts(), function (item) {
                                if (item.contactId() == companyContact.contactId()) {
                                    newCompanyContacts.remove(companyContact);
                                }
                            });
                            selectedStore().users.remove(companyContact);
                        }
                    }
                    view.hideCompanyContactDialog();

                });
                confirmation.show();
                return;
            },
            selectedCompanyContactEmail = ko.observable(),
            onEditCompanyContact = function (companyContact) {
                //selectedCompanyContact(companyContact);
                //selectedCompanyContactEmail(companyContact.email());
                isSavingNewCompanyContact(false);

                companyContactEditorViewModel.selectItem(companyContact);
                selectedCompanyContact().reset();

                view.showCompanyContactDialog();
                if (selectedCompanyContact().contactId() !== undefined && selectedStore().companyId() !== undefined) {
                    var scope = 2;
                    getCompanyContactVariableForEditContact(selectedCompanyContact().contactId(), scope);
                }
            },
            closeCompanyContact = function () {
                selectedBussinessAddressId(undefined);
                view.hideCompanyContactDialog();
                isSavingNewCompanyContact(false);
            },
            onCloseCompanyContact = function () {
                selectedBussinessAddressId(undefined);
                view.hideCompanyContactDialog();
                companyContactEditorViewModel.revertItem();
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
            newSavingCompanyContactIdCount = -1,
            //Add Company Contact Id
            addCompanyContactId = function () {
                newSavingCompanyContactIdCount = newSavingCompanyContactIdCount - 1;
            },
            onSaveCompanyContact = function () {
                if (doBeforeSaveCompanyContact()) {
                    //#region Editting Company Case companyid > 0
                    if (selectedStore().companyId() > 0) {
                        var storeGotChanges = selectedStore().hasChanges();
                        selectedCompanyContact().companyId(selectedStore().companyId());
                        var companyContact = selectedCompanyContact().convertToServerData();
                        _.each(selectedCompanyContact().companyContactVariables(), function (contactVariable) {
                            companyContact.ScopVariables.push(contactVariable.convertToServerData(contactVariable));
                        });
                        dataservice.saveCompanyContact(
                            companyContact,
                            {
                                success: function (data) {
                                    if (data) {
                                        var savedCompanyContact = model.CompanyContact.Create(data);
                                        //updating selected contact rolename
                                        _.each(roles(), function (role) {
                                            if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                                savedCompanyContact.roleName(role.roleName());
                                                data.RoleName = role.roleName();
                                            }
                                        });
                                        if (selectedCompanyContact().isDefaultContact()) {
                                            _.each(selectedStore().users(), function (user) {
                                                if (user.isDefaultContact()) {
                                                    if (selectedCompanyContact().contactId() != user.contactId()) {
                                                        user.isDefaultContact(false);
                                                    }
                                                }
                                            });
                                        }
                                        if (selectedCompanyContact().contactId() <= 0 || selectedCompanyContact().contactId() == undefined) {
                                            selectedStore().users.splice(0, 0, savedCompanyContact);
                                        }
                                        else {
                                            selectedCompanyContact(savedCompanyContact);
                                            var count = 0;
                                            _.each(selectedStore().users(), function (user) {
                                                if (user.contactId() == savedCompanyContact.contactId()) {
                                                    var totalCount = contactCompanyPager().totalCount();
                                                    selectedStore().users.remove(user);
                                                    selectedStore().users.splice(count, 0, savedCompanyContact);
                                                    contactCompanyPager().totalCount(totalCount);
                                                }
                                                count = count + 1;
                                            });
                                        }
                                        if (!storeGotChanges) {
                                            selectedStore().reset();
                                        }
                                        toastr.success("Saved Successfully");
                                        closeCompanyContact();
                                    }
                                },
                                error: function (response) {
                                    isLoadingStores(false);
                                    toastr.error("Error: Failed To Save Contact " + response, "", ist.toastrOptions);
                                    if (response == "Duplicate Email/Username are not allowed") {
                                        selectedCompanyContact().email(selectedCompanyContactEmail());
                                    } else {
                                        onCloseCompanyContact();
                                    }
                                }
                            });
                    }
                        //#endregion
                        //#region New Company Case 
                    else {
                        if (selectedCompanyContact().contactId() < 0 && isSavingNewCompanyContact() === true) {
                            if (!findEmailDuplicatesInCompanyContacts()) {
                                if (selectedCompanyContact().isDefaultContact()) {
                                    _.each(selectedStore().users(), function (user) {
                                        if (user.isDefaultContact()) {
                                            user.isDefaultContact(false);
                                        }
                                    });
                                }
                                selectedStore().users.splice(0, 0, selectedCompanyContact());
                                //Editorial view model
                                if (selectedCompanyContact().bussinessAddressId() != undefined) {
                                    selectedCompanyContact().bussinessAddress(getAddressByAddressId(selectedCompanyContact().bussinessAddressId()));
                                }
                                if (selectedCompanyContact().shippingAddressId() != undefined) {
                                    selectedCompanyContact().shippingAddress(getAddressByAddressId(selectedCompanyContact().shippingAddressId()));
                                }
                                //updating selected contact rolename
                                _.each(roles(), function (role) {
                                    if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                        selectedCompanyContact().roleName(role.roleName());
                                    }
                                });
                                newCompanyContacts.push(selectedCompanyContact());
                                addCompanyContactId();
                                closeCompanyContact();
                            }

                        } else {
                            if (!findEmailDuplicatesInCompanyContacts()) {
                                //pushing item in editted CompanyContacts List
                                if (selectedCompanyContact().contactId() != undefined) {
                                    var match = ko.utils.arrayFirst(edittedCompanyContacts(), function (item) {
                                        return (selectedCompanyContact().contactId() === item.contactId());
                                    });

                                    if (match === undefined || match === null) {
                                        edittedCompanyContacts.push(selectedCompanyContact());
                                    }
                                }
                                //updating selected contact rolename
                                _.each(roles(), function (role) {
                                    if (role.roleId() == selectedCompanyContact().contactRoleId()) {
                                        selectedCompanyContact().roleName(role.roleName());
                                    }
                                });
                                onCloseCompanyContact();
                            }
                        }
                    }
                    //#endregion
                }
            },
            //Method to find email duplicates, returns True is match found else return false
            findEmailDuplicatesInCompanyContacts = function () {
                var flag = false;
                _.each(newCompanyContacts(), function (companyContact) {
                    if (companyContact.email() == selectedCompanyContact().email() && companyContact.contactId() != selectedCompanyContact().contactId()) {
                        selectedCompanyContact().email(undefined);
                        toastr.error('Duplicate Email/Username are not allowed', "", ist.toastrOptions);
                        flag = true;
                    }
                });
                return flag;
            },
            UserProfileImageFileLoadedCallback = function (file, data) {
                selectedCompanyContact().image(data);
                selectedCompanyContact().fileName(file.name);
            },
            selectedCsvFileForCompanyContact = function (file, data) {
                dataservice.importCompanyContact({
                    FileName: file.name,
                    FileBytes: data,
                    CompanyId: selectedStore().companyId()
                }, {
                    success: function (successData) {
                        toastr.success("Company Contacts imported successfully!");
                        searchCompanyContact();
                    },
                    error: function (response) {
                        toastr.error("Company Contacts failed to import! " + response);
                    }
                });
            },

            getAddressByAddressId = function (addressId) {
                var result = _.find(allCompanyAddressesList(), function (address) {
                    return address.addressId() == parseInt(addressId);
                });
                return result;
            },
            //search Company Contact By Filter
            searchCompanyContactByFilter = function () {
                contactCompanyPager().reset();
                searchCompanyContact();
            },
            // #endregion

            // #region _________P A Y M E N T    G A T E W A Y _________________
            newPaymentGatewayId = -1,
            addNewPaymentGatewayId = function () {
                newPaymentGatewayId = newPaymentGatewayId - 1;
            },
            isAccessCodeSectionVisible = ko.observable(false),
            paymentMethodName = ko.observable(),
            //Selected Payment Gateway
            paymentGatewayEditorViewModel = new ist.ViewModel(model.PaymentGateway),
            selectedPaymentGateway = paymentGatewayEditorViewModel.itemForEditing,

            // Template Chooser For Payment Gateway
            templateToUsePaymentGateways = function (paymentGateway) {
                return (paymentGateway === selectedPaymentGateway() ? 'editPaymentGatewayTemplate' : 'itemPaymentGatewayTemplate');
            },
            //Payment Gateway filer string FYP
            paymentGatewayFilter = ko.observable(),
            // Search function for Payment Method
            onSearchpaymentMethod = function () {
                getPaymentGateway();
            },
            //Get Payment Method
            getPaymentGateway = function () {
                dataservice.getPaymentGateways({
                    SearchFilter: paymentGatewayFilter(),
                    CompanyId: selectedStore().companyId()
                }, {
                    success: function (data) {
                        selectedStore().paymentGateway.removeAll();
                        if (data != null) {
                            _.each(data.PaymentGateways, function (item) {
                                var module = model.PaymentGateway.Create(item);
                                selectedStore().paymentGateway.push(module);
                            });
                        }
                    },
                    error: function (response) {
                        toastr.error("Error: Failed To load Payment Gateways " + response, "", ist.toastrOptions);
                    }
                });
            },

            //Create Payment Gateway
            onCreateNewPaymentGateway = function () {
                var paymentGateway = new model.PaymentGateway();
                paymentGateway.isActive(false);
                selectedPaymentGateway(paymentGateway);
                view.showPaymentGatewayDialog();
            },
            // Delete a Payment Gateway
            onDeletePaymentGateway = function (paymentGateway) {
                // Ask for confirmation
                confirmation.afterProceed(function () {
                    selectedStore().paymentGateway.remove(paymentGateway);
                    view.hidePaymentGatewayDialog();
                });
                confirmation.show();
                return;
            },
            onEditPaymentGateway = function (paymentGateway) {
                paymentGatewayEditorViewModel.selectItem(paymentGateway);
                selectedPaymentGateway().reset();
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
                    if (selectedPaymentGateway().paymentGatewayId() == undefined) {// || selectedPaymentGateway().paymentGatewayId() <= 0
                        selectedPaymentGateway().paymentGatewayId(newPaymentGatewayId);
                        addNewPaymentGatewayId();
                    }
                    var notFound = true;
                    var count = 0;
                    _.each(selectedStore().paymentGateway(), function (item) {
                        if (selectedPaymentGateway().isActive() && item.paymentGatewayId() != selectedPaymentGateway().paymentGatewayId()) {
                            item.isActive(false);
                        }
                        if (notFound && item.paymentGatewayId() == selectedPaymentGateway().paymentGatewayId()) {
                            selectedStore().paymentGateway.remove(item);
                            selectedStore().paymentGateway.splice(count, 0, selectedPaymentGateway());
                            notFound = false;
                        }
                        count = count + 1;
                    });
                    if (notFound) {
                        selectedStore().paymentGateway.splice(0, 0, selectedPaymentGateway());
                    }
                    
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
            // Ttile while add/edit category
            productCategoryTitle = ko.computed(function () {
                if (selectedProductCategoryForEditting() != undefined) {
                    var val = selectedProductCategoryForEditting().categoryName() != '' && selectedProductCategoryForEditting().categoryName() != undefined ? selectedProductCategoryForEditting().categoryName() : '';
                    return productCategoryStatus();
                }
            }),
            productCategoryStatus = ko.observable(''),
            //Deleted Product Categories List
            deletedProductCategories = ko.observableArray([]),
            //Editted Product Categories List
            edittedProductCategories = ko.observableArray([]),
            //New Added Product Categories List
            newProductCategories = ko.observableArray([]),
            //Select Product Category
            selectProductCategory = function (category, event) {
                if (selectedProductCategory() != category) {
                    selectedProductCategory(category);
                    selectedCategoryName(category.categoryName());
                    // Notify the event subscribers
                    view.productCategorySelectedEvent(ko.isObservable(category.productCategoryId) ?
                        category.productCategoryId() : category.productCategoryId);
                }
            },
            //Select Child Product Category
            selectChildProductCategory = function (categoryId, event) {
                selectedProductCategory(undefined);

                var id = $(event.target).closest('li')[0].id;
                selectedCategoryName(event.target.innerText);
                if (id) {
                    // Notify the event subscribers
                    view.productCategorySelectedEvent(id);
                }
                event.stopImmediatePropagation();
            },
            changeIcon = function (event) {
                if (event.target.classList.contains("fa-chevron-circle-right")) {
                    event.target.classList.remove("fa-chevron-circle-right");
                    event.target.classList.add("fa-chevron-circle-down");
                } else {
                    event.target.classList.remove("fa-chevron-circle-down");
                    event.target.classList.add("fa-chevron-circle-right");
                }
            },
            //Get Category Child List Items
            getCategoryChildListItems = function (dataRecieved, event) {
                changeIcon(event);
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
                    id: id
                }, {
                    success: function (data) {
                        if (data.ProductCategories != null) {
                            _.each(data.ProductCategories, function (productCategory) {
                                $("#" + id).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectChildProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + productCategory.ProductCategoryId + '> <div class="dd-handle-list cursorShape" ><i class="fa fa-chevron-circle-right " data-bind="click: $root.getCategoryChildListItems"></i></div><div class="dd-handle col-sm-12"><span class="col-sm-10 cursorShape">' + productCategory.CategoryName + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link cursorShape" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>');
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
                        $("#categoryTabItems li a").first().trigger("click");
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Error: Failed To load Categories " + response, "", ist.toastrOptions);
                    }
                }); 1
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
                //$('.nav-tabs li:first-child a').tab('show');
                selectedProductCategory(undefined);
                var productCategory = new model.ProductCategory();
                //Set Product category value for by default
                productCategory.isShelfProductCategory(false);
                productCategory.isEnabled(true);
                productCategory.isPublished(true);
                //Setting Product Category Editting
                selectedProductCategoryForEditting(productCategory);
                productCategoryStatus("Add/Edit Category Details");

                //Setting drop down list of parent
                //putting all list of categories
                populatedParentCategoriesList.removeAll();
                _.each(parentCategories(), function (category) {
                    populatedParentCategoriesList.splice(0, 0, category);
                });
                isSavingNewProductCategory(true);
                view.showStoreProductCategoryDialog();
                // $('#productCatFirstTab').addClass('active');
                //$('.nav-tabs li:eq(0) a').tab('show');
                //$('a[href=#productCatFirstTab]').click();
                //Resetting all list of territories (making isSelected fields False)
                _.each(addressTerritoryList(), function (territory) {
                    if (territory.isSelected()) {
                        territory.isSelected(false);
                    }
                });
                $("#categoryTabItems li a").first().trigger("click");
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
                                //Update Product category Territories
                                UpdateProductCategoryTerritories(data.CategoryTerritories);
                                selectedProductCategoryForEditting().parentCategoryId(data.ParentCategoryId);
                                selectedProductCategoryForEditting().reset();
                                view.showStoreProductCategoryDialog();
                            }
                            isLoadingStores(false);
                            $("#categoryTabItems li a").first().trigger("click");
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Error: Failed To load Category " + response, "", ist.toastrOptions);
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
                                productCategoryStatus("Modify Category Details");
                                isSavingNewProductCategory(false);
                                //Update Product category Territories
                                UpdateProductCategoryTerritories(data.CategoryTerritories);
                                selectedProductCategoryForEditting().reset();
                                view.showStoreProductCategoryDialog();
                            }
                            isLoadingStores(false);
                            $("#categoryTabItems li a").first().trigger("click");
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Error: Failed To load Category " + response, "", ist.toastrOptions);
                        }
                    });
                    //selectedProductCategory(productCategory);
                } else {
                    editNewAddedProductCategory();
                }

            },
            //Update Product Category Territories
            UpdateProductCategoryTerritories = function (categoryTerritories) {
                //#region Update Category Territories
                //Resetting all list of territories (making isSelected fields False)
                _.each(addressTerritoryList(), function (territory) {
                    if (territory.isSelected()) {
                        territory.isSelected(false);
                    }
                });
                //Setting IsSelected Fields of territories
                _.each(categoryTerritories, function (categoryTerritory) {
                    _.each(addressTerritoryList(), function (territory) {
                        if (territory.territoryId() == categoryTerritory.TerritoryId) {
                            territory.isSelected(true);
                        }
                    });
                });
                //#endregion
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
            onArchiveCategory = function () {
                confirmation.messageText("Do you want to delete category?");
                confirmation.afterProceed(deleteCategory);
                confirmation.afterCancel(function () {
                });
                confirmation.show();
                return;
            },
            deleteCategory = function () {
                dataservice.deleteProductCategoryById({
                    ProductCategoryId: selectedProductCategoryForEditting().productCategoryId()
                }, {
                    success: function (data) {
                        if (data != null) {
                            var obj = null;
                            _.each(parentCategories(), function (item) {
                                if (item.productCategoryId === selectedProductCategoryForEditting().productCategoryId()) {
                                    obj = item;
                                }
                            });
                            parentCategories.remove(obj);
                            var val = '#' + selectedProductCategoryForEditting().productCategoryId();
                            $(val).remove();
                            onCloseProductCategory();
                            toastr.success("Category deleted successfully!");
                        }
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Error: Failed To delete Category!");
                    }
                });
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
                }
            },
            //Computed To set Product Category dirty Flag 
            setProductCategoryDirtyFlag = ko.computed(function () {
                if (addressTerritoryList().length > 0) {
                    _.filter(addressTerritoryList(), function (territory) {
                        return territory.isSelected() == true;
                    });
                    if (selectedProductCategoryForEditting() != undefined) {
                        if (selectedProductCategoryForEditting().isCategoryTerritoriesListChanged()) {
                            selectedProductCategoryForEditting().isCategoryTerritoriesListChanged(false);
                        } else {
                            selectedProductCategoryForEditting().isCategoryTerritoriesListChanged(true);
                        }
                    }
                }
            }),
            //On Save Product Category
            onSaveProductCategory = function () {

                if (doBeforeSaveProductCategory()) {
                    selectedProductCategoryForEditting().companyId(selectedStore().companyId());

                    //#region Category Territories
                    var productCategoryToSave = selectedProductCategoryForEditting().convertToServerData();
                    productCategoryToSave.CategoryTerritories = [];
                    _.each(addressTerritoryList(), function (territory) {
                        if (territory.isSelected()) {
                            productCategoryToSave.CategoryTerritories.push(territory.convertToServerData());
                        }
                    });
                    //#endregion
                    dataservice.saveProductCategory(
                            productCategoryToSave
                        , {
                            success: function (data) {

                                //#region If Parent Category Id == Null
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
                                        selectedStore().productCategories.splice(0, 0, model.ProductCategory.Create(data));
                                    }
                                        //Creating new Product category
                                    else {
                                        selectedStore().productCategories.splice(0, 0, model.ProductCategory.Create(data));
                                        toastr.success("Category Added Successfully");
                                    }
                                    //$("#nestable2").append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + data.ProductCategoryId + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    //ko.applyBindings(view.viewModel, $("#" + data.ProductCategoryId)[0]);
                                    isLoadingStores(false);
                                    view.hideStoreProductCategoryDialog();
                                }
                                    //#endregion

                                    //#region Else Parent Category Id != null
                                else {
                                    newProductCategories.push(model.ProductCategory.Create(data));
                                    selectedProductCategoryForEditting(model.ProductCategory.Create(data));

                                    if ($("#" + selectedProductCategoryForEditting().productCategoryId()).length > 0) {
                                        $("#" + selectedProductCategoryForEditting().productCategoryId()).remove();
                                    }
                                    //$("#" + selectedProductCategoryForEditting().parentCategoryId()).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems" ><i class="fa fa-bars"></i></div><div class="dd-handle"><span >' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    $("#" + selectedProductCategoryForEditting().parentCategoryId()).append('<ol class="dd-list"> <li class="dd-item dd-item-list" data-bind="click: $root.selectProductCategory, css: { selectedRow: $data === $root.selectedProductCategory}" id =' + selectedProductCategoryForEditting().productCategoryId() + '> <div class="dd-handle-list" data-bind="click: $root.getCategoryChildListItems"><i class="fa fa-bars"></i></div><div class="dd-handle col-sm-12"><span class="col-sm-10">' + selectedProductCategoryForEditting().categoryName() + '</span><div class="nested-links col-sm-2"><a data-bind="click: $root.onEditChildProductCategory" class="nested-link" title="Edit Category"><i class="fa fa-pencil"></i></a></div></div></li></ol>'); //data-bind="click: $root.getCategoryChildListItems"
                                    //if (!flagAlreadyExist) {
                                    ko.applyBindings(view.viewModel, $("#" + selectedProductCategoryForEditting().productCategoryId())[0]);
                                    //}
                                    toastr.success("Category Updated Successfully");
                                }
                                //#endregion

                                //var category = {
                                //    productCategoryId: data.ProductCategoryId,
                                //    categoryName: data.CategoryName,
                                //    parentCategoryId: data.ParentCategoryId
                                //};
                                //parentCategories.push(category);

                                isLoadingStores(false);
                                view.hideStoreProductCategoryDialog();
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To Save Category " + response, "", ist.toastrOptions);
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
                        } else if (selectedProductCategoryForEditting().productCategoryId() != undefined && selectedProductCategoryForEditting().productCategoryId() < 0) {
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
                errorList.removeAll();
                if (!selectedStore().isValid()) {
                    selectedStore().errors.showAllMessages();
                    setValidationSummary(selectedStore());
                    flag = false;
                }
                //1- New saving company should have 1 address and 1 user
                //2- if company is editting then company should have a 1 address and 1 user in database after saving
                //1
                if (!(newAddresses().length - deletedAddresses().length) > 1 || (selectedStore().addresses().length == 0 && newAddresses().length == 0 && deletedAddresses().length == 0)) {
                    errorList.push({ name: "At least one address required.", element: searchAddressFilter.domElement });
                    flag = false;
                }
                if (!(newCompanyContacts().length - deletedCompanyContacts().length) > 1 || (selectedStore().users().length == 0 && newCompanyContacts().length == 0 && deletedCompanyContacts().length == 0)) {
                    errorList.push({ name: "At least one user required.", element: searchCompanyContactFilter.domElement });
                    flag = false;
                }

                //if (productPriorityRadioOption() === "1" && selectedItemsForOfferList().length === 0) {
                //    errorList.push({ name: "At least One Product to Prioritize..", element: productError.domElement });
                //    flag = false;
                //}
                if (selectedStore().companyId() == undefined) {
                    var haveIsDefaultTerritory = false;
                    var haveIsBillingDefaultAddress = false;
                    var haveIsShippingDefaultAddress = false;
                    var haveIsDefaultAddress = false;
                    var haveIsDefaultUser = false;
                    if (selectedStore().type() == 3) {
                        _.each(selectedStore().companyTerritories(), function (territory) {
                            if (territory.isDefault()) {
                                haveIsDefaultTerritory = true;
                            }
                        });
                    }
                    if (selectedStore().type() == 4) {
                        haveIsDefaultTerritory = true;
                    }
                    if (selectedStore().activeBannerSetId.error) {
                        errorList.push({ name: "At least one Banner Set required.", element: selectedStore().activeBannerSetId.domElement });
                        flag = false;
                    }
                    _.each(selectedStore().addresses(), function (address) {
                        if (address.isDefaultTerrorityBilling()) {
                            haveIsBillingDefaultAddress = true;
                        }
                        if (address.isDefaultTerrorityShipping()) {
                            haveIsShippingDefaultAddress = true;
                        }
                        if (address.isDefaultAddress()) {
                            haveIsDefaultAddress = true;
                        }
                    });
                    _.each(selectedStore().users(), function (user) {
                        if (user.isDefaultContact()) {
                            haveIsDefaultUser = true;
                        }
                    });
                    if (!haveIsDefaultTerritory) {
                        errorList.push({ name: "At least one default territory required.", element: searchCompanyTerritoryFilter.domElement });
                        flag = false;
                    }
                    if (!haveIsBillingDefaultAddress && selectedStore().type() != 4) {
                        errorList.push({ name: "At least one Territory Default Billing Address required.", element: searchAddressFilter.domElement });
                        flag = false;
                    }
                    if (!haveIsShippingDefaultAddress && selectedStore().type() != 4) {
                        errorList.push({ name: "At least one Territory Default Shipping Address required.", element: searchAddressFilter.domElement });
                        flag = false;
                    }
                    if (!haveIsDefaultAddress) {
                        errorList.push({ name: "At least one Company Default Address required.", element: searchAddressFilter.domElement });
                        flag = false;
                    }
                    if (!haveIsDefaultUser) {
                        errorList.push({ name: "At least one Default Company Contact required.", element: searchCompanyContactFilter.domElement });
                        flag = false;
                    }
                }
                return flag;
            },

            //Filter States based on Country
            filterStates = ko.computed(function () {
                if (selectedAddress() !== undefined && selectedAddress().country() !== undefined) {
                    filteredStates.removeAll();
                    _.each(states(), function (item) {
                        if (item.CountryId === selectedAddress().country()) {
                            filteredStates.push(item);
                        }
                    });
                }
            }, this),
            // Set Validation Summary
            setValidationSummary = function (selectedItem) {

                if (selectedItem.name.error) {
                    errorList.push({ name: selectedItem.name.domElement.name, element: selectedItem.name.domElement });
                }
                if (selectedItem.webAccessCode.error) {
                    errorList.push({ name: selectedItem.webAccessCode.domElement.name, element: selectedItem.webAccessCode.domElement });
                }
                if (selectedItem.activeBannerSetId.error) {
                    errorList.push({ name: selectedItem.activeBannerSetId.domElement.name, element: selectedItem.activeBannerSetId.domElement });
                }
                if (selectedItem.taxRate.error) {
                    errorList.push({ name: selectedItem.taxRate.domElement.name, element: selectedItem.taxRate.domElement });
                }
            },
            // Go To Element
            gotoElement = function (validation) {
                view.gotoElement(validation.element);
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
            saveStore = function (callback) {
                if (doBeforeSave()) {
                    var storeToSave = model.Store().convertToServerData(selectedStore());

                    //#region Field Variables
                    _.each(fieldVariablesOfStoreType(), function (scopeVariable) {
                        storeToSave.ScopeVariables.push(scopeVariable.convertToServerData(scopeVariable));
                    });

                    _.each(fieldVariables(), function (fieldVariable) {
                        var field = fieldVariable.convertToServerData(fieldVariable);
                        _.each(fieldVariable.variableOptions(), function (optionItem, index) {
                            optionItem.sortOrder(index + 1);
                            field.VariableOptions.push(optionItem.convertToServerData(optionItem));
                        });
                        storeToSave.FieldVariables.push(field);
                    });
                    //Smart Forms
                    if (selectedStore().companyId() === 0 || selectedStore().companyId() === undefined) {
                        _.each(smartForms(), function (item) {
                            var smartFormServer = item.convertToServerData(item);
                            _.each(item.smartFormDetails(), function (smartFormDetail) {
                                smartFormServer.SmartFormDetails.push(smartFormDetail.convertToServerData(smartFormDetail));
                            });
                            storeToSave.SmartForms.push(smartFormServer);
                        });
                    }


                    //endregion
                    //#region Company Territories
                    _.each(newCompanyTerritories(), function (territory, index) {
                        var territoryServerModel = territory.convertToServerData();
                        _.each(selectedStore().companyTerritories()[index].scopeVariables(), function (item) {
                            territoryServerModel.ScopeVariables.push(item.convertToServerData(item));
                        });
                        storeToSave.NewAddedCompanyTerritories.push(territoryServerModel);
                    });
                    _.each(edittedCompanyTerritories(), function (territory) {
                        storeToSave.EdittedCompanyTerritories.push(territory.convertToServerData());
                    });
                    _.each(deletedCompanyTerritories(), function (territory) {
                        storeToSave.DeletedCompanyTerritories.push(territory.convertToServerData());
                    });
                    //#endregion
                    //Secondary Pages
                    //_.each(newAddedSecondaryPage(), function (sPage) {
                    //    storeToSave.NewAddedCmsPages.push(sPage.convertToServerData(sPage));
                    //});
                    //_.each(editedSecondaryPage(), function (sPage) {
                    //    storeToSave.EditCmsPages.push(sPage.convertToServerData(sPage));
                    //});
                    //_.each(deletedSecondaryPage(), function (sPage) {
                    //    storeToSave.DeletedCmsPages.push(sPage.convertToServerData(sPage));
                    //});
                    //Page category
                    _.each(pageCategories(), function (pageCategory) {
                        storeToSave.PageCategories.push(pageCategory.convertToServerData(pageCategory));
                    });
                    //#region Emails (Campaigns)
                    _.each(newAddedCampaigns(), function (email) {
                        var emailServer = email.convertToServerData(email);
                        _.each(email.campaignImages(), function (campaignImage) {
                            emailServer.CampaignImages.push(campaignImage.convertToServerData(campaignImage));
                        });
                        storeToSave.NewAddedCampaigns.push(emailServer);
                    });
                    _.each(editedCampaigns(), function (email) {
                        var emailServer = email.convertToServerData(email);
                        _.each(email.campaignImages(), function (campaignImage) {
                            emailServer.CampaignImages.push(campaignImage.convertToServerData(campaignImage));
                        });
                        storeToSave.EdittedCampaigns.push(emailServer);
                    });
                    _.each(deletedCampaigns(), function (email) {
                        var emailServer = email.convertToServerData(email);
                        storeToSave.DeletedCampaigns.push(emailServer);
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
                    _.each(newAddresses(), function (address, index) {
                        var addressServerModel = address.convertToServerData();
                        _.each(selectedStore().addresses()[index].scopeVariables(), function (item) {
                            addressServerModel.ScopeVariables.push(item.convertToServerData(item));
                        });
                        storeToSave.NewAddedAddresses.push(addressServerModel);
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

                        var contact = companyContact.convertToServerData();
                        _.each(companyContact.companyContactVariables(), function (contactVariable) {
                            contact.ScopVariables.push(contactVariable.convertToServerData(contactVariable));
                        });
                        storeToSave.NewAddedCompanyContacts.push(contact);
                    });
                    _.each(edittedCompanyContacts(), function (companyContact) {
                        storeToSave.EdittedCompanyContacts.push(companyContact.convertToServerData());
                    });
                    _.each(deletedCompanyContacts(), function (companyContact) {
                        storeToSave.DeletedCompanyContacts.push(companyContact.convertToServerData());
                    });
                    //#endregion
                    //#region Products
                    //_.each(ist.storeProduct.viewModel.newAddedProducts(), function (product) {
                    //    if (product.id() < 0) {
                    //        product.id(undefined);
                    //    }
                    //    storeToSave.NewAddedProducts.push(product.convertToServerData());
                    //});
                    //_.each(ist.storeProduct.viewModel.edittedProducts(), function (product) {
                    //    storeToSave.EdittedProducts.push(product.convertToServerData());
                    //});
                    //_.each(ist.storeProduct.viewModel.deletedproducts(), function (product) {
                    //    storeToSave.Deletedproducts.push(product.convertToServerData());
                    //});

                    //#endregion

                    //#region Widget Section in Themes& Widget
                    _.each(selectedItemsForOfferList(), function (item, index) {
                        item.sortOrder(index + 1);
                        item.companyId(storeToSave.CompanyId);
                        storeToSave.CmsOffers.push(item.convertToServerData());
                    });

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
                                //#region Req: Not Close Store Edit Dialog On Saving

                                //isStoreEditorVisible(false);
                                //isEditorVisible(false);
                                getStoreForEditting();
                                getBaseData();
                                $('.nav-tabs li:first-child a').tab('show');
                                $('.nav-tabs li:eq(0) a').tab('show');
                                //#endregion

                                //new store adding
                                if (selectedStore().companyId() == undefined || selectedStore().companyId() == 0) {
                                    selectedStore().companyId(data.CompanyId);
                                    selectedStore().storeImageFileBinary(data.StoreImagePath);
                                    if (selectedStore().type() == "4") {
                                        selectedStore().type("Retail");
                                    } else if (selectedStore().type() == "3") {
                                        selectedStore().type("Corporate");
                                    }
                                    stores.splice(0, 0, selectedStore());
                                }
                                if (selectedStoreListView() && selectedStoreListView().companyId() == selectedStore().companyId()) {
                                    _.each(stores(), function (store) {
                                        if (store.companyId() == selectedStore().companyId()) {
                                            store.name(selectedStore().name());
                                            store.url(selectedStore().url());
                                            store.status(selectedStore().status());
                                            store.storeImageFileBinary(data.StoreImagePath);
                                            if (selectedStore().type() == "4") {
                                                store.type("Retail");
                                            } else if (selectedStore().type() == "3") {
                                                store.type("Corporate");
                                            }
                                        }
                                    });
                                }
                                //selectedStore().storeId(data.StoreId);


                                toastr.success("Successfully saved.");
                                resetObservableArrays();
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Update . Error: " + response, "", ist.toastrOptions);
                                isStoreEditorVisible(false);
                            }
                        });
                }
            },
            //Open Store Dialog
            openEditDialog = function () {
                storeStatus("Modify Store Details");
                isEditorVisible(true);
                getStoreForEditting();
                view.initializeForm();
                getBaseData();
                //getBaseDataFornewCompany();
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
                        emails.removeAll();
                        if (data != null) {
                            selectedStore(model.Store.Create(data.Company));
                            orderCount(data.NewOrdersCount || 0);
                            userCount(data.NewUsersCount || 0);
                            addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                            companyTerritoryPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().companyTerritories, searchCompanyTerritory));
                            contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));
                            //Seconday Page List And Pager
                            secondaryPagePager(new pagination.Pagination({ PageSize: 5 }, selectedStore().secondaryPages, getSecondoryPages));
                            // System Page List And Pager
                            systemPagePager(new pagination.Pagination({ PageSize: 5 }, selectedStore().systemPages, getSystemPages));
                            companyBannerSetList.removeAll();
                            companyBanners.removeAll();
                            filteredCompanyBanners.removeAll();
                            //Cms Offers
                            selectedItemsForOfferList.removeAll();

                            if (data.Company) {
                                if (data.Company.Addresses) {
                                    _.each(data.Company.Addresses, function (addressItem) {
                                        var address = new model.Address.Create(addressItem);
                                        selectedStore().addresses.push(address);
                                    });
                                }

                                if (data.Company.CompanyContacts) {
                                    _.each(data.Company.CompanyContacts, function (companyContactItem) {
                                        var companyContact = new model.CompanyContact.Create(companyContactItem);
                                        selectedStore().users.push(companyContact);
                                    });
                                }

                                if (data.Company.ColorPalletes) {
                                    _.each(data.Company.ColorPalletes, function (item) {
                                        selectedStore().colorPalette(model.ColorPalette.Create(item));
                                    });
                                }

                                if (data.Company.Campaigns) {
                                    _.each(data.Company.Campaigns, function (item) {
                                        var campaign = model.Campaign.Create(item);
                                        _.each(item.CampaignImages, function (campaignImage) {
                                            campaign.campaignImages.push(model.CampaignImage.Create(campaignImage));
                                        });
                                        emails.push(campaign);
                                    });
                                }

                                if (data.Company.CompanyBannerSets) {
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
                                }

                                if (data.Company.CmsOffers) {
                                    _.each(data.Company.CmsOffers, function (item) {
                                        selectedItemsForOfferList.push(model.CmsOffer.Create(item));
                                    });
                                }

                                if (data.Company.MediaLibraries) {
                                    //Media Library
                                    _.each(data.Company.MediaLibraries, function (item) {
                                        selectedStore().mediaLibraries.push(model.MediaLibrary.Create(item));
                                    });
                                }

                                selectedStore().activeBannerSetId(data.Company.ActiveBannerSetId);
                                selectedStore().currentThemeId(data.Company.CurrentThemeId);
                                selectedTheme(data.Company.CurrentThemeId);
                            }

                            if (data.SecondaryPageResponse) {
                                if (data.SecondaryPageResponse.CmsPages) {
                                    _.each(data.SecondaryPageResponse.CmsPages, function (item) {
                                        selectedStore().secondaryPages.push(model.SecondaryPageListView.Create(item));
                                    });
                                    secondaryPagePager().totalCount(data.SecondaryPageResponse.RowCount || 0);
                                }

                                if (data.SecondaryPageResponse.SystemPages) {
                                    _.each(data.SecondaryPageResponse.SystemPages, function (item) {
                                        selectedStore().systemPages.push(model.SecondaryPageListView.Create(item));
                                    });
                                    systemPagePager().totalCount(data.SecondaryPageResponse.SystemPagesRowCount || 0);
                                }
                            }

                            storeImage(data.ImageSource);
                        }

                        allPagesWidgets.removeAll();
                        pageSkinWidgets.removeAll();
                        selectedCurrentPageId(undefined);
                        selectedCurrentPageCopy(undefined);
                        newUploadedMediaFile(model.MediaLibrary());
                        //Update Cost Centers Selection 
                        updateSelectedStoreCostCenters();
                        selectedStore().reset();
                        storeHasChanges.reset();
                        isLoadingStores(false);
                        view.initializeLabelPopovers();
                        view.wireupThemeListClick();
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Failed to Load Stores . Error: " + response, "", ist.toastrOptions);
                        view.initializeLabelPopovers();
                        view.wireupThemeListClick();
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
                            resetStoreEditor();
                        }
                    });
                    confirmation.show();
                    return;
                }
                isEditorVisible(false);
            },
            resetStoreEditor = function () {
                editorViewModel.revertItem();
                allPagesWidgets.removeAll();
                pageSkinWidgets.removeAll();
                selectedCurrentPageId(undefined);
                resetObservableArrays();
                selectedStore().reset();
                storeHasChanges.reset();
            },
            resetFilterSection = function () {
                searchFilter(undefined);
                getStores();
            },
            //Get Base Data By company Id
            getBaseData = function () {
                dataservice.getBaseData({
                    companyId: selectedStoreListView().companyId()
                }, {
                    success: function (data) {
                        if (data != null) {
                            //systemUsers.removeAll();
                            addressCompanyTerritoriesFilter.removeAll();
                            contactCompanyTerritoriesFilter.removeAll();
                            addressTerritoryList.removeAll();
                            //roles.removeAll();
                            //registrationQuestions.removeAll();
                            allCompanyAddressesList.removeAll();
                            //costCentersList.removeAll();
                            //pageCategories.removeAll();
                            //cmsPagesBaseData.removeAll();
                            //_.each(data.CmsPageDropDownList, function (item) {
                            //    cmsPagesBaseData.push(item);
                            //});
                            //_.each(data.SystemUsers, function (item) {
                            //    var systemUser = new model.SystemUser.Create(item);
                            //    systemUsers.push(systemUser);
                            //});
                            _.each(data.CompanyTerritories, function (item) {
                                var territory = new model.CompanyTerritory.Create(item);
                                addressCompanyTerritoriesFilter.push(territory);
                                contactCompanyTerritoriesFilter.push(territory);
                                addressTerritoryList.push(territory);
                            });
                            //_.each(data.CompanyContactRoles, function (item) {
                            //    var role = new model.Role.Create(item);
                            //    roles.push(role);
                            //});
                            //_.each(data.RegistrationQuestions, function (item) {
                            //    var registrationQuestion = new model.RegistrationQuestion.Create(item);
                            //    registrationQuestions.push(registrationQuestion);
                            //});
                            _.each(data.Addresses, function (item) {
                                var address = new model.Address.Create(item);
                                allCompanyAddressesList.push(address);
                            });
                            //_.each(data.PageCategories, function (item) {
                            //    pageCategories.push(model.PageCategory.Create(item));
                            //});
                            //_.each(data.PaymentMethods, function (item) {
                            //    paymentMethods.push(model.PaymentMethod.Create(item));
                            //});
                            ////Email Event List
                            //emailEvents.removeAll();
                            //if (data.EmailEvents !== null) {
                            //    ko.utils.arrayPushAll(emailEvents(), data.EmailEvents);
                            //    emailEvents.valueHasMutated();
                            //}

                            //_.each(data.Widgets, function (item) {
                            //    widgets.push(model.Widget.Create(item));
                            //});

                            //Field VariableF or Field variable List View
                            fieldVariables.removeAll();
                            fieldVariablePager(new pagination.Pagination({ PageSize: 5 }, fieldVariables, getFieldVariables));
                            if (data.FieldVariableResponse && data.FieldVariableResponse.FieldVariables) {
                                _.each(data.FieldVariableResponse.FieldVariables, function (item) {
                                    var field = model.FieldVariable();
                                    field.id(item.VariableId);
                                    field.variableName(item.VariableName);
                                    field.scopeName(item.ScopeName);
                                    field.typeName(item.TypeName);
                                    field.variableTag(item.VariableTag);
                                    fieldVariables.push(field);
                                });
                                fieldVariablePager().totalCount(data.FieldVariableResponse.RowCount);
                            }

                            //Smart Form List View
                            smartFormPager(new pagination.Pagination({ PageSize: 5 }, smartForms, getSmartForms));
                            smartForms.removeAll();
                            if (data.SmartFormResponse && data.SmartFormResponse.SmartForms) {
                                _.each(data.SmartFormResponse.SmartForms, function (item) {
                                    var smartForm = model.SmartForm();
                                    smartForm.id(item.SmartFormId);
                                    smartForm.name(item.Name);
                                    smartForm.heading(item.Heading);
                                    smartForms.push(smartForm);
                                });
                                smartFormPager().totalCount(data.SmartFormResponse.TotalCount);
                            }

                            if (data.FieldVariableForSmartForms != null) {
                                //Field Variable For Smart Forms
                                _.each(data.FieldVariableForSmartForms, function (item) {
                                    fieldVariablesForSmartForm.push(model.FieldVariableForSmartForm.Create(item));
                                });
                            }
                            _.each(systemVariablesForSmartForms(), function (item) {
                                fieldVariablesForSmartForm.push(item);
                            });
                            //Themes 
                            themes.removeAll();
                            if (data.Themes) {
                                ko.utils.arrayPushAll(themes(), data.Themes);
                                themes.valueHasMutated();
                            }

                            cmsPagesForStoreLayout.removeAll();
                            if (data.CmsPageDropDownList !== null) {
                                ko.utils.arrayPushAll(cmsPagesForStoreLayout(), data.CmsPageDropDownList);
                                cmsPagesForStoreLayout.valueHasMutated();

                                //_.each(cmsPagesBaseData(), function (item) {
                                //    cmsPagesForStoreLayout.push(item);
                                //});
                            }

                            //CostCenterVariables
                            priceFlags.removeAll();
                            if (data.PriceFlags !== null) {
                                ko.utils.arrayPushAll(priceFlags(), data.PriceFlags);
                                priceFlags.valueHasMutated();
                            }


                            ////Countries 
                            //countries.removeAll();
                            //ko.utils.arrayPushAll(countries(), data.Countries);
                            //countries.valueHasMutated();
                            ////States 
                            //states.removeAll();
                            //ko.utils.arrayPushAll(states(), data.States);
                            //states.valueHasMutated();

                        }
                        selectedStore().reset();
                        storeHasChanges.reset();
                        isLoadingStores(false);
                        isBaseDataLoded(true);
                        view.initializeLabelPopovers();
                        view.wireupThemeListClick();
                    },
                    error: function (response) {
                        isLoadingStores(false);
                        toastr.error("Failed to Load Stores . Error: " + response, "Please ReOpen Store", ist.toastrOptions);
                        view.initializeLabelPopovers();
                        view.wireupThemeListClick();
                    }
                });
            },
            //work update: to upgrade performance of screen calling this base data on store list view loading
            //Get Base Data For New Company
            getBaseDataFornewCompany = function () {
                if (!isBaseDataLoaded()) {
                    dataservice.getBaseData({
                    }, {
                        success: function (data) {
                            if (data != null) {
                                currencySymbol(data.CurrencySymbol);
                                systemUsers.removeAll();
                                systemVariablesForSmartForms.removeAll();
                                addressCompanyTerritoriesFilter.removeAll();
                                contactCompanyTerritoriesFilter.removeAll();
                                addressTerritoryList.removeAll();
                                addressTerritoryList.removeAll();
                                roles.removeAll();
                                registrationQuestions.removeAll();
                                allCompanyAddressesList.removeAll();
                                pageCategories.removeAll();
                                // cmsPagesBaseData.removeAll();
                                costCentersList.removeAll();
                                _.each(data.SystemUsers, function (item) {
                                    var systemUser = new model.SystemUser.Create(item);
                                    systemUsers.push(systemUser);
                                });
                                //_.each(data.CmsPageDropDownList, function (item) {
                                //    cmsPagesBaseData.push(item);
                                //});
                                _.each(data.CompanyContactRoles, function (item) {
                                    var role = new model.Role.Create(item);
                                    roles.push(role);
                                });
                                _.each(data.RegistrationQuestions, function (item) {
                                    var registrationQuestion = new model.RegistrationQuestion.Create(item);
                                    registrationQuestions.push(registrationQuestion);
                                });

                                if (data.SystemVariablesForSmartForms != null) {
                                    //System Variable For Smart Forms
                                    _.each(data.SystemVariablesForSmartForms, function (item) {
                                        systemVariablesForSmartForms.push(model.FieldVariableForSmartForm.Create(item));
                                    });
                                }
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
                                //Countries 
                                countries.removeAll();
                                ko.utils.arrayPushAll(countries(), data.Countries);
                                countries.valueHasMutated();
                                //States 
                                states.removeAll();
                                ko.utils.arrayPushAll(states(), data.States);
                                states.valueHasMutated();


                                _.each(data.Widgets, function (item) {
                                    widgets.push(model.Widget.Create(item));
                                });

                                _.each(data.CostCenterDropDownList, function (item) {
                                    costCentersList.push(model.CostCenter.Create(item));
                                });
                                //Sefault Sprite Image
                                selectedStore().userDefinedSpriteImageSource(data.DefaultSpriteImageSource);
                                selectedStore().userDefinedSpriteImageFileName("default.jpg");
                                selectedStore().defaultSpriteImageSource(data.DefaultSpriteImageSource);
                                selectedStore().customCSS(data.DefaultCompanyCss);
                                organizationId(data.OrganisationId);
                                privateStoreName(data.CorporateStoreNameWebConfigValue);
                                publicStoreName(data.RetailStoreNameWebConfigValue);
                                isBaseDataLoaded(true);
                            }
                            isLoadingStores(false);
                        },
                        error: function (response) {
                            isLoadingStores(false);
                            toastr.error("Failed to Load Stores Base Data. Error: " + response, "Please Reload", ist.toastrOptions);
                        }
                    });
                }
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
                storeStatus("Store Details");
                isUserAndAddressesTabOpened(false);
                isStoreVariableTabOpened(false);
                isBaseDataLoded(false);
                isThemeNameSet(false);
                selectedTheme(undefined);

                pickUpLocationValue(undefined);
                companyTerritoryCounter = -1,
                selectedStore().addresses.removeAll();
                selectedStore().companyTerritories.removeAll();
                selectedStore().users.removeAll();
                selectedStore().mediaLibraries.removeAll();
                selectedStore().mapImageUrlBinary(undefined);
                selectedStore().storeWorkflowImage(undefined);
                allCompanyAddressesList.removeAll();
                contactCompanyTerritoriesFilter.removeAll();
                emails.removeAll();
                deletedAddresses.removeAll();
                edittedAddresses.removeAll();
                fieldVariables.removeAll();
                newAddresses.removeAll();
                deletedCompanyTerritories.removeAll();
                edittedCompanyTerritories.removeAll();
                newCompanyTerritories.removeAll();
                deletedCompanyContacts.removeAll();
                edittedCompanyContacts.removeAll();
                newCompanyContacts.removeAll();
                parentCategories.removeAll();
                themes.removeAll();
                cmsPagesForStoreLayout.removeAll();
                addressCompanyTerritoriesFilter.removeAll();
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
                //ist.storeProduct.viewModel.resetObservables();
                selectedItemsForOfferList.removeAll();
                selectedItemForRemove(undefined);
                selectedItemForAdd(undefined);
                productPriorityRadioOption("1");
                errorList.removeAll();
                smartForms.removeAll();
                fieldVariables.removeAll();
                fieldVariablesOfContactType.removeAll();
                fieldVariablesOfAddressType.removeAll();
                fieldVariablesOfTerritoryType.removeAll();
                fieldVariablesOfStoreType.removeAll();
                newAddedCampaigns.removeAll();
                filteredCompanyBanners.removeAll();
                editedCampaigns.removeAll();
                deletedCampaigns.removeAll();
                companyBanners.removeAll();
                companyBannerSetList.removeAll();
                fieldVariablesForSmartForm.removeAll();
                fieldVariablePager(new pagination.Pagination({ PageSize: 5 }, fieldVariables, getFieldVariables));
                smartFormPager(new pagination.Pagination({ PageSize: 5 }, smartForms, getSmartForms));
                companyTerritoryPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().companyTerritories, searchCompanyTerritory));
                secondaryPagePager(new pagination.Pagination({ PageSize: 5 }, fieldVariables, getSecondoryPages));
                addressPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().addresses, searchAddress));
                contactCompanyPager(new pagination.Pagination({ PageSize: 5 }, selectedStore().users, searchCompanyContact));
                selectedCompanyDomainItem(undefined);
                _.each(costCentersList(), function (costCenter) {
                    costCenter.isSelected(false);
                });
                //companyTerritoryPager().totalCount(0);
            },
            //#endregion
            //#endregion

            //#region _________P R O D U C T S ______________________
            isProductTabVisited = ko.observable(false),
            getProducts = function () {

                // Resetting Filter on tab chnage 
                if (ist.product.viewModel.filterText() !== undefined && ist.product.viewModel.filterText() !== '') {
                    ist.product.viewModel.filterText('');
                    isProductTabVisited(false);
                }

                if (!isProductTabVisited()) {
                    isProductTabVisited(true);
                    ist.product.viewModel.initializeForStore(selectedStore().companyId(), selectedStore().taxRate());
                }
                //Reset Product Categories List On Every Time Tab Selection
                $('.dd-list').closest('li').each(function (index) {
                    $($(this).children()[0].children[0]).removeClass("fa fa-chevron-circle-down").addClass("fa fa-chevron-circle-right");
                });
                $($($('.dd-list')[0]).children()).each(function (index) {
                    $($(this).children()[0].children[0]).removeClass("fa fa-chevron-circle-down").addClass("fa fa-chevron-circle-right");
                });
                $('.dd-list').closest('li').children('ol').hide();
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
                if (!selectedStore().companyId()) {
                    return;
                }

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
                        toastr.error("Failed to Load Page Widgets . Error: " + response, "", ist.toastrOptions);
                    }
                });
            },
            // Widget being dropped
            // ReSharper disable UnusedParameter
            dropped = function (source, target, event) {
                //selectedStore().storeLayoutChange("change");
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
                    toastr.error("Before add widget please select page !", "", ist.toastrOptions);
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
                        toastr.error("Failed to Load Page Widgets . Error: " + response, "", ist.toastrOptions);
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
                    //selectedStore().storeLayoutChange("change");
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
                    toastr.error("Before add widget please select page !", "", ist.toastrOptions);
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
                        toastr.error("Failed to Load Page Widgets . Error: " + response, "", ist.toastrOptions);
                    }
                });
            },
            //Delete Page Layout Widget
            deletePageLayoutWidget = function (widget) {
                if (widget !== undefined && widget !== null) {
                    pageSkinWidgets.remove(widget);
                    //selectedStore().storeLayoutChange("change");
                }
            },
            //show Ck Editor Dialog
            showCkEditorDialog = function (widget) {
                ckEditorOpenFrom("StoreLayout");
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
                //selectedStore().storeLayoutChange("change");
                selectedWidget(undefined);
                view.hideCkEditorDialogDialog();
            },
            //#endregion

            //#region _________WIDGETS IN Themes & Widgets Tab _________________
            //Open Dialog from Featured Product Row
            openItemsForWidgetsDialogFromFeatured = function () {
                productsFilterHeading("Featured Products");

                productsFilterSubHeadingAll("All Featured Products");
                productsFilterSubHeadingSelected("Selected Featured Products");
                selectedOfferType(1);
                resetItems();
                view.showItemsForWidgetsDialog();
            },
            //Open Dialog from Popular Product Row
            openItemsForWidgetsDialogFromPopular = function () {
                productsFilterHeading("Popular Products");

                productsFilterSubHeadingAll("All Popular Products");
                productsFilterSubHeadingSelected("Selected Popular Products");

                selectedOfferType(2);
                resetItems();
                view.showItemsForWidgetsDialog();
            },
            //Open Dialog from Special Product Row
            openItemsForWidgetsDialogFromSpecial = function () {
                productsFilterHeading("Special Products");
                productsFilterSubHeadingAll("All Special Products");
                productsFilterSubHeadingSelected("Selected Special Products");
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
                    var removeOfferList = [];
                    _.each(selectedItemsForOfferList(), function (offerItem) {
                        if (selectedOfferType() !== offerItem.offerType()) {
                            removeOfferList.push(offerItem);
                        }
                    });
                    _.each(removeOfferList, function (offerItem) {
                        selectedItemsForOfferList.remove(offerItem);
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
                    if (selectedMediaFile().fakeId() === undefined) {
                        selectedStore().storeBackgroudImagePath(selectedMediaFile().filePath());
                    } else {
                        selectedStore().storeBackgroudImageImageSource(selectedMediaFile().fileSource());
                    }
                    hideMediaLibraryDialog();
                }
                    //If Open From Company Banner
                else if (mediaLibraryOpenFrom() === "CompanyBanner") {
                    if (selectedMediaFile().id() > 0) {
                        selectedCompanyBanner().filePath(selectedMediaFile().filePath());
                    } else {
                        selectedCompanyBanner().filePath(selectedMediaFile().id());
                        selectedCompanyBanner().fileBinary(selectedMediaFile().fileSource());
                        selectedCompanyBanner().imageSource(selectedMediaFile().fileSource());
                    }
                    hideMediaLibraryDialog();
                }
                    //If Open From Secondary Page
                else if (mediaLibraryOpenFrom() === "SecondaryPage") {
                    if (selectedMediaFile().id() > 0) {
                        selectedSecondaryPage().pageBanner(selectedMediaFile().filePath());
                        hideMediaLibraryDialog();
                    } else {
                        //selectedSecondaryPage().pageBanner(selectedMediaFile().id());
                        //selectedSecondaryPage().imageSrc(selectedMediaFile().fileSource());
                        toastr.error("You can not select newly added media file.");
                    }

                }
                //    //If Open From Product Category Banner
                //else if (mediaLibraryOpenFrom() === "ProductCategoryBanner") {
                //    if (selectedMediaFile().id() > 0) {
                //        selectedProductCategoryForEditting().imagePath(selectedMediaFile().filePath());
                //    } else {
                //        selectedProductCategoryForEditting().imagePath(selectedMediaFile().id());
                //    }
                //    selectedProductCategoryForEditting().productCategoryImageFileBinary(selectedMediaFile().fileSource());
                //}
                //    //If Open From Product Category Thumbnail
                //else if (mediaLibraryOpenFrom() === "ProductCategoryThumbnail") {
                //    if (selectedMediaFile().id() > 0) {
                //        selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().filePath());
                //    } else {
                //        selectedProductCategoryForEditting().thumbnailPath(selectedMediaFile().id());
                //    }
                //    selectedProductCategoryForEditting().productCategoryThumbnailFileBinary(selectedMediaFile().fileSource());
                //}

                //Hide gallery

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
                                //_.each(countries(), function(country) {
                                //    if (address.countryId() == country.CountryId) {
                                //        selectedPickupAddress().countryName(country.CountryName);
                                //    }
                                //});
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
                    // Check for Null value // Added by khurram
                    if (selectedCostCenter) {
                        selectedCostCenter.isSelected(true);
                    }
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

            //#region ________ Field Variable___________
            //Active Field Variable
            selectedFieldVariable = ko.observable(),
            selectedFieldVariableForListView = ko.observable(),
            //Selected Field Option
            selectedFieldOption = ko.observable(),
            //Field Variables List
            fieldVariables = ko.observableArray([]),
            //Field Variables For Smart Form
            fieldVariablesForSmartForm = ko.observableArray([]),
            //Use in User (contact) Or Use in Company Contact
            fieldVariablesOfContactType = ko.observableArray([]),
            //Address Field variables
            fieldVariablesOfAddressType = ko.observableArray([]),
            //Territory Field variables
            fieldVariablesOfTerritoryType = ko.observableArray([]),
            //Store Field variables
            fieldVariablesOfStoreType = ko.observableArray([]),
            //Variable Option Fake ID counter
            fakeIdCounter = ko.observable(0),
            //Create New Field Variable
            onAddVariableDefination = function () {
                selectedFieldVariable(model.FieldVariable());
                selectedFieldOption(undefined);
                view.showVeriableDefinationDialog();
            },
            //Save Field Variable
            onSaveFieldVariable = function (fieldVariable) {
                if (doBeforeSaveFieldVariable()) {
                    selectedFieldOption(undefined);
                    var selectedScope = _.find(contextTypes(), function (scope) {
                        return scope.id == fieldVariable.scope();
                    });
                    fieldVariable.scopeName(selectedScope.name);
                    var selectedType = _.find(varibaleTypes(), function (type) {
                        return type.id == fieldVariable.variableType();
                    });
                    fieldVariable.typeName(selectedType.name);
                    fieldVariable.companyId(selectedStore().companyId());

                    // //In New Store, Edit Field Variable
                    if (fieldVariable.id() === undefined && fieldVariable.fakeId() < 0) {
                        updateFieldVariableWithNewStore(fieldVariable);
                        view.hideVeriableDefinationDialog();
                    }
                        //New Store And New Field Variable Added Case
                    else if (fieldVariable.id() === undefined && fieldVariable.fakeId() === undefined && selectedStore().companyId() === undefined) {
                        fieldVariables.splice(0, 0, fieldVariable);
                        view.hideVeriableDefinationDialog();
                        fieldVariable.fakeId(fakeIdCounter() - 1);
                        fakeIdCounter(fakeIdCounter() - 1);

                        //Add to Smart Form Variable List
                        addToSmartFormVariableList(fieldVariable);
                        addFieldVariableToItsScopeTypeList(fieldVariable);
                    }
                        //In case Of Edit Store , Field variable direct save to db. 
                    else if (selectedStore().companyId() !== undefined) {
                        //In Case of Edit Company 
                        var field = fieldVariable.convertToServerData(fieldVariable);
                        _.each(fieldVariable.variableOptions(), function (optionItem, index) {
                            optionItem.sortOrder(index + 1);
                            field.VariableOptions.push(optionItem.convertToServerData(optionItem));
                        });
                        isStoreVariableTabOpened(false);
                        saveField(field);
                    }
                }
            },
            //Add To list based on its scope type
            addFieldVariableToItsScopeTypeList = function (fieldVariable) {
                var scopeVariable = addToCompanyContactsVariable(fieldVariable);
                //In Case of Context/Scope Type Contact
                if (fieldVariable.scope() === 2) {
                    //Add new variable to already created contacts
                    _.each(newCompanyContacts(), function (contact) {
                        contact.companyContactVariables.push(scopeVariableMapper(scopeVariable));
                    });
                }
                    //Store  Type
                else if (fieldVariable.scope() === 1) {

                }
                    //Address Type
                else if (fieldVariable.scope() === 3) {

                }
                    //Territory Type
                else if (fieldVariable.scope() === 4) {
                    _.each(selectedStore().companyTerritories(), function (territory) {
                        territory.scopeVariables.push(scopeVariable);
                    });
                }
            },

            addToCompanyContactsVariable = function (fieldVariable) {
                var scopeVariable = model.ScopeVariable();
                scopeVariable.fakeId(fieldVariable.fakeId());
                scopeVariable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                scopeVariable.type(fieldVariable.variableType());
                scopeVariable.title(fieldVariable.variableTitle());
                scopeVariable.scope(fieldVariable.scope());
                scopeVariable.variableId(fieldVariable.id());

                _.each(fieldVariable.variableOptions(), function (item) {
                    scopeVariable.variableOptions.push(item);
                });
                if (fieldVariable.scope() === 1) {
                    fieldVariablesOfStoreType.push(scopeVariable);
                }
                else if (fieldVariable.scope() === 2) {
                    fieldVariablesOfContactType.push(scopeVariable);
                }
                else if (fieldVariable.scope() === 3) {
                    fieldVariablesOfAddressType.push(scopeVariable);
                }
                else if (fieldVariable.scope() === 4) {
                    fieldVariablesOfTerritoryType.push(scopeVariable);
                }
                return scopeVariable;
            },
            //In Case Of New Store, edit Field variable
            updateFieldVariableWithNewStore = function (fieldVariable) {
                updateContactVariables(fieldVariable);
                updateTrritoryVariables(fieldVariable);
                updateAddressVariables(fieldVariable);
                updateStoreVariables(fieldVariable);
                updateSmartFormVariables(fieldVariable);
            },
            updateAddressVariables = function (fieldVariable) {
                //#region  Scope variables For Address
                var variable = _.find(fieldVariablesOfAddressType(), function (item) {
                    return item.fakeId() === fieldVariable.fakeId();
                });
                if (variable) {
                    //In Case of Scope Type Change
                    if (variable.scope() !== fieldVariable.scope()) {
                        //remove From Address variable List
                        fieldVariablesOfAddressType.remove(variable);
                        //Remove From Each Address
                        _.each(selectedStore().addresses(), function (address) {
                            var scopeVariable = _.find(address.scopeVariables(), function (item) {
                                return item.fakeId() === fieldVariable.fakeId();
                            });
                            if (scopeVariable) {
                                address.scopeVariables.remove(scopeVariable);
                            }
                        });
                    }
                    else {
                        variable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                        variable.type(fieldVariable.variableType());
                        variable.title(fieldVariable.variableTitle());
                        variable.variableOptions.removeAll();
                        _.each(fieldVariable.variableOptions(), function (item) {
                            variable.variableOptions.push(item);
                        });
                    }
                    //Change in Already existed Addressess
                    _.each(selectedStore().addresses(), function (address) {
                        var scopeVariable = _.find(address.scopeVariables(), function (item) {
                            return item.fakeId() === fieldVariable.fakeId();
                        });
                        if (scopeVariable) {
                            scopeVariable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                            scopeVariable.title(fieldVariable.variableTitle());
                            scopeVariable.type(fieldVariable.variableType());
                            scopeVariable.scope(fieldVariable.scope());
                            scopeVariable.optionId(scopeVariable.value());
                            scopeVariable.variableOptions.removeAll();
                            ko.utils.arrayPushAll(scopeVariable.variableOptions, fieldVariable.variableOptions());
                            scopeVariable.variableOptions.valueHasMutated();
                        }
                    });

                }
                    //In Case of Scope Type Change To Address Then add it to Address variable List
                else if (variable === undefined && fieldVariable.scope() === 3) {
                    var scopeVariable1 = addToCompanyContactsVariable(fieldVariable);
                    //Add new variable to already created Address
                    _.each(selectedStore().addresses(), function (address) {
                        address.scopeVariables.push(scopeVariableMapper(scopeVariable1));
                    });
                }
                //#endregion
            },
            updateStoreVariables = function (fieldVariable) {
                //#region  Scope variables For Contact
                var fieldvariableOfStoreType = _.find(fieldVariablesOfStoreType(), function (item) {
                    return item.fakeId() === fieldVariable.fakeId();
                });
                if (fieldvariableOfStoreType) {
                    //In Case of Scope Type Change
                    if (fieldvariableOfStoreType.scope() !== fieldVariable.scope()) {
                        //remove From Conatct variable List
                        fieldVariablesOfStoreType.remove(fieldvariableOfStoreType);
                    }
                    else {
                        fieldvariableOfStoreType.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                        fieldvariableOfStoreType.type(fieldVariable.variableType());
                        fieldvariableOfStoreType.title(fieldVariable.variableTitle());
                        fieldvariableOfStoreType.variableOptions.removeAll();
                        _.each(fieldVariable.variableOptions(), function (item) {
                            fieldvariableOfStoreType.variableOptions.push(item);
                        });
                    }
                }
                    //In Case of Scope Type Change To Store Then add it to Store variable List
                else if (fieldvariableOfStoreType === undefined && fieldVariable.scope() === 1) {
                    var scopeVariable1 = addToCompanyContactsVariable(fieldVariable);
                }
                //#endregion
            },

            updateContactVariables = function (fieldVariable) {
                //#region  Scope variables For Contact
                var fieldvariableOfContactType = _.find(fieldVariablesOfContactType(), function (item) {
                    return item.fakeId() === fieldVariable.fakeId();
                });
                if (fieldvariableOfContactType) {
                    //In Case of Scope Type Change
                    if (fieldvariableOfContactType.scope() !== fieldVariable.scope()) {
                        //remove From Conatct variable List
                        fieldVariablesOfContactType.remove(fieldvariableOfContactType);
                        //Remove From Each contacts
                        _.each(newCompanyContacts(), function (contact) {
                            var scopeVariable = _.find(contact.companyContactVariables(), function (item) {
                                return item.fakeId() === fieldVariable.fakeId();
                            });
                            if (scopeVariable) {
                                contact.companyContactVariables.remove(scopeVariable);
                            }
                        });
                    }
                    else {
                        fieldvariableOfContactType.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                        fieldvariableOfContactType.type(fieldVariable.variableType());
                        fieldvariableOfContactType.title(fieldVariable.variableTitle());
                        fieldvariableOfContactType.variableOptions.removeAll();
                        _.each(fieldVariable.variableOptions(), function (item) {
                            fieldvariableOfContactType.variableOptions.push(item);
                        });
                    }
                    //Change in Already existed Contacts
                    _.each(newCompanyContacts(), function (contact) {
                        var scopeVariable = _.find(contact.companyContactVariables(), function (item) {
                            return item.fakeId() === fieldVariable.fakeId();
                        });
                        if (scopeVariable) {
                            scopeVariable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                            scopeVariable.title(fieldVariable.variableTitle());
                            scopeVariable.type(fieldVariable.variableType());
                            scopeVariable.scope(fieldVariable.scope());
                            scopeVariable.optionId(scopeVariable.value());
                            scopeVariable.variableOptions.removeAll();
                            ko.utils.arrayPushAll(scopeVariable.variableOptions, fieldVariable.variableOptions());
                            scopeVariable.variableOptions.valueHasMutated();
                        }
                    });

                }
                    //In Case of Scope Type Change To Contact Then add it to contact variable List
                else if (fieldvariableOfContactType === undefined && fieldVariable.scope() === 2) {
                    var scopeVariable1 = addToCompanyContactsVariable(fieldVariable);
                    //Add new variable to already created contacts
                    _.each(newCompanyContacts(), function (contact) {
                        contact.companyContactVariables.push(scopeVariableMapper(scopeVariable1));
                    });
                }
                //#endregion
            },
            updateTrritoryVariables = function (fieldVariable) {
                //#region  Scope variables For Territory
                var variable = _.find(fieldVariablesOfTerritoryType(), function (item) {
                    return item.fakeId() === fieldVariable.fakeId();
                });
                if (variable) {
                    //In Case of Scope Type Change
                    if (variable.scope() !== fieldVariable.scope()) {
                        //remove From Territory variable List
                        fieldVariablesOfTerritoryType.remove(variable);
                        //Remove From Each Territory
                        _.each(selectedStore().companyTerritories(), function (territory) {
                            var scopeVariable = _.find(territory.scopeVariables(), function (item) {
                                return item.fakeId() === fieldVariable.fakeId();
                            });
                            if (scopeVariable) {
                                territory.scopeVariables.remove(scopeVariable);
                            }
                        });
                    }
                    else {
                        variable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                        variable.type(fieldVariable.variableType());
                        variable.title(fieldVariable.variableTitle());
                        variable.variableOptions.removeAll();
                        _.each(fieldVariable.variableOptions(), function (item) {
                            variable.variableOptions.push(item);
                        });
                    }
                    //Change in Already existed Territories
                    _.each(selectedStore().companyTerritories(), function (territory) {
                        var scopeVariable = _.find(territory.scopeVariables(), function (item) {
                            return item.fakeId() === fieldVariable.fakeId();
                        });
                        if (scopeVariable) {
                            scopeVariable.value(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                            scopeVariable.title(fieldVariable.variableTitle());
                            scopeVariable.type(fieldVariable.variableType());
                            scopeVariable.scope(fieldVariable.scope());
                            scopeVariable.optionId(scopeVariable.value());
                            scopeVariable.variableOptions.removeAll();
                            ko.utils.arrayPushAll(scopeVariable.variableOptions, fieldVariable.variableOptions());
                            scopeVariable.variableOptions.valueHasMutated();
                        }
                    });

                }
                    //In Case of Scope Type Change To Territory Then add it to Territory variable List
                else if (variable === undefined && fieldVariable.scope() === 4) {
                    var scopeVariable1 = addToCompanyContactsVariable(fieldVariable);
                    //Add new variable to already created Territories
                    _.each(selectedStore().companyTerritories(), function (territory) {
                        territory.scopeVariables.push(scopeVariableMapper(scopeVariable1));
                    });
                }
                //#endregion
            },
            updateSmartFormVariables = function (fieldVariable) {
                var fieldvariableOfSmartForm = _.find(fieldVariablesForSmartForm(), function (item) {
                    return item.id() === fieldVariable.id();
                });
                if (fieldvariableOfSmartForm) {
                    updateSmartFormVariable(fieldvariableOfSmartForm, fieldVariable);
                }
            },
            updateSmartFormVariableOnUpdatingCustomCrmField = function (fieldVariable) {
                var fieldvariableOfSmartForm = _.find(fieldVariablesForSmartForm(), function (item) {
                    return item.id() === fieldVariable.id();
                });
                if (fieldvariableOfSmartForm) {
                    updateSmartFormVariable(fieldvariableOfSmartForm, fieldVariable);
                }
            },
            updateSmartFormVariable = function (fieldvariableOfSmartForm, fieldVariable) {
                fieldvariableOfSmartForm.variableName(fieldVariable.variableName());
                fieldvariableOfSmartForm.variableTag(fieldVariable.variableTag());
                fieldvariableOfSmartForm.scopeName(fieldVariable.scopeName());
                fieldvariableOfSmartForm.scope(fieldVariable.scope());
                fieldvariableOfSmartForm.typeName(fieldVariable.typeName());
                fieldvariableOfSmartForm.waterMark(fieldVariable.waterMark());
                fieldvariableOfSmartForm.variableType(fieldVariable.variableType());
                fieldvariableOfSmartForm.defaultValue(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                fieldvariableOfSmartForm.title(fieldVariable.variableTitle());
            },
            //Add to Smart Form Variable List
            addToSmartFormVariableList = function (fieldVariable) {
                //Field Variable For Smart Form
                var fieldVariableForSmartForm = model.FieldVariableForSmartForm();
                if (selectedStore().companyId() === undefined) {
                    fieldVariableForSmartForm.id(fieldVariable.fakeId());
                } else {
                    fieldVariableForSmartForm.id(fieldVariable.id());
                }
                fieldVariableForSmartForm.variableName(fieldVariable.variableName());
                fieldVariableForSmartForm.variableTag(fieldVariable.variableTag());
                fieldVariableForSmartForm.scopeName(fieldVariable.scopeName());
                fieldVariableForSmartForm.scope(fieldVariable.scope());
                fieldVariableForSmartForm.typeName(fieldVariable.typeName());
                fieldVariableForSmartForm.waterMark(fieldVariable.waterMark());
                fieldVariableForSmartForm.variableType(fieldVariable.variableType());
                fieldVariableForSmartForm.defaultValue(fieldVariable.variableType() === 1 ? fieldVariable.defaultValue() : fieldVariable.defaultValueForInput());
                fieldVariableForSmartForm.title(fieldVariable.variableTitle());
                fieldVariablesForSmartForm.push(fieldVariableForSmartForm);
            },
            //save Field variable
            saveField = function (fieldVariable) {
                dataservice.saveFieldVariable(fieldVariable, {
                    success: function (data) {
                        if (selectedFieldVariable().id() === undefined) {
                            selectedFieldVariable().id(data);
                            fieldVariables.splice(0, 0, selectedFieldVariable());
                            addToSmartFormVariableList(selectedFieldVariable());


                        } else {
                            updateFieldVariable();
                        }

                        view.hideVeriableDefinationDialog();
                        toastr.success("Successfully saved.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage, "", ist.toastrOptions);

                        } else {

                            toastr.error("Failed to saved.", "", ist.toastrOptions);
                        }

                    }
                });

            },

            //Update Field variable
            updateFieldVariable = function () {
                var updatedFieldVariable = _.find(fieldVariables(), function (field) {
                    return field.id() == selectedFieldVariable().id();
                });
                var selectedScope = _.find(contextTypes(), function (scope) {
                    return scope.id == selectedFieldVariable().scope();
                });
                updatedFieldVariable.scopeName(selectedScope.name);
                var selectedType = _.find(varibaleTypes(), function (type) {
                    return type.id == selectedFieldVariable().variableType();
                });
                updatedFieldVariable.typeName(selectedType.name);
                updatedFieldVariable.variableType(selectedFieldVariable().variableType());

                updatedFieldVariable.variableName(selectedFieldVariable().variableName());
                updatedFieldVariable.variableTag(selectedFieldVariable().variableTag());
                updatedFieldVariable.variableTitle(selectedFieldVariable().variableTitle());
                updatedFieldVariable.waterMark(selectedFieldVariable().waterMark());
                updateSmartFormVariableOnUpdatingCustomCrmField(updatedFieldVariable);
            },
            //Do Before Save Field Variable
            doBeforeSaveFieldVariable = function () {
                var flag = true;
                if (!selectedFieldVariable().isValid()) {
                    selectedFieldVariable().errors.showAllMessages();
                    flag = false;
                }
                //if ((selectedFieldVariable().variableType() === 2 && selectedFieldOption() !== undefined && !selectedFieldOption().isValid())) {
                //    flag = false;
                //}
                if (selectedStore().companyId() === undefined) {
                    if (isFiedlVariableNameOrTagDuplicate()) {
                        flag = false;
                    }
                }
                return flag;
            },

            isFiedlVariableNameOrTagDuplicate = function () {
                var flag = false;
                if (selectedFieldVariable().variableName() !== undefined) {
                    var fieldVariableName = _.find(fieldVariables(), function (item) {
                        return item.variableName() !== undefined && item.fakeId() !== selectedFieldVariable().fakeId() && (item.variableName().toLowerCase() === selectedFieldVariable().variableName().toLowerCase());
                    });
                    if (fieldVariableName !== undefined) {
                        flag = true;
                        toastr.error("Field Variable already exist with same Name.", "", ist.toastrOptions);
                    }
                }
                if (selectedFieldVariable().variableTag()) {
                    var fieldVariableTag = _.find(fieldVariables(), function (item) {
                        return item.variableTag() !== undefined && item.fakeId() !== selectedFieldVariable().fakeId() && (item.variableTag().toLowerCase() === selectedFieldVariable().variableTag().toLowerCase());
                    });
                    if (fieldVariableTag !== undefined) {
                        flag = true;
                        toastr.error("Field Variable already exist with same Tag.", "", ist.toastrOptions);
                    }
                }

                return flag;
            },
            //Add Field Option
            onAddFieldOption = function () {
                if (selectedFieldOption() === undefined || selectedFieldOption().isValid()) {
                    var option = model.VariableOption();
                    option.fakeId(fakeIdCounter() - 1);
                    option.id(fakeIdCounter() - 1);
                    fakeIdCounter(fakeIdCounter() - 1);
                    selectedFieldOption(option);
                    selectedFieldVariable().variableOptions.splice(0, 0, option);
                }
            },
            //Edit Variable Option
            onEditVariableOption = function (option) {
                if (selectedFieldOption() === undefined || selectedFieldOption().isValid()) {
                    selectedFieldOption(option);
                }

            },
            //Delete Variable Option
            onDeleteVariableOption = function (option) {
                if (selectedFieldOption() === option) {
                    selectedFieldOption(undefined);
                }
                selectedFieldVariable().variableOptions.remove(option);
            },

            // Template Chooser
            templateToUseForVariableOption = function (vOption) {
                return (vOption === selectedFieldOption() ? 'editVariableOptionTemplate' : 'itemVariableOptionTemplate');
            },

            //edit Field Variable
            onEditFieldVariable = function (fieldVariable) {
                selectedFieldVariableForListView(fieldVariable);
                selectedFieldOption(undefined);
                if (selectedStore().companyId() === undefined) {
                    selectedFieldVariable(fieldVariable);
                    view.showVeriableDefinationDialog();
                } else {
                    getFieldVariableDetail(fieldVariable);
                }
            },
            //variable Scope
            contextTypes = ko.observableArray([{ id: 1, name: "Store" },
                                  { id: 2, name: "Contact" },
                                  { id: 3, name: "Address" },
                                  { id: 4, name: "Territory" }]),
            //Varibale Types
            varibaleTypes = ko.observableArray([{ id: 1, name: "Dropdown" },
                    { id: 2, name: "Input" }]),

            //Get FieldV ariables        
            getFieldVariables = function () {
                dataservice.getFieldVariablesByCompanyId({
                    CompanyId: selectedStore().companyId(),
                    PageSize: fieldVariablePager().pageSize(),
                    PageNo: fieldVariablePager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc()
                }, {
                    success: function (data) {

                        fieldVariables.removeAll();
                        _.each(data.FieldVariables, function (item) {
                            var field = model.FieldVariable();
                            field.id(item.VariableId);
                            field.variableName(item.VariableName);
                            field.scopeName(item.ScopeName);
                            field.typeName(item.TypeName);
                            field.variableTag(item.VariableTag);
                            fieldVariables.push(field);
                        });
                        fieldVariablePager().totalCount(data.RowCount);
                    },
                    error: function (response) {
                        toastr.error("Failed To Load Users" + response, "", ist.toastrOptions);
                    }
                });
            },
            //Get Field Variable Detail
            getFieldVariableDetail = function (field) {
                dataservice.getFieldVariableDetailById({
                    fieldVariableId: field.id(),
                }, {
                    success: function (data) {
                        if (data != null) {
                            var fieldvariable = model.FieldVariable.Create(data);
                            _.each(data.VariableOptions, function (item) {
                                fieldvariable.variableOptions.push(model.VariableOption.Create(item));
                            });
                            selectedFieldVariable(fieldvariable);
                            selectedFieldVariable().reset();
                            view.showVeriableDefinationDialog();
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load Detail . Error: ", "", ist.toastrOptions);
                    }
                });
            },
            //Get Company Contact Variables 
            getCompanyContactVariables = function () {
                //Company is in edit mode and contact also in open for edit
                if (selectedCompanyContact().contactId() !== undefined && selectedStore().companyId() !== undefined) {
                    var scope = 2;
                    getCompanyContactVariableForEditContact(selectedCompanyContact().contactId(), scope);

                }
            },
            getScopeVariables = function () {
                if (selectedStore().companyId() !== undefined && !isStoreVariableTabOpened()) {
                    isStoreVariableTabOpened(true);
                    var scope = 1;
                    getCompanyContactVariableForEditContact(selectedStore().companyId(), scope);
                }
            },
            //In Case Scope Variables Edit
            getCompanyContactVariableForEditContact = function (id, scope) {
                dataservice.getScopeVaribableByContactId({
                    id: id,
                    scope: scope
                }, {
                    success: function (data) {
                        if (data != null) {
                            var isStoreDirty = hasChangesOnStore();
                            //Scope Type Contact
                            if (scope == 2) {
                                var contactHasChanges = selectedCompanyContact().hasChanges();
                                selectedCompanyContact().companyContactVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    selectedCompanyContact().companyContactVariables.push(contactVariable);
                                });
                                if (!contactHasChanges) {
                                    selectedCompanyContact().reset();
                                }
                            }
                                //Scope Type Territory
                            else if (scope == 4) {
                                var territoryHasChanges = selectedCompanyTerritory().hasChanges();
                                selectedCompanyTerritory().scopeVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    selectedCompanyTerritory().scopeVariables.push(contactVariable);
                                });
                                if (!territoryHasChanges) {
                                    selectedCompanyTerritory().reset();
                                }
                            }
                                //Scope Type Address
                            else if (scope == 3) {
                                var addressHasChanges = selectedAddress().hasChanges();
                                selectedAddress().scopeVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    selectedAddress().scopeVariables.push(contactVariable);
                                });
                                if (!addressHasChanges) {
                                    selectedAddress().reset();
                                }
                            }
                            else if (scope == 1) {
                                fieldVariablesOfStoreType.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    fieldVariablesOfStoreType.push(contactVariable);
                                });


                            }
                            if (!isStoreDirty) {
                                storeHasChanges.reset();
                            }
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load.", "", ist.toastrOptions);
                    }
                });
            },
            //New Added Company Contact In Edit Store
            getCompanyContactVariable = function (scope) {
                dataservice.getCmpanyContactVaribableByCompanyId({
                    companyId: selectedStore().companyId(),
                    scope: scope
                }, {
                    success: function (data) {
                        if (data != null) {
                            if (scope === 2) {
                                var contactHasChanges = selectedCompanyContact().hasChanges();
                                selectedCompanyContact().companyContactVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);
                                    });
                                    selectedCompanyContact().companyContactVariables.push(contactVariable);
                                });
                                if (!contactHasChanges) {
                                    selectedCompanyContact().reset();
                                }
                            }
                                //Territory
                            else if (scope === 4) {
                                var territoryHasChanges = selectedCompanyTerritory().hasChanges();
                                selectedCompanyTerritory().scopeVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    selectedCompanyTerritory().scopeVariables.push(contactVariable);
                                });
                                if (!territoryHasChanges) {
                                    selectedCompanyTerritory().reset();
                                }
                            }
                                //Address
                            else if (scope === 3) {
                                var addressHasChanges = selectedAddress().hasChanges();
                                selectedAddress().scopeVariables.removeAll();
                                _.each(data, function (item) {
                                    var contactVariable = model.ScopeVariable.Create(item);
                                    _.each(item.VariableOptions, function (option) {
                                        var variableOption = model.VariableOption.Create(option);
                                        contactVariable.variableOptions.push(variableOption);

                                    });
                                    selectedAddress().scopeVariables.push(contactVariable);
                                });
                                if (!addressHasChanges) {
                                    selectedAddress().reset();
                                }
                            }

                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load.", "", ist.toastrOptions);
                    }
                });
            },

            //on Change Variable option selected value in Company contact
            onVariableOptionDropDownChange = function (contactVariable) {
                contactVariable.value(contactVariable.optionId());
            },

            onRemoveFieldVariable = function (variable) {
                confirmation.afterProceed(function () {
                    deleteFieldVariable(variable.convertToServerData(variable));
                });
                confirmation.afterCancel();
                confirmation.show();
            },
            deleteFieldVariable = function (fieldVariable) {
                dataservice.deleteFieldVariable(fieldVariable, {
                    success: function (data) {
                        fieldVariables.remove(selectedFieldVariableForListView());
                        view.hideVeriableDefinationDialog();
                        toastr.success("Successfully removed.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage, "", ist.toastrOptions);

                        } else {

                            toastr.error("Failed to remove.", "", ist.toastrOptions);
                        }

                    }
                });

            },
            //#endregion ________ Field Variable___________

            //#region ________ Smart Form___________
            //Active Smart Form
            //selectedSmartForm = ko.observable(),
            smartFormEditorViewModel = new ist.ViewModel(model.SmartForm),
            selectedSmartForm = smartFormEditorViewModel.itemForEditing,
            //Group Caption Text
            groupCaption = ko.observable("Drag a Group Caption"),
            //line Seperator
            lineSeperator = ko.observable("Drag a Line Separator"),
            //Smart Form List
            smartForms = ko.observableArray([]),
            //Create Smart Form
            addSmartForm = function () {
                selectedSmartForm(model.SmartForm());
                selectedSmartForm().heading("");
                view.showSmartFormDialog();

            },
            // Returns the item being dragged
            draggedVariableField = function (source) {
                selectedSmartForm().dropFrom("VariableField");
                return {
                    row: source.$parent,
                    data: source.$data
                };
            },
            //Dragged Group Caption
            draggedGroupCaption = function (source) {
                selectedSmartForm().dropFrom("GroupCaption");
                return {
                    row: source.$parent,
                    data: source.$data
                };
            },
            //Dragged Line Seperator
            draggedLineSeperator = function (source) {
                selectedSmartForm().dropFrom("LineSeperator");
                return {
                    row: source.$parent,
                    data: source.$data
                };
            },
            //Smart Form Droped Area
            droppedSmartFormArea = function (source, target, event) {
                if (source !== undefined && source !== null) {
                    var smartFormDetail = model.SmartFormDetail();
                    smartFormDetail.isRequired("0");
                    if (source.data.dropFrom === undefined && source.row.dropFrom() === "VariableField") {
                        smartFormDetail.objectType(3);
                        smartFormDetail.variableId(source.data.id());
                        var title = (source.data.title() === (null || undefined)) ? "" : source.data.title();
                        var waterMark = (source.data.waterMark() === null || source.data.waterMark() === undefined) ? "" : source.data.waterMark();
                        var htmlData = "";
                        if (source.data.variableType() === 1) {
                            htmlData = "<div style=\"border:2px dotted silver;height:80px\"><div class=\"col-lg-12\"><div class=\"col-lg-6\"><label style=\"margin-left:9px;\">" + title + "</label><div class=\"col-lg-12\"><select disabled class=\"form-control\"><option>" + waterMark + "</option></select></div></div></div>";

                        } else {
                            htmlData = "<div style=\"border:2px dotted silver;height:80px\"><div class=\"col-lg-12\"><label style=\"margin-left:9px;\">" + title + "</label><div><input type=\"text\" disabled class=\"form-control\" value=\"" + waterMark + "\"></div></div></div>";
                        }
                        smartFormDetail.html(htmlData);
                        selectedSmartForm().smartFormDetails.push(smartFormDetail);
                    }
                    else if (source.data.dropFrom !== undefined && source.data.dropFrom() === "GroupCaption") {
                        smartFormDetail.objectType(1);
                        //smartFormDetail.html("<span><b>This is a very long long group caption which can be edited in line and can also be deleted. If deleted then the whole content below it will jump</b></span>");
                        selectedSmartForm().smartFormDetails.push(smartFormDetail);
                    }
                    else if (source.data.dropFrom !== undefined && source.data.dropFrom() === "LineSeperator") {
                        smartFormDetail.objectType(2);
                        smartFormDetail.html("<hr style=\"height:3px;border:none;color:#333;background-color:black;\" />");
                        //smartFormDetail.html("<div style=\"float:left\"><hr style=\"height:3px;border:none;color:#333;background-color:black;\" /></div><div><input type=\"button\" data-bind=\"click:$root.deleteSmartFormItem\"/></div>");
                        selectedSmartForm().smartFormDetails.push(smartFormDetail);
                    }
                }
            },

            //Remove Smart Form Item
            deleteSmartFormItem = function (formItem) {

                // Ask for confirmation
                confirmation.afterProceed(function () {
                    selectedSmartForm().smartFormDetails.remove(formItem);
                });
                confirmation.show();

            },
            //Save Smart Form
            onSaveSmartForm = function (smartForm) {
                if (doBeforeSaveSmartForm()) {
                    _.each(smartForm.smartFormDetails(), function (item, index) {
                        item.sortOrder(index + 1);
                    });
                    selectedSmartForm().companyId(selectedStore().companyId());
                    if (selectedStore().companyId() !== undefined) {
                        var smartFormServer = smartForm.convertToServerData(smartForm);
                        _.each(smartForm.smartFormDetails(), function (item) {
                            smartFormServer.SmartFormDetails.push(item.convertToServerData(item));
                        });
                        saveSmartForm(smartFormServer);

                    }
                    else if (smartForm.id() === undefined) {
                        smartForm.id(0);
                        smartForms.splice(0, 0, smartForm);
                        view.hideSmartFormDialog();
                    } else {
                        view.hideSmartFormDialog();
                    }
                }
            },
            saveSmartForm = function (smartForm) {
                dataservice.saveSmartForm(smartForm, {
                    success: function (data) {
                        if (selectedSmartForm().id() === undefined) {
                            selectedSmartForm().id(data);
                            smartForms.splice(0, 0, selectedSmartForm());
                        }
                        else {
                            _.each(smartForms(), function(smartFormitem) {
                                if (smartFormitem.id() == selectedSmartForm().id()) {
                                    smartFormitem.name(selectedSmartForm().name());
                                    smartFormitem.heading(selectedSmartForm().heading());
                                }
                            });
                        }
                        view.hideSmartFormDialog();
                        toastr.success("Successfully saved.");
                    },
                    error: function (exceptionMessage, exceptionType) {

                        if (exceptionType === ist.exceptionType.MPCGeneralException) {

                            toastr.error(exceptionMessage);

                        } else {

                            toastr.error("Failed to save.", "", ist.toastrOptions);
                        }

                    }
                });
            },
            //Do Before Save Smart Form
            doBeforeSaveSmartForm = function () {
                var flag = true;
                if (!selectedSmartForm().isValid()) {
                    selectedSmartForm().errors.showAllMessages();
                    flag = false;
                }
                return flag;
            },

            //Edit Smart Form
            onEditSmartForm = function (smartForm) {
                if (smartForm.id() === undefined || smartForm.id() === 0) {
                    //selectedSmartForm(smartForm);
                    smartFormEditorViewModel.selectItem(smartForm);
                    selectedSmartForm().reset();
                    view.showSmartFormDialog();
                } else {
                    getSmartFormDetail(smartForm);
                }
            },
            //Get Smart Forms        
            getSmartForms = function () {
                dataservice.getSmartFormsByCompanyId({
                    CompanyId: selectedStore().companyId(),
                    PageSize: smartFormPager().pageSize(),
                    PageNo: smartFormPager().currentPage(),
                    SortBy: sortOn(),
                    IsAsc: sortIsAsc()
                }, {
                    success: function (data) {

                        smartForms.removeAll();
                        _.each(data.SmartForms, function (item) {
                            var smartForm = model.SmartForm();
                            smartForm.id(item.SmartFormId);
                            smartForm.name(item.Name);
                            smartForm.heading(item.Heading);
                            smartForms.push(smartForm);
                        });

                        smartFormPager().totalCount(data.TotalCount);
                    },
                    error: function (response) {
                        toastr.error("Failed To Load Smart Forms.", "", ist.toastrOptions);
                    }
                });
            },
            //Get Smart Form Detail
            getSmartFormDetail = function (smartForm) {
                dataservice.getSmartFormDetailBySmartFormId({
                    smartFormId: smartForm.id(),
                }, {
                    success: function (data) {
                        if (data != null) {
                            smartForm.smartFormDetails.removeAll();
                            //selectedSmartForm(smartForm);
                            smartFormEditorViewModel.selectItem(smartForm);
                            _.each(data, function (item) {
                                var smartFormDetail = model.SmartFormDetail.Create(item);
                                if (item.ObjectType === 3) {
                                    var title = item.Title === null ? "" : item.Title, waterMark = (item.WaterMark === null || item.WaterMark === undefined) ? "" : item.WaterMark;
                                    if (item.VariableType === 1) {
                                        smartFormDetail.html("<div style=\"border:2px dotted silver;height:80px\"><div class=\"col-lg-12\"><div class=\"col-lg-6\"><label style=\"margin-left:9px;\">" + title + "</label><div class=\"col-lg-12\"><select disabled class=\"form-control\"><option>" + waterMark + "</option></select></div></div></div>");

                                    } else {
                                        smartFormDetail.html("<div style=\"border:2px dotted silver;height:80px\"><div class=\"col-lg-12\"><label style=\"margin-left:9px;\">" + title + "</label><div><input type=\"text\" disabled class=\"form-control\" value=\"" + waterMark + "\"></div></div></div>");
                                    }
                                } else if (item.ObjectType === 2) {
                                    smartFormDetail.html("<hr style=\"height:3px;border:none;color:#333;background-color:black;\" />");
                                }
                                selectedSmartForm().smartFormDetails.push(smartFormDetail);
                            });
                            selectedSmartForm().reset();
                            view.showSmartFormDialog();
                        }
                    },
                    error: function (response) {
                        toastr.error("Failed to load Detail.", "", ist.toastrOptions);
                    }
                });
            },
            //#endregion ________ Smart Form___________

            // Store Has Changes
            // ReSharper disable InconsistentNaming
            storeHasChanges = new ko.dirtyFlag({
                // ReSharper restore InconsistentNaming
                emails: emails,
                companyBannerSetList: companyBannerSetList,
                companyBanners: companyBanners,
                newAddedSecondaryPage: newAddedSecondaryPage,
                editedSecondaryPage: editedSecondaryPage,
                deletedSecondaryPage: deletedSecondaryPage,
                pageSkinWidgets: pageSkinWidgets,
                costCentersList: costCentersList,
                fieldVariablesOfStoreType: fieldVariablesOfStoreType,
                newAddedCampaigns: newAddedCampaigns,
                editedCampaigns: editedCampaigns,
                deletedCampaigns: deletedCampaigns,

            }),
            // Has Changes
            hasChangesOnStore = ko.computed(function () {
                return ((selectedStore() && selectedStore().hasChanges()) || storeHasChanges.isDirty());
            }),
            //Store workflow Image Files Loaded Callback
            storeWorkflowImageLoadedCallback = function (file, data) {
                //  selectedStore().storeWorkflowImageBinary(data);
                selectedStore().storeWorkflowImage(data);
            },
            //Store Map Image File Loaded Callback
            storeMapImageLoadedCallback = function (file, data) {
                selectedStore().mapImageUrlBinary(data);
            },
            //Initialize
            // ReSharper disable once AssignToImplicitGlobalInFunctionScope
            initialize = function (specifiedView) {
                view = specifiedView;
                ko.applyBindings(view.viewModel, view.bindingRoot);
                //ko.applyBindings(view.viewModel, document.getElementById('singleArea'));
                pager(new pagination.Pagination({ PageSize: 5 }, stores, getStores));
                getStores();
                getBaseDataFornewCompany();
                view.initializeForm();
            },
            // On Delete Store Permanently
            onDeletePermanent = function () {
                confirmation.afterProceed(function () {
                    deleteCompanyPermanently(selectedStore().companyId());
                });
                confirmation.show();
            },
            // Get Company By Id
            getCompanyByIdFromListView = function(id) {
                return stores.find(function(store) {
                    return store.companyId() === id;
                });
            },
            // Delete Company Permanently
            deleteCompanyPermanently = function (id) {
                dataservice.deleteCompanyPermanent({ CompanyId: id }, {
                    success: function () {
                        toastr.success("Store deleted successfully!");
                        isEditorVisible(false);
                        if (selectedStore()) {
                            var store = getCompanyByIdFromListView(selectedStore().companyId());
                            if (store) {
                                stores.remove(store);
                            }
                        }
                        resetStoreEditor();
                    },
                    error: function (response) {
                        toastr.error("Failed to delete store. Error: " + response, "", ist.toastrOptions);
                    }
                });
            };
                //#region _________R E T U R N_____________________

                return {
                    //storeProduct: storeProduct,
                    MultipleImageFilesLoadedCallback: MultipleImageFilesLoadedCallback,
                    SecondaryImageFileLoadedCallback: SecondaryImageFileLoadedCallback,
                    filteredCompanySetId: filteredCompanySetId,
                    stores: stores,
                    openReport: openReport,
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
                    getStoresByFilter: getStoresByFilter,
                    doBeforeSave: doBeforeSave,
                    saveStore: saveStore,
                    countries: countries,
                    states: states,
                    filteredStates: filteredStates,
                    //saveNewStore: saveNewStore,
                    //saveEdittedStore: saveEdittedStore,
                    openEditDialog: openEditDialog,
                    getStoreForEditting: getStoreForEditting,
                    closeEditDialog: closeEditDialog,
                    resetFilterSection: resetFilterSection,
                    isUserAndAddressesTabOpened: isUserAndAddressesTabOpened,
                    isBaseDataLoaded: isBaseDataLoaded,
                    openDomainInTab: openDomainInTab,
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
                    companyCMYKColoreditorViewModel: companyCmykColoreditorViewModel,
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
                    companyTerritoryFilterSelected: companyTerritoryFilterSelected,
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
                    searchCompanyTerritoryByFilter: searchCompanyTerritoryByFilter,
                    //#endregion Company territory
                    //#region Addresses
                    selectedAddress: selectedAddress,
                    deletedAddresses: deletedAddresses,
                    edittedAddresses: edittedAddresses,
                    newAddresses: newAddresses,
                    addressPager: addressPager,
                    searchAddressFilter: searchAddressFilter,
                    searchAddressByFilter: searchAddressByFilter,
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
                    userAndAddressesTabSelected: userAndAddressesTabSelected,
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
                    closeCompanyContact: closeCompanyContact,
                    selectedCompanyContact: selectedCompanyContact,
                    companyContactFilter: companyContactFilter,
                    deletedCompanyContacts: deletedCompanyContacts,
                    edittedCompanyContacts: edittedCompanyContacts,
                    newCompanyContacts: newCompanyContacts,
                    //contactCompanyPager: contactCompanyPager,
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
                    searchCompanyContactByFilter: searchCompanyContactByFilter,
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
                    setProductCategoryDirtyFlag: setProductCategoryDirtyFlag,
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
                    selectedCsvFileForCompanyContact: selectedCsvFileForCompanyContact,
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
                    //storeProductsViewModel: storeProductsViewModel,
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
                    errorList: errorList,
                    gotoElement: gotoElement,
                    productError: productError,
                    onAddVariableDefination: onAddVariableDefination,
                    contextTypes: contextTypes,
                    varibaleTypes: varibaleTypes,
                    selectedFieldVariable: selectedFieldVariable,
                    onSaveFieldVariable: onSaveFieldVariable,
                    fieldVariables: fieldVariables,
                    onEditFieldVariable: onEditFieldVariable,
                    selectedFieldOption: selectedFieldOption,
                    // selectFieldOption: selectFieldOption,
                    templateToUseForVariableOption: templateToUseForVariableOption,
                    onEditVariableOption: onEditVariableOption,
                    onDeleteVariableOption: onDeleteVariableOption,
                    onAddFieldOption: onAddFieldOption,
                    fieldVariablePager: fieldVariablePager,
                    getCompanyContactVariables: getCompanyContactVariables,
                    onVariableOptionDropDownChange: onVariableOptionDropDownChange,
                    fieldVariablesOfContactType: fieldVariablesOfContactType,
                    fieldVariablesForSmartForm: fieldVariablesForSmartForm,
                    addSmartForm: addSmartForm,
                    draggedVariableField: draggedVariableField,
                    draggedGroupCaption: draggedGroupCaption,
                    groupCaption: groupCaption,
                    draggedLineSeperator: draggedLineSeperator,
                    lineSeperator: lineSeperator,
                    selectedSmartForm: selectedSmartForm,
                    droppedSmartFormArea: droppedSmartFormArea,
                    deleteSmartFormItem: deleteSmartFormItem,
                    onSaveSmartForm: onSaveSmartForm,
                    productsFilterHeading: productsFilterHeading,
                    productsFilterSubHeadingAll: productsFilterSubHeadingAll,
                    productsFilterSubHeadingSelected: productsFilterSubHeadingSelected,
                    cmsPagesBaseData: cmsPagesBaseData,
                    smartForms: smartForms,
                    onEditSmartForm: onEditSmartForm,
                    secondayPageIsDisplayInFooterHandler: secondayPageIsDisplayInFooterHandler,
                    smartFormPager: smartFormPager,
                    userCount: userCount,
                    orderCount: orderCount,
                    onChangeBannerSet: onChangeBannerSet,
                    themes: themes,
                    productCategoryTitle: productCategoryTitle,
                    ckEditorOpenFrom: ckEditorOpenFrom,
                    storeWorkflowImageLoadedCallback: storeWorkflowImageLoadedCallback,
                    selectedTheme: selectedTheme,
                    onApplyTheme: onApplyTheme,
                    storeStatus: storeStatus,
                    storeHeading: storeHeading,
                    productStatus: productStatus,
                    storeHasChanges: storeHasChanges,
                    hasChangesOnStore: hasChangesOnStore,
                    calculateTaxByServiceHandler: calculateTaxByServiceHandler,
                    vatHandler: vatHandler,
                    storeMapImageLoadedCallback: storeMapImageLoadedCallback,
                    fieldVariablesOfAddressType: fieldVariablesOfAddressType,
                    fieldVariablesOfTerritoryType: fieldVariablesOfTerritoryType,
                    fieldVariablesOfStoreType: fieldVariablesOfStoreType,
                    getScopeVariables: getScopeVariables,
                    systemPagePager: systemPagePager,
                    getSystemPages: getSystemPages,
                    selectChildProductCategory: selectChildProductCategory,
                    onArchiveCategory: onArchiveCategory,
                    priceFlags: priceFlags,
                    onCreatePublicStore: onCreatePublicStore,
                    onCreatePrivateStore: onCreatePrivateStore,
                    onDeletePermanent: onDeletePermanent,
                    selectTheme: selectTheme,
                    selectedThemeName: selectedThemeName,
                    onDeleteMedia: onDeleteMedia,
                    defaultSortBy: defaultSortBy,
                    currencySymbol: currencySymbol,
                    calculateCyanValue: calculateCyanValue,
                    calculateBlackValue: calculateBlackValue,
                    calculateYellowValue: calculateYellowValue,
                    calculateMagentaValue: calculateMagentaValue,
                    selectedHexValue: selectedHexValue,
                    onRemoveFieldVariable: onRemoveFieldVariable,
                    paymentGatewayFilter: paymentGatewayFilter,
                    onSearchpaymentMethod: onSearchpaymentMethod,
                    selectedCategoryName: selectedCategoryName

                };
                //#endregion
            })()
        };
        return ist.stores.viewModel;
    });

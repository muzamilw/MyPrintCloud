/*
    Module with the view model for the Product.
*/
define("product/product.viewModel",
    ["jquery", "amplify", "ko", "product/product.dataservice", "product/product.model", "common/pagination", "common/confirmation.viewModel",
        "common/phraseLibrary.viewModel", "common/sharedNavigation.viewModel", "common/stockItem.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, phraseLibrary, shared, stockDialog) {
        var ist = window.ist || {};
        ist.product = {
            viewModel: (function () {
                var // the view 
                    view,
                    // #region Arrays
                    // Items
                    products = ko.observableArray([]),
                    // Items to Relate
                    productsToRelate = ko.observableArray([]),
                    // Error List
                    errorList = ko.observableArray([]),
                    // Press Items
                    pressItems = ko.observableArray([]),
                    // Cost Centres
                    costCentres = ko.observableArray([]),
                    // Section Flags
                    sectionFlags = ko.observableArray([]),
                    // Countries
                    countries = ko.observableArray([]),
                    // States
                    states = ko.observableArray([]),
                    // Suppliers
                    suppliers = ko.observableArray([]),
                    // Product Categories
                    productCategories = ko.observableArray([]),
                    // Parent Product Categories
                    parentProductCategories = ko.computed(function () {
                        if (!productCategories) {
                            return [];
                        }

                        return productCategories.filter(function (productCategory) {
                            return !productCategory.parentCategoryId;
                        });
                    }),
                    // Template Categories
                    templateCategories = ko.observableArray([]),
                    // Category Regions
                    categoryRegions = ko.observableArray([]),
                    // Category Types
                    categoryTypes = ko.observableArray([]),
                    // Paper Sizes
                    paperSizes = ko.observableArray([]),
                    // Currency Unit fOr Organisation 
                    currencyUnit = ko.observable(),
                    // Length Unit fOr Organisation 
                    lengthUnit = ko.observable(),
                    // Selected Region Id
                    selectedRegionId = ko.observable(),
                    // Selected Category Type Id
                    selectedCategoryTypeId = ko.observable(),
                    // Selected Category
                    selectedDesignerCategory = ko.observable(),
                    // Available Template Categories
                    availableCategoriesForTemplate = ko.computed(function () {
                        if (!templateCategories) {
                            return [];
                        }

                        return templateCategories.filter(function (productCategory) {
                            return (!selectedRegionId() || productCategory.regionId === selectedRegionId()) &&
                                (!selectedCategoryTypeId() || productCategory.typeId === selectedCategoryTypeId());
                        });
                    }),
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is List View Active
                    isListViewVisible = ko.observable(false),
                    // Is Grid View Active
                    isGridViewVisible = ko.observable(true),
                    // Is Product Editor Visible
                    isProductDetailsVisible = ko.observable(false),
                    // Is Designer Category Base Data Loaded
                    isDesignerCategoryBaseDataLoaded = ko.observable(false),
                    // #endregion Busy Indicators
                    // #region Observables
                    // Item File Types
                    itemFileTypes = {
                        thumbnail: 1,
                        grid: 2,
                        imagePath: 3,
                        file1: 4
                    },
                    // filter
                    filterText = ko.observable(),
                    // filter for Related Items
                    filterTextForRelatedItems = ko.observable(),
                    // Active Product
                    selectedProduct = ko.observable(),
                    // Page Header 
                    pageHeader = ko.computed(function () {
                        return selectedProduct() && selectedProduct().productName() ? selectedProduct().productName() : 'Products';
                    }),
                    // Sort On
                    sortOn = ko.observable(1),
                    // Sort Order -  true means asc, false means desc
                    sortIsAsc = ko.observable(true),
                    // Pagination
                    pager = ko.observable(new pagination.Pagination({ PageSize: 5 }, products)),
                    // Pagination For Item Relater Dialog
                    itemRelaterPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, productsToRelate)),
                    // Pagination For Press Dialog
                    pressDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, pressItems)),
                    // Current Page - Editable
                    currentPageCustom = ko.computed({
                        read: function () {
                            return pager().currentPage();
                        },
                        write: function (value) {
                            if (!value) {
                                return;
                            }
                            var page = parseInt(value);
                            if (page === pager().currentPage() || !pager().isPageValid(page)) {
                                return;
                            }
                            pager().currentPage(page);
                            getItems();
                        }
                    }),
                    // Item Actions
                    itemActions = {
                        onSaveVideo: function () {
                            closeVideoDialog();
                        },
                        onSelectRelatedItem: function () {
                            closeRelatedItemDialog();
                        },
                        onChooseStockItem: function (stockCategoryId) {
                            openStockItemDialog(stockCategoryId);
                        },
                        onSelectStockItem: function () {
                        },
                        onUpdateItemAddonCostCentre: function () {
                            openItemAddonCostCentreDialog();
                        },
                        onSaveItemAddonCostCentre: function () {
                            closeItemAddonCostCentreDialog();
                        },
                        onCostCentreChange: function (costCentreId, activeItemAddonCostCentre) {
                            setCostCentreToActiveItemAddonCostCentre(costCentreId, activeItemAddonCostCentre);
                        },
                        onFlagChange: function (flagId, itemId) {
                            getItemPriceMatricesForItemByFlag(flagId, itemId);
                        },
                        onPreBuiltTemplateSelected: function() {
                            selectPreBuiltTemplate();
                        },
                        onSelectPressItem: function () {
                            closePressDialog();
                        }
                    },
                    // Item State Tax Constructor Params
                    itemStateTaxConstructorParams = { countries: countries(), states: states() },
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    // press Dialog Filter
                    pressDialogFilter = ko.observable(),
                    //#endregion
                    // #region Utility Functions
                    toggleView = function (data, e) {
                        view.changeView(e);
                    },
                    // Set List View Active
                    setListViewActive = function () {
                        isListViewVisible(true);
                        isGridViewVisible(false);
                    },
                    // Set Grid View Active
                    setGridViewActive = function () {
                        isListViewVisible(false);
                        isGridViewVisible(true);
                    },
                    // Create New Product
                    createProduct = function () {
                        selectedProduct(model.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                        openProductEditor();
                    },
                    // Edit Product
                    editProduct = function (data) {
                        getItemById(data.id(), openProductEditor);
                    },
                    // Select Designer Category for Product
                    selectDesignerCategoryForProduct = function() {
                        // Get Designer Category From list
                        if (selectedProduct().templateTypeUi() !== '3' || !selectedProduct().designerCategoryId()) {
                            return;
                        }

                        var designerCategory = getDesignerCategoryById(selectedProduct().designerCategoryId());
                        if (!designerCategory) {
                            return;
                        }

                        selectedRegionId(designerCategory.regionId);
                        selectedCategoryTypeId(designerCategory.typeId);
                        selectedDesignerCategory(selectedProduct().designerCategoryId());
                    },
                    // Open Editor
                    openProductEditor = function () {
                        // Show Basic Details Tab
                        view.showBasicDetailsTab();
                        // Show Details
                        isProductDetailsVisible(true);
                        
                        // WIre up tab shown event
                        view.wireUpTabShownEvent();
                        
                        // Wire Up Navigation Control - If has Changes and user navigates to another page
                        shared.initialize(selectedProduct, function(navigateCallback) {
                            onSaveProduct(null, null, navigateCallback);
                        });
                        
                        // Initialize Label Popovers
                        view.initializeLabelPopovers();
                    },
                    // Initialize Product Category Dialog
                    initializeProductCategoryDialog = function() {
                        // Set Product Category true/false for popup
                        productCategories.each(function (productCategory) {
                            var productCategoryItem = selectedProduct().productCategoryItems.find(function (pci) {
                                return pci.categoryId() === productCategory.id;
                            });

                            if (productCategoryItem) {
                                productCategory.isSelected(true);
                            }
                            else {
                                productCategory.isSelected(false);
                            }
                        });
                        
                        // Update Input Checked States in Bindings
                        view.updateInputCheckedStates();
                    },
                    // On Close Editor
                    onCloseProductEditor = function () {
                        if (selectedProduct().hasChanges()) {
                            confirmation.messageText("Do you want to save changes?");
                            confirmation.afterProceed(onSaveProduct);
                            confirmation.afterCancel(function () {
                                selectedProduct().reset();
                                closeProductEditor();
                            });
                            confirmation.show();
                            return;
                        }
                        closeProductEditor();
                    },
                    // Close Editor
                    closeProductEditor = function () {
                        selectedProduct(model.Item.Create({}, itemActions, itemStateTaxConstructorParams));
                        resetVideoCounter();
                        isProductDetailsVisible(false);
                        selectedDesignerCategory(undefined);
                        selectedRegionId(undefined);
                        selectedCategoryTypeId(undefined);
                        errorList.removeAll();
                        shared.reset();
                        selectedProduct(undefined);
                    },
                    // On Archive
                    onArchiveProduct = function (item) {
                        confirmation.afterProceed(function () {
                            archiveProduct(item.id());
                        });
                        confirmation.show();
                    },
                    // Active Video Item
                    activeVideo = ko.observable(),
                    // Added Video Counter
                    videoCounter = -1,
                    // Reset Video Counter
                    resetVideoCounter = function () {
                        videoCounter = -1;
                    },
                    // On Add Video
                    onAddVideo = function () {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(model.ItemVideo.Create({ VideoId: 0, ItemId: selectedProduct().id() }));
                    },
                    onEditVideo = function (itemVideo) {
                        // Open Video Dialog  
                        openVideoDialog();
                        activeVideo(itemVideo);
                    },
                    // Save Video
                    saveVideo = function () {
                        if (activeVideo().id() === 0) { // Add
                            activeVideo().id(videoCounter);
                            selectedProduct().addVideo(activeVideo());
                            videoCounter -= 1;
                            return;
                        }
                        closeVideoDialog();
                    },
                    // Open Video Dialog
                    openVideoDialog = function () {
                        // Reset Current Video Item
                        activeVideo(model.ItemVideo.Create({}));
                        view.showVideoDialog();
                    },
                    // Close Video Dialog
                    closeVideoDialog = function () {
                        view.hideVideoDialog();
                    },
                    // On Add Related Item
                    onAddRelatedItem = function () {
                        // Reset Filter & Pager
                        filterTextForRelatedItems(undefined);
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items 
                        getItemsToRelate(openRelatedItemDialog);
                    },
                    // Open Related Item Dialog
                    openRelatedItemDialog = function () {
                        view.showRelatedItemDialog();
                    },
                    // Close Related Item Dialog
                    closeRelatedItemDialog = function () {
                        view.hideRelatedItemDialog();
                    },
                    // Select Job Description
                    selectJobDescription = function (jobDescription, e) {
                        selectedJobDescription(e.currentTarget.id);
                    },
                    // Open Stock Item Dialog
                    openStockItemDialog = function (stockCategoryId) {
                        stockDialog.show(function(stockItem) {
                            selectedProduct().onSelectStockItem(stockItem);
                        }, stockCategoryId, false);
                    },
                    // Search Press Items
                    searchPressItems = function () {
                        pressDialogPager().reset();
                        getPressItems();
                    },
                    // Reset Stock Items
                    resetPressItems = function () {
                        // Reset Text 
                        resetPressDialogFilter();
                        // Filter Record
                        searchPressItems();
                    },
                    // Reset Press Dialog Filter
                    resetPressDialogFilter = function() {
                        // Reset Text 
                        pressDialogFilter(undefined);
                    },
                    // Open Stock Item Dialog
                    openPressDialog = function () {
                        resetPressDialogFilter();
                        view.showPressDialog();
                        searchPressItems();
                    },
                    // Close Stock Item Dialog
                    closePressDialog = function () {
                        view.hidePressDialog();
                    },
                    // Edit Item Section
                    editSectionSignature = function (itemSection) {
                        selectedProduct().selectItemSection(itemSection);
                        openSignatureDialog();
                    },
                    // Open Signature Dialog
                    openSignatureDialog = function() {
                        view.showSignatureDialog();
                    },
                    // Close Signature Dialog
                    closeSignatureDialog = function () {
                        view.hideSignatureDialog();
                    },
                    // Open Item Addon Cost Centre Dialog
                    openItemAddonCostCentreDialog = function () {
                        view.showItemAddonCostCentreDialog();
                    },
                    // Close Item Addon Cost Centre Dialog
                    closeItemAddonCostCentreDialog = function () {
                        view.hideItemAddonCostCentreDialog();
                    },
                    // Get Cost Centre By Id
                    getCostCentreById = function (id) {
                        if (costCentres().length === 0) {
                            return null;
                        }

                        return costCentres.find(function (costCentre) {
                            return costCentre.id === id;
                        });
                    },
                    // Set Cost Centre to active Item Add on cost centre
                    setCostCentreToActiveItemAddonCostCentre = function (costCentreId, activeItemAddonCostCentre) {
                        var costCentre = getCostCentreById(costCentreId);

                        if (!costCentre) {
                            return;
                        }

                        activeItemAddonCostCentre.costCentreName(costCentre.name);
                        activeItemAddonCostCentre.costCentreType(costCentre.type);
                    },
                    // open Product Category Dialog
                    openProductCategoryDialog = function () {
                        getProductCategories(selectedCompany(), function() {
                            initializeProductCategoryDialog();
                            view.showProductCategoryDialog();
                        });
                    },
                    // open Product Category Dialog
                    closeProductCategoryDialog = function () {
                        view.hideProductCategoryDialog();
                    },
                    // Toggle Child Categories
                    toggleChildCategories = function (data, event) {
                        // If Child Categories exist then don't send call
                        if (view.toggleChildCategories(event)) {
                            return;
                        }
                        var categoryId = view.getCategoryIdFromElement(event);
                        getChildCategories(categoryId, event);
                    },
                    // Update Product Categories to Selected Product
                    updateProductCategories = function () {
                        selectedProduct().updateProductCategoryItems(productCategories());
                        view.hideProductCategoryDialog();
                    },
                    // update Checked state for category
                    updateCheckedStateForCategory = function (data, event) {
                        var categoryId = view.getCategoryIdFromElement(event);
                        // get category by id
                        var productCategory = productCategories.find(function (pcat) {
                            return pcat.id === categoryId;
                        });

                        if (!productCategory) {
                            return false;
                        }

                        if ($(event.target).is(':checked')) {
                            productCategory.isSelected(true);
                        }
                        else {
                            productCategory.isSelected(false);
                        }

                        return true;
                    },
                    // Open Phrase Library
                    openPhraseLibrary = function () {
                        phraseLibrary.show(function(phrase) {
                            updateJobDescription(phrase);
                        });
                    },
                    // Update Job Description
                    updateJobDescription = function (phrase) {
                        if (!phrase) {
                            return;
                        }

                        // Set Phrase to selected Job Description
                        if (selectedJobDescription() === 'txtDescription1') {
                            selectedProduct().jobDescription1(selectedProduct().jobDescription1() ? selectedProduct().jobDescription1() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription2') {
                            selectedProduct().jobDescription2(selectedProduct().jobDescription2() ? selectedProduct().jobDescription2() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription3') {
                            selectedProduct().jobDescription3(selectedProduct().jobDescription3() ? selectedProduct().jobDescription3() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription4') {
                            selectedProduct().jobDescription4(selectedProduct().jobDescription4() ? selectedProduct().jobDescription4() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription5') {
                            selectedProduct().jobDescription5(selectedProduct().jobDescription5() ? selectedProduct().jobDescription5() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription6') {
                            selectedProduct().jobDescription6(selectedProduct().jobDescription6() ? selectedProduct().jobDescription6() + ' ' + phrase : phrase);
                        }
                        else if (selectedJobDescription() === 'txtDescription7') {
                            selectedProduct().jobDescription7(selectedProduct().jobDescription7() ? selectedProduct().jobDescription7() + ' ' + phrase : phrase);
                        }
                    },
                    // ON Pre-Build Template Option Selected 
                    selectPreBuiltTemplate = function() {
                        confirmation.messageText("Do you want to keep existing Template Objects?");
                        confirmation.afterProceed(function() {
                            selectedProduct().templateTypeMode(2);
                        });
                        confirmation.afterCancel(function () {
                            selectedProduct().templateTypeMode(1);
                        });
                        confirmation.show();
                    },
                    // Get Designer Category by Id
                    getDesignerCategoryById = function(categoryId) {
                        return _.find(availableCategoriesForTemplate(), function (category) {
                            return category.id === categoryId;
                        });
                    },
                    // Edit Template
                    editTemplate = function(product) {
                        view.editTemplate(product);
                    },
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 5 }, products, getItems));

                        itemRelaterPager(new pagination.Pagination({ PageSize: 5 }, productsToRelate, getItemsToRelate));
                        
                        pressDialogPager(new pagination.Pagination({ PageSize: 5 }, pressItems, getPressItems));

                        // Get Base Data
                        getBaseData();

                        // Get Items
                        getItems();

                        // Set Open From Flag to false - so that popup don't show until button gets clicked
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                        
                        // Subscribe Designer Category Selection
                        selectedDesignerCategory.subscribe(function(value) {
                            if (!selectedProduct()) {
                                return;
                            }
                            
                            if (value === selectedProduct().designerCategoryId()) {
                                return;
                            }

                            selectedProduct().designerCategoryId(value);
                            
                            // Get Designer Category From list
                            var designerCategory = getDesignerCategoryById(value);

                            if (!designerCategory) {
                                return;
                            }
                            
                            // Set Zoom Factor and Scalar default
                            selectedProduct().zoomFactor(designerCategory.zoomFactor);
                            selectedProduct().scalar(designerCategory.scalarFactor);
                        });
                    },
                    // #region For Store
                    // Selected Company Id
                    selectedCompany = ko.observable(),
                    // Initialize the view model from Store
                    initializeForStore = function(companyId) {
                        if (selectedCompany() !== companyId) {
                            selectedCompany(companyId);
                        }

                        var productDetailBinding = $("#productDetailBinding")[0];
                        var productBinding = $("#productBinding")[0];
                        setTimeout(function () {
                            ko.cleanNode(productBinding);
                            ko.cleanNode(productDetailBinding);
                            ko.applyBindings(view.viewModel, productBinding);
                            ko.applyBindings(view.viewModel, productDetailBinding);
                        }, 1000);
                        
                        // Get Items for Store
                        resetFilter();
                    },
                    // #endregion
                    // Map Products 
                    mapProducts = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Item.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(products(), itemsList);
                        products.valueHasMutated();
                    },
                    // Map Products To Relate
                    mapProductsToRelate = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            if (item.ItemId !== selectedProduct().id()) { // Can't Relate Product with itself
                                itemsList.push(model.ItemRelatedItem.Create({
                                    RelatedItemId: item.ItemId,
                                    RelatedItemName: item.ProductName,
                                    RelatedItemCode: item.ProductCode
                                }));
                            }
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(productsToRelate(), itemsList);
                        productsToRelate.valueHasMutated();
                    },
                    // Map Press Items 
                    mapPressItems = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Machine.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(pressItems(), itemsList);
                        pressItems.valueHasMutated();
                    },
                    // Filter Products to Relate
                    filterProductsToRelate = function () {
                        // Reset Pager
                        itemRelaterPager().reset();
                        // Get Items to Relate
                        getItemsToRelate();
                    },
                    // Reset Filtered Products
                    resetFilteredProducts = function () {
                        // Reset Text 
                        filterTextForRelatedItems(undefined);
                        // Filter Record
                        filterProductsToRelate();
                    },
                    // Filter Products
                    filterProducts = function () {
                        // Reset Pager
                        pager().reset();
                        // Get Items
                        getItems();
                    },
                    // Reset Filter
                    resetFilter = function () {
                        // Reset Text 
                        filterText(undefined);
                        // Filter Record
                        filterProducts();
                    },
                    // On Save Product
                    onSaveProduct = function (data, event, navigateCallback) {
                        if (!doBeforeSave()) {
                            return;
                        }

                        var callback =  closeProductEditor;
                        if (selectedProduct().isFinishedGoodsUi() === '1') {
                            callback = function () {
                                promptForDesigner(selectedProduct());
                                closeProductEditor();
                            };
                        }
                        saveProduct(callback, navigateCallback);
                    },
                    // Prompt for Designer
                    promptForDesigner = function(product) {
                        confirmation.messageText("Do you want to open designer?");
                        confirmation.afterProceed(function() {
                            editTemplate(product);
                        });
                        confirmation.afterCancel();
                        confirmation.show();
                    },
                    // Do Before Save
                    doBeforeSave = function () {
                        var flag = true;
                        if (!selectedProduct().isValid()) {
                            selectedProduct().showAllErrors();
                            selectedProduct().setValidationSummary(errorList);
                            flag = false;
                        }
                        return flag;
                    },
                    // On Clone Product
                    onCloneProduct = function(data) {
                        cloneProduct(data, openProductEditor);
                    },
                    // Go To Element
                    gotoElement = function (validation) {
                        view.gotoElement(validation.element);
                    },
                    // Get Item From List
                    getItemFromList = function (id) {
                        return products.find(function (item) {
                            return item.id() === id;
                        });
                    },
                    // Map Cost Centres
                    mapCostCentres = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CostCentre.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(costCentres(), itemsList);
                        costCentres.valueHasMutated();
                    },
                    // Map Countreis
                    mapCountries = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Country.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(countries(), itemsList);
                        countries.valueHasMutated();
                    },
                    // Map State
                    mapStates = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.State.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(states(), itemsList);
                        states.valueHasMutated();
                    },
                    // Map Section Flags
                    mapSectionFlags = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.SectionFlag.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(sectionFlags(), itemsList);
                        sectionFlags.valueHasMutated();
                    },
                    // Map Suppliers
                    mapSuppliers = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.Company.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(suppliers(), itemsList);
                        suppliers.valueHasMutated();
                    },
                    // Map Product Categories
                    mapProductCategories = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.ProductCategory.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(productCategories(), itemsList);
                        productCategories.valueHasMutated();
                    },
                    // Map Designer Categories
                    mapDesignerCategories = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.ProductCategoryForTemplate.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(templateCategories(), itemsList);
                        templateCategories.valueHasMutated();
                    },
                    // Map Category Regions
                    mapCategoryRegions = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CategoryRegion.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(categoryRegions(), itemsList);
                        categoryRegions.valueHasMutated();
                    },
                    // Map Category Types
                    mapCategoryTypes = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.CategoryType.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(categoryTypes(), itemsList);
                        categoryTypes.valueHasMutated();
                    },
                    // Map Paper Sizes
                    mapPaperSizes = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.PaperSize.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(paperSizes(), itemsList);
                        paperSizes.valueHasMutated();
                    },
                    // Set Item Price Matrices to Current Item against selected Flag
                    setItemPriceMatricesToItem = function (itemPriceMatrices) {
                        // Only ask for confirmation if it is not a new product
                        if ((!itemPriceMatrices || itemPriceMatrices.length === 0) && selectedProduct().id()) {
                            confirmation.messageText("There are no price items against this flag. Do you want to Add New?");
                            confirmation.afterProceed(selectedProduct().setItemPriceMatrices);
                            confirmation.afterCancel(selectedProduct().removeExistingPriceMatrices);
                            confirmation.show();
                            return;
                        }

                        // Set Price Matrix to Item against selected Flag
                        selectedProduct().setItemPriceMatrices(itemPriceMatrices);
                    },
                    // Get Item From list by id
                    getItemByIdLocal = function(id) {
                        return products.find(function (item) {
                            return item.id() === id;
                        });
                    },
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                costCentres.removeAll();
                                countries.removeAll();
                                states.removeAll();
                                sectionFlags.removeAll();
                                suppliers.removeAll();
                                templateCategories.removeAll();
                                categoryRegions.removeAll();
                                categoryTypes.removeAll();
                                paperSizes.removeAll();
                                lengthUnit(undefined);
                                currencyUnit(undefined);
                                if (data) {
                                    mapCostCentres(data.CostCentres);

                                    // Map Countries
                                    mapCountries(data.Countries);

                                    // Map States
                                    mapStates(data.States);

                                    // Map Section Flags
                                    mapSectionFlags(data.SectionFlags);

                                    // Map Suppliers
                                    mapSuppliers(data.Suppliers);
                                    
                                    // Map Product Categories
                                    mapDesignerCategories(data.TemplateCategories);
                                    
                                    // Map Category Regions
                                    mapCategoryRegions(data.CategoryRegions);
                                    
                                    // Map Category Types
                                    mapCategoryTypes(data.CategoryTypes);
                                    
                                    // Map Paper Sizes
                                    mapPaperSizes(data.PaperSizes);
                                    
                                    // Map Units
                                    lengthUnit(data.LengthUnit || undefined);
                                    currencyUnit(data.CurrencyUnit || undefined);

                                    // Assign countries & states to StateTaxConstructorParam
                                    itemStateTaxConstructorParams.countries = countries();
                                    itemStateTaxConstructorParams.states = states();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data" + response);
                            }
                        });
                    },
                    // Get Base Data For Designer Category
                    getBaseDataForDesignerCategory = function () {
                        if (isDesignerCategoryBaseDataLoaded()) {
                            // Set Designer Category if Active Product has Template Type = 3
                            // Update Category Region, Type and Designer Category
                            selectDesignerCategoryForProduct();
                            return;
                        }
                        
                        dataservice.getBaseDataForDesignerCategory({
                            success: function (data) {
                                templateCategories.removeAll();
                                categoryRegions.removeAll();
                                categoryTypes.removeAll();
                                if (data) {
                                    // Map Product Categories
                                    mapDesignerCategories(data.TemplateCategories);

                                    // Map Category Regions
                                    mapCategoryRegions(data.CategoryRegions);

                                    // Map Category Types
                                    mapCategoryTypes(data.CategoryTypes);
                                }

                                isDesignerCategoryBaseDataLoaded(true);
                                
                                // Set Designer Category if Active Product has Template Type = 3
                                // Update Category Region, Type and Designer Category
                                selectDesignerCategoryForProduct();
                            },
                            error: function (response) {
                                toastr.error("Failed to load base data for Designer" + response);
                            }
                        });
                    },
                    // Save Product
                    saveProduct = function (callback, navigateCallback) {
                        var product = selectedProduct().convertToServerData();
                        // If opened from store
                        if (selectedCompany()) {
                            product.CompanyId = selectedCompany();
                        }
                        
                        dataservice.saveItem(product, {
                            success: function (data) {
                                if (!selectedProduct().id()) {
                                    // Update Id
                                    selectedProduct().id(data.ItemId);

                                    // Update Min Price
                                    selectedProduct().miniPrice(data.MinPrice || 0);

                                    // Add to top of list
                                    products.splice(0, 0, selectedProduct());
                                }
                                else {
                                    // Get Item
                                    var item = getItemFromList(selectedProduct().id());
                                    if (item) {
                                        item.productCode(data.ProductCode);
                                        item.productName(data.ProductName);
                                        item.isEnabled(data.IsEnabled);
                                        item.isPublished(data.IsPublished);
                                        item.miniPrice(data.MinPrice || 0);
                                    }
                                }

                                toastr.success("Saved Successfully.");

                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                                
                                if (navigateCallback && typeof navigateCallback === "function") {
                                    navigateCallback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Product. Error: " + response);
                            }
                        });
                    },
                    // Clone Product
                    cloneProduct = function (item, callback) {
                        dataservice.cloneItem({ ItemId: item.id() }, {
                            success: function (data) {
                                if (data) {
                                    var newItem = model.Item.Create(data, itemActions, itemStateTaxConstructorParams);
                                    // Add to top of list
                                    products.splice(0, 0, newItem);
                                    selectedProduct(newItem);
                                    
                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }

                                toastr.success("Cloned Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to Clone Product. Error: " + response);
                            }
                        });
                    },
                    // archive Product
                    archiveProduct = function (id) {
                        dataservice.archiveItem({
                            ItemId: id
                        }, {
                            success: function () {
                                // Remove that product from list
                                var item = getItemByIdLocal(id);
                                if (item) {
                                    items.remove(item);
                                }
                                toastr.success("Archived Successfully.");
                            },
                            error: function (response) {
                                toastr.error("Failed to archive Product. Error: " + response);
                            }
                        });
                    },
                    // Get Item Price Matrices for Item By Flag
                    getItemPriceMatricesForItemByFlag = function (flagId, itemId) {
                        dataservice.getItemPriceMatricesForItemByFlagId({
                            FlagId: flagId,
                            ItemId: itemId
                        }, {
                            success: function (data) {
                                setItemPriceMatricesToItem(data);
                            },
                            error: function (response) {
                                toastr.error("Failed to load item price matrix" + response);
                            }
                        });
                    },
                    // Get Press Items
                    getPressItems = function () {
                        dataservice.getMachines({
                            SearchString: pressDialogFilter(),
                            PageSize: pressDialogPager().pageSize(),
                            PageNo: pressDialogPager().currentPage()
                        }, {
                            success: function (data) {
                                pressItems.removeAll();
                                if (data && data.TotalCount > 0) {
                                    pressDialogPager().totalCount(data.TotalCount);
                                    mapPressItems(data.Machines);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load Press items" + response);
                            }
                        });
                    },
                    // Get Items
                    getItems = function () {
                        isLoadingProducts(true);
                        dataservice.getItems({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage(),
                            CompanyId: selectedCompany()
                        }, {
                            success: function (data) {
                                products.removeAll();
                                if (data && data.TotalCount > 0) {
                                    pager().totalCount(data.TotalCount);
                                    mapProducts(data.Items);
                                }
                                isLoadingProducts(false);
                                view.initializeProductMinMaxSlider();
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load items" + response);
                                view.initializeProductMinMaxSlider();
                            }
                        });
                    },
                    // Get Items To Relate
                    getItemsToRelate = function (callback) {
                        dataservice.getItems({
                            SearchString: filterTextForRelatedItems(),
                            PageSize: itemRelaterPager().pageSize(),
                            PageNo: itemRelaterPager().currentPage()
                        }, {
                            success: function (data) {
                                productsToRelate.removeAll();
                                if (data && data.TotalCount > 0) {
                                    itemRelaterPager().totalCount(data.TotalCount);
                                    mapProductsToRelate(data.Items);
                                }
                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load items" + response);
                            }
                        });
                    },
                    // Get Item By Id
                    getItemById = function (id, callback) {
                        isLoadingProducts(true);
                        dataservice.getItem({
                            id: id
                        }, {
                            success: function (data) {
                                if (data) {
                                    selectedProduct(model.Item.Create(data, itemActions, itemStateTaxConstructorParams));

                                    if (callback && typeof callback === "function") {
                                        callback();
                                    }
                                }
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load item details" + response);
                            }
                        });
                    },
                    // Get Category Child List Items
                    getChildCategories = function (id, event) {
                        dataservice.getProductCategoryChilds({
                            id: id,
                        }, {
                            success: function (data) {
                                if (data.ProductCategories != null) {
                                    _.each(data.ProductCategories, function (productCategory) {
                                        productCategory.ParentCategoryId = id;
                                        var category = model.ProductCategory.Create(productCategory);
                                        if (selectedProduct()) {
                                            var productCategoryItem = selectedProduct().productCategoryItems.find(function (pCatItem) {
                                                return pCatItem.categoryId() === category.id;
                                            });

                                            if (productCategoryItem) {
                                                category.isSelected(true);
                                            }
                                        }
                                        productCategories.push(category);
                                        view.appendChildCategory(event, category);
                                        initializeProductCategoryDialog();
                                    });
                                }
                            },
                            error: function (response) {
                                isLoadingStores(false);
                                toastr.error("Error: Failed To load Categories " + response);
                            }
                        });
                    },
                    // Get Product Categories
                    getProductCategories = function (id, callback) {
                        dataservice.getProductCategories({
                            id: id ? id : 0,
                        }, {
                            success: function (data) {
                                productCategories.removeAll();
                                if (data != null) {
                                    // Map Product Categories
                                    mapProductCategories(data);
                                }

                                if (callback && typeof callback === "function") {
                                    callback();
                                }
                            },
                            error: function (response) {
                                toastr.error("Error: Failed To load Categories " + response);
                            }
                        });
                    };
                // #endregion Service Calls

                return {
                    // Observables
                    selectedProduct: selectedProduct,
                    sortOn: sortOn,
                    sortIsAsc: sortIsAsc,
                    isLoadingProducts: isLoadingProducts,
                    products: products,
                    isListViewVisible: isListViewVisible,
                    isGridViewVisible: isGridViewVisible,
                    isProductDetailsVisible: isProductDetailsVisible,
                    pager: pager,
                    errorList: errorList,
                    currentPageCustom: currentPageCustom,
                    filterText: filterText,
                    pageHeader: pageHeader,
                    filterTextForRelatedItems: filterTextForRelatedItems,
                    itemRelaterPager: itemRelaterPager,
                    activeVideo: activeVideo,
                    productsToRelate: productsToRelate,
                    selectedJobDescription: selectedJobDescription,
                    itemFileTypes: itemFileTypes,
                    costCentres: costCentres,
                    sectionFlags: sectionFlags,
                    suppliers: suppliers,
                    productCategories: productCategories,
                    parentProductCategories: parentProductCategories,
                    availableCategoriesForTemplate: availableCategoriesForTemplate,
                    categoryRegions: categoryRegions,
                    categoryTypes: categoryTypes,
                    selectedRegionId: selectedRegionId,
                    selectedCategoryTypeId: selectedCategoryTypeId,
                    selectedDesignerCategory: selectedDesignerCategory,
                    pressDialogFilter: pressDialogFilter,
                    pressDialogPager: pressDialogPager,
                    pressItems: pressItems,
                    paperSizes: paperSizes,
                    selectedCompany: selectedCompany,
                    currencyUnit: currencyUnit,
                    lengthUnit: lengthUnit,
                    // Utility Methods
                    initialize: initialize,
                    resetFilter: resetFilter,
                    filterProducts: filterProducts,
                    resetFilteredProducts: resetFilteredProducts,
                    filterProductsToRelate: filterProductsToRelate,
                    toggleView: toggleView,
                    setListViewActive: setListViewActive,
                    setGridViewActive: setGridViewActive,
                    editProduct: editProduct,
                    createProduct: createProduct,
                    onSaveProduct: onSaveProduct,
                    onCloseProductEditor: onCloseProductEditor,
                    onArchiveProduct: onArchiveProduct,
                    closeVideoDialog: closeVideoDialog,
                    onAddVideo: onAddVideo,
                    onEditVideo: onEditVideo,
                    onAddRelatedItem: onAddRelatedItem,
                    saveVideo: saveVideo,
                    selectJobDescription: selectJobDescription,
                    openItemAddonCostCentreDialog: openItemAddonCostCentreDialog,
                    closeItemAddonCostCentreDialog: closeItemAddonCostCentreDialog,
                    gotoElement: gotoElement,
                    toggleChildCategories: toggleChildCategories,
                    updateProductCategories: updateProductCategories,
                    openProductCategoryDialog: openProductCategoryDialog,
                    closeProductCategoryDialog: closeProductCategoryDialog,
                    updateCheckedStateForCategory: updateCheckedStateForCategory,
                    openPhraseLibrary: openPhraseLibrary,
                    getBaseDataForDesignerCategory: getBaseDataForDesignerCategory,
                    openPressDialog: openPressDialog,
                    resetPressItems: resetPressItems,
                    searchPressItems: searchPressItems,
                    closePressDialog: closePressDialog,
                    editSectionSignature: editSectionSignature,
                    closeSignatureDialog: closeSignatureDialog,
                    onCloneProduct: onCloneProduct,
                    editTemplate: editTemplate,
                    // For Store
                    initializeForStore: initializeForStore
                    // For Store
                    // Utility Methods

                };
            })()
        };
        return ist.product.viewModel;
    });

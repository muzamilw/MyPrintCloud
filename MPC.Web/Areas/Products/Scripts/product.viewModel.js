/*
    Module with the view model for the Product.
*/
define("product/product.viewModel",
    ["jquery", "amplify", "ko", "product/product.dataservice", "product/product.model", "common/pagination", "common/confirmation.viewModel",
        "common/phraseLibrary.viewModel"],
    function ($, amplify, ko, dataservice, model, pagination, confirmation, phraseLibrary) {
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
                    // Stock Items
                    stockItems = ko.observableArray([]),
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
                    // #endregion Arrays
                    // #region Busy Indicators
                    isLoadingProducts = ko.observable(false),
                    // Is List View Active
                    isListViewVisible = ko.observable(false),
                    // Is Grid View Active
                    isGridViewVisible = ko.observable(true),
                    // Is Product Editor Visible
                    isProductDetailsVisible = ko.observable(false),
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
                    // Pagination For Stock Item Dialog
                    stockDialogPager = ko.observable(new pagination.Pagination({ PageSize: 5 }, stockItems)),
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
                        onChooseStockItem: function () {
                            openStockItemDialog();
                        },
                        onSelectStockItem: function () {
                            closeStockItemDialog();
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
                        }
                    },
                    // Item State Tax Constructor Params
                    itemStateTaxConstructorParams = { countries: countries(), states: states() },
                    // Selected Job Description
                    selectedJobDescription = ko.observable(),
                    // Stock Dialog Filter
                    stockDialogFilter = ko.observable(),
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
                    // Open Editor
                    openProductEditor = function () {
                        // Show Details
                        isProductDetailsVisible(true);
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
                        // Show Basic Details Tab
                        view.showBasicDetailsTab();
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
                    // Search Stock Items
                    searchStockItems = function () {
                        stockDialogPager().reset();
                        getStockItems();
                    },
                    // Reset Stock Items
                    resetStockItems = function () {
                        // Reset Text 
                        stockDialogFilter(undefined);
                        // Filter Record
                        searchStockItems();
                    },
                    // Open Stock Item Dialog
                    openStockItemDialog = function () {
                        view.showStockItemDialog();
                        searchStockItems();
                    },
                    // Close Stock Item Dialog
                    closeStockItemDialog = function () {
                        view.hideStockItemDialog();
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
                        view.showProductCategoryDialog();
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
                    // Initialize the view model
                    initialize = function (specifiedView) {
                        view = specifiedView;
                        ko.applyBindings(view.viewModel, view.bindingRoot);

                        pager(new pagination.Pagination({ PageSize: 5 }, products, getItems));

                        itemRelaterPager(new pagination.Pagination({ PageSize: 5 }, productsToRelate, getItemsToRelate)),

                        stockDialogPager(new pagination.Pagination({ PageSize: 5 }, stockItems, getStockItems)),

                        // Get Base Data
                        getBaseData();

                        // Get Items
                        getItems();

                        // Set Open From Flag to false - so that popup don't show until button gets clicked
                        phraseLibrary.isOpenFromPhraseLibrary(false);
                    },
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
                    // Map Stock Items 
                    mapStockItems = function (data) {
                        var itemsList = [];
                        _.each(data, function (item) {
                            itemsList.push(model.StockItem.Create(item));
                        });

                        // Push to Original Array
                        ko.utils.arrayPushAll(stockItems(), itemsList);
                        stockItems.valueHasMutated();
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
                    onSaveProduct = function () {
                        if (!doBeforeSave()) {
                            return;
                        }

                        saveProduct(closeProductEditor);
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
                    // Get Base Data
                    getBaseData = function () {
                        dataservice.getBaseData({
                            success: function (data) {
                                costCentres.removeAll();
                                countries.removeAll();
                                states.removeAll();
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
                                    mapProductCategories(data.ProductCategories);

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
                    // Save Product
                    saveProduct = function (callback) {
                        dataservice.saveItem(selectedProduct().convertToServerData(), {
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
                            },
                            error: function (response) {
                                toastr.error("Failed to Save Product. Error: " + response);
                            }
                        });
                    },
                    // archive Product
                    archiveProduct = function () {
                        dataservice.archiveItem({
                            ItemId: selectedProduct().id()
                        }, {
                            success: function () {
                                selectedProduct().isArchived(true);
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
                    // Get Stock Items
                    getStockItems = function () {
                        dataservice.getStockItems({
                            SearchString: stockDialogFilter(),
                            PageSize: stockDialogPager().pageSize(),
                            PageNo: stockDialogPager().currentPage()
                        }, {
                            success: function (data) {
                                stockItems.removeAll();
                                if (data && data.TotalCount > 0) {
                                    stockDialogPager().totalCount(data.TotalCount);
                                    mapStockItems(data.StockItems);
                                }
                            },
                            error: function (response) {
                                toastr.error("Failed to load stock items" + response);
                            }
                        });
                    },
                    // Get Items
                    getItems = function () {
                        isLoadingProducts(true);
                        dataservice.getItems({
                            SearchString: filterText(),
                            PageSize: pager().pageSize(),
                            PageNo: pager().currentPage()
                        }, {
                            success: function (data) {
                                products.removeAll();
                                if (data && data.TotalCount > 0) {
                                    pager().totalCount(data.TotalCount);
                                    mapProducts(data.Items);
                                }
                                isLoadingProducts(false);
                            },
                            error: function (response) {
                                isLoadingProducts(false);
                                toastr.error("Failed to load items" + response);
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
                    //Get Category Child List Items
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
                                    });
                                }
                            },
                            error: function (response) {
                                isLoadingStores(false);
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
                    stockDialogPager: stockDialogPager,
                    stockDialogFilter: stockDialogFilter,
                    stockItems: stockItems,
                    costCentres: costCentres,
                    sectionFlags: sectionFlags,
                    suppliers: suppliers,
                    productCategories: productCategories,
                    parentProductCategories: parentProductCategories,
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
                    searchStockItems: searchStockItems,
                    resetStockItems: resetStockItems,
                    openStockItemDialog: openStockItemDialog,
                    closeStockItemDialog: closeStockItemDialog,
                    openItemAddonCostCentreDialog: openItemAddonCostCentreDialog,
                    closeItemAddonCostCentreDialog: closeItemAddonCostCentreDialog,
                    gotoElement: gotoElement,
                    toggleChildCategories: toggleChildCategories,
                    updateProductCategories: updateProductCategories,
                    openProductCategoryDialog: openProductCategoryDialog,
                    closeProductCategoryDialog: closeProductCategoryDialog,
                    updateCheckedStateForCategory: updateCheckedStateForCategory,
                    openPhraseLibrary: openPhraseLibrary
                    // Utility Methods

                };
            })()
        };
        return ist.product.viewModel;
    });
